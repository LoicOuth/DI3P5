import { Injectable } from '@angular/core';
import { IMenu } from '../../interfaces/IMenu.interface';
import { environment } from 'src/environments/environment';
import { Observable, from, pipe } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AddLinkDto } from '../../models/Dtos/AddLink.dto';
import { PagesFeature } from '../../store/pages/pages.feature';
import { Store } from '@ngrx/store';
import { SignalrService } from '../site/signalr.service';
import { ADD_LINK, UPDATE_LINK_POSITION } from '../../constants/events.constant';
import { UpdateLinkPositionDto } from '../../models/Dtos/UpdateLinkPosition.dto';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  private siteId?: string;

  constructor(private httpClient: HttpClient, private store: Store, private signalr: SignalrService) { 
    this.store.select(PagesFeature.selectIdSite).subscribe(el => this.siteId = el)
  }

  public getAll(idSite: string): Observable<IMenu> {
    return this.httpClient.get<IMenu>(`${environment.api_url}menu?siteId=${idSite}`)
  }

  public addLink(dto: AddLinkDto): Observable<void> {
    return from(this.signalr.connection!.send(ADD_LINK, dto, this.siteId))
  }

  public updateLinkPosition(dto: UpdateLinkPositionDto): Observable<void> {
    return from(this.signalr.connection!.send(UPDATE_LINK_POSITION, dto, this.siteId))
  }
}
