import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastComponent } from '../toast/toast/toast.component';

import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../login/authentication.service';
import { ToastService } from '../toast/toast.service';
import { UserService } from './user.service';
import { User, UserType } from '../models/user.model';
import { OrdersComponent } from '../orders/orders.component';
import { ChatComponent } from '../messages/chat/chat.component';
import { Chat } from '../models/chat.model';
import { MessageService } from '../messages/message.service';
import { Message } from '../models/message.model';
import { UserTypeService } from '../login/user-type.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ToastComponent, OrdersComponent, ChatComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  viewOrders: boolean = false;
  messaging: boolean = false;
  user: Partial<User> | null = null;
  userId: number = 0;
  isCurrentUser: boolean = false;
  isCustomer: boolean = false;
  loading: boolean = true;
  error: string | null = null;
  editMode: boolean = false;
  profileForm!: FormGroup;
  ordersCount: number = 0;
  currentUserId: number = 0;
  currentChat: Chat = {
    otherUser: {
      id: 0,
      firstName: '',
      lastName: ''
    },
    messages: []
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthenticationService,
    private fb: FormBuilder,
    private toastService: ToastService,
    private messageService: MessageService,  private userTypeService: UserTypeService
  ) {}

  ngOnInit(): void {
    this.isCustomer = this.authService.isCustomer();
    this.currentUserId = this.authService.getUserId();
    
    this.route.params.subscribe(params => {
      this.userId = +params['id'];
      if (this.userId) {
        this.loadUser(this.userId);
      } else {
        this.error = "Identifiant d'utilisateur invalide";
        this.loading = false;
      }
    });
  }

  loadUser(userId: number): void {
    this.loading = true;
    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        this.user = user;
        this.loading = false;
        this.isCurrentUser = this.authService.getUserId() === userId.toString();
        this.initForm();
        this.messageService.getMessagesByUserId(this.currentUserId)
          .subscribe((messages: Message[]) => {
            var chats = this.messageService.getChats(messages, this.currentUserId);
            const foundChat = chats.find(c => c.otherUser.id === this.user?.id);
            if (foundChat) {
              this.currentChat = foundChat;
            } else {
              this.currentChat= {
                otherUser: {
                  id: this.user?.id?this.user?.id: 0,
                  firstName: this.user?.firstName?this.user?.firstName: '',
                  lastName: this.user?.lastName?this.user?.lastName: ''
                },
                messages: []
              }
              
            }
          }, (error) => {
            console.error("Erreur de récupération des messages:", error);
          });
      },
      error: (err) => {
        this.error = "Impossible de charger les détails de l'utilisateur";
        this.loading = false;
      }
    });
  }

  initForm(): void {
    if (!this.user) return;

    this.profileForm = this.fb.group({
      firstName: [this.user.firstName, [Validators.required, Validators.minLength(2)]],
      lastName: [this.user.lastName, [Validators.required, Validators.minLength(2)]],
      address: [this.user.address, [Validators.required, Validators.minLength(2)]]
    });
  }

  toggleEditMode(): void {
    this.editMode = !this.editMode;
    if (!this.editMode) {
      this.initForm(); 
    }
  }

  onSubmit(): void {
    if (this.profileForm.invalid) {
      Object.keys(this.profileForm.controls).forEach(key => {
        const control = this.profileForm.get(key);
        control?.markAsTouched();
      });
      return;
    }

    if (!this.user) return;

    const updatedUser: Partial<User> = {
      ...this.user,
      firstName: this.profileForm.value.firstName,
      lastName: this.profileForm.value.lastName,
      address: this.profileForm.value.address,

    };

    this.userService.updateUserById(this.userId, updatedUser).subscribe({
      next: (response) => {
        this.loadUser(this.userId);
        this.editMode = false;
        this.toastService.show('Profil mis à jour avec succès', 'success');
      },
      error: (err) => {
        console.error('Erreur lors de la mise à jour du profil', err);
        this.toastService.show('Erreur lors de la mise à jour du profil', 'danger');
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/dashboard']);
  }

  showOrders(){
    this.viewOrders= true;
  }

  hideOrders(){
    this.viewOrders = false;
  }

  startMessaging(){
    this.messaging = true;
  }

  hideMessaging(){
    this.messaging = false;
  }

  isUserCustomer(): boolean {
    return this.user?.userType === UserType.CUSTOMER;
  }

  // Getter pour vérifier si l'utilisateur est un artisan
  isUserArtisan(): boolean {
    return this.user?.userType === UserType.ARTISAN;
  }

  getUserTypeLabel(): string {
    if (!this.user) return '';
    return this.userTypeService.getLabel(this.user.userType ?? 'INCONNU');
  }

  getUserTypeBadgeClass(): string {
    if (!this.user) return '';
    return this.userTypeService.getBadgeClass(this.user.userType ?? 'INCONNU');
  }
}
