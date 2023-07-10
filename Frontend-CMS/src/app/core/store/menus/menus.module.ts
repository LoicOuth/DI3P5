import { NgModule } from '@angular/core'
import { EffectsModule } from '@ngrx/effects'
import { StoreModule } from '@ngrx/store'
import { MenusFeature } from './menus.feature'
import { MenusEffects } from './menus.effects'

@NgModule({
   imports: [
      StoreModule.forFeature(MenusFeature),
      EffectsModule.forFeature([MenusEffects])
   ],
})
export class MenusModule { }