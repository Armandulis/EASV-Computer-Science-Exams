import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../Models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  apiUrl = 'http://altas.gear.host/api/';

  constructor(private http: HttpClient) { }

  getAllProducts(filter: string): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl + 'products' + filter);
  }
  createProduct(newProduct: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl + 'products', newProduct);
  }

  getByID(id: string): Observable<Product> {
    return this.http.get<Product>(this.apiUrl + 'products/' + id)
  }
  updateProduct(id: string, updatedProduct: Product): Observable<Product> {
    return this.http.put<Product>(this.apiUrl + 'products/' + id, updatedProduct );
  }
  deleteProduct(id: string) {
    return this.http.delete<Product>(this.apiUrl + 'products/' + id);
  }
}
