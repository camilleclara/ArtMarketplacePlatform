import { Component } from '@angular/core';
import { Order } from '../models/order.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OrderService } from './order.service';
import { AuthenticationService } from '../login/authentication.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
    orders: Order[]=[];
    editingOrder: Order | null = null;
    orderForm: FormGroup = new FormGroup({
      id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("", [Validators.required])
    }) 
  constructor(private orderService: OrderService, private authService: AuthenticationService) {
    this.initForm();
  }
  ngOnInit(): void {
    console.log('✅ Dashboard Artisan créé avec succès !');
  }
  
  initForm(){
      this.orderService.GetOrdersByArtisanId(this.authService.getUserId())
        .subscribe((response: Order[]) => {
          this.orders=response
        });
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("", [Validators.required])
      });
      this.editingOrder = null;
    };
}
