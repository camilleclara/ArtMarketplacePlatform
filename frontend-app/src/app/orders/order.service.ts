import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Delivery } from '../models/delivery.model';

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
  UpdateDeliveryStatus(orderid: number, activeDelivery: Delivery)
  {
    return this.http.put(`https://localhost:7279/api/Delivery/status/${orderid}`, activeDelivery)
  }
}
