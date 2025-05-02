import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BasketItem } from '../../models/basket.model';
import { BasketService } from '../basket.service';
import { Router } from '@angular/router';
import { ModalService } from '../../modal/modal.service';
import { AuthenticationService } from '../../login/authentication.service';
import { OrderSummaryModalComponent } from '../../modal/order-summary-modal/order-summary-modal.component';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  basketItems: BasketItem[] = [];
  totalPrice = 0;

  constructor(
    private basketService: BasketService,
    private router: Router,
    private modalService: ModalService,
    private authService: AuthenticationService,
  ) {}
  
  ngOnInit(): void {
    this.basketService.getBasket().subscribe(items => {
      this.basketItems = items;
      this.totalPrice = this.basketService.getTotalPrice();
    });
  }
  
  increaseQuantity(productId: number): void {
    const item = this.basketItems.find(i => i.product.id === productId);
    if (item) {
      this.basketService.addToBasket(item.product);
    }
  }
  
  decreaseQuantity(productId: number): void {
    this.basketService.removeFromBasket(productId);
  }
  
  removeItem(productId: number, event: Event): void {
    event.preventDefault();
    while (this.basketService.getItemQuantity(productId) > 0) {
      this.basketService.removeFromBasket(productId);
    }
  }
  
  clearBasket(): void {
    this.basketService.clearBasket();
  }
  
  checkout(): void {
    if (this.authService.isLoggedIn()) {
      this.showOrderSummary();
    } else {
      this.showAuthModal();
    }
  }

  showOrderSummary(): void {
    const modalRef = this.modalService.openModal(OrderSummaryModalComponent);
    modalRef.componentInstance.basketItems = this.basketItems;
    modalRef.componentInstance.totalPrice = this.totalPrice;
    
    modalRef.result.then((result) => {
      if (result === 'confirmed') {
        // La commande est confirmée, nettoyer le panier
        this.basketService.clearBasket();
        this.router.navigate(['/dashboard-customer']);
      }
    }, (reason) => {
      // Modal fermé sans confirmation
      console.log('Order modal closed', reason);
    });
  }

  showAuthModal(): void {
    window.alert("need to login or create account");
  }
  
  continueShoppingClick(): void {
    this.router.navigate(['/dashboard-customer']);
  }
}
