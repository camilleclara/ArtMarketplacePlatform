import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) {}

  GetProducts(): Observable<any>{
    return this.http.get("https://localhost:7279/Product")
  }

  GetAdminProducts(): Observable<any>{
    return this.http.get("https://localhost:7279/Product/Admin")
  }

  GetProductsByArtisanId(id: number): Observable<any>{
    return this.http.get(`https://localhost:7279/api/Artisans/${id}/products`)
  }

  GetArtisanProductById(artisanId: number, productId: number): Observable<any>{
    return this.http.get(`https://localhost:7279/api/Artisans/${artisanId}/products/${productId}`)
  }

  UpdateProduct(artisanId: number, productId: number, updatedProduct: Product): Observable<Product> {
    return this.http.put<Product>(`https://localhost:7279/api/Artisans/${artisanId}/products/${productId}`, updatedProduct);  
  }

  DeleteProduct(artisanId: number, productId: number): Observable<Product> {
    return this.http.delete<Product>(`https://localhost:7279/api/Artisans/${artisanId}/products/${productId}`);  
  }

  CreateProduct(artisanId: number, newProduct: Product): Observable<Product> {
    return this.http.post<Product>(`https://localhost:7279/api/Artisans/${artisanId}/products`, newProduct);  
  }
}
