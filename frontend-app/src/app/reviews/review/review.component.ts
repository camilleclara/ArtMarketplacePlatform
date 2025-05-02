import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReviewService } from '../review.service';
import { AuthenticationService } from '../../login/authentication.service';
import { Review } from '../../models/review.model';
import { CommonModule } from '@angular/common';
import { ReviewResponseComponent } from '../response/review-response/review-response.component';
import { ProductService } from '../../products/product.service';
import { AddReviewComponent } from '../add-review/add-review.component';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [CommonModule, ReviewResponseComponent, AddReviewComponent],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {
  @Input() reviewsByProduct!: Review[];
  respondingToReviewId: number | null = null;
  allowedToComment: boolean = false;
  isAddingReview: boolean = false;
  @Output() refreshRequest = new EventEmitter<boolean>();
  

  constructor(private reviewService: ReviewService, private authService: AuthenticationService, private productService: ProductService){}

  ngOnInit(): void{
    this.setAllowedToComment();
 }
  getProductName(): string {
    console.log(this.reviewsByProduct)
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

  isProductArtisan(){
    return this.authService.getUserId() == this.reviewsByProduct[0].product.artisanId;
  }

  setAllowedToComment(){
    const customerId = this.authService.getUserId(); // Ou autre méthode d'obtention du customerId

    this.productService.GetReviewableProductsForCustomer(customerId).subscribe({
      next: (reviewableProductIds: number[]) => {
        this.allowedToComment = reviewableProductIds.includes(this.reviewsByProduct[0].productId);
      },
      error: (err) => {
        console.error('Erreur lors de la récupération des produits commentables', err);
        this.allowedToComment = false;
      }
    });
  }

  canComment(): boolean {
    return this.allowedToComment;
  }

  startAddingReview(): void {
    this.isAddingReview = true;
  }

  cancelAddingReview(): void {
    this.isAddingReview = false;
  }

  onReviewSubmitted(newReview: Review): void {
    this.isAddingReview = false;
    this.refreshRequest.emit(true);
  }
}
