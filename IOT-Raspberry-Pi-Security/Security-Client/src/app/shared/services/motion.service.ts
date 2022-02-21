import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Socket} from 'ngx-socket-io'
import {Observable, of} from 'rxjs';
import {map} from 'rxjs/operators';
import {Motion} from "../models/motion";
import {environment} from "../../../environments/environment";
import {ChartData} from "../models/chart-data";
import {DataSeries} from "../models/data-series";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root'
})

/**
 * Class MotionService
 */
export class MotionService {

  /**
   * MotionService  constructor
   * @param http HttpClient - send requests to the API
   * @param socket Socket - Handles socket connection
   */
  constructor(private http: HttpClient,
              private socket: Socket) {
  }

  /**
   * Listen for new Motion triggers
   * @param sensorId string - Sensor's id
   * @return Observable<any> - Observable
   */
  listenForDataSeries(sensorId: string): Observable<any> {

    // Log error
    this.socket.on('connect_error', (err: any) => {
      console.log(err);
    });

    // Listen for new Motion events for a specific sensor
    return this.socket.fromEvent<Motion>(sensorId)
      .pipe(map(motion => {
        return {
          name: motion.measurementTime?.toString(),
          value: 1
        };
      }));
  }

  /**
   * Listen for new Sensors to be paired
   * @return Observable<string> - Observable of sensor's id that is ready to be paired
   */
  listenSensorToPair(): Observable<string> {

    // Log error
    this.socket.on('connect_error', (err: any) => {
      console.log(err);
    });

    // Listen for new sensors that are ready to be paired
    return this.socket.fromEvent<string>('sensor/pair');
  }

  /**
   * Gets all motion data with sensor's id - groups motions together and puts them in DataSeries
   * @param selectedSensorId string|null - Motions with sensor's id
   * @return Observable<DataSeries> - Observable
   */
  getMotionSeries(selectedSensorId: string | null): Observable<DataSeries> {

    return this.http.get(environment.apiURL + '/motion/all?sensorId=' + selectedSensorId, {headers: AuthService.getBearerTokenHeader()})
      .pipe(
        // Map motions into DataSeries
        map(motionList => {
          const dataSeries: DataSeries = new DataSeries();
          const chartData: ChartData[] = [];

          // We're working with array of Motion
          if (motionList instanceof Array) {
            dataSeries.motionList = motionList;

            // Loop through motions - motions that were capture on the same date
            motionList.forEach(motion => {

              // First 10 characters represent date: 2021-12-05
              const chartDataName = motion.measurementTime.substring(0, 10);

              const foundSameDayChartData: ChartData | undefined = chartData.find(({name}) => name === chartDataName);
              const foundItemIndex : number = chartData.findIndex(({name}) => name === chartDataName);

              // If chart already exists, update it's values
              if (foundSameDayChartData && foundItemIndex > -1 ) {
                foundSameDayChartData.value++;
                chartData[foundItemIndex] = foundSameDayChartData;
              } else {
                // Else create new chartData
                chartData.push({
                  name: chartDataName,
                  value: 1
                });
              }
            });
          }

          dataSeries.chartData = chartData;
          return dataSeries;
        })
      );
  }
}
