import { Component, HostBinding } from '@angular/core';

@Component({
  selector: 'app-mostsold',
  styleUrls: ['./mostsold.component.scss'],
  templateUrl: './mostsold.component.html',
})
export class MostsoldComponent {
  @HostBinding('class.mostsold') private readonly mostsold = true;
  private tableHeader: string[] = [
    'Product',
    'Units Sold',
  ];

  private data: object[] = [
    {
      Product: 'TheMostBestestProduct',
      Units: 10000,
    },
    {
      Product: 'TheohDamn',
      Units: 9000,
    },

    {
      Product: 'DoingOkay',
      Units: 4000,
    },


    {
      Product: 'Couldbebetter',
      Units: 1,
    },

    ];
}
