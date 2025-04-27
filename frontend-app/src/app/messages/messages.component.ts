import { Component } from '@angular/core';
import { Chat } from '../models/chat.model';
import { AuthenticationService } from '../login/authentication.service';
import { MessageService } from './message.service';
import { Message } from '../models/message.model';
import { ChatComponent } from './chat/chat.component';

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [ChatComponent],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent {
  chats: Chat[] = [];
  currentUserId = this.authService.getUserId();
  selectedChat: Chat | null = null;

  constructor(private messageService: MessageService, private authService: AuthenticationService) {}
  ngOnInit(){
    this.messageService.getMessagesByUserId(this.currentUserId)
      .subscribe((messages: Message[]) => {
        this.chats = this.messageService.getChats(messages, this.currentUserId);
      }, (error) => {
        console.error("Erreur de récupération des messages:", error);
      });
  }
  selectChat(chat: Chat) {
    this.selectedChat = chat;
  }
}
