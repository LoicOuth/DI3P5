import { ElementsFeature } from './elements.feature'
import { NgModule } from '@angular/core'
import { EffectsModule } from '@ngrx/effects'
import { StoreModule } from '@ngrx/store'
import { ElementsEffects } from './elements.effects'

@NgModule({
   imports: [
      StoreModule.forFeature(ElementsFeature),
      EffectsModule.forFeature([ElementsEffects])
   ],
})
export class ElementsModule { }