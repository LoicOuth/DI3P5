import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import IAuth from '../../interfaces/IAuth.interface'
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  login(code: string, verifier: string, siteId: string): Observable<IAuth> {

    return this.httpClient.post<IAuth>(`${environment.auth_url}connect/token`,
      new URLSearchParams({
        grant_type: "authorization_code",
        code,
        client_id: environment.client_id,
        client_secret: environment.client_secret,
        redirect_uri: `${environment.redirect_url}?verifier=${verifier}&site=${siteId}`,
        code_verifier: verifier
      }),
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
      })
  }

  refresh(refreshToken: string): Observable<IAuth> {
    return this.httpClient.post<IAuth>(`${environment.auth_url}connect/token`,
      new URLSearchParams({
        grant_type: "refresh_token",
        client_id: environment.client_id,
        client_secret: environment.client_secret,
        refresh_token: refreshToken
      }),
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
      })
  }

}
