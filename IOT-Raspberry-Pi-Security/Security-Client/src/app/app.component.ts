import {Component} from '@angular/core';
import {GlobalService} from "./shared/services/global.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

/**
 * Class AppComponent
 */
export class AppComponent {

  // Template
  public isUserLoggedIn = false;

  /**
   * AppComponent constructor
   * @param globalService Global Service - Keep track of access_token in sessionStorage
   * @param router Router - Navigate user
   */
  constructor(private globalService: GlobalService,
              private router: Router) {

    // Check if user is logged in
    this.globalService.tokenValue.subscribe( token => {
      this.isUserLoggedIn = token !== null;
    });
  }

  /**
   * Removes access_token from sessionStorage and redirects user to login page
   */
  public logout() : void {
    // Save JWT in local storage
    this.globalService.theToken = null;

    // Redirect user
    this.router.navigate(['login'] );
  }
}
