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

  GetOrdersByPartnerId(id: number): Observable<any>{
    return this.http.get(`https://localhost:7279/api/Order/partner/${id}`)
}
  GetOrdersByCustomerId(id: number): Observable<any>{
    return this.http.get(`https://localhost:7279/api/Order/customer/${id}`)
}
  GetOrderById(id: number): Observable<any>{
    return this.http.get(`https://localhost:7279/api/Order/${id}`)
  }
  UpdateDeliveryStatus(orderid: number, activeDelivery: Delivery)
  {
    return this.http.put(`https://localhost:7279/api/Delivery/status/${orderid}`, activeDelivery)
  }
  createOrder(order: any): Observable<any> {
    console.log("create");
    return this.http.post(`https://localhost:7279/api/Order`, order);
  }
}
