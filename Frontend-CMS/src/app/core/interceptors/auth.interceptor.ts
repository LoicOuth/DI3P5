import {
  Observable,
  catchError,
  filter,
  switchMap,
  take,
  throwError,
} from 'rxjs';
import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { AuthFeature } from '../store/auth/auth.feature';
import IAuth from '../interfaces/IAuth.interface';
import { AuthActions } from '../store/auth/auth.action';
import { selectAccessToken } from '../store/auth/auth.selectors';
import { Actions, ofType } from '@ngrx/effects';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private auth?: IAuth | null;

  constructor(private store: Store, private actions$: Actions) {
    this.store
      .select(AuthFeature.selectAuth)
      .subscribe((el) => (this.auth = el));
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.auth?.access_token) {
      request = this.addToken(request, this.auth.access_token);
    }

    return next.handle(request).pipe(
      catchError((error) => {
        if (
          error instanceof HttpErrorResponse &&
          error.status === 401 &&
          this.auth?.refresh_token
        ) {
          return this.handle401Error(request, next, this.auth.refresh_token);
        } else {
          return throwError(() => new Error('Error'));
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>, token: string) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  private handle401Error(
    request: HttpRequest<any>,
    next: HttpHandler,
    refreshToken: string
  ) {
    this.store.dispatch(AuthActions.refreshAuth({ refreshToken }));

    return this.actions$.pipe(
      ofType(AuthActions.authSuccess),
      take(1),
      switchMap(() => {
        return this.store.select(selectAccessToken).pipe(
          filter((token) => token != null),
          take(1),
          switchMap((token) => {
            return next.handle(this.addToken(request, token!));
          })
        );
      })
    );
  }
}
