import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule} from '@angular/router';
import { ThemeModule } from 'theme';

import { ProductsComponent } from './products.component';
import { TableCardComponent } from './table-card';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ThemeModule,
    FormsModule,
  ],
  declarations: [
    ProductsComponent,
    TableCardComponent,
  ],
  exports: [
  ],
})
export class ProductsModule { }
