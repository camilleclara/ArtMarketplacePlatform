import { Component, Input } from '@angular/core';
import { Order } from '../../../models/order.model';
import { OrderService } from '../../order.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  @Input() orderId!: number;
  order: Order | null = null;
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private orderService: OrderService) {}

  ngOnInit(): void {
    if (this.orderId) {
      this.fetchOrder();
    }
  }

  fetchOrder() {
    this.isLoading = true;
    this.orderService.GetOrderById(this.orderId).subscribe({
      next: (order: Order | null) => {
        this.order = order;
        console.log(order);
        this.isLoading = false;
      },
      error: (err: any) => {
        this.error = 'Erreur lors de la récupération de la commande.';
        console.error(err);
        this.isLoading = false;
      }
    });
  }
}
