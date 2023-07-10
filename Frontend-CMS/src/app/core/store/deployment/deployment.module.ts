import { NgModule } from '@angular/core'
import { EffectsModule } from '@ngrx/effects'
import { StoreModule } from '@ngrx/store'
import { DeploymentFeature } from './deployment.feature'
import { DeploymentEffects } from './deployment.effects'

@NgModule({
   imports: [
      StoreModule.forFeature(DeploymentFeature),
      EffectsModule.forFeature([DeploymentEffects])
   ],
})
export class DeploymentModule { }