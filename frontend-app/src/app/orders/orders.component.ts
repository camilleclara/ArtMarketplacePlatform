
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
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, OrderComponent, RouterModule],
  templateUrl: './orders.component.html',
  styleUrl: './orders.component.css'
})
export class OrdersComponent {
  @Input() displayForArtisan!: boolean;
  @Input() displayForDeliveryPartner: boolean = false;
  
  orders: Order[]=[];
  orderDetails = false;
  detailsId: number = 0;
  editingOrder: Order | null = null;
  editingDeliveryOrderId: number | null = null;
  categories = Object.values(DeliveryStatus);
  allowedStatuses: DeliveryStatus[] = [];
  partners: User[] = []; // Liste des partenaires de livraison
  artisans: User[] = []; // Liste des artisans pour les partenaires de livraison
  
  orderForm: FormGroup = new FormGroup({
    id: new FormControl("", [Validators.required]),
    customerId: new FormControl("", [Validators.required]),
    status: new FormControl("", [Validators.required]),
    partnerId: new FormControl(null), // Champ pour le partenaire de livraison
    estimatedDeliveryDate: new FormControl(null) // Nouveau champ pour l'heure estimée de livraison
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
    // Charger les artisans si on est dans la vue partenaire de livraison
    if (this.displayForDeliveryPartner) {
      this.loadArtisans();
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
  
  loadArtisans() {
    this.userService.getAllArtisans().subscribe({
      next: (artisans: User[]) => {
        this.artisans = artisans;
        console.log('Artisans chargés:', this.artisans);
      },
      error: (error: any) => {
        console.error('Erreur lors du chargement des artisans:', error);
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
        partnerId: new FormControl(null),
        estimatedDeliveryDate: new FormControl(null)
      });
      //Statuts autorisés pour l'artisan
      this.allowedStatuses = [
        DeliveryStatus.PROCESSING,
        DeliveryStatus.SHIPPED,
        DeliveryStatus.CANCELLED
      ];
      this.editingOrder = null;
    }
    else if(this.displayForDeliveryPartner) {
      // Charger les commandes attribuées au partenaire de livraison
      this.orderService.GetOrdersByPartnerId(this.authService.getUserId())
      .subscribe((response: Order[]) => {
        this.orders = response;
      });
      
      this.orderForm = new FormGroup({
        id: new FormControl("", [Validators.required]),
        customerId: new FormControl("", [Validators.required]),
        status: new FormControl("", [Validators.required]),
        partnerId: new FormControl(null),
        estimatedDeliveryDate: new FormControl(null, [Validators.pattern(/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}$/)])
      });
      
      // Statuts autorisés pour le partenaire de livraison
      this.allowedStatuses = [
        DeliveryStatus.SHIPPED,
        DeliveryStatus.INTRANSIT,
        DeliveryStatus.CANCELLED,
        DeliveryStatus.DELIVERED
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
        partnerId: new FormControl(null),
        estimatedDeliveryDate: new FormControl(null)
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
      this.orderForm.get('partnerId')?.setValue(
        orderToEdit.activeDelivery.partnerId || null
      );
      
      // Définir l'heure estimée de livraison si elle existe
      if (orderToEdit.activeDelivery.estimatedDate) {
        this.orderForm.get('estimatedDeliveryDate')?.setValue(
          orderToEdit.activeDelivery.estimatedDate
        );
      }
      
      //Ajout temporaire du statut courant à la liste des statuts autorisés s'il n'y est pas déjà
      if (!this.allowedStatuses.includes(currentStatus as DeliveryStatus)) {
        this.allowedStatuses = [...this.allowedStatuses, currentStatus as DeliveryStatus];
      }
    }
  }
  
  onViewDetails(orderId: number){
    console.log(this.orders);
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
    } else if (this.displayForDeliveryPartner) {
      this.allowedStatuses = [
        DeliveryStatus.SHIPPED,
        DeliveryStatus.INTRANSIT,
        DeliveryStatus.CANCELLED,
        DeliveryStatus.DELIVERED
      ];
    } else {
      this.allowedStatuses = [
        DeliveryStatus.RECEIVED
      ];
    }
  }
  
  onSaveDeliveryStatus(order: Order) {
    console.log('Données de la livraison avant mise à jour:', order.activeDelivery);
    
    if (order.activeDelivery != null) {
      // Si activeDelivery existe déjà, mettre à jour ses propriétés
      order.activeDelivery.deliStatus = this.orderForm.get('status')?.value;
      order.activeDelivery.partnerId = this.orderForm.get('partnerId')?.value;
      
      // Ajouter l'heure estimée de livraison si disponible
      const estimatedTime = this.orderForm.get('estimatedDeliveryDate')?.value;
      order.activeDelivery.estimatedDate = estimatedTime;
    } else {
      console.log("Creating new delivery");
      // Créer un nouvel objet activeDelivery uniquement si nécessaire
      order.activeDelivery = { 
        id: 0, // Pour une nouvelle livraison
        orderId: order.id, 
        deliStatus: this.orderForm.get('status')?.value,
        partnerId: this.orderForm.get('partnerId')?.value,
        estimatedDate: this.orderForm.get('estimatedDeliveryDate')?.value || undefined
      };
    }
    
    console.log('Données envoyées pour mise à jour:', order.activeDelivery);
    
    this.orderService.UpdateDeliveryStatus(order.id, order.activeDelivery)
      .subscribe({
        next: (updatedDelivery: any) => {
          console.log('Réponse du serveur après mise à jour:', updatedDelivery);
          
          // Réinitialiser le formulaire et l'état d'édition
          this.editingDeliveryOrderId = null;
          this.resetAllowedStatuses();
          
          // Forcer le rechargement complet des commandes depuis le backend
          this.reloadOrders();
        },
        error: (e) => {
          console.error('Erreur lors de la mise à jour:', e);
        }
      });
  }
  
  // Nouvelle méthode pour recharger les commandes depuis le backend
  reloadOrders() {
    if (this.displayForArtisan) {
      this.orderService.GetOrdersByArtisanId(this.authService.getUserId())
        .subscribe({
          next: (response: Order[]) => {
            console.log('Commandes rechargées depuis le backend:', response);
            this.orders = response;
          },
          error: (error) => {
            console.error('Erreur lors du rechargement des commandes:', error);
          }
        });
    } else if (this.displayForDeliveryPartner) {
      this.orderService.GetOrdersByPartnerId(this.authService.getUserId())
        .subscribe({
          next: (response: Order[]) => {
            console.log('Commandes rechargées depuis le backend:', response);
            this.orders = response;
          },
          error: (error) => {
            console.error('Erreur lors du rechargement des commandes:', error);
          }
        });
    } else {
      this.orderService.GetOrdersByCustomerId(this.authService.getUserId())
        .subscribe({
          next: (response: Order[]) => {
            console.log('Commandes rechargées depuis le backend:', response);
            this.orders = response;
          },
          error: (error) => {
            console.error('Erreur lors du rechargement des commandes:', error);
          }
        });
    }
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
  
  getArtisanName(id: number|undefined){
    if (!this.artisans || id === null || id === undefined) {
      return 'Non assigné';
    }
    const artisan = this.artisans.find(a => a.id === id);
    if (artisan) {
      return `${artisan.firstName} ${artisan.lastName}`;
    }
    return 'Artisan inconnu';
  }
}