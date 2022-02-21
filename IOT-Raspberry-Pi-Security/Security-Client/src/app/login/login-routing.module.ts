import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {RegisterComponent} from "./register/register.component";
import {ProfileComponent} from "./profile/profile.component";

// Base: '/login'
const routes: Routes = [
  {
    // Example ".com/login/"
    path: '',
    component: LoginComponent
  },
  {
    // Example ".com/login/register"
    path: 'register',
    component: RegisterComponent
  },
  {
    // Example ".com/login/profile"
    path: 'profile',
    component: ProfileComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

/**
 * Class LoginRoutingModule
 */
export class LoginRoutingModule { }
