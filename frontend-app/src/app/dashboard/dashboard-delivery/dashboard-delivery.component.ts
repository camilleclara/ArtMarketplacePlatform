import { Component } from '@angular/core';
import { OrdersComponent } from '../../orders/orders.component';

@Component({
  selector: 'app-dashboard-delivery',
  standalone: true,
  imports: [OrdersComponent],
  templateUrl: './dashboard-delivery.component.html',
  styleUrl: './dashboard-delivery.component.css'
})
export class DashboardDeliveryComponent {

}
