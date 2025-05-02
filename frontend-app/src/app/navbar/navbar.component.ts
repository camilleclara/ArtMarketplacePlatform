import { Component } from '@angular/core';
import { AuthenticationService } from '../login/authentication.service';
import { BasketIconComponent } from '../basket/basket-icon/basket-icon.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [BasketIconComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(private authService: AuthenticationService){}

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
  logout(): void {
    this.authService.logout();
    window.location.href = '/login';
  }
  isAdmin(): boolean {
    return this.authService.isAdmin();
  }
  isCustomer(): boolean {
    return this.authService.isCustomer();
  }
}
