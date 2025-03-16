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

  constructor(private authService: AuthenticationService, private router: Router){
    console.log("login");
    this.loginForm = new FormGroup({
      userName: new FormControl("", [Validators.required ]),
      password: new FormControl("", [Validators.required])
    })
  }

  login(){
    this.authService.Login(this.loginForm.value.userName, this.loginForm.value.password).subscribe((response: any)=>{
      if(response.token){
        sessionStorage.setItem("jwt", response.token);
        this.router.navigate(["/dashboard"]);
      }
    })
  }
}
