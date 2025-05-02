import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationService } from '../../login/authentication.service';
import { OrderService } from '../../orders/order.service';
import { forkJoin } from 'rxjs';

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
    private router: Router, 
    private authService: AuthenticationService,
    private orderService: OrderService
  ) {}

  closeModal() {
    this.activeModal.close();
  }

  numberOfArtisans(): number {
    const artisanIds = new Set<number>();

    for (const item of this.basketItems) {
      if (item.product?.artisanId != null) {
        artisanIds.add(item.product.artisanId);
      }
    }

    return artisanIds.size;
  }

  groupOrdersByArtisan(): any[] {
    const grouped: { [artisanId: number]: any[] } = {};
  
    this.basketItems.forEach(item => {
      const artisanId = item.product.artisanId;
      if (!grouped[artisanId]) {
        grouped[artisanId] = [];
      }
      grouped[artisanId].push(item);
    });
  
    const customerId = this.authService.getUserId();
  
    return Object.entries(grouped).map(([artisanId, items]) => {
      const products = items.map((item: any) => ({
        productId: item.product.id,
        quantity: item.quantity
      }));
  
      const total = items.reduce((sum: number, item: any) =>
        sum + (item.product.price || 0) * item.quantity, 0);
  
      return {
        artisanId: artisanId,
        customerId,
        total,
        activeDelivery: {},
        products
      };
    });
  }

  confirmOrder() {
    this.activeModal.close('confirmed');
    this.router.navigate(['/dashboard-customer']);

    const orders = this.groupOrdersByArtisan();

    const requests = orders.map(order =>
      this.orderService.createOrder(order)
    );
  
    forkJoin(requests).subscribe({
      next: () => {
        this.activeModal.close('confirmed');
        this.router.navigate(['/dashboard-customer']);
      },
      error: err => {
        console.error('Erreur lors de la création de commandes :', err);
      }
    });
  }

  getDeliveryCost(): number {
    return this.numberOfArtisans() * 5;
  }
  
  getFinalTotal(): number {
    return this.totalPrice + this.getDeliveryCost();
  }
}

