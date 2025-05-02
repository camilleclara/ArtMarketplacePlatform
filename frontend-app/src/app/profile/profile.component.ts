import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastComponent } from '../toast/toast/toast.component';

import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../login/authentication.service';
import { ToastService } from '../toast/toast.service';
import { UserService } from './user.service';
import { User } from '../models/user.model';
import { OrdersComponent } from '../orders/orders.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ToastComponent, OrdersComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
  viewOrders: boolean = false;
  user: Partial<User> | null = null;
  userId: number = 0;
  isCurrentUser: boolean = false;
  isCustomer: boolean = false;
  loading: boolean = true;
  error: string | null = null;
  editMode: boolean = false;
  profileForm!: FormGroup;
  ordersCount: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private authService: AuthenticationService,
    private fb: FormBuilder,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.isCustomer = this.authService.isCustomer();
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
      lastName: [this.user.lastName, [Validators.required, Validators.minLength(2)]]
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
}
