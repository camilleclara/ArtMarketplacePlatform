import { Component } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(private authService: AuthenticationService, private router: Router){
    this.loginForm = new FormGroup({
      userName: new FormControl("", [Validators.required ]),
      password: new FormControl("", [Validators.required])
    })
  }

  login(){
    this.errorMessage = ''; // Reset avant de tenter
    this.authService.Login(this.loginForm.value.userName, this.loginForm.value.password).subscribe({
      next: (response: any) => {
        console.log(response);
        if(response.token){

          sessionStorage.setItem("jwt", response.token);
          this.router.navigate(["/dashboard"]);
        }
      },
      error: (error) => {
        console.error(error);
        this.errorMessage = 'Login ou mot de passe invalide, veuillez réessayer';
      }
    });
  }
}
