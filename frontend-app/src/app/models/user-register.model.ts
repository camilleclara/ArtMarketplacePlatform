import { Product } from "./product.model";

export interface UserRegister {
    id: number;
    firstName: string;
    lastName: string;
    address: string;
    login: string;
    password: string;
    role: string;
}