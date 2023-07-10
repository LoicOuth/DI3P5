import { NgModule } from '@angular/core'
import { EffectsModule } from '@ngrx/effects'
import { StoreModule } from '@ngrx/store'
import { AuthFeature } from './auth.feature'
import { AuthEffects } from './auth.effects'

@NgModule({
   imports: [
      StoreModule.forFeature(AuthFeature),
      EffectsModule.forFeature([AuthEffects])
   ],
})
export class AuthModule { }