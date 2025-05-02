import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-order-summary-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-summary-modal.component.html',
  styleUrl: './order-summary-modal.component.css'
})
export class OrderSummaryModalComponent {
  @Input() basketItems: any[] = [];
  @Input() totalPrice: number = 0;

  constructor(
    public activeModal: NgbActiveModal,
    private router: Router
  ) {}

  closeModal() {
    this.activeModal.close();
  }

  confirmOrder() {
    this.activeModal.close('confirmed');
    this.router.navigate(['/dashboard-customer']);
  }
}

