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
    allowedStatuses: DeliveryStatus[] = [];
    orderForm: FormGroup = new FormGroup({
      id: new FormControl("", [Validators.required]),
      customerId: new FormControl("", [Validators.required]),
      status: new FormControl("", [Validators.required])
    }) 
  constructor(private orderService: OrderService, private authService: AuthenticationService) {
    
  }
  ngOnInit(): void {
    this.initForm();
  }
  
  initForm(){
    if(this.displayForArtisan){
      this.orderService.GetOrdersByArtisanId(this.authService.getUserId())
      .subscribe((response: Order[]) => {
        this.orders=response
      });
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
        customerId: new FormControl("", [Validators.required]),
        status: new FormControl("NEW", [Validators.required])
      });
      //Statuts autorisés pour l'artisan
      this.allowedStatuses = [
        DeliveryStatus.PROCESSING,
        DeliveryStatus.SHIPPED,
        DeliveryStatus.CANCELLED
      ];
      this.editingOrder = null;
    }
    else {
      console.log("in")
      this.orderService.GetOrdersByCustomerId(this.authService.getUserId())
      .subscribe((response: Order[]) => {
        this.orders=response
      });
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
        customerId: new FormControl("", [Validators.required]),
        status: new FormControl("NEW", [Validators.required])
      });
      // Statut autorisé uniquement pour le customer
      this.allowedStatuses = [
        DeliveryStatus.RECEIVED
      ];
      this.editingOrder = null;
    }
  };
    
  onEditDeliveryStatus(orderId: number) {
    this.editingDeliveryOrderId = orderId;
    const orderToEdit = this.orders.find(o => o.id === orderId);
    if (orderToEdit && orderToEdit.activeDelivery) {
      const currentStatus = orderToEdit.activeDelivery.deliStatus;
      this.orderForm.get('status')?.setValue(currentStatus);
      
      //Ajout temporaire du statut courant à la liste des statuts autorisés s'il n'y est pas déjà
      if (!this.allowedStatuses.includes(currentStatus as DeliveryStatus)) {
        this.allowedStatuses = [...this.allowedStatuses, currentStatus as DeliveryStatus];
      }
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
    // Réinitialiser les statuts autorisés à leur valeur d'origine après annulation
    this.resetAllowedStatuses();
  }
  
  private resetAllowedStatuses() {
    if (this.displayForArtisan) {
      this.allowedStatuses = [
        DeliveryStatus.PROCESSING,
        DeliveryStatus.SHIPPED,
        DeliveryStatus.CANCELLED
      ];
    } else {
      this.allowedStatuses = [
        DeliveryStatus.RECEIVED
      ];
    }
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
          // Réinitialiser les statuts autorisés après sauvegarde
          this.resetAllowedStatuses();
        },
        error: (e) => {
          console.error('Erreur lors de la mise à jour');
          console.log(e)
        }
      });
  }
}