import { Injectable } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  constructor(private modalService: NgbModal) { }

  openModal(content: any, options = {}) {
    return this.modalService.open(content, { 
      centered: true, 
      backdrop: 'static',
      ...options 
    });
  }
}
