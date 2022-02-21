import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {OverviewComponent} from "./overview/overview.component";

// Base: '/overview'
const routes: Routes = [
  {
    // Example ".com/overview/"
    path: '',
    component: OverviewComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

/**
 * Class OverviewRoutingModule
 */
export class OverviewRoutingModule { }
