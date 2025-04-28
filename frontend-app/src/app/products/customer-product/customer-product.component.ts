import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Product } from '../../models/product.model';
import { Router } from '@angular/router';
import { ProductImage } from '../../models/product-image.model';

@Component({
  selector: 'app-customer-product',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './customer-product.component.html',
  styleUrl: './customer-product.component.css'
})
export class CustomerProductComponent {
  @Input() product!: Product;

  constructor(private router: Router) {}

  getFirstImage(): string {
    if (this.product.productImages && this.product.productImages.length > 0) {
      
      const image: ProductImage = this.product.productImages[0];
      return `data:${image.mimeType};base64,${image.content}`;
    }
    return 'assets/default-product.jpg';
  }

  goToDetails() {
    this.router.navigate(['/product', this.product.id]);
  }
}
