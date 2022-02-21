import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";

const routes: Routes = [
  {
    // Example ".com/"
    path: '',
    component: HomeComponent
  },
  {
    // Example: ".com/overview"
    path: 'overview',

    loadChildren: () => ( import('./sensors-overview/overview.module') ).then( m => m.OverviewModule)
  },
  {
    // Example: ".com/login"
    path: 'login',

    loadChildren: () => ( import('./login/login.module') ).then( m => m.LoginModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

/**
 * Class AppRoutingModule
 */
export class AppRoutingModule { }
