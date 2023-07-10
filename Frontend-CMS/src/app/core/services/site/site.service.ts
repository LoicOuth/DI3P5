import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ISite } from '../../interfaces/ISite.interface';
import { environment } from 'src/environments/environment';
import * as SignalR from '@microsoft/signalr';
import { ADD_TO_GROUP, ON_PROGRESS } from '../../constants/events.constant';
import { IProgress } from '../../interfaces/IProgress.interface';
import { Store } from '@ngrx/store';
import { DeploymentActions } from '../../store/deployment/deployment.actions';
import { selectAccessToken } from '../../store/auth/auth.selectors';

@Injectable({
  providedIn: 'root',
})
export class SiteService {
  accessToken?: string;

  constructor(private httpClient: HttpClient, private store: Store) {
    this.store
      .select(selectAccessToken)
      .subscribe((el) => (this.accessToken = el!));
  }

  getSiteFromId(siteId: string) {
    return this.httpClient.get<ISite>(`${environment.api_url}site/${siteId}`);
  }

  newDeployment(siteId: string, comment: string) {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl(environment.deployment_hub_url, {
        accessTokenFactory: () => this.accessToken ?? '',
      })
      .build();

    connection?.start().then(() => {
      connection?.send(ADD_TO_GROUP, siteId);

      connection?.on(ON_PROGRESS, (data: IProgress) =>
        this.store.dispatch(DeploymentActions.onProgress({ progress: data }))
      );
    });

    return this.httpClient.post(`${environment.api_url}site/${siteId}/deploy`, {
      siteId,
      comment,
    });
  }
}
