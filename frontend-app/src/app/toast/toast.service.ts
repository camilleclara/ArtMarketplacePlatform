import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Toast {
  message: string;
  type: 'success' | 'warning' | 'danger' | 'info';
  delay?: number;
}

@Injectable({
  providedIn: 'root'
})

export class ToastService {

  private toastSubject = new BehaviorSubject<Toast | null>(null);
  
  constructor() { } 
  show(message: string, type: 'success' | 'warning' | 'danger' | 'info' = 'success', delay: number = 5000): void {
    this.toastSubject.next({ message, type, delay });
  }
  clear(): void {
    this.toastSubject.next(null);
  }
  get toast$(): Observable<Toast | null> {
    return this.toastSubject.asObservable();
  }
}
