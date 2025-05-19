import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
   private baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) {}

  GetProducts(): Observable<any>{
    return this.http.get(`${this.baseUrl}/Product`);
  }

  GetProductById(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/Product/${id}`)
  }

  GetProductsByArtisanId(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/api/Artisans/${id}/products`)
  }

  GetArtisanProductById(artisanId: number, productId: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/api/Artisans/${artisanId}/products/${productId}`)
  }

  UpdateProduct(artisanId: number, productId: number, updatedProduct: Product): Observable<Product> {
    console.log(updatedProduct)
    return this.http.put<Product>(`${this.baseUrl}/api/Artisans/${artisanId}/products/${productId}`, updatedProduct);  
  }

  DeleteProduct(artisanId: number, productId: number): Observable<Product> {
    return this.http.delete<Product>(`${this.baseUrl}/api/Artisans/${artisanId}/products/${productId}`);  
  }

  CreateProduct(artisanId: number, newProduct: Product): Observable<Product> {
    return this.http.post<Product>(`${this.baseUrl}/api/Artisans/${artisanId}/products`, newProduct);  
  }

  GetReviewableProductsForCustomer(customerId: number): Observable<any>{
    return this.http.get<Product>(`${this.baseUrl}/Product/reviewable/${customerId}`)
  }
}
