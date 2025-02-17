import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) {

  }

  getUserRoles(): any {
    let token = sessionStorage.getItem("jwt")??"";
    let decodedToken: any = jwtDecode(token);
    return decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  }

   Login(userName: string, password: string): Observable<any>{
      return this.http.post(`https://localhost:7279/authentication/Login?password=${password}&login=${userName}`, null);
   }
}
