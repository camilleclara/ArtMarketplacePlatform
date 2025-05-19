import { Product } from "./product.model";
import { User } from "./user.model";

export interface Message {
    id: number;
    productId: number | null;
    msgFromId: number;
    msgToId: number;
    content: string;
    msgFrom?: User;
    msgTo?: User;
    product?: Product;
    created: Date;
  }