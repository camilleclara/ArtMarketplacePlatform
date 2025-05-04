import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User, UserTypeOption } from '../../models/user.model';
import { AdminService } from '../admin.service';
import { EditUserModalComponent } from '../../modal/edit-user-modal.component.ts/edit-user-modal.component';
import { ModalService } from '../../modal/modal.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserTypeService } from '../../login/user-type.service';

@Component({
  selector: 'app-admin-users',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, EditUserModalComponent],
  templateUrl: './admin-users.component.html',
  styleUrl: './admin-users.component.css'
})
export class AdminUsersComponent {
  users: User[] = [];
  filteredUsers: User[] = [];
  isLoading: boolean = false;
  searchTerm: string = '';
  filterRole: string = '';
  userTypeOptions: UserTypeOption[] = [];
  
  constructor(
    private adminService: AdminService,
    private modalService: ModalService,
    public userTypeService: UserTypeService
  ) { }

  ngOnInit(): void {
    this.loadUsers();
    this.userTypeOptions = this.userTypeService.getAllUserTypes();
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
    const modalRef = this.modalService.openModal(EditUserModalComponent);
    modalRef.componentInstance.user = {...user};
    
    // S'abonner à l'événement userSaved émis par la modale
    modalRef.componentInstance.userSaved.subscribe((updatedUser: User) => {
      this.adminService.updateUser(updatedUser.id, updatedUser).subscribe({
        next: () => {
          this.loadUsers();
        },
        error: (error) => {
          console.error('Error updating user', error);
        }
      });
    });
    
    // S'assurer que nous rechargeons la liste après la fermeture de la modale
    // (au cas où l'utilisateur est enregistré via la modale)
    modalRef.result.then(
      (result) => {
        // Modale fermée avec le bouton "Enregistrer"
        this.loadUsers();
      },
      (reason) => {
        // Modale fermée autrement (annulée, clôturée, etc.)
        console.log('Modal dismissed');
      }
    ).catch(error => console.error('Modal error', error));
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
}
