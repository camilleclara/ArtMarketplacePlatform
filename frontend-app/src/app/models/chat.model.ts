import { Message } from "./message.model";
import { Product } from "./product.model";
import { User } from "./user.model";

export interface Chat {
    otherUser: { id: number; firstName: string; lastName: string };
    messages: Message[];
  }