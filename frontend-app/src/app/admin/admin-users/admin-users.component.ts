import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../models/user.model';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent {
  users: User[] = [];
  filteredUsers: User[] = [];
  editingUser: User | null = null;
  isLoading: boolean = false;
  searchTerm: string = '';
  filterRole: string = '';
  
  userForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    login: new FormControl('', [Validators.required, Validators.email]),
    userType: new FormControl('', [Validators.required])
  });

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.isLoading = true;
    this.adminService.getAllUsers().subscribe({
      next: (data) => {
        this.users = data;
        this.applyFilters();
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading users', error);
        this.isLoading = false;
      }
    });
  }

  onSearch(event: Event): void {
    this.searchTerm = (event.target as HTMLInputElement).value.toLowerCase();
    this.applyFilters();
  }

  onFilterRole(userType: string): void {
    this.filterRole = userType;
    this.applyFilters();
  }

  applyFilters(): void {
    this.filteredUsers = this.users.filter(user => {
      const matchesSearch = this.searchTerm ?
        user.firstName.toLowerCase().includes(this.searchTerm) ||
        user.lastName.toLowerCase().includes(this.searchTerm) ||
        user.login.toLowerCase().includes(this.searchTerm) : true;
        
      const matchesRole = this.filterRole ? user.userType === this.filterRole : true;
      
      return matchesSearch && matchesRole;
    });
  }

  editUser(user: User): void {
    this.editingUser = {...user};
    this.userForm.patchValue({
      firstName: user.firstName,
      lastName: user.lastName,
      login: user.login,
      userType: user.userType
    });
  }

  approveUser(userId: number): void {
    if (confirm('Êtes-vous sûr de vouloir approuver cet utilisateur ?')) {
      this.adminService.approveUser(userId).subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (error) => {
          console.error('Error approving user', error);
        }
      });
    }
  }

  deactivateUser(userId: number): void {
    if (confirm('Êtes-vous sûr de vouloir désactiver cet utilisateur ?')) {
      this.adminService.deactivateUser(userId).subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (error) => {
          console.error('Error deactivating user', error);
        }
      });
    }
  }

  saveUser(): void {
    if (this.userForm.invalid || !this.editingUser) {
      return;
    }

    const updatedUser: User = {
      ...this.editingUser,
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      login: this.userForm.value.login,
      userType: this.userForm.value.userType
    };

    this.adminService.updateUser(updatedUser.id, updatedUser).subscribe({
      next: () => {
        this.cancelEdit();
        this.loadUsers();
      },
      error: (error) => {
        console.error('Error updating user', error);
      }
    });
  }

  cancelEdit(): void {
    this.editingUser = null;
    this.userForm.reset();
  }
}
