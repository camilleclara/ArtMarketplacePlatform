import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { Product } from '../models/product.model';
import { Order } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private baseUrl = 'https://localhost:7279';

  constructor(private http: HttpClient) { }

  // Gestion des utilisateurs
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/api/Users`);
  }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/api/Users/${userId}`);
  }

  updateUser(userId: number, user: User): Observable<User> {
    console.log(user);
    return this.http.put<User>(`${this.baseUrl}/api/Users/${userId}`, user);
  }

  approveUser(userId: number): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/api/users/${userId}/approve`, {});
  }

  deactivateUser(userId: number): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/api/users/${userId}/deactivate`, {});
  }


  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/Product/admin`);
  }

  approveProduct(productId: number): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/Product/approve/${productId}`, {});
  }

  rejectProduct(productId: number): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/Product/deactivate/${productId}`, {});
  }

  updateProduct(productId: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.baseUrl}/Product/${productId}`, product);
  }

  deleteProduct(productId: number): Observable<void> {
    console.log(productId);
    return this.http.delete<void>(`${this.baseUrl}/Product/${productId}`);
  }


  getOrderStatistics(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/statistics/orders`);
  }
  
  getTrendingProducts(limit: number = 10): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/statistics/trending-products?limit=${limit}`);
  }
  
  getUserActivityStatistics(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/api/statistics/user-activity`);
  }

  getRecentOrders(limit: number = 10): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.baseUrl}/api/statistics/recent-orders?limit=${limit}`);
  }
}
