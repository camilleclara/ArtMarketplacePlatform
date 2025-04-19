import { Delivery } from "./delivery.model";

export interface Order {
    id: number;
    total: number;
    artisanId: number;
    customerId: string;
    activeDelivery: Delivery;
}