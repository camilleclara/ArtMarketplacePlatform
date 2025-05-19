import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Review } from '../models/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http:HttpClient) { }

  GetReviewsByArtisanId(id: number): Observable<any>{
      return this.http.get(`https://localhost:7279/Review/artisan/${id}`);
  }

  createReview(review: Partial<Review>): Observable<Review> {
    return this.http.post<Review>(`https://localhost:7279/Review`, review);
  }
}
