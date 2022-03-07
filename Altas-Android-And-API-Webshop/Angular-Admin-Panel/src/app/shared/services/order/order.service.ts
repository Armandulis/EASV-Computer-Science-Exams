import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductStatus } from '../../Models/productStatus';
import { dataFilter } from '../../Models/dataFilter';
import {environment} from "../../../../environments/environment";

@Injectable({ providedIn: 'root' })
export class OrderService {
  private apiUrl = `${environment.apiBaseUrl}/api/`;

  constructor(private http: HttpClient) { }


  getProductStatusFiltered(LastItemId: string): Observable<ProductStatus[]> {
    return this.http.get<ProductStatus[]>(this.apiUrl + 'productStatus' + LastItemId);
  }

  getOrderByID(id: string): Observable<ProductStatus> {
    return this.http.get<ProductStatus>(this.apiUrl + 'products/' + id)
  }
  updateOrder(updatedStatus: ProductStatus): Observable<ProductStatus> {
    return this.http.put<ProductStatus>(this.apiUrl + 'productStatus', updatedStatus );
  }
  deleteOrder(id: string) {
    return this.http.delete<ProductStatus>(this.apiUrl + 'products/' + id);
  }
}
