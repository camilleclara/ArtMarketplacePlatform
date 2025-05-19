import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from '../models/message.model';
import { Chat } from '../models/chat.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
 private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getMessagesByUserId(userId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/Message/user/${userId}`)
  }

  sendMessage(message: Message){
    console.log(message);
    return this.http.post<Message>(`${this.baseUrl}/Message`, message);
  }
  getChats(messages: Message[], currentUserId: number): Chat[] {
    // Vérifier si nous avons des messages
    if (!messages || messages.length === 0) {
      console.log("Aucun message reçu");
      return [];
    }
  
    console.log(`Traitement de ${messages.length} messages pour l'utilisateur ${currentUserId}`);
    
    const chatMap = new Map<number, Chat>();
  
    for (const message of messages) {
      // Vérifier que le message a bien les propriétés attendues
      if (!message.msgFromId || !message.msgToId) {
        console.error("Message mal formaté:", message);
        continue;
      }
  
      // Identifier l'autre utilisateur dans la conversation
      let otherUserId: number;
      let otherUser: any;
      
      if (message.msgFromId == currentUserId) {
        otherUserId = message.msgToId;
        otherUser = message.msgTo;
      } else{
        otherUserId = message.msgFromId;
        otherUser = message.msgFrom;
      }
      
      if (!chatMap.has(otherUserId)) {
        chatMap.set(otherUserId, {
          otherUser: otherUser,
          messages: []
        });
      }

      chatMap.get(otherUserId)!.messages.push(message);
    }

    const chats = Array.from(chatMap.values());
    
    //yrier les messages dans chaque chat par date de création
    chats.forEach(chat => {
      chat.messages.sort((a, b) => new Date(a.created).getTime() - new Date(b.created).getTime());
    });
  
    //Trier les chats par date du message le plus récent
    chats.sort((a, b) => {
      if (a.messages.length === 0) return 1;
      if (b.messages.length === 0) return -1;
      const lastMsgA = a.messages[a.messages.length - 1];
      const lastMsgB = b.messages[b.messages.length - 1];
      return new Date(lastMsgB.created).getTime() - new Date(lastMsgA.created).getTime();
    });
    return chats;
  }
}
