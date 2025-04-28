import { Product } from "./product.model";

export interface UserRegister {
    id: number;
    firstName: string;
    lastName: string;
    login: string;
    password: string;
    role: string;
}