import { Component, HostBinding } from '@angular/core';

import { BlankLayoutCardComponent } from 'app/shared/components/blank-layout-card';

@Component({
  selector: 'app-error',
  styleUrls: ['../../../shared/components/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './error.component.html',
})
export class ErrorComponent extends BlankLayoutCardComponent { }
