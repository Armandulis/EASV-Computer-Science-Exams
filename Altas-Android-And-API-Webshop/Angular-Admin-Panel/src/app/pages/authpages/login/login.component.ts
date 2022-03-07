import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../shared/services';
import { BlankLayoutCardComponent } from 'app/shared/components/blank-layout-card';
import {loginInput} from "../../../shared/Models/loginInput";

@Component({
  selector: 'app-login',
  styleUrls: ['../../../shared/components/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './login.component.html',
})
export class LoginComponent extends BlankLayoutCardComponent implements OnInit {
  public loginForm: FormGroup;
  private email;
  private password;
  public emailPattern = '^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$';
  public error: string;
  logininfo: loginInput;
  constructor(private authService: AuthService,
              private fb: FormBuilder,
              private router: Router) {
    super();

    this.loginForm = this.fb.group({
      password: new FormControl('', Validators.required),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(this.emailPattern),
        Validators.maxLength(20),
      ]),
    });
    this.email = this.loginForm.get('email');
    this.password = this.loginForm.get('password');
  }

  public ngOnInit() {
    this.authService.logout();
    this.loginForm.valueChanges.subscribe(() => {
      this.error = null;
    });
  }

  public login() {
    this.error = null;
    this.logininfo = this.loginForm.value;
    if (this.loginForm.valid) {
      this.authService.login(this.logininfo)
        .subscribe(res => {
            localStorage.setItem("idToken", res.idToken);
            localStorage.setItem("email", res.email);
            localStorage.setItem("expiresIn", res.expiresIn + "");
            localStorage.setItem("localId", res.localId);
          this.router.navigate(['/app/dashboard'])
          },
                   error => this.error = error.message);
    }
  }

  public onInputChange(event) {
    event.target.required = true;
  }
}
