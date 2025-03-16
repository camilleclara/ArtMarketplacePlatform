import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) {

  }
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
}
