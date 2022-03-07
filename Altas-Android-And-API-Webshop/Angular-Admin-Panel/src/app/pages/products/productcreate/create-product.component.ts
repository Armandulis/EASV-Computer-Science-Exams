import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UpgradableComponent } from 'theme/components/upgradable';
import { ProductService } from '../../../shared/services/product/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product-form',
  template: `<app-product-form></app-product-form>`,
})
export class CreateProductComponent extends UpgradableComponent{

}



