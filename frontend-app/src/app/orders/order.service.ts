import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http:HttpClient) 
  {
    
  }
  GetOrdersByArtisanId(id: number): Observable<any>{
      return this.http.get(`https://localhost:7279/api/Order/artisan/${id}`)
    }
}
