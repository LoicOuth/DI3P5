import { Observable } from 'rxjs'
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { environment } from 'src/environments/environment'
import AddSubdomainDto from '../../models/Dtos/AddSubdomain.dto'

@Injectable({
  providedIn: 'root'
})
export class OvhService {

  constructor(private httpClient: HttpClient) { }

  public checkAvailability(subDomain: string): Observable<boolean> {
    return this.httpClient.get<boolean>(`${environment.api_url}ovh/subdomain/check?SubDomain=${subDomain}`)
  }

  public createSubdomain(subDomainDto: AddSubdomainDto): Observable<void> {
    return this.httpClient.post<void>(`${environment.api_url}ovh/subdomain/create`, subDomainDto)
  }
}
