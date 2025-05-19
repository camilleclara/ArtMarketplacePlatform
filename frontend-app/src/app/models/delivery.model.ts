import { DeliveryStatus } from "./delivery-status.enum copy";

export interface Delivery {
    id: number;
    orderId: number;
    partnerId: number;
    price?: number;
    deliStatus?: DeliveryStatus;
    estimatedDate?: string;
    deliveryDate?: string;
}