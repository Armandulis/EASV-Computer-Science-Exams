import { NgModule } from '@angular/core';
import {LoginRoutingModule} from "./login-routing.module";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {ReactiveFormsModule} from "@angular/forms";
import { ProfileComponent } from './profile/profile.component';
import {CommonModule} from "@angular/common";

@NgModule({
  declarations: [LoginComponent, RegisterComponent, ProfileComponent],
  imports: [
    LoginRoutingModule,
    CommonModule,

    // Angular
    ReactiveFormsModule
  ]
})

/**
 * Class LoginModule
 */
export class LoginModule { }
