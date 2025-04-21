import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReviewService } from '../review.service';
import { AuthenticationService } from '../../login/authentication.service';
import { Review } from '../../models/review.model';
import { CommonModule } from '@angular/common';
import { ReviewResponseComponent } from '../response/review-response/review-response.component';
import { ReviewsComponent } from '../reviews.component';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [CommonModule, ReviewResponseComponent],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {
  @Input() reviewsByProduct!: Review[];
  respondingToReviewId: number | null = null;
  @Output() refreshRequest = new EventEmitter<boolean>();

  constructor(private reviewService: ReviewService, private authService: AuthenticationService){}
  getProductName(): string {
    return this.reviewsByProduct[0].product.name
  }

  getCustomerReviews(): Review[] {
    return this.reviewsByProduct.filter((review: { fromArtisan: any; }) => !review.fromArtisan);
  }

  getArtisanResponse(customerId: string): Review | undefined {
    return this.reviewsByProduct.find(
      (      review: { fromArtisan: any; customerId: string; }) => review.fromArtisan && review.customerId === customerId
    );
  }

  getFullStars(score: number): number[] {
    return Array(score).fill(0);
  }
  
  getEmptyStars(score: number): number[] {
    return Array(5 - score).fill(0);
  }

  startResponse(reviewId: number): void {
    this.respondingToReviewId = reviewId;
  }
  
  cancelResponse(): void {
    this.respondingToReviewId = null;
  }
  
  onResponseSubmitted(newResponse: Review): void {
    this.respondingToReviewId = null;
    this.refreshRequest.emit(true);
  }
}
