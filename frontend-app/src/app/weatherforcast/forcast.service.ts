import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ForcastService {

  constructor(private http:HttpClient) {
    
  }
  // GetForecast():Observable<any>  {
  //   const token= sessionStorage.getItem("jwt");
  //   return this.http.get("https://localhost:7279/WeatherForecast", {headers: {'Authorization':'Bearer '+token, 'Content-Type': 'application/json'} })
  // }
  GetForecast(): Observable<any>{
    return this.http.get("https://localhost:7279/WeatherForecast")
  }

  GetForecastTwee(): Observable<any>{
    return this.http.get("https://localhost:7279/WeatherForecast/GetTwee")
  }

}
