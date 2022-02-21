import {Component, OnInit} from '@angular/core';
import {Auth} from "../../shared/models/auth";
import {AuthService} from "../../shared/services/auth.service";
import {Router} from "@angular/router";
import {GlobalService} from "../../shared/services/global.service";
import {take} from "rxjs/operators";

@Component({
  selector: 'app-overview',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

/**
 * Class LoginComponent
 */
export class LoginComponent implements OnInit {

  // Template
  public error: string = '';

  /**
   * LoginComponent constructor
   * @param authService AuthService - Handles authentication
   * @param router Router - Navigate user
   * @param globalService GlobalService - Manage access_token in local storage
   */
  constructor(
    private authService: AuthService,
    private router: Router,
    private globalService: GlobalService
  ) {
  }

  /**
   * Initializes the component
   */
  ngOnInit(): void {
  }

  /**
   * Try to log in user with provided credentials
   * @param username string - user's submitted username
   * @param password string - user's submitted password
   */
  submitLogin(username: string, password: string) {
    this.error = '';

    // Get user's input
    const auth: Auth = new Auth();
    auth.username = username;
    auth.password = password;

    // Login user
    this.authService.login(auth)
      .pipe(
        take(1)
      )
      .subscribe(token => {

        // Make sure that access_token is set
        if (token.access_token === undefined || token.access_token === null) {
          this.error = 'Something went wrong, could not login';
          return;
        }

        // Save JWT in local storage
        this.globalService.setUser(token.user);
        this.globalService.theToken = token.access_token;

        // Redirect user
        this.router.navigate(['overview']);
      });
  }
}
