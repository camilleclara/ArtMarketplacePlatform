import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { Router } from '@angular/router';
import { ProductImage } from '../../models/product-image.model';
import { BasketService } from '../../basket/basket.service';

@Component({
  selector: 'app-customer-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './customer-product.component.html',
  styleUrl: './customer-product.component.css'
})
export class CustomerProductComponent {
  @Input() product!: Product;

  constructor(private router: Router,  private basketService: BasketService) {}

  getFirstImage(): string {
    if (this.product.productImages && this.product.productImages.length > 0) {
      
      const image: ProductImage = this.product.productImages[0];
      return `data:${image.mimeType};base64,${image.content}`;
    }
    return 'assets/default-product.jpg';
  }

  goToDetails(event: Event) {
    //Pour empêcher d'ajouter au panier, stop propagation de l'event
    event.stopPropagation();
    this.router.navigate(['/product', this.product.id]);
  }

  addToBasket(event: Event): void {
    event.stopPropagation(); // Empêche la navigation vers les détails
    this.basketService.addToBasket(this.product);
  }

  removeFromBasket(event: Event): void {
    event.stopPropagation(); // Empêche la navigation vers les détails
    this.basketService.removeFromBasket(this.product.id);
  }

  getQuantityInBasket(): number {
    return this.basketService.getItemQuantity(this.product.id);
  }
}
