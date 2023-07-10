import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { StoreModule } from '@ngrx/store'
import { PagesModule } from './core/store/pages/pages.module'
import { EffectsModule } from '@ngrx/effects'
import { StoreDevtoolsModule } from '@ngrx/store-devtools'
import { SharedModule } from './shared/shared.module'
import { DeploymentModule } from './core/store/deployment/deployment.module'
import { SplashModule } from './core/store/splash/splash.module'
import { ElementsModule } from './core/store/elements/elements.module'
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component'
import { CallbackComponent } from './pages/callback/callback.component'
import { AuthModule } from './core/store/auth/auth.module'
import { HTTP_INTERCEPTORS } from '@angular/common/http'
import { AuthInterceptor } from './core/interceptors/auth.interceptor'
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component'
import { DeploymentComponent } from './components/deployment/deployment.component'
import { FirstDeploymentComponent } from './pages/first-deployment/first-deployment.component'
import { MenusModule } from './core/store/menus/menus.module'
import { NotFoundComponent } from './pages/not-found/not-found.component'
import { ErrorPageComponent } from './components/error-page/error-page.component'
import { RouterModule } from '@angular/router'

@NgModule({
  declarations: [
    AppComponent,
    ConfirmDialogComponent,
    CallbackComponent,
    UnauthorizedComponent,
    DeploymentComponent,
    FirstDeploymentComponent,
    NotFoundComponent,
    ErrorPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    StoreModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: false, // Restrict extension to log-only mode
      autoPause: true, // Pauses recording actions and state changes when the extension window is not open
    }),
    EffectsModule.forRoot([]),
    SharedModule,
    ElementsModule,
    PagesModule,
    SplashModule,
    DeploymentModule,
    AuthModule,
    MenusModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
