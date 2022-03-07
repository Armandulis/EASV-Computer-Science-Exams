import { Component, Input } from '@angular/core';

import { SidebarComponent as BaseSidebarComponent } from 'theme/components/sidebar';

@Component({
  selector: 'app-sidebar',
  styleUrls: ['../../../../theme/components/sidebar/sidebar.component.scss', './sidebar.component.scss'],
  templateUrl: '../../../../theme/components/sidebar/sidebar.component.html',
})
export class SidebarComponent extends BaseSidebarComponent {
  public title = 'ALTAS';
  public menu = [
    { name: 'Dashboard', link: '/app/dashboard', icon: 'dashboard' },
    { name: 'Products', link: '/app/products', icon: 'local_offer'},
    { name: 'Create Account', link: '/sign-up', icon: 'person' },

  ];
}
