import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Delivery } from '../models/delivery.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
   private baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) 
  {
    
  }
  GetOrdersByArtisanId(id: number): Observable<any>{
      return this.http.get(`${this.baseUrl}/api/Order/artisan/${id}`)
  }

  GetOrdersByPartnerId(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/api/Order/partner/${id}`)
}
  GetOrdersByCustomerId(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/api/Order/customer/${id}`)
}
  GetOrderById(id: number): Observable<any>{
    return this.http.get(`${this.baseUrl}/api/Order/${id}`)
  }
  UpdateDeliveryStatus(orderid: number, activeDelivery: Delivery)
  {
    return this.http.put(`${this.baseUrl}/api/Delivery/status/${orderid}`, activeDelivery)
  }
  createOrder(order: any): Observable<any> {
    console.log("create");
    return this.http.post(`${this.baseUrl}/api/Order`, order);
  }
}
