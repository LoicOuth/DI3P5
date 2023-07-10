import { PagesFeature } from 'src/app/core/store/pages/pages.feature'
import {
  ADD_ELEMENT,
  UPDATE_ELEMENT_STYLE,
  UPDATE_ELEMENT_CONTENT,
  UPDATE_ELEMENT_POSITION,
  DELETE_ELEMENT,
  UPDATE_LINK_POSITION,
} from './../../constants/events.constant'
import { SignalrService } from './../site/signalr.service'
import { environment } from 'src/environments/environment'
import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { IElement } from '../../interfaces/IElement.interface'
import { Observable, from } from 'rxjs'
import { Store } from '@ngrx/store'
import AddStyleDto from '../../models/Dtos/AddStyle.dto'
import AddElementDto from '../../models/Dtos/AddElement.dto'
import UpdateContentDto from '../../models/Dtos/UpdateContent.dto'
import UpdatePositionDto from '../../models/Dtos/UpdatePosition.dto'
import { IStyle } from '../../interfaces/IStyle.interface'
import { ElementsActions } from '../../store/elements/elements.actions'
import { UpdateLinkPositionDto } from '../../models/Dtos/UpdateLinkPosition.dto'

@Injectable({
  providedIn: 'root',
})
export class ElementsService {
  private siteId?: string

  constructor(
    private httpClient: HttpClient,
    private signalr: SignalrService,
    private store: Store
  ) {
    this.store
      .select(PagesFeature.selectIdSite)
      .subscribe((el) => (this.siteId = el))
  }

  updateClassInStore(style: IStyle) {
    this.store.dispatch(ElementsActions.addStyle(style))
  }

  getAll(idPage: string) {
    return this.httpClient.get<Array<IElement>>(
      `${environment.api_url}element?pageId=${idPage}`
    )
  }

  addElement(element: AddElementDto): Observable<void> {
    return from(
      this.signalr.connection!.send(ADD_ELEMENT, element, this.siteId)
    )
  }

  deleteElement(elementId: string): Observable<void> {
    return from(
      this.signalr.connection!.send(DELETE_ELEMENT, { elementId }, this.siteId)
    )
  }

  addStyle(newStyle: AddStyleDto): Observable<void> {
    return from(
      this.signalr.connection!.send(UPDATE_ELEMENT_STYLE, newStyle, this.siteId)
    )
  }

  updateContent(newContent: UpdateContentDto): Observable<void> {
    return from(
      this.signalr.connection!.send(
        UPDATE_ELEMENT_CONTENT,
        newContent,
        this.siteId
      )
    )
  }

  updatePosition(newPosition: UpdatePositionDto): Observable<void> {
    return from(
      this.signalr.connection!.send(
        UPDATE_ELEMENT_POSITION,
        newPosition,
        this.siteId
      )
    )
  }

  updateLinkPosition(newPosition: UpdateLinkPositionDto): Observable<void> {
    return from(
      this.signalr.connection!.send(
        UPDATE_LINK_POSITION,
        newPosition,
        this.siteId
      )
    )
  }

  updateUrlElement(file: File, elementId: string): Observable<IElement> {
    const formData = new FormData()
    formData.append('file', file)
    formData.append('elementId', elementId)

    return this.httpClient.post<IElement>(
      `${environment.api_url}element?groupName=${this.siteId}`,
      formData,
      {
        headers: {
          "Access-Control-Allow-Origin": "*"
        }
      }
    )
  }
}
