import { Component, Input } from '@angular/core';
import { ReviewService } from '../review.service';
import { AuthenticationService } from '../../login/authentication.service';
import { Review } from '../../models/review.model';

@Component({
  selector: 'app-review',
  standalone: true,
  imports: [],
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {
  @Input() review!: Review;
  constructor(private reviewService: ReviewService, private authService: AuthenticationService){

  }
}
