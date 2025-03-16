import { Component } from '@angular/core';
import { ProductService } from './product.service';
import { AuthenticationService } from '../login/authentication.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
 products: any;
  /**
   *
   */
  constructor(private productService: ProductService, private authService: AuthenticationService) {
    this.productService.GetProductsByArtisanId(authService.getUserId())
    .subscribe(response => {
      this.products=response
    });
  }
}
