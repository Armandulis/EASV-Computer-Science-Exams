import {Component, OnInit} from '@angular/core';
import {Auth} from "../../shared/models/auth";
import {AuthService} from "../../shared/services/auth.service";
import {GlobalService} from "../../shared/services/global.service";
import {Router} from "@angular/router";
import {take} from "rxjs/operators";

@Component({
  selector: 'app-overview',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

/**
 * Class RegisterComponent
 */
export class RegisterComponent implements OnInit {

  // Template
  public error = '';

  /**
   * RegisterComponent constructor
   * @param authService AuthService - Handles authentication
   * @param router Router - Navigate user
   * @param globalService GlobalService - Manage access_token in local storage
   */
  constructor(private authService: AuthService,
              private router: Router,
              private globalService: GlobalService) {
  }

  /**
   * Initializes the component
   */
  ngOnInit(): void {
  }

  /**
   * Try to register in user with provided credentials
   * @param username string - user's submitted username
   * @param password string - user's submitted password
   * @param passwordRepeat string - user's submitted password repeat
   */
  submitRegister(username: string, password: string, passwordRepeat: string) {

    // Validate user's input
    if (!this.validateInput(username, password, passwordRepeat)) {
      return;
    }

    const auth: Auth = new Auth();
    auth.username = username;
    auth.password = password;

    this.authService.register(auth)
      .pipe( take(1) )
      .subscribe(
      // Successful response
      token => {

        // Make sure that access_token is set
        if (token.access_token === undefined || token.access_token === null) {
          this.error = 'Something went wrong, could not register';
        }

        // Make user to log in again
        this.router.navigate(['login']);
      },

      // Error Response
      errorResponse => {
        this.error = errorResponse.error.message;
      }
    );
  }

  /**
   * Validates input, returns true if validation passed
   * @param username string - user's submitted username
   * @param password string - user's submitted password
   * @param passwordRepeat string - user's submitted password repeat
   * @return true if validation passed
   */
  validateInput(username: string, password: string, passwordRepeat: string): boolean {
    // Reset error
    this.error = '';

    // Check if passwords match
    if (password !== passwordRepeat) {
      this.error = 'Passwords did not match!';
      return false;
    }

    // Check if password is long enough
    if (password.length < 6) {
      this.error = 'Passwords needs to be at least 7 characters long!';
      return false;
    }

    return true;
  }
}
