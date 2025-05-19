import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-auth-modl',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './auth-modal.component.html',
  styleUrl: './auth-modal.component.css'
})
export class AuthModalComponent {
  constructor(
    public activeModal: NgbActiveModal,
    private router: Router
  ) {}

  navigate(path: string): void {
    this.activeModal.close(path);
  }
}
