import { Component } from '@angular/core';
import { ProductService } from './product.service';

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
  constructor(private productService: ProductService) {
    this.productService.GetProducts()
        .subscribe(response => {
          console.log("response", response)
          this.products=response
  
        });
    
  }
}
