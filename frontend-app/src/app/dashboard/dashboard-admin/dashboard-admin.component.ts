import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminProductsComponent } from '../../admin/admin-products/admin-products.component';
import { AdminUsersComponent } from '../../admin/admin-users/admin-users.component';
import { AdminStatisticsComponent } from '../../admin/admin-statistics/admin-statistics.component';

@Component({
  selector: 'app-dashboard-admin',
  standalone: true,
  imports: [CommonModule, 
    RouterModule,
    AdminUsersComponent,
    AdminProductsComponent, AdminStatisticsComponent],
  templateUrl: './dashboard-admin.component.html',
  styleUrl: './dashboard-admin.component.css'
})
export class DashboardAdminComponent {
  activeTab: string = 'statistics';

  setActiveTab(tab: string): void {
    this.activeTab = tab;
  }
}
