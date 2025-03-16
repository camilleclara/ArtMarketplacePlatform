import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  ARTISAN_ROLE: string = 'ARTISAN';
  ADMIN_ROLE: string = 'ADMIN';
  CUSTOMER_ROLE: string = 'CUSTOMER';
  DELIVERY_ROLE: string = 'DELIVERY_PARTNER';


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
      return this.http.post(`https://localhost:7279/api/Auth/login?password=${password}&login=${userName}`, null);
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
}
