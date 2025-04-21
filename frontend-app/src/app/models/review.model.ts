import { Product } from "./product.model";
import { User } from "./user.model";

export interface Review {
    content: string;
    id: number;
    productId: string;
    customerId: string;
    product: Product;
    customer: User;
    fromArtisan: boolean;
    score: number;
    created: Date;
}