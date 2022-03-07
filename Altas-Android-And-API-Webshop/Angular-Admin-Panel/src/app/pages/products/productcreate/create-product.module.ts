import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialAngularSelectModule } from 'material-angular-select';

import { ThemeModule } from 'theme';

import { ProductFormComponent } from './productform';
import { CreateProductComponent } from './create-product.component';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialAngularSelectModule,
  ],
  declarations: [
    CreateProductComponent,
    ProductFormComponent,
  ],
  providers: [],
})
export class CreateProductModule { }
