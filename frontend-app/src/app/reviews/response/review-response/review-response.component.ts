import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Review } from '../../../models/review.model';
import { ReviewService } from '../../review.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-review-response',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './review-response.component.html',
  styleUrl: './review-response.component.css'
})
export class ReviewResponseComponent {
  @Input() review!: Review;
  @Output() responseSubmitted = new EventEmitter<Review>();
  @Output() cancelled = new EventEmitter<void>();
  
  responseForm: FormGroup;
  

  constructor(
    private reviewService: ReviewService,
    private fb: FormBuilder
  ) {
    this.responseForm = this.fb.group({
      content: ['', Validators.required]
    });
  }

  submitResponse(): void {
    if (this.responseForm.invalid) return;

    const response: Partial<Review> = {
      productId: this.review.productId,
      customerId: this.review.customerId,
      score: this.review.score,
      content: this.responseForm.get('content')?.value,
      fromArtisan: true
    };
    console.log(response);
    this.reviewService.createReview(response).subscribe({
      next: (createdReview) => {
        this.responseSubmitted.emit(createdReview);
        this.responseForm.reset();
      },
      error: (error) => {
        console.error('Erreur lors de la création de la réponse:', error);
      }
    });
  }

  cancel(): void {
    this.cancelled.emit();
    this.responseForm.reset();
  }
}
