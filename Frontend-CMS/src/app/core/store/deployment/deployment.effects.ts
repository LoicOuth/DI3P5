import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { DeploymentActions } from './deployment.actions';
import { SiteService } from '../../services/site/site.service';
import {
  EMPTY,
  catchError,
  exhaustMap,
  mergeMap,
  of,
  switchMap,
  withLatestFrom,
} from 'rxjs';
import { Store } from '@ngrx/store';
import { DeploymentFeature } from './deployment.feature';
import { OvhService } from '../../services/ovh/ovh.service';
import AddSubdomainDto from '../../models/Dtos/AddSubdomain.dto';

@Injectable()
export class DeploymentEffects {
  constructor(
    private actions$: Actions,
    private store$: Store,
    private siteService: SiteService,
    private ovhService: OvhService
  ) {}

  loadSite$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeploymentActions.loadSite),
      exhaustMap((action) =>
        this.siteService.getSiteFromId(action.siteId).pipe(
          mergeMap((site) => of(DeploymentActions.loadSuccess({ site: site }))),
          catchError(() => EMPTY)
        )
      )
    )
  );

  createSubDomain$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DeploymentActions.createSubdomain),
      withLatestFrom(this.store$.select(DeploymentFeature.selectSite)),
      switchMap(([action, store]) =>
        this.ovhService
          .createSubdomain(new AddSubdomainDto(store!.id, action.subDomain))
          .pipe(
            mergeMap(() =>
              of(DeploymentActions.loadSite({ siteId: store!.id }))
            )
          )
      )
    )
  );

  startDeployment$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(DeploymentActions.startDeployment),
        withLatestFrom(this.store$.select(DeploymentFeature.selectSite)),
        switchMap(([action, store]) => {
          this.store$.dispatch(
            DeploymentActions.changePublishing({ isPublishing: true })
          );

          return this.siteService.newDeployment(store!.id, action.comment);
        })
      ),
    { dispatch: false }
  );
}
