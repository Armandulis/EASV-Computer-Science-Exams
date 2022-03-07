import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AuthInterceptor, AuthService } from './shared/services';

import { AppRoutingModule } from './app-routing.module';
import { PagesRoutingModule } from "./pages/authpages/pages-routing.module";
import { AppComponent } from './app.component';
import { DashboardModule } from './pages/dashboard';
import { ProductsModule } from './pages/products';
import { CreateProductModule } from './pages/products/productcreate';
import { UpdateProductModule } from './pages/products/productupdate';
import { PagesModule } from "./pages/authpages/pages.module";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PagesRoutingModule,
    DashboardModule,
    ProductsModule,
    CreateProductModule,
    UpdateProductModule,
    HttpClientModule,
    PagesModule,
  ],
  providers: [
    AuthService,
    {

      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,

    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
