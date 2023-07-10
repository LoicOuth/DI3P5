import { SplashFeature } from './splash.feature'
import { NgModule } from '@angular/core'
import { StoreModule } from '@ngrx/store'

@NgModule({
   imports: [
      StoreModule.forFeature(SplashFeature)
   ],
})
export class SplashModule { }