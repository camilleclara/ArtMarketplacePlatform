import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductImage } from '../../models/product-image.model';
import { Product } from '../../models/product.model';
import { ProductCategoryService } from '../../login/product-category.service';

@Component({
  selector: 'app-edit-product-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-product-modal.component.html',
  styleUrl: './edit-product-modal.component.css'
})
export class EditProductModalComponent {
  @Input() product: Product | null = null;
  @Output() save = new EventEmitter<Product>();
  @Output() cancel = new EventEmitter<void>();

  categories: string[] = [];
  
  productForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    price: new FormControl('', [Validators.required, Validators.min(0)]),
    category: new FormControl('', [Validators.required]),
    isAvailable: new FormControl(true),
  });
  
  constructor(private productCategoryService: ProductCategoryService) { 
    this.categories = this.productCategoryService.getAllCategories();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product'] && this.product) {
      this.productForm.patchValue({
        name: this.product.name,
        description: this.product.description,
        price: this.product.price,
        category: this.product.category,
        isAvailable: this.product.isAvailable
      });
    }
  }

  saveProduct(): void {
    if (this.productForm.invalid || !this.product) {
      return;
    }
    
    const updatedProduct: Product = {
      ...this.product,
      name: this.productForm.value.name,
      description: this.productForm.value.description,
      price: this.productForm.value.price,
      category: this.productForm.value.category,
      isAvailable: this.productForm.value.isAvailable
    };
    
    this.save.emit(updatedProduct);
  }

  cancelEdit(): void {
    this.cancel.emit();
  }

  getImageSrc(image: ProductImage): string {
    return `data:${image.mimeType};base64,${image.content}`;
  }
}
