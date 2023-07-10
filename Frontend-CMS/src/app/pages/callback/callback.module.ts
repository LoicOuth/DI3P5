import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { CallbackRoutingModule } from './callback-routing.module'
import { SharedModule } from 'src/app/shared/shared.module'


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    CallbackRoutingModule
  ]
})
export class CallbackModule { }
