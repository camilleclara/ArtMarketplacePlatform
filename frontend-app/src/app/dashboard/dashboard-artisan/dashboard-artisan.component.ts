import { Component } from '@angular/core';
import { OrdersComponent } from '../../orders/orders.component';
import { ProductsComponent } from "../../products/products.component";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [OrdersComponent, ProductsComponent],
  templateUrl: './dashboard-artisan.component.html',
  styleUrl: './dashboard-artisan.component.css'
})
export class DashboardArtisanComponent {

}
