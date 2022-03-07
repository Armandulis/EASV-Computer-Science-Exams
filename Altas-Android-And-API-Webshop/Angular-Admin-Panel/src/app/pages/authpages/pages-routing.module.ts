import { ModuleWithProviders, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutsModule } from 'app/shared/layouts';
import { BlankLayoutComponent } from 'app/shared/layouts/blank-layout';
import { ErrorComponent } from './error';
import { ForgotPasswordComponent } from './forgot-password';
import { LoginComponent } from './login';
import { SignUpComponent } from './sign-up';
import {AuthGuard} from "../../shared/services/auth";

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: BlankLayoutComponent,
        children: [
          { path: '404', component: ErrorComponent, pathMatch: 'full' },
          { path: 'login', component: LoginComponent, pathMatch: 'full' },
          { path: 'sign-up', component: SignUpComponent, pathMatch: 'full', canActivate: [AuthGuard] },
          { path: 'forgot-password', component: ForgotPasswordComponent, pathMatch: 'full' },
          { path: '**', redirectTo: '404' },
        ],
      },
    ]),
    LayoutsModule,
  ],
  exports: [RouterModule],
})
export class PagesRoutingModule { }
