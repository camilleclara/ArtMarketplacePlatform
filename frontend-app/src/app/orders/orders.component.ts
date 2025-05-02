import { Component, Input } from '@angular/core';
import { Order } from '../models/order.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrderService } from './order.service';
import { AuthenticationService } from '../login/authentication.service';
import { CommonModule } from '@angular/common';
import { DeliveryStatus } from '../models/delivery-status.enum copy';
import { OrderComponent } from './order/order/order.component';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, OrderComponent],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
    @Input()displayForArtisan!: boolean;
    orders: Order[]=[];
    orderDetails = false;
    detailsId: number = 0;
    editingOrder: Order | null = null;
    editingDeliveryOrderId: number | null = null;
    categories = Object.values(DeliveryStatus);
    orderForm: FormGroup = new FormGroup({
      id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("", [Validators.required])
    }) 
  constructor(private orderService: OrderService, private authService: AuthenticationService) {
    this.initForm();
  }
  ngOnInit(): void {
    
  }
  
  initForm(){
    if(this.displayForArtisan){
      console.log("in if");
      console.log(this.displayForArtisan);
      this.orderService.GetOrdersByArtisanId(this.authService.getUserId())
      .subscribe((response: Order[]) => {
        this.orders=response
      });
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("NEW", [Validators.required])
      });
      this.editingOrder = null;
      }
    else {
      this.orderService.GetOrdersByCustomerId(this.authService.getUserId())
      .subscribe((response: Order[]) => {
        this.orders=response
      });
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("NEW", [Validators.required])
      });
      this.editingOrder = null;
    }
    };
    onEditDeliveryStatus(orderId: number) {
      this.editingDeliveryOrderId = orderId;
      const orderToEdit = this.orders.find(o => o.id === orderId);
      if (orderToEdit && orderToEdit.activeDelivery) {
        this.orderForm.get('status')?.setValue(orderToEdit.activeDelivery.deliStatus);
      }
    }
    onViewDetails(orderId: number){
      console.log(orderId);
      this.orderDetails = true;
      this.detailsId = orderId;
    }
    onHideDetails(orderId: number) {
      if (this.detailsId === orderId) {
        this.orderDetails = false;
        this.detailsId = 0;
      }
    }
    
    onCancelEdit() {
      this.editingDeliveryOrderId = null;
    }
    
    onSaveDeliveryStatus(order: Order) {
      if (order.activeDelivery != null){
        order.activeDelivery.deliStatus = this.orderForm.get('status')?.value;
      }
      else {
        order.activeDelivery = { id : 0, orderId : order.id, deliStatus: this.orderForm.get('status')?.value }
      }
      console.log(order);
      this.orderService.UpdateDeliveryStatus(order.id, order.activeDelivery)
        .subscribe({
          next: () => {
            this.editingDeliveryOrderId = null;
          },
          error: (e) => {
            console.error('Erreur lors de la mise à jour');
            console.log(e)
          }
        });
    }
}
