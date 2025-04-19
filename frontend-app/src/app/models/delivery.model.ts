import { ProductImage } from "./product-image.model";

export interface Delivery {
    id: number;
    name: string;
    orderId: string;
    price?: number;
    deliStatus?: number;
    estimatedDate?: string;
    deliveryDate?: boolean;
}