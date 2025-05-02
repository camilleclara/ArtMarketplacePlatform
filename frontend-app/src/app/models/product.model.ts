import { ProductImage } from "./product-image.model";
import { Review } from "./review.model";

export interface Product {
    id: number;
    name: string;
    description: string;
    price?: number;
    artisanId: number;
    artisanName?: string;
    category?: string;
    isAvailable: boolean;
    productImages?: ProductImage[];
    reviews?: Review[];
}