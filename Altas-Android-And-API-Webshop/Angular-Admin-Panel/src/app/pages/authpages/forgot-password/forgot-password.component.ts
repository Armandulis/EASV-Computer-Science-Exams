import { Component, HostBinding } from '@angular/core';

import { BlankLayoutCardComponent } from 'app/shared/components/blank-layout-card';

@Component({
  selector: 'app-forgot-password',
  styleUrls: ['../../../shared/components/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './forgot-password.component.html',
})
export class ForgotPasswordComponent extends BlankLayoutCardComponent { }
