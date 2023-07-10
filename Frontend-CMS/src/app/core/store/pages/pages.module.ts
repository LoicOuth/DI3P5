import { NgModule } from '@angular/core'
import { EffectsModule } from '@ngrx/effects'
import { StoreModule } from '@ngrx/store'
import { PagesEffects } from './pages.effects'
import { PagesFeature } from './pages.feature'

@NgModule({
   imports: [
      StoreModule.forFeature(PagesFeature),
      EffectsModule.forFeature([PagesEffects])
   ],
})
export class PagesModule { }