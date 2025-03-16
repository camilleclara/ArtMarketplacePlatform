import { Component } from '@angular/core';
import { ProductService } from './product.service';
import { AuthenticationService } from '../login/authentication.service';
import { Product } from '../models/Product.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductCategory } from '../models/product-category.enum';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
 products: Product[]=[];
 editingProduct: Product | null = null;
 productForm: FormGroup = new FormGroup({
  name: new FormControl("", [Validators.required]),
  description: new FormControl("", [Validators.required]),
  price: new FormControl("", [Validators.required]),
  category: new FormControl("", [Validators.required])
}) 

 //TODO retrieve from backend instead
 categories = Object.values(ProductCategory);


  /**
   *
   */
  constructor(private productService: ProductService, private authService: AuthenticationService) {
    this.initForm();
  }

  initForm(){
    this.productService.GetProductsByArtisanId(this.authService.getUserId())
    .subscribe(response => {
      this.products=response
    });
    this.productForm = new FormGroup({
      name: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      price: new FormControl("", [Validators.required]),
      category: new FormControl("", [Validators.required])
    }) 
    this.editingProduct = null;

  }

  onEdit(id: number): any{
    this.productService.GetArtisanProductById(this.authService.getUserId(),id).subscribe((product: Product) => {
      this.editingProduct = product; 
      console.log(this.editingProduct);
      this.productForm.patchValue({
        name: product.name,
        description: product.description,
        price: product.price,
        category: product.category // Doit correspondre à une option du select
      });// Mettre à jour le produit en cours d'édition
    });
  }

  onSubmit(id: number) {    
    if (this.productForm.invalid) {
      return; // Ne rien faire si le formulaire est invalide
    }
  
    // Récupérer les données du formulaire
    const updatedProduct: Product = {
      ...this.editingProduct,  // Conserver les autres valeurs (ex: id, artisanId, isActive)
      ...this.productForm.value, // Mettre à jour avec les nouvelles valeurs du formulaire
    };
    // TODO: Use EventEmitter with form value    console.warn(this.profileForm.value);  
    this.productService.UpdateProduct(this.authService.getUserId(),id, updatedProduct).subscribe(
      (response) => {
        this.initForm();

      });// Mettre à jour le produit en cours d'édition
  }
}
