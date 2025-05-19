import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { AdminService } from '../admin.service';
import { ProductImage } from '../../models/product-image.model';
import { EditProductModalComponent } from '../../modal/edit-product-modal/edit-product-modal.component';
import { ProductCategoryService } from '../../login/product-category.service';


@Component({
  selector: 'app-admin-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, EditProductModalComponent],
  templateUrl: './admin-products.component.html',
  styleUrl: './admin-products.component.css'
})
export class AdminProductsComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  editingProduct: Product | null = null;
  isLoading: boolean = false;
  searchTerm: string = '';
  filterCategory: string = '';
  categories: string[] = [];

  constructor(
    private adminService: AdminService,
    private productCategoryService: ProductCategoryService
  ) { }
  
  ngOnInit(): void {
    this.categories = this.productCategoryService.getAllCategories();
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
  
  onSaveProduct(updatedProduct: Product): void {
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
  }
  
  getImageSrc(image: ProductImage): string {
    return `data:${image.mimeType};base64,${image.content}`;
  }
}