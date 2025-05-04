export interface User {
    id: number;
    firstName: string;
    lastName: string;
    login: string;
    ordersPlaced: number;
    ordersFulfilled: number;
    role: string;
    userType: string;
    isActive?: boolean;
}

export enum UserType {
    CUSTOMER = 'CUSTOMER',
    ARTISAN = 'ARTISAN',
    ADMIN = 'ADMIN',
    DELIVERY_PARTNER = 'DELIVERYPARTNER'
  }
  
  export interface UserTypeOption {
    value: string;
    label: string;
    badgeClass: string;
  }