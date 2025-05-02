import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CustomerProductComponent } from '../../products/customer-product/customer-product.component';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductService } from '../../products/product.service';
import { ToastService } from '../../toast/toast.service';
import { ToastComponent } from "../../toast/toast/toast.component";

@Component({
  selector: 'app-dashboard-customer',
  standalone: true,
  imports: [CommonModule, CustomerProductComponent, ReactiveFormsModule, ToastComponent],
  templateUrl: './dashboard-customer.component.html',
  styleUrl: './dashboard-customer.component.css'
})
export class DashboardCustomerComponent {
  products: Product[] = [];
  filteredProducts: Product[] = [];

  categoryFilter = new FormControl('');
  artisanFilter = new FormControl('');
  priceSortOrder = new FormControl('asc');

  constructor(private productService: ProductService, private toastService: ToastService) {}

  ngOnInit() {
    this.productService.GetProducts().subscribe((data: Product[]) => {
      this.products = data;
      this.products = this.products.filter(p => p.isAvailable);
      this.filteredProducts = [...this.products];
      
    });

    this.categoryFilter.valueChanges.subscribe(_ => this.applyFilters());
    this.artisanFilter.valueChanges.subscribe(_ => this.applyFilters());
    this.priceSortOrder.valueChanges.subscribe(_ => this.applyFilters());
    this.checkForRecentOrder();
  }

  applyFilters() {
    let filtered = [...this.products];
  
    if (this.categoryFilter.value) {
      filtered = filtered.filter(p => p.category === this.categoryFilter.value);
    }
    const artisanId = this.artisanFilter?.value ? +this.artisanFilter.value : null;
    
    if (artisanId) {
      filtered = filtered.filter(p => p.artisanId === artisanId);
    }
  
    if (this.priceSortOrder.value === 'asc') {
      filtered.sort((a, b) => {
        const priceA = a.price ?? 0;
        const priceB = b.price ?? 0;
        return priceA - priceB;
      });
    } else if (this.priceSortOrder.value === 'desc') {
      filtered.sort((a, b) => {
        const priceA = a.price ?? 0;
        const priceB = b.price ?? 0;
        return priceB - priceA;
      });
    }
  
    this.filteredProducts = filtered;
  }

  getUniqueCategories(): (string| undefined)[] {
    return [...new Set(this.products.map(p => p.category))];
  }

  getUniqueArtisans(): { id: number, name: string }[] {
    const uniqueArtisanMap = new Map<number, string>();
    for (const product of this.products) {
      if (!uniqueArtisanMap.has(product.artisanId)) {
        uniqueArtisanMap.set(product.artisanId, product.artisanName ?? '');
      }
    }
    return Array.from(uniqueArtisanMap, ([id, name]) => ({ id, name }));
  }

  checkForRecentOrder() {
    const orderJustConfirmed = localStorage.getItem('orderJustConfirmed');
    
    if (orderJustConfirmed === 'true') {
      this.toastService.show(
        'Votre commande a été passée avec succès. Merci pour votre achat !', 
        'success', 
        6000
      );
      
      localStorage.removeItem('orderJustConfirmed');
    }
  }
}
