import { Component, OnInit } from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor } from '@angular/common';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import {
  MessageCreateDto,
  MessageResponseDto,
} from '../../shared/DTOs/message.dto';

@Component({
  selector: 'app-chat',
  imports: [
    FormsModule,
    NgIf,
    NgFor,
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit {
  user: string = '';
  content: string = '';
  isLoading: boolean = false;
  messages: MessageResponseDto[] = [];

  constructor(private chatService: ChatService) {}

  ngOnInit(): void {
    this.chatService.getAllMessages().subscribe({
      next: (messages: MessageResponseDto[]) => {
        this.messages = messages;
      },
      error: (error: any) => {
        console.error('Hiba az üzenetek betöltése során:', error);
      },
    });
  }

  onSubmit(): void {
    if (!this.content.trim() || !this.user.trim()) {
      alert('Minden mező kitöltése kötelező!');
      return;
    }
    this.isLoading = true;

    const messageDto: MessageCreateDto = {
      content: this.content.trim(),
      user: this.user.trim(),
    };

    this.chatService.createMessage(messageDto).subscribe({
      next: (newMessage: MessageResponseDto) => {
        this.messages.push(newMessage);

        this.user = '';
        this.content = '';

        console.log('Üzenet sikeresen létrehozva:', newMessage);
        this.isLoading = false;
      },
      error: (error: any) => {
        console.error('Hiba az üzenet létrehozása során:', error);
        alert('Hiba történt az üzenet elküldése során!');
        this.isLoading = false;
      },
    });
  }

  loadAllMessages(): void {
    this.chatService.getAllMessages().subscribe({
      next: (messages: MessageResponseDto[]) => {
        this.messages = messages;
      },
      error: (error: any) => {
        console.error('Hiba az üzenetek betöltése során:', error);
      },
    });
  }
}
