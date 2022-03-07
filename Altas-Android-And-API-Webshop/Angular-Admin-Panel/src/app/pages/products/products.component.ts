import { Component, HostBinding } from '@angular/core';

import { UpgradableComponent } from 'theme/components/upgradable';

@Component({
  selector: 'app-products',
  styleUrls: ['../charts/charts.component.scss'],
  templateUrl: './products.component.html',
})
export class ProductsComponent extends UpgradableComponent {
  @HostBinding('class.mdl-grid') private readonly mdlGrid = true;
  @HostBinding('class.mdl-grid--no-spacing') private readonly mdlGridNoSpacing = true;
}
