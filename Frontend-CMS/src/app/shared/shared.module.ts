import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { MatButtonModule } from '@angular/material/button'
import { MatSidenavModule } from '@angular/material/sidenav'
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatIconModule } from '@angular/material/icon'
import { MatDividerModule } from '@angular/material/divider'
import { MatSelectModule } from '@angular/material/select'
import { MatMenuModule } from '@angular/material/menu'
import { MatCardModule } from '@angular/material/card'
import { DragDropModule } from '@angular/cdk/drag-drop'
import { MatTooltipModule } from '@angular/material/tooltip'
import { MatInputModule } from '@angular/material/input'
import { MatButtonToggleModule } from '@angular/material/button-toggle'
import { MatDialogModule } from '@angular/material/dialog'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { MatSliderModule } from '@angular/material/slider'
import { MatExpansionModule } from '@angular/material/expansion'
import { MatTabsModule } from '@angular/material/tabs'
import { HttpClientModule } from '@angular/common/http'
import { SplashScreenComponent } from '../components/splash-screen/splash-screen.component'
import { ProgressBarComponent } from '../components/deployment/progress-bar/progress-bar.component'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { MatPaginatorModule } from '@angular/material/paginator'
import { RouterModule } from '@angular/router'

@NgModule({
  declarations: [SplashScreenComponent, ProgressBarComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDividerModule,
    MatSelectModule,
    MatMenuModule,
    MatCardModule,
    DragDropModule,
    MatTooltipModule,
    MatInputModule,
    MatButtonToggleModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSliderModule,
    MatExpansionModule,
    MatTabsModule,
    MatCheckboxModule,
    HttpClientModule,
    FormsModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    RouterModule
  ],
  exports: [
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatDividerModule,
    MatSelectModule,
    MatMenuModule,
    MatCardModule,
    DragDropModule,
    MatTooltipModule,
    MatInputModule,
    MatButtonToggleModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatSliderModule,
    MatExpansionModule,
    MatTabsModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,

    HttpClientModule,
    FormsModule,

    SplashScreenComponent,
    ProgressBarComponent,
  ],
})
export class SharedModule { }
