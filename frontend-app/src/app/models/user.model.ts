import { Product } from "./product.model";

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