import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutsModule } from './shared/layouts';
import { CommonLayoutComponent } from './shared/layouts/common-layout';
import { DashboardComponent } from './pages/dashboard';
import { ProductsComponent } from './pages/products';
import { CreateProductComponent } from './pages/products/productcreate';
import { UpdateProductComponent } from './pages/products/productupdate';
import { AuthGuard } from "./shared/services/auth/auth.guard";


@NgModule({
  imports: [
    RouterModule.forRoot(
      [
        { path: '', redirectTo: 'app/dashboard', pathMatch: 'full'},
        { path: 'app', component: CommonLayoutComponent, children: [
            { path: 'dashboard', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthGuard] },
            { path: 'products', component: ProductsComponent, pathMatch: 'full', canActivate: [AuthGuard]},
            { path: 'productcreate', component: CreateProductComponent, pathMatch: 'full', canActivate: [AuthGuard] },
            { path: 'productupdate/:id', component: UpdateProductComponent, pathMatch: 'full',canActivate: [AuthGuard] },
            ],
        },
      ],
      { useHash: true },
    ),
    LayoutsModule,
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
