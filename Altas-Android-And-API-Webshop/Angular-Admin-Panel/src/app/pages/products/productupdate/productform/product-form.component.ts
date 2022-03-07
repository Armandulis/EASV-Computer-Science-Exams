import { Component, HostBinding } from '@angular/core';
import { ProductService } from '../../../../shared/services/product/product.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { Product } from 'app/shared/Models/product';

@Component({
  selector: 'app-product-form',
  styleUrls: ['./product-form.component.scss'],
  templateUrl: 'product-form.component.html',
})
export class ProductFormComponent {
  @HostBinding('class.productform') private productForm = true;

  updateProductForm = new FormGroup({
                                id: new FormControl(''),
                                title: new FormControl(''),
                                description: new FormControl(''),
                                brand: new FormControl(''),
                                type: new FormControl(''),
                                price: new FormControl(''),
                                pictureUrl: new FormControl(''),

                              });

  id: string;
  constructor(private service: ProductService, private router: Router, private route: ActivatedRoute) {}
  product: Product;
  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')
    this.service.getByID(this.id).subscribe(o =>
                                            { this.product = o
                                              this.updateProductForm.patchValue(this.product);

    })

  }



  save() {
    this.product = this.updateProductForm.value;
    const productid = this.updateProductForm.get('id').value;
    this.service.updateProduct(productid, this.product).subscribe(o => { this.router.navigateByUrl('/app/products'); } );

  }

  delete() {
    const productid = this.updateProductForm.get('id').value;

    this.service.deleteProduct(productid).subscribe(o => {this.router.navigateByUrl('/app/products')})

  }

}
