import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FirstDeploymentComponent } from './first-deployment.component'

const routes: Routes = [
  { path: '', component: FirstDeploymentComponent },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FirstDeploymentRoutingModule { }
