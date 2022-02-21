import {Component, OnDestroy, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {GlobalService} from "../../shared/services/global.service";
import {ScaleType} from "@swimlane/ngx-charts";
import {MotionService} from "../../shared/services/motion.service";
import {Subject} from 'rxjs';
import {takeUntil} from "rxjs/operators";
import {Motion} from "../../shared/models/motion";
import {ChartData} from "../../shared/models/chart-data";

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})

/**
 * Class OverviewComponent
 */
export class OverviewComponent implements OnInit, OnDestroy {

  // Template
  // Chart options
  public data: ChartData[] = [];
  public colorScheme = {
    name: 'red',
    selectable: true,
    group: ScaleType.Linear,
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };
  public isDateSelected: boolean = false;

  public sensorIds: string[] = [];
  public selectedSensorId: string | null = null;
  public selectedDayMotionCaptures: Motion[] = [];

  // Component
  private notifier$ = new Subject(); // Helps to unsubscribe from subscriptions
  private motionList: Motion[] = [];
  private motionSeriesNotifier$ = new Subject();
  private selectedDay: any = null;


  /**
   * OverviewComponent constructor
   * @param router Router - Navigate user
   * @param globalService GlobalService - Manage access_token in local storage
   * @param motionService MotionService - Receive motion data
   */
  constructor(
    private router: Router,
    private globalService: GlobalService,
    private motionService: MotionService
  ) {
    // Check if user is logged in
    this.globalService.tokenValue.subscribe(token => {

      if (token === null) {
        // Redirect user
        // this.router.navigate(['login']); // TODO uncomment this
      }
    });

    // Get user
    const userIds = this.globalService.getUser()?.sensorIds;
    this.sensorIds = userIds ? userIds : [];
  }

  /**
   * Initializes the component
   */
  ngOnInit(): void {
  }

  /**
   * User selected date - display that day's pictures
   * @param day - User's selected day
   */
  onSelect(day: any): void {

    // Reset selection values
    this.isDateSelected = true;
    this.selectedDayMotionCaptures = [];
    this.selectedDay = day;

    // Only show motions of the selected day
    this.motionList.forEach((motion) => {
      if (motion.measurementTime?.toString().substring(0, 10) === day) {
        this.selectedDayMotionCaptures.push(motion);
      }
    });
  }

  /**
   * Runs when the component is destroyed
   * Unsubscribe from all subscriptions
   */
  ngOnDestroy(): void {
    this.notifier$.next('');
    this.notifier$.complete();
    this.resetMotionSeriesNotifier();
  }

  /**
   * Reset subscription notifier
   */
  resetMotionSeriesNotifier(): void {
    this.motionSeriesNotifier$.next('');
    this.motionSeriesNotifier$.complete();
    this.motionSeriesNotifier$ = new Subject();
  }

  /**
   * Updates selected motion id & starts listening for new motion series
   * @param sensorId string - motion Id
   */
  updateSelectedSensorId(sensorId: string): void {
    this.selectedSensorId = sensorId;
    this.getMotionSeries();
    this.motionService.listenForDataSeries(sensorId)
      .pipe(
        takeUntil(this.notifier$)
      ).subscribe(() => {
      this.getMotionSeries();
    })
  }

  /**
   * Gets motion data series for chart
   */
  private getMotionSeries(): void {
    // Reset values
    this.motionList = [];
    this.data = [];
    this.resetMotionSeriesNotifier();

    // Get motion series
    this.motionService.getMotionSeries(this.selectedSensorId)
      .pipe(
        takeUntil(this.motionSeriesNotifier$)
      )
      .subscribe(dataSeries => {
        // Set data
        this.motionList = dataSeries.motionList;
        this.data = dataSeries.chartData;

        // Update pictures that we need to display
        if (this.selectedDay) {
          this.onSelect(this.selectedDay);
        }
      });
  }
}
