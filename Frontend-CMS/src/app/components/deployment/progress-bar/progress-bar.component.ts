import { Component, OnInit, Input } from '@angular/core'
import { Store } from '@ngrx/store'
import { interval, map, switchMap, takeWhile } from 'rxjs'
import { BuildResult } from 'src/app/core/enums/BuildResult.enum'
import { DeploymentActions } from 'src/app/core/store/deployment/deployment.actions'
import { DeploymentFeature } from 'src/app/core/store/deployment/deployment.feature'

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
})
export class ProgressBarComponent implements OnInit {
  @Input() isBig: boolean = true;

  private currentPercentage = 0;

  BuildResult = BuildResult;
  disapere = false;

  progressState = this.store.select(DeploymentFeature.selectProgress);
  siteUrl = '';
  siteId = ''


  percentage$ = this.progressState.pipe(
    map((state) => state.progress),
    switchMap((targetPercentage) => {
      return interval(35).pipe(
        map(() => {
          if (this.currentPercentage <= targetPercentage) {
            this.currentPercentage++
          }
          return this.currentPercentage
        }),
        takeWhile(() => this.currentPercentage <= targetPercentage)
      )
    })
  );

  tooltip$ = this.progressState.pipe(
    map((el) => {
      switch (el.step) {
        case 1:
          return 'Step 1/4 : Get environment informations'
        case 2:
          return 'Step 2/4 : Generate your website'
        case 3:
          return 'Step 3/4 : Upload files'
        case 4:
          if (el.result === BuildResult.Succeeded) {
            return 'Successful deployment'
          } else if (el.result === BuildResult.None) {
            return 'Step 4/4 : Deploying website on the server'
          } else {
            return 'Error during deployment'
          }
        default:
          return ''
      }
    })
  );

  constructor(private store: Store) { }

  ngOnInit(): void {
    this.store.select(DeploymentFeature.selectSite).subscribe((el) => {
      this.siteUrl = `https://${el?.subDomain}.${el?.domain}`
      this.siteId = el?.id!
    })
  }

  changeDeploymentState() {
    this.disapere = true
    setTimeout(() => {
      this.store.dispatch(
        DeploymentActions.changePublishing({ isPublishing: false })
      )
    }, 350)
  }
}
