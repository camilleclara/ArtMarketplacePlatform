import { DeliveryStatus } from "./delivery-status.enum copy";

export interface Delivery {
    id: number;
    orderId: number;
    price?: number;
    deliStatus?: DeliveryStatus;
    estimatedDate?: string;
    deliveryDate?: boolean;
}