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
    this.reviewService.GetReviewsByArtisanId(this.authService.getUserId())
    .subscribe((response: Review[]) => {
              this.reviews=response
              this.groupReviewsByProduct();
            });
  }
  groupReviewsByProduct(): void {
    // Implémentation manuelle du groupement sans lodash
    this.reviewsByProduct = {};
    
    this.reviews.forEach(review => {
      if (!this.reviewsByProduct[review.productId]) {
        this.reviewsByProduct[review.productId] = [];
      }
      this.reviewsByProduct[review.productId].push(review);
      console.log(this.reviewsByProduct)
      console.log(this.reviewsByProduct[3]);
    });
  }

  getProductName(productId: string): string {
    const reviews = this.reviewsByProduct[productId];
    return reviews && reviews.length > 0 ? reviews[0].product.name : 'Produit inconnu';
  }

  getCustomerReviews(productId: string): Review[] {
    return this.reviewsByProduct[productId].filter(review => !review.fromArtisan);
  }

  getArtisanResponse(productId: string, customerId: string): Review | undefined {
    return this.reviewsByProduct[productId].find(
      review => review.fromArtisan && review.customerId === customerId
    );
  }
  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }
  getFullStars(score: number): number[] {
    return Array(score).fill(0);
  }
  
  getEmptyStars(score: number): number[] {
    return Array(5 - score).fill(0);
  }
}
