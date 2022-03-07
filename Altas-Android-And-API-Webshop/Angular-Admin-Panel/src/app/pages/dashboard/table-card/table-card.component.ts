import { Component, HostBinding, OnInit, OnChanges} from '@angular/core';
import { ProductStatus } from '../../../shared/Models/productStatus';
import { Router } from '@angular/router';
import {OrderService} from "../../../shared/services/order/order.service";
import {dataFilter} from "../../../shared/Models/dataFilter";
import {Product} from "../../../shared/Models/product";

@Component({
  selector: 'app-table-card',
  styleUrls: ['./table-card.component.scss'],
  templateUrl: './table-card.component.html',
})
export class TableCardComponent {
  @HostBinding('class.projects-table') private readonly projectsTable = true;

  constructor(private service: OrderService, private router: Router) { }

  private tableHeader: string[] = [
    'Order ID',
    'User ID',
    'Product',
    'Purchase Date',
    'Status',
  ];
  private productstatuses: ProductStatus[]
  public  status = ['Delivery confirmed', 'Order placed', 'Delivering', 'Order Completed', 'Cancelled']
  private LastItemId: string;


  ngOnInit() {
    this.LastItemId = "?LastItemId=";
    this.service.getProductStatusFiltered(this.LastItemId).subscribe(res => {this.productstatuses = res});

}
  statusChanges(item: ProductStatus, data: { title: string, value: string }) {

    item.status = data.title
    this.service.updateOrder(item).subscribe()
  }
  loadmore(){
    let last = this.productstatuses[this.productstatuses.length-1];
    const id = last.id
    this.LastItemId = "?LastItemId=" + id
    this.service.getProductStatusFiltered(this.LastItemId).subscribe(
      res => {this.productstatuses = [...this.productstatuses, ...res]})
  }
}
