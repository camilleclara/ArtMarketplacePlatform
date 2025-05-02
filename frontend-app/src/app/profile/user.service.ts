import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`https://localhost:7279/api/Users/${userId}`);
  }

  updateUserById(userId: number, user: Partial<User>): Observable<User> {
    return this.http.put<User>(`https://localhost:7279/api/Users/${userId}`, user);
  }
}
