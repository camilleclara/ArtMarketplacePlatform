import { Component } from '@angular/core';
import { ProductService } from './product.service';
import { AuthenticationService } from '../login/authentication.service';
import { Product } from '../models/product.model';
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
    category: new FormControl("", [Validators.required]),
    isAvailable: new FormControl(false, [Validators.required])
  }) 

 //TODO retrieve from backend instead
 categories = Object.values(ProductCategory);

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
      category: new FormControl("", [Validators.required]),
      isAvailable: new FormControl(false, [Validators.required])    
    });
    this.editingProduct = null;
  };

  onEdit(id: number): any{
    this.productService.GetArtisanProductById(this.authService.getUserId(),id).subscribe((product: Product) => {
      this.editingProduct = product; 
      console.log(product)
      this.productForm.patchValue({
        name: product.name,
        description: product.description,
        price: product.price,
        category: product.category,
        isAvailable: product.isAvailable
      });
    });
  };

  onSubmit(id: number) {
    //TODO: intercept errors
    //TODO: toast for success & errors
    if (this.productForm.invalid) {
      return;
    }
    const updatedProduct: Product = {
      ...this.editingProduct,  // Conserver les autres valeurs (ex: id, artisanId, isAvailable)
      ...this.productForm.value, // Mettre à jour avec les nouvelles valeurs du formulaire
    };
    this.productService.UpdateProduct(this.authService.getUserId(),id, updatedProduct).subscribe(
      (response) => {
        this.initForm();
      });
  };

  onDelete(id: number) {
    const confirmed = window.confirm("Êtes-vous sûr de vouloir supprimer ce produit ?");
  
  if (confirmed) {
    // Appeler le service pour supprimer le produit
    this.productService.DeleteProduct(this.authService.getUserId(), id).subscribe(
      (response) => {
        // Réinitialiser la liste des produits après suppression
        //TODO: toast de confirmation
        this.initForm();
      }
      //,
      // (error) => {
      //   // Gérer l'erreur si nécessaire
      //   console.error('Erreur lors de la suppression du produit', error);
      // }
    );
  }
  }

  onCancel(){
    this.initForm();
  }
}
