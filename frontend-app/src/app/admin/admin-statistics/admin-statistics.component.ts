import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Product } from '../../models/product.model';
import { Order } from '../../models/order.model';
import { AdminService } from '../admin.service';
import { ProductCategoryService } from '../../login/product-category.service';
import { DeliveryStatus } from '../../models/delivery-status.enum copy';


@Component({
  selector: 'app-admin-statistics',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './admin-statistics.component.html',
  styleUrl: './admin-statistics.component.css'
})
export class AdminStatisticsComponent {
  isLoading: boolean = false;
  orderStats: any = null;
  trendingProducts: Product[] = [];
  userActivity: any = null;
  recentOrders: Order[] = [];
  categories = this.productCategoriesService.getCategories()

  constructor(private adminService: AdminService, private productCategoriesService: ProductCategoryService) { }

  ngOnInit(): void {
    this.loadAllStatistics();
  }

  loadAllStatistics(): void {
    this.isLoading = true;
    
    // Load order statistics
    this.adminService.getOrderStatistics().subscribe({
      next: (data) => {
        this.orderStats = data;
      },
      error: (error) => {
        console.error('Error loading order statistics', error);
      }
    });
    
    // Load trending products
    this.adminService.getTrendingProducts(5).subscribe({
      next: (data) => {
        this.trendingProducts = data;
      },
      error: (error) => {
        console.error('Error loading trending products', error);
      }
    });
    
    // Load user activity
    this.adminService.getUserActivityStatistics().subscribe({
      next: (data) => {
        this.userActivity = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading user activity', error);
        this.isLoading = false;
      }
    });
    
    // Load recent orders
    this.adminService.getRecentOrders(10).subscribe({
      next: (data) => {
        this.recentOrders = data;
      },
      error: (error) => {
        console.error('Error loading recent orders', error);
      }
    });
  }

  // Calculate percentage for visual display
  getPercentage(value: number, total: number): number {
    return total > 0 ? (value / total) * 100 : 0;
  }
}
