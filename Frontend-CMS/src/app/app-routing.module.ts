import { NgModule } from '@angular/core'
import type { Routes } from '@angular/router'
import { RouterModule } from '@angular/router'
import { AuthGuard } from './core/guards/auth.guard'
import { NotFoundComponent } from './pages/not-found/not-found.component'

const routes: Routes = [
  { path: 'site/:id/edit', loadChildren: () => import('./layout/layout.module').then(m => m.LayoutModule), canActivate: [AuthGuard] },
  { path: 'site/:id/deployment', loadChildren: () => import('./pages/first-deployment/first-deployment.module').then(m => m.FirstDeploymentModule), canActivate: [AuthGuard] },
  { path: 'unauthorized', loadChildren: () => import('./pages/unauthorized/unauthorized.module').then(m => m.UnauthorizedModule) },
  { path: 'callback', loadChildren: () => import('./pages/callback/callback.module').then(m => m.CallbackModule) },
  { path: '**', pathMatch: 'full', component: NotFoundComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
