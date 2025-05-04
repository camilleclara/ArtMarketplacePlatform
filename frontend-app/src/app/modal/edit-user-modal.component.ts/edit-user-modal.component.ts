import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User, UserTypeOption } from '../../models/user.model';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { UserTypeService } from '../../login/user-type.service';

@Component({
  selector: 'app-edit-user-modal.component.ts',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-user-modal.component.html',
  styleUrl: './edit-user-modal.component.css'
})

  export class EditUserModalComponent implements OnInit {
    @Input() user: User | null = null;
  @Output() userSaved = new EventEmitter<User>();
  
  userForm: FormGroup = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    login: new FormControl('', [Validators.required, ]),
    userType: new FormControl('', [Validators.required])
  });

  userTypeOptions: UserTypeOption[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private userTypeService: UserTypeService
  ) {}

  ngOnInit(): void {
    // Charger les types d'utilisateur
    this.userTypeOptions = this.userTypeService.getAllUserTypes();
    
    if (this.user) {
      this.userForm.patchValue({
        firstName: this.user.firstName,
        lastName: this.user.lastName,
        login: this.user.login,
        userType: this.user.userType
      });
    }
  }

  saveUser(): void {
    if (this.userForm.invalid || !this.user) {
      return;
    }

    const updatedUser: User = {
      ...this.user,
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      login: this.userForm.value.login,
      userType: this.userForm.value.userType
    };

    // Émettre l'événement avec l'utilisateur mis à jour
    this.userSaved.emit(updatedUser);
    
    // Fermer la modale avec le résultat (l'utilisateur mis à jour)
    // Cela sera capturé par le `result.then()` dans le composant parent
    this.activeModal.close(updatedUser);
  }

  cancel(): void {
    this.activeModal.dismiss();
  }
}
