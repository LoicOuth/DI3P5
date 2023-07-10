import { environment } from 'src/environments/environment'
import { SignalrService } from './../core/services/site/signalr.service'
import { SplashActions } from './../core/store/splash/splash.actions'
import { SplashFeature } from './../core/store/splash/splash.feature'
import { ElementsFeature } from './../core/store/elements/elements.feature'
import { IPage } from './../core/interfaces/IPage.interface'
import { Component, OnInit } from '@angular/core'
import { MatSelectChange } from '@angular/material/select'
import { Store } from '@ngrx/store'
import { map } from 'rxjs'
import { PagesActions } from '../core/store/pages/pages.actions'
import { PagesFeature, PagesState } from '../core/store/pages/pages.feature'
import { ActivatedRoute, ParamMap, Router } from '@angular/router'
import { ElementsActions } from '../core/store/elements/elements.actions'
import { MatDialog } from '@angular/material/dialog'
import { ConfirmDialogComponent } from '../components/confirm-dialog/confirm-dialog.component'
import { ConfirmDialogModel } from '../core/models/ConfirmDialogModel'
import { AddPageComponent } from '../components/add-page/add-page.component'
import { DeploymentActions } from '../core/store/deployment/deployment.actions'
import {
  DeploymentFeature,
  DeploymentState,
} from '../core/store/deployment/deployment.feature'
import { DeploymentComponent } from '../components/deployment/deployment.component'
import { MenusAction } from '../core/store/menus/menus.action'
import { ManageTemplateComponent } from '../components/manage-template/manage-template.component'
import { PreviewPageComponent } from '../components/preview-page/preview-page.component'

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css'],
})
export class LayoutComponent implements OnInit {
  pagesState?: PagesState
  deploymentState?: DeploymentState
  idSite?: string
  returnUrl = `${environment.home_url}/account/site`
  ManageTemplateComponent = ManageTemplateComponent;

  constructor(
    private store: Store,
    private signalrService: SignalrService,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) {
    this.store
      .select(PagesFeature.selectPagesState)
      .subscribe((state) => (this.pagesState = state))

    this.store
      .select(DeploymentFeature.selectDeploymentState)
      .subscribe((state) => (this.deploymentState = state))
  }

  splashLoading = this.store.select(SplashFeature.selectIsLoading);

  ngOnInit(): void {
    this.route.paramMap.subscribe(async (params: ParamMap) => {
      this.idSite = params.get('id')!

      this.store.dispatch(SplashActions.setSplash({ isLoading: true }))
      this.store.dispatch(
        PagesActions.loadPages({ idSite: this.idSite, page: null })
      )
      this.store.dispatch(DeploymentActions.loadSite({ siteId: this.idSite }))
      this.store.dispatch(
        MenusAction.loadMenus({ idSite: this.idSite, menu: null })
      )
      this.signalrService.startService(this.idSite)
    })
  }

  onSelectedPageChange(event: MatSelectChange) {
    this.store.dispatch(
      PagesActions.changeSelected({
        page: this.pagesState?.pages?.find(
          (el: IPage) => el.id == event.value
        )!,
      })
    )
  }

  propertyOpened() {
    return this.store.select(ElementsFeature.selectSelectedElement).pipe(
      map((item) => {
        return item != null
      })
    )
  }

  resetProperty() {
    this.store.dispatch(ElementsActions.resetSelectedElement())
  }

  putSiteOnline() {
    if (
      this.deploymentState?.site &&
      this.deploymentState.site.domain === null
    ) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        maxWidth: '400px',
        data: new ConfirmDialogModel(
          'Do you really want to put your site online ?'
        ),
      })

      dialogRef.afterClosed().subscribe((dialogResult) => {
        if (dialogResult) {
          this.signalrService.stopService()
          this.router.navigate([`/site/${this.idSite}/deployment`])
        }
      })
    } else {
      this.dialog.open(DeploymentComponent, {
        width: '30%',
      })
    }
  }

  previewSite() {
    this.dialog.open(PreviewPageComponent, {
      width: '100%',
      height: '100%',
    })
  }

  public createPage(): void {
    this.dialog.open(AddPageComponent, {
      maxWidth: '400px',
    })
  }
}
