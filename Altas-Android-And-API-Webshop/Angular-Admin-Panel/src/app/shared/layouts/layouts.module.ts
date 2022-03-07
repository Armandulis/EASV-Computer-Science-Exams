import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BlankLayoutCardComponent } from 'app/shared/components/blank-layout-card';
import { SidebarComponent } from 'app/shared/components/sidebar';
import { ThemeModule } from 'theme';
import { BlankLayoutComponent } from './blank-layout';
import { CommonLayoutComponent } from './common-layout';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    RouterModule,
  ],
  declarations: [
    CommonLayoutComponent,
    BlankLayoutComponent,
    BlankLayoutCardComponent,
    SidebarComponent,
  ],
  exports: [
    CommonLayoutComponent,
    BlankLayoutComponent,
  ],
})
export class LayoutsModule { }
