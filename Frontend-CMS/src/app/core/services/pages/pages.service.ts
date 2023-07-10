import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { environment } from 'src/environments/environment'
import { IPage } from '../../interfaces/IPage.interface'
import { AddPage } from '../../models/Dtos/AddPage.dto'
import { Observable, from } from 'rxjs'
import { UpdatePage } from '../../models/Dtos/UpdatePage.dto'
import { SignalrService } from '../site/signalr.service'
import { UPDATE_PAGE_INFORMATIONS } from '../../constants/events.constant'
import { PagesFeature } from '../../store/pages/pages.feature'
import { Store } from '@ngrx/store'

@Injectable({
  providedIn: 'root'
})
export class PagesService {

  private siteId?: string

  constructor(private httpClient: HttpClient, private signalr: SignalrService, private store: Store) {
    this.store.select(PagesFeature.selectIdSite).subscribe(el => this.siteId = el)
  }

  public getAll(idSite: string) {
    return this.httpClient.get<Array<IPage>>(`${environment.api_url}page?siteId=${idSite}`)
  }

  public addPage(dto: AddPage): Observable<IPage> {
    return this.httpClient.post<IPage>(`${environment.api_url}page`, dto)
  }

  public updatePage(dto: UpdatePage): Observable<void> {
    return from(this.signalr.connection!.send(UPDATE_PAGE_INFORMATIONS, dto, this.siteId))
  }
}
