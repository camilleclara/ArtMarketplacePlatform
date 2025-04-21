import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http:HttpClient) { }

  GetReviewsByArtisanId(id: number): Observable<any>{
      return this.http.get(`https://localhost:7279/Review/artisan/${id}`);
    }
}
