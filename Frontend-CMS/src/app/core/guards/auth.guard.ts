import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router'
import { Store } from '@ngrx/store'
import IAuth from '../interfaces/IAuth.interface'
import { AuthFeature } from '../store/auth/auth.feature'
import { AuthActions } from '../store/auth/auth.action'
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private auth: IAuth | null = null;

  constructor(private store: Store, private router: Router) {
    this.store
      .select(AuthFeature.selectAuth)
      .subscribe((el) => (this.auth = el))
  }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (!environment.production)
      return true

    if (this.auth)
      return true

    const authStorage = localStorage.getItem('auth')
    const idSite = route.paramMap.get('id')

    if (authStorage && idSite) {
      this.store.dispatch(
        AuthActions.refreshAuth({
          refreshToken: (JSON.parse(authStorage) as IAuth).refresh_token,
          siteId: idSite
        })
      )

      return false
    }

    this.router.navigate(['unauthorized'])
    return false
  }
}
