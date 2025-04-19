import { Component } from '@angular/core';
import { ProductService } from './product.service';
import { AuthenticationService } from '../login/authentication.service';
import { Product } from '../models/product.model';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductCategory } from '../models/product-category.enum';
import { CommonModule } from '@angular/common';
import { ProductImage } from '../models/product-image.model';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {
  title = "";
  products: Product[]=[];
  editingProduct: Product | null = null;
  creatingProduct: Product | null = null;
  edition: boolean = false;
  creation: boolean = false;
  newProduct: Product = {
    id: 0,
    artisanId: this.authService.getUserId(),
    name: '',
    description: '',
    isAvailable: true,
    productImages: [
      {
        id: 0,
        name: '',
        productId: 0,
        mimeType: "image/jpeg",
        content: "base64encodedcontent"
      }
    ]
  }

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
      isAvailable: new FormControl(false, [Validators.required]),
      productImages: new FormControl([])
    });
    this.editingProduct = null;
  };

  onEdit(id: number): any{
    this.title = "Modifier un produit"
    this.productService.GetArtisanProductById(this.authService.getUserId(),id).subscribe((product: Product) => {
      this.editingProduct = product; 
      this.edition = true;
      this.creation = false;
      console.log(product)
      this.productForm.patchValue({
        name: product.name,
        description: product.description,
        price: product.price,
        category: product.category,
        isAvailable: product.isAvailable,
        productImages: product.productImages
      });
    });
  };
  onImageSelected(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (!target.files || target.files.length === 0) return;
  
    Array.from(target.files).forEach((file: File) => {
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = (reader.result as string).split(',')[1];
  
        const newImage: ProductImage = {
          id: 0, // Will be set by backend if necessary
          name: file.name,
          productId: this.editingProduct?.id || 0,
          mimeType: file.type,
          content: base64
        };
  
        if (this.editingProduct) {
          if (!this.editingProduct.productImages) {
            this.editingProduct.productImages = [];
          }
        
          this.editingProduct.productImages.push(newImage);
        }
      };
      reader.readAsDataURL(file);
    });
  }
  onCreate(){
    this.title = "Créer un produit"
    this.creation = true;
    this.edition = false;
    this.editingProduct = {
      id: 0,
      artisanId: this.authService.getUserId(),
      name: '',
      description: '',
      price: 0,
      category: '',
      isAvailable: true,
      productImages: []
    };
      this.edition = true;
      this.productForm.patchValue({
        name: this.newProduct.name,
        description: this.newProduct.description,
        price: this.newProduct.price,
        category: this.newProduct.category,
        isAvailable: this.newProduct.isAvailable,
        productImages: this.newProduct.productImages
      });
  }


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
    if(this.edition){
      console.log(updatedProduct);
      this.productService.UpdateProduct(this.authService.getUserId(),id, updatedProduct).subscribe(
        (response) => {
          console.log(response)
          this.initForm();
        });
    }
    if(this.creation){
      console.log(updatedProduct);
      this.productService.CreateProduct(updatedProduct.artisanId, updatedProduct).subscribe(
        (response) => {
          this.initForm();
        });
      console.log("ok")
    }
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

  getImageSrc(image: ProductImage): string {
    return `data:${image.mimeType};base64,${image.content}`;
  }

  removeImage(index: number): void {
    if (this.editingProduct && this.editingProduct.productImages) {
      // Supprimer l'image de l'array
      this.editingProduct.productImages.splice(index, 1);
      
      // Mettre à jour le formulaire si nécessaire
      this.productForm.patchValue({
        productImages: this.editingProduct.productImages
      });
    }
  }
}
