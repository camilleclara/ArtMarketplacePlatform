import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BasketService } from '../basket.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-basket-icon',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './basket-icon.component.html',
  styleUrl: './basket-icon.component.css'
})
export class BasketIconComponent {
  itemCount = 0;
  constructor(
    private basketService: BasketService,
    private router: Router
  ) {}
  
  ngOnInit(): void {
    this.basketService.getBasket().subscribe(() => {
      this.itemCount = this.basketService.getTotalItems();
    });
  }
  
  goToBasket(): void {
    this.router.navigate(['/basket']);
  }
}
