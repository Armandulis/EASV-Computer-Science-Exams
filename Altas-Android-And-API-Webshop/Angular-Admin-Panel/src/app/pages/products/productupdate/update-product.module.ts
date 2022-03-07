import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialAngularSelectModule } from 'material-angular-select';

import { ThemeModule } from 'theme';

import { ProductFormComponent } from './productform';
import { UpdateProductComponent } from './update-product.component';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    ReactiveFormsModule,
    FormsModule,
    MaterialAngularSelectModule,
  ],
  declarations: [
    UpdateProductComponent,
    ProductFormComponent,
  ],
  providers: [],
})
export class UpdateProductModule { }
