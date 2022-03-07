import { Component, HostBinding, OnInit } from '@angular/core';
import { Product } from '../../../shared/Models/product';
import { ProductService } from '../../../shared/services/product/product.service';
import { Router, RouterModule} from '@angular/router';

@Component({
  selector: 'app-table-card',
  styleUrls: ['./table-card.component.scss'],
  templateUrl: './table-card.component.html',
})
export class TableCardComponent implements OnInit {
  @HostBinding('class.projects-table') private readonly projectsTable = true;

  private tableHeader: string[] = [
    'ID',
    'TITLE',
    'DESCRIPTION',
    'BRAND',
    'PRICE',
    'VIEW',
  ];


  constructor(private service: ProductService, private router: Router) { }
  products: Product[];

  //comment
  loadMore() {
    let last = this.products[this.products.length-1];
    const id = last.id
    const LastItemId = "?OrderBy=LastItemId=" + id
    this.service.getAllProducts(LastItemId).subscribe(res => {this.products = [...this.products, ...res]})
  }


  ngOnInit() {
    const filter = "?OrderBy=Price&SearchWord="
  this.service.getAllProducts(filter).subscribe(o => { this.products = o; });
  }
}
