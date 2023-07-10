import { Injectable } from "@angular/core"
import { Actions, createEffect, ofType } from "@ngrx/effects"
import { EMPTY, catchError, exhaustMap, mergeMap, of, tap } from "rxjs"
import { AuthService } from "../../services/auth/auth.service"
import { AuthActions } from "./auth.action"
import { Router } from "@angular/router"

@Injectable()
export class AuthEffects {

   constructor(
      private actions$: Actions,
      private router: Router,
      private authService: AuthService
   ) { }

   login$ = createEffect(() => this.actions$.pipe(
      ofType(AuthActions.auth),
      exhaustMap((action) => this.authService.login(action.code, action.verifier, action.siteId)
         .pipe(
            mergeMap(auth => {

               localStorage.setItem('auth', JSON.stringify(auth))

               return of(
                  AuthActions.authSuccess({ auth, siteId: action.siteId })
               )
            })
         )
      )
   ))

   refresh$ = createEffect(() => this.actions$.pipe(
      ofType(AuthActions.refreshAuth),
      exhaustMap((action) => this.authService.refresh(action.refreshToken)
         .pipe(
            mergeMap(auth => {

               localStorage.setItem('auth', JSON.stringify(auth))

               return of(
                  AuthActions.authSuccess({ auth, siteId: action.siteId })
               )
            }),
            catchError(() => {
               this.router.navigate(['unauthorized'])
               return EMPTY
            }),
         )
      )
   ))

   navigateOnAuthSuccess$ = createEffect(() => this.actions$.pipe(
      ofType(AuthActions.authSuccess),
      tap(data => {
         if(data.siteId) {
            this.router.navigate([`/site/${data.siteId}/edit`])
         }
      })
   ),
      { dispatch: false }
   )
}