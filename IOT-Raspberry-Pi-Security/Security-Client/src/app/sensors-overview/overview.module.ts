import { NgModule } from '@angular/core';
import {OverviewRoutingModule} from "./overview-routing.module";
import {OverviewComponent} from "./overview/overview.component";
import {NgxChartsModule} from "@swimlane/ngx-charts";
import {MatCardModule} from '@angular/material/card';

@NgModule({
  declarations: [OverviewComponent],
  imports: [
    OverviewRoutingModule,
    NgxChartsModule,
    MatCardModule
  ]
})

/**
 * Class OverviewModule
 */
export class OverviewModule { }
