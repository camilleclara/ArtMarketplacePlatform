import { Component } from '@angular/core';
import { ProductService } from '../products/product.service';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {
  products: any;
  /**
   *
   */
  constructor(private productService: ProductService) {
    this.productService.GetAdminProducts()
        .subscribe(response => {
          this.products=response
        });
    
  }
}
