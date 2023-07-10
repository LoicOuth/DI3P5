import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { UnauthorizedRoutingModule } from './unauthorized-routing.module'
import { SharedModule } from 'src/app/shared/shared.module'

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    UnauthorizedRoutingModule
  ]
})
export class UnauthorizedModule { }
