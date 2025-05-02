import { Injectable } from '@angular/core';
import { BasketItem } from '../models/basket.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  private basketItems: BasketItem[] = [];
  private basketSubject = new BehaviorSubject<BasketItem[]>([]);
  
  constructor() {
    // Récupérer le panier sauvegardé dans le localStorage si disponible
    const savedBasket = localStorage.getItem('basket');
    if (savedBasket) {
      try {
        this.basketItems = JSON.parse(savedBasket);
        this.basketSubject.next(this.basketItems);
      } catch (e) {
        console.error('Erreur lors de la récupération du panier:', e);
      }
    }
  }

  getBasket(): Observable<BasketItem[]> {
    return this.basketSubject.asObservable();
  }

  addToBasket(product: Product): void {
    const existingItem = this.basketItems.find(item => item.product.id === product.id);
    
    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      this.basketItems.push({ product, quantity: 1 });
    }
    
    this.updateBasket();
  }

  removeFromBasket(productId: number): void {
    const itemIndex = this.basketItems.findIndex(item => item.product.id === productId);
    
    if (itemIndex !== -1) {
      if (this.basketItems[itemIndex].quantity > 1) {
        this.basketItems[itemIndex].quantity -= 1;
      } else {
        this.basketItems.splice(itemIndex, 1);
      }
      
      this.updateBasket();
    }
  }

  clearBasket(): void {
    this.basketItems = [];
    this.updateBasket();
  }

  getItemQuantity(productId: number): number {
    const item = this.basketItems.find(item => item.product.id === productId);
    return item ? item.quantity : 0;
  }

  getTotalPrice(): number {
    return this.basketItems.reduce((total, item) => {
      return total + (item.product.price || 0) * item.quantity;
    }, 0);
  }

  getTotalItems(): number {
    return this.basketItems.reduce((total, item) => total + item.quantity, 0);
  }

  private updateBasket(): void {
    this.basketSubject.next([...this.basketItems]);
    localStorage.setItem('basket', JSON.stringify(this.basketItems));
  }
}
