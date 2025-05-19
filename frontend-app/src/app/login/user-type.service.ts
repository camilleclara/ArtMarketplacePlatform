import { Injectable } from '@angular/core';
import { UserType, UserTypeOption } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserTypeService {
  private userTypes: UserTypeOption[] = [
    { 
      value: UserType.CUSTOMER, 
      label: 'Client', 
      badgeClass: 'badge bg-primary'
    },
    { 
      value: UserType.ARTISAN, 
      label: 'Artisan', 
      badgeClass: 'badge bg-success'
    },
    { 
      value: UserType.ADMIN, 
      label: 'Administrateur', 
      badgeClass: 'badge bg-danger'
    },
    { 
      value: UserType.DELIVERY_PARTNER, 
      label: 'Partenaire livraison', 
      badgeClass: 'badge bg-secondary'
    }
  ];

  constructor() { }

  getAllUserTypes(): UserTypeOption[] {
    return this.userTypes;
  }

  getUserTypeOption(userType: string): UserTypeOption {
    const found = this.userTypes.find(type => type.value === userType);
    return found || { 
      value: userType, 
      label: userType, 
      badgeClass: 'badge bg-secondary'
    };
  }

  getBadgeClass(userType: string): string {
    return this.getUserTypeOption(userType).badgeClass;
  }

  getLabel(userType: string): string {
    return this.getUserTypeOption(userType).label;
  }
}