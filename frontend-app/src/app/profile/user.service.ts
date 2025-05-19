import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
   private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/api/Users/${userId}`);
  }

  updateUserById(userId: number, user: Partial<User>): Observable<User> {
    return this.http.put<User>(`${this.baseUrl}/api/Users/${userId}`, user);
  }

  getAllDeliveryPartners(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/api/Users/partners`);
  }

  getAllArtisans(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/api/Users/artisans`);
  }

}
