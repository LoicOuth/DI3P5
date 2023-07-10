import { Component } from '@angular/core';
import { Store } from '@ngrx/store'
import { OvhService } from 'src/app/core/services/ovh/ovh.service'
import { DeploymentActions } from 'src/app/core/store/deployment/deployment.actions'
import { DeploymentFeature } from 'src/app/core/store/deployment/deployment.feature'

interface SubDomain {
  value: string,
  isAvailable: boolean | null
}

@Component({
  selector: 'app-first-deployment',
  templateUrl: './first-deployment.component.html',
  styleUrls: ['./first-deployment.component.css']
})
export class FirstDeploymentComponent {

  site = this.store.select(DeploymentFeature.selectSite)

  subDomain: SubDomain = {
    value: "",
    isAvailable: null
  }

  constructor(
    private store: Store, 
    private ovhService: OvhService 
  ) { }

  resetAvailability(): void {
    this.subDomain.isAvailable = null
  }

  checkAvailability(): void {
    this.ovhService.checkAvailability(this.subDomain.value)
      .subscribe(isSubDomainAvailable => this.subDomain.isAvailable = isSubDomainAvailable)
  }

  getSubdomain(): void {
    this.store.dispatch(DeploymentActions.createSubdomain({ subDomain: this.subDomain.value }))
  }
}
