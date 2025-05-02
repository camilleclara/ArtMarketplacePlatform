import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { Toast, ToastService } from '../toast.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule, NgbToastModule],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent {
  toast: Toast | null = null;
  private subscription: Subscription | null = null;

  constructor(private toastService: ToastService) {}
  
  ngOnInit(): void {
    this.subscription = this.toastService.toast$.subscribe(toast => {
      this.toast = toast;
      
      if (toast && toast.delay) {
        setTimeout(() => {
          if (this.toast === toast) {
            this.toastService.clear();
          }
        }, toast.delay);
      }
    });
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  close(): void {
    this.toastService.clear();
  }
}
