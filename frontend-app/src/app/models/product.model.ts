import { ProductImage } from "./product-image.model";

export interface Product {
    id: number;
    name: string;
    description: string;
    price?: number;
    artisanId: number;
    category?: string;
    isAvailable: boolean;
    productImages?: ProductImage[]
}