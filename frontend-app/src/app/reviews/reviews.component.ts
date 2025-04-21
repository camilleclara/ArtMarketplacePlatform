import { Component } from '@angular/core';
import { Review } from '../models/review.model';
import { ReviewService } from './review.service';
import { AuthenticationService } from '../login/authentication.service';
import { ReviewComponent } from './review/review.component';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-reviews',
  standalone: true,
  imports: [ReviewComponent, CommonModule],
  templateUrl: './reviews.component.html',
  styleUrl: './reviews.component.css'
})
export class ReviewsComponent {
  reviews: Review[] = [];
  reviewsByProduct: { [key: string]: Review[] } = {};

  constructor(private reviewService: ReviewService, private authService: AuthenticationService){}

  ngOnInit(): void {
    this.loadInitialData();
  }
  reloadData(): void {
    // Recharger les données comme dans ngOnInit
    this.loadInitialData();
  }

  loadInitialData(): void {
    this.reviewService.GetReviewsByArtisanId(this.authService.getUserId())
    .subscribe((response: Review[]) => {
              this.reviews=response
              this.groupReviewsByProduct();
            });
  }
  groupReviewsByProduct(): void {
    this.reviewsByProduct = {};
    
    this.reviews.forEach(review => {
      if (!this.reviewsByProduct[review.productId]) {
        this.reviewsByProduct[review.productId] = [];
      }
      this.reviewsByProduct[review.productId].push(review);
    });
  }
  
  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
}
