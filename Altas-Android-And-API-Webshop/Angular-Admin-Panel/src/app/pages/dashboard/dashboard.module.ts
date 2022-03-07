import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ThemeModule } from 'theme';
import { DashboardComponent } from './dashboard.component';
import { MostsoldComponent } from './mostsold';
import { TableCardComponent } from './table-card';
import { TrendingComponent } from './trending';
import {MaterialAngularSelectModule} from "material-angular-select";

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    FormsModule,
    MaterialAngularSelectModule,
  ],
  declarations: [
    DashboardComponent,
    TableCardComponent,
    TrendingComponent,
    MostsoldComponent,
  ],
  exports: [
    TrendingComponent,
  ],
})
export class DashboardModule { }
