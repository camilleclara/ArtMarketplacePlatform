import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductCategory } from '../../models/product-category.enum';
import { AdminService } from '../admin.service';
import { ProductImage } from '../../models/product-image.model';

@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-products.component.html',
  styleUrl: './admin-products.component.css'
})
export class AdminProductsComponent {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  editingProduct: Product | null = null;
  isLoading: boolean = false;
  searchTerm: string = '';
  filterCategory: string = '';
  categories = Object.values(ProductCategory);
  
  
  productForm: FormGroup = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    price: new FormControl('', [Validators.required, Validators.min(0)]),
    category: new FormControl('', [Validators.required]),
    isAvailable: new FormControl(true),
  });
  

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.isLoading = true;
      this.adminService.getAllProducts().subscribe({
        next: (data) => {
          this.products = data;
          this.applyFilters();
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error loading products', error);
          this.isLoading = false;
        }
      });
    
  }

  onSearch(event: Event): void {
    this.searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.applyFilters();
  }

  onFilterCategory(category: string): void {
    this.filterCategory = category;
    this.applyFilters();
  }

  // onFilterApproval(status: string): void {
  //   this.filterApproval = status;
  //   this.loadProducts();
  // }

  applyFilters(): void {
    this.filteredProducts = this.products.filter(product => {
      const matchesSearch = this.searchTerm ?
        product.name.toLowerCase().includes(this.searchTerm) ||
        product.description.toLowerCase().includes(this.searchTerm) : true;
        
      const matchesCategory = this.filterCategory ? product.category === this.filterCategory : true;
      
      return matchesSearch && matchesCategory;
    });
  }

  editProduct(product: Product): void {
    this.editingProduct = {...product};
    this.productForm.patchValue({
      name: product.name,
      description: product.description,
      price: product.price,
      category: product.category,
      isAvailable: product.isAvailable
    });
  }

  approveProduct(productId: number): void {
    if (confirm('Êtes-vous sûr de vouloir approuver ce produit ?')) {
      this.adminService.approveProduct(productId).subscribe({
        next: () => {
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error approving product', error);
        }
      });
    }
  }

  confirmReject(id: number): void {

    if (confirm('Êtes-vous sûr de vouloir supprimer ce produit ? Cette action est irréversible.')) {
      this.adminService.rejectProduct(id).subscribe({
        next: () => {
          this.cancelEdit();
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error rejecting product', error);
        }
      });
    }
  }
  
  deleteProduct(productId: number): void {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce produit ? Cette action est irréversible.')) {
      this.adminService.deleteProduct(productId).subscribe({
        next: () => {
          this.loadProducts();
        },
        error: (error) => {
          console.error('Error deleting product', error);
        }
      });
    }
  }

  saveProduct(): void {
    if (this.productForm.invalid || !this.editingProduct) {
      return;
    }

    const updatedProduct: Product = {
      ...this.editingProduct,
      name: this.productForm.value.name,
      description: this.productForm.value.description,
      price: this.productForm.value.price,
      category: this.productForm.value.category,
      isAvailable: this.productForm.value.isAvailable
    };

    this.adminService.updateProduct(updatedProduct.id, updatedProduct).subscribe({
      next: () => {
        this.cancelEdit();
        this.loadProducts();
      },
      error: (error) => {
        console.error('Error updating product', error);
      }
    });
  }

  cancelEdit(): void {
    this.editingProduct = null;
    this.productForm.reset();
  }

  getImageSrc(image: ProductImage): string {
    return `data:${image.mimeType};base64,${image.content}`;
  }
}
