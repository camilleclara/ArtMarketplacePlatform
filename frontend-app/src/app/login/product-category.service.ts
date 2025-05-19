import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ProductCategory } from '../models/product-category.enum';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoryService {

  constructor(private http: HttpClient) {}

  getCategories(): string[] {
    return ['PAINTING', 'POTTERY', 'JEWELS', 'CLOTHES', 'FURNITURE']
  }

  getAllCategories(): string[] {
    return Object.values(ProductCategory);
  }

  getCategoryLabels(): { [key: string]: string } {
    //TODO add backend endpoint GetAllCategories
    return {
      [ProductCategory.PAINTING]: 'Peinture',
      [ProductCategory.POTTERY]: 'Poterie',
      [ProductCategory.JEWELS]: 'Bijoux',
      [ProductCategory.CLOTHES]: 'Vêtements',
      [ProductCategory.FURNITURE]: 'Mobilier'
    };
  }
}