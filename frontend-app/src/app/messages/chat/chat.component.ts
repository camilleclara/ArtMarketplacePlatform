import { Component, Input } from '@angular/core';
import { Chat } from '../../models/chat.model';
import { MessageComponent } from '../message/message.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [MessageComponent, ReactiveFormsModule],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})


export class ChatComponent {
  @Input() chat!: Chat;
  @Input() currentUserId!: number;

  
  messageForm: FormGroup;

  constructor(private fb: FormBuilder, private messageService: MessageService) {
    this.messageForm = this.fb.group({
      message: ['', Validators.required]
    });
  }
  sendMessage(){
    if (this.messageForm.valid) {
      const messageContent = this.messageForm.value.message.trim();
      if (messageContent) {
        let newMsg = {
          id: 0,
          content: messageContent,
          productId: null,
          msgFromId: this.currentUserId,
          msgToId: this.chat.otherUser.id,
          created: new Date()
        }
        this.chat.messages.push(newMsg);
        this.messageService.sendMessage(newMsg)
        this.messageForm.reset(); // Reset pour vider l'input après envoi
      }
    }
  }
}
