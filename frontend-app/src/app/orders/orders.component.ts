import { Component } from '@angular/core';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  ngOnInit(): void {
    console.log('✅ Dashboard Artisan créé avec succès !');
  }
}
