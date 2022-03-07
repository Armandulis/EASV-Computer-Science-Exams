import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../../shared/services';

import { BlankLayoutCardComponent } from 'app/shared/components/blank-layout-card';
import {loginInput} from "../../../shared/Models/loginInput";

@Component({
  selector: 'app-sign-up',
  styleUrls: ['../../../shared/components/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './sign-up.component.html',
})
export class SignUpComponent extends BlankLayoutCardComponent implements OnInit {

  public signupForm: FormGroup;
  private email;
  private password;
  public emailPattern = '^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$';
  public error: string;
  private signup: loginInput;

  constructor(private authService: AuthService,
              private fb: FormBuilder,
              private router: Router) {
    super();

    this.signupForm = this.fb.group({
      password: new FormControl('', Validators.required),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(this.emailPattern),
        Validators.maxLength(20),
      ]),
    });
    this.email = this.signupForm.get('email');
    this.password = this.signupForm.get('password');

  }

  public ngOnInit() {
    this.authService.logout();
    this.signupForm.valueChanges.subscribe(() => {
      this.error = null;
    });
  }

  public login() {
    this.error = null;
    if (this.signupForm.valid) {
      this.signup = this.signupForm.getRawValue()
      this.authService.signup(this.signup)
        .subscribe(res => this.router.navigate(['/login']),
                   error => this.error = error.message);
    }
  }

  public onInputChange(event) {
    event.target.required = true;
  }
}
