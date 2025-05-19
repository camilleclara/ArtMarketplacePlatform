import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';
import { User, UserType } from '../models/user.model';
import { UserRegister } from '../models/user-register.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private baseUrl = environment.apiUrl;
  readonly CUSTOMER_ROLE = UserType.CUSTOMER;
  readonly ARTISAN_ROLE = UserType.ARTISAN;
  readonly ADMIN_ROLE = UserType.ADMIN;
  readonly DELIVERY_PARTNER_ROLE = UserType.DELIVERY_PARTNER;


  constructor(private http: HttpClient) {

  }

  getUserRole(): any {
    let token = sessionStorage.getItem("jwt")??"";
    if (!token) return '';
    let decodedToken: any = jwtDecode(token);
    return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  }

  getUserId(): any {
    let token = sessionStorage.getItem("jwt")??"";
    if (!token) return '';
    let decodedToken: any = jwtDecode(token);
    return decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
  }

   Login(userName: string, password: string): Observable<any>{
      return this.http.post(`${this.baseUrl}/api/Auth/login?password=${password}&login=${userName}`, null);
   }

   isAdmin(): boolean {
    return this.getUserRole() === this.ADMIN_ROLE;
   }

   isArtisan(): boolean {
    return this.getUserRole() === this.ARTISAN_ROLE;
   }

   isCustomer(): boolean {
    return this.getUserRole() === this.CUSTOMER_ROLE;
   }

   isLoggedIn(): boolean {
    const token = sessionStorage.getItem("jwt");
    return !!token;
  }
  logout(): void {
    sessionStorage.removeItem("jwt");
  }

  register(user: UserRegister):Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Auth/register?login=${user.login}&firstName=${user.firstName}&lastName=${user.lastName}&password=${user.password}&role=${user.role}&address=${user.address}`, user);
  }
}
