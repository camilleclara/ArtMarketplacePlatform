import { Delivery } from "./delivery.model";
import { OrderedProduct } from "./ordered-product.model";

export interface Order {
    id: number;
    total: number;
    artisanId: number;
    customerId: string;
    customerName?: string;
    artisanName?: string;
    activeDelivery: Delivery;
    products: OrderedProduct[];
}