import {
  ADD_TO_GROUP,
  DELETE_MY_ELEMENT,
  UPDATE_PAGE,
  REFRESH_MENU
} from './../../constants/events.constant'
import { ElementsActions } from './../../store/elements/elements.actions'
import { Injectable } from '@angular/core'
import * as SignalR from '@microsoft/signalr'
import { Store } from '@ngrx/store'
import { environment } from 'src/environments/environment'
import { IElement } from '../../interfaces/IElement.interface'
import {
  NEW_ELEMENT,
  UPDATE_ELEMENT,
  UPDATE_ELEMENTS,
} from '../../constants/events.constant'
import { selectAccessToken } from '../../store/auth/auth.selectors'
import { Router } from '@angular/router'
import { IPage } from '../../interfaces/IPage.interface'
import { PagesActions } from '../../store/pages/pages.actions'
import { IMenu } from '../../interfaces/IMenu.interface'
import { MenusAction } from '../../store/menus/menus.action'
import { useCypressSignalRMock } from 'cypress-signalr-mock'

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  accessToken?: string
  connection?: SignalR.HubConnection

  constructor(private store: Store, private router: Router) {
    this.store
      .select(selectAccessToken)
      .subscribe((el) => (this.accessToken = el!))
    this.initConnection()
  }

  private initConnection() {
    this.connection = useCypressSignalRMock('elements') ?? new SignalR.HubConnectionBuilder()
      .withUrl(environment.default_hub_url, {
        accessTokenFactory: () => this.accessToken ?? '',
      })
      .build()
  }

  async startService(idSite: string): Promise<void> {
    try {
      await this.connection?.start()
      this.connection?.send(ADD_TO_GROUP, idSite)

      this.connection?.on(UPDATE_ELEMENT, (data: IElement) =>
        this.store.dispatch(ElementsActions.updateElement({ element: data }))
      )

      this.connection?.on(REFRESH_MENU, (data: IMenu) =>
        this.store.dispatch(MenusAction.updateMenus({ menu: data }))
      )

      this.connection?.on(NEW_ELEMENT, (data: IElement) =>
        this.store.dispatch(ElementsActions.newElement({ element: data }))
      )

      this.connection?.on(UPDATE_ELEMENTS, (data: Array<IElement>) =>
        this.store.dispatch(
          ElementsActions.elementsLoadedSuccess({ elements: data })
        )
      )

      this.connection?.on(DELETE_MY_ELEMENT, (data: string) =>
        this.store.dispatch(
          ElementsActions.deleteElementInStore({ elementId: data })
        )
      )

      this.connection?.on(UPDATE_PAGE, (data: IPage) =>
        this.store.dispatch(PagesActions.updatePageSuccess({ page: data }))
      )
    } catch (error) {
      console.error('Could not start SignalR connection:', error)
      this.router.navigate(['unauthorized'])
    }
  }

  async stopService(): Promise<void> {
    if (this.connection?.state === SignalR.HubConnectionState.Connected) {
      try {
        await this.connection?.stop()
      } catch (error) {
        console.error('Could not stop SignalR connection:', error)
      }
    }
  }
}
