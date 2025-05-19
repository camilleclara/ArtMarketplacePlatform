import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../models/review.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
   private baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  GetReviewsByArtisanId(id: number): Observable<any>{
      return this.http.get(`${this.baseUrl}/Review/artisan/${id}`);
  }

  createReview(review: Partial<Review>): Observable<Review> {
    return this.http.post<Review>(`${this.baseUrl}/Review`, review);
  }
}
