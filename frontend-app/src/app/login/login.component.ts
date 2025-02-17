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
    this.loginForm = new FormGroup({
      userName: new FormControl("", [Validators.required ]),
      password: new FormControl("", [Validators.required])
    })
  }

  login(){
    console.log(this.loginForm.value);
    this.authService.Login(this.loginForm.value.userName, this.loginForm.value.password).subscribe((response: any)=>{
      console.log("response", response);
      if(response.token){
        sessionStorage.setItem("jwt", response.token);
        this.router.navigate(["/"]);
        //TODO: redirect to page you came from
      }
    })
    console.log(this.loginForm.value);
  }
}
