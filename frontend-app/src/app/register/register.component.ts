import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../login/authentication.service';
import { Router } from '@angular/router';
import { UserRegister } from '../models/user-register.model';
import { UserType } from '../models/user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

  registerForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';
  CUSTOMER_ROLE = UserType.CUSTOMER;
  ARTISAN_ROLE = UserType.ARTISAN;
  DELIVERY_PARTNER_ROLE = UserType.DELIVERY_PARTNER;

  constructor(private authService: AuthenticationService, private router: Router) {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      login: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      address: new FormControl('', [Validators.required]),
      role: new FormControl('', [Validators.required])
    });
  }

  register() {
    this.errorMessage = '';
    this.successMessage = '';

    if (this.registerForm.valid) {
      const userRegister: UserRegister = {
        id: 0, // pas besoin d'envoyer l'id ici car il sera géré côté backend
        ...this.registerForm.value
      };

      this.authService.register(userRegister).subscribe({
        next: (response: any) => {
          this.successMessage = 'Registration successful!';
          this.registerForm.reset();
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 1500);
        },
        error: (error) => {
          console.error(error);
          this.errorMessage = 'Registration failed. Please try again.';
        }
      });
    }
  }
}
