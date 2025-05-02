import { Component } from '@angular/core';
import { Product } from '../../models/product.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../product.service';
import { BasketService } from '../../basket/basket.service';
import { ToastService } from '../../toast/toast.service';
import { ProductImage } from '../../models/product-image.model';
import { ToastComponent } from '../../toast/toast/toast.component';
import { CommonModule } from '@angular/common';
import { ReviewComponent } from '../../reviews/review/review.component';

@Component({
  selector: 'app-customer-product-detail',
  standalone: true,
  imports: [ToastComponent, CommonModule, ReviewComponent],
  templateUrl: './customer-product-detail.component.html',
  styleUrl: './customer-product-detail.component.css'
})
export class CustomerProductDetailComponent {
  product: Product | null = null;
  loading: boolean = true;
  error: string | null = null;
  selectedImageIndex: number = 0;
  quantity: number = 1;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private basketService: BasketService,
    private toastService: ToastService

  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const productId = +params['id']; // Convertir en nombre
      if (productId) {
        this.loadProduct(productId);
      } else {
        this.error = "Identifiant de produit invalide";
        this.loading = false;

      }
    });
  }

  loadProduct(productId: number): void {
    this.loading = true;
    this.productService.GetProductById(productId).subscribe({
      next: (product) => {
        this.product = product;
        console.log(this.product);
        this.loading = false;
        const quantityInBasket = this.basketService.getItemQuantity(productId);

        if (quantityInBasket > 0) {
          this.quantity = quantityInBasket;
        }
      },
      error: (err) => {
        console.error('Erreur lors du chargement du produit', err);
        this.error = "Impossible de charger les détails du produit";
        this.loading = false;
      }
    });
  }

  getObjectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  getImageUrl(image: ProductImage): string {
    return `data:${image.mimeType};base64,${image.content}`;
  }

  getDefaultImage(): string {
    return 'assets/default-product.jpg';
  }

  getImages(): ProductImage[] {
    return this.product?.productImages?.length ? this.product.productImages : [];
  }

  selectImage(index: number): void {
    this.selectedImageIndex = index;
  }

  getCurrentImage(): string {
    const images = this.getImages();
    if (images.length > 0 && this.selectedImageIndex < images.length) {
      return this.getImageUrl(images[this.selectedImageIndex]);
    }
    return this.getDefaultImage();
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  increaseQuantity(): void {
    this.quantity++;
  }

  addToBasket(): void {
    if (!this.product) return;
    const currentQuantity = this.basketService.getItemQuantity(this.product.id);
    const quantityToAdd = this.quantity - currentQuantity;

    if (quantityToAdd > 0) {
      for (let i = 0; i < quantityToAdd; i++) {
        this.basketService.addToBasket(this.product);
      }
      this.toastService.show(`${this.quantity} × ${this.product.name} ajouté au panier`, 'success');
    } else if (quantityToAdd < 0) {
      for (let i = 0; i < Math.abs(quantityToAdd); i++) {
        this.basketService.removeFromBasket(this.product.id);
      }
      this.toastService.show(`Quantité de ${this.product.name} mise à jour`, 'info');
    } else {
      this.toastService.show(`${this.product.name} est déjà dans votre panier`, 'info');
    }
  }

  goToBasket(): void {
    this.router.navigate(['/basket']);
  }

  goBack(): void {
    this.router.navigate(['/dashboard-customer']);
  }

  refreshProduct(): void {
    if (this.product) {
      this.loadProduct(this.product.id);
      this.toastService.show('Avis ajouté avec succès', 'success');
    }
  }
}
