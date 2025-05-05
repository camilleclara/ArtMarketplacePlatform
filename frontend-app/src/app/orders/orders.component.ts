import { Component, Input } from '@angular/core';
import { Order } from '../models/order.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrderService } from './order.service';
import { AuthenticationService } from '../login/authentication.service';
import { CommonModule } from '@angular/common';
import { DeliveryStatus } from '../models/delivery-status.enum copy';
import { OrderComponent } from './order/order/order.component';
import { User } from '../models/user.model';
import { UserService } from '../profile/user.service';

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
  partners: User[] = []; // Liste des partenaires de livraison

  orderForm: FormGroup = new FormGroup({
    id: new FormControl("", [Validators.required]),
    customerId: new FormControl("", [Validators.required]),
    status: new FormControl("", [Validators.required]),
    partnerId: new FormControl(null) // Nouveau champ pour le partenaire de livraison
  }) 
  
  constructor(
    private orderService: OrderService, 
    private authService: AuthenticationService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.initForm();
    
    // Charger les partenaires de livraison si on est dans la vue artisan
    if (this.displayForArtisan) {
      this.loadDeliveryPartners();
    }
  }
    
  loadDeliveryPartners() {
    this.userService.getAllDeliveryPartners().subscribe({
      next: (partners) => {
        this.partners = partners;
        console.log('Partenaires de livraison chargés:', this.partners);
      },
      error: (error) => {
        console.error('Erreur lors du chargement des partenaires de livraison:', error);
      }
    });
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
        status: new FormControl("NEW", [Validators.required]),
        partnerId: new FormControl(null) // Ajout du champ pour le partenaire de livraison
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
        status: new FormControl("NEW", [Validators.required]),
        partnerId: new FormControl(null) // Même ajout ici pour cohérence
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
      
      // Définir le partenaire de livraison actuel s'il existe
      // Utiliser explicitement ngValue null pour permettre la désélection
      this.orderForm.get('partnerId')?.setValue(
        orderToEdit.activeDelivery.partnerId || null
      );
      
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
      // Ajouter le partenaire de livraison sélectionné
      order.activeDelivery.partnerId = this.orderForm.get('partnerId')?.value;
    }
    else {
      order.activeDelivery = { 
        id: 0, 
        orderId: order.id, 
        deliStatus: this.orderForm.get('status')?.value,
        partnerId: this.orderForm.get('partnerId')?.value 
      }
    }
    console.log('Mise à jour de la commande:', order);
    this.orderService.UpdateDeliveryStatus(order.id, order.activeDelivery)
      .subscribe({
        next: (updatedDelivery:any) => {
          // Mettre à jour l'objet order dans la liste locale
          const index = this.orders.findIndex(o => o.id === order.id);
          if (index !== -1) {
            // Mise à jour de la livraison dans notre liste locale
            this.orders[index].activeDelivery = updatedDelivery;
            
            // Forcer la détection de changement en créant une nouvelle référence
            this.orders = [...this.orders];
          }
          
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
  getPartnerName(id: number|undefined){
    if (!this.partners || id === null || id === undefined) {
      return 'Non assigné';
    }
    
    const partner = this.partners.find(p => p.id === id);
    if (partner) {
      return `${partner.firstName} ${partner.lastName}`;
    }
    
    return 'Non assigné';
  }
}


