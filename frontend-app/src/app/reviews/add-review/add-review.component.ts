import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Review } from '../../models/review.model';
import { ReviewService } from '../review.service';
import { AuthenticationService } from '../../login/authentication.service';

@Component({
  selector: 'app-add-review',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-review.component.html',
  styleUrl: './add-review.component.css'
})
export class AddReviewComponent {

  @Input() productId!: number;
  @Output() reviewSubmitted = new EventEmitter<Review>();
  @Output() cancelReview = new EventEmitter<void>();
  reviewForm!: FormGroup;
  currentRating: number = 0;
  hoverRating: number = 0;

  constructor(
    private fb: FormBuilder,
    private reviewService: ReviewService,
    private authService: AuthenticationService
  ) {}
  
  ngOnInit(): void {
    this.initForm();
  }
  
  initForm(): void {
    this.reviewForm = this.fb.group({
      rating: [null, [Validators.required, Validators.min(1), Validators.max(5)]],
      comment: ['', [Validators.required, Validators.minLength(10)]]
    });
  }
  
  setRating(rating: number): void {
    this.currentRating = rating;
    this.reviewForm.get('rating')?.setValue(rating);
  }
  
  onSubmit(): void {
    if (this.reviewForm.invalid) {
      Object.keys(this.reviewForm.controls).forEach(key => {
        const control = this.reviewForm.get(key);
        control?.markAsTouched();
      });
      return;
    }
    
    const review: Partial<Review> = {
      id: 0,
      productId: this.productId,
      customerId: this.authService.getUserId(),
      content: this.reviewForm.value.comment,
      score: this.reviewForm.value.rating,
      fromArtisan: false,
      created: new Date()
    };
    
    this.reviewService.createReview(review).subscribe({
      next: (createdReview: Review) => {
        this.reviewSubmitted.emit(createdReview);
        this.resetForm();
      },
      error: (err: any) => {
        console.error('Erreur lors de la création de la review', err);
      }
    });
  }
  
  cancel(): void {
    this.cancelReview.emit();
    this.resetForm();
  }
  
  private resetForm(): void {
    this.reviewForm.reset();
    this.currentRating = 0;
    this.hoverRating = 0;
  }
}
