import {Component, OnDestroy, OnInit} from '@angular/core';
import {AuthService} from "../../shared/services/auth.service";
import {Router} from "@angular/router";
import {GlobalService} from "../../shared/services/global.service";
import {User} from "../../shared/models/user";
import {Subject} from 'rxjs';
import {take, takeUntil} from "rxjs/operators";
import {MotionService} from "../../shared/services/motion.service";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})

/**
 * Class ProfileComponent
 */
export class ProfileComponent implements OnInit, OnDestroy {

  // Template
  public user: User | null = null;
  public error: string | null = null;
  public sensorsToPair: String[] = [];

  // Component
  private notifier$ = new Subject(); // Helps to unsubscribe from subscriptions

  /**
   * ProfileComponent constructor
   * @param authService AuthService - Handles user and authentication
   * @param router Router - Navigate user
   * @param globalService GlobalService - Manage access_token in local storage
   * @param motionService MotionService - Pair new sensors to the user
   */
  constructor(
    private authService: AuthService,
    private router: Router,
    private globalService: GlobalService,
    private motionService: MotionService
  ) {
  }

  /**
   * Initializes the controller
   */
  ngOnInit(): void {

    // Check if user is logged in
    this.globalService.tokenValue
      .pipe(
        takeUntil(this.notifier$)
      )
      .subscribe(token => {

        // TODO also introduce auth guard in Angular
        if (token === null) {
          // Redirect user
          // this.router.navigate(['login']); // TODO uncomment this
        }
      });

    // Get user
    this.user = this.globalService.getUser();

    // Listen for new sensors to pair
    this.motionService.listenSensorToPair().pipe(
      takeUntil(this.notifier$)
    )
      .subscribe(sensorId => {
        if (this.user && !this.user.sensorIds.find(sensor => sensor == sensorId)) {
          this.sensorsToPair.push(sensorId);
        }
      });
  }

  /**
   * Updates the user
   * @param name string - New user's name
   */
  public submitUpdate(name: string) {

    this.error = null;

    // If user is set
    if (this.user) {
      // Update user's values
      this.user.name = name;
      this.updateUser();
    }
  }

  /**
   * Saves user with new paired sensor values
   * @param sensorId string - new Sensor id
   */
  pairNewSensor(sensorId: any): void {

    // Add sensor to the user
    if (this.user) {

      // Validate user's input
      if (this.user.sensorIds.find(existingSensorId => existingSensorId === sensorId)) {
        this.error = 'Sensor Id already exists! Check overview for a list of sensor ids'
        return;
      }

      if (sensorId === '') {
        this.error = 'SensorId cannot be empty!';
        return;
      }

      // Do not show already paired sensors
      this.sensorsToPair.filter(sensor => sensor == sensorId);
      this.user.sensorIds.push(sensorId);
      this.updateUser();
    }
  }

  /**
   * Executes update
   */
  private updateUser(): void {

    // Make sure that the user is set
    if (this.user) {

      // Update user
      this.authService.updateUser(this.user)
        .pipe(
          take(1)
        )
        .subscribe(dataResponse => {
          // There's some weird typescript error when trying to use dataResponse - it's just an Object
          const dataObject = JSON.stringify(dataResponse);
          const jsonObject = JSON.parse(dataObject);

          // If API updated the user - update it on the client as well
          if (jsonObject.acknowledged) {
            this.globalService.setUser(this.user);
            alert('updated!');
          } else {
            alert('update failed!');
          }
        });
    }
  }

  /**
   * Runs when the component is destroyed
   * Unsubscribe from all subscriptions
   */
  ngOnDestroy(): void {
    this.notifier$.next('');
    this.notifier$.complete();
  }
}
