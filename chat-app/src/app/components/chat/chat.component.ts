import { Component, inject, OnInit } from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor } from '@angular/common';
import {
  MessageCreateDto,
  MessageResponseDto,
} from '../../shared/DTOs/message.dto';

@Component({
  selector: 'app-chat',
  imports: [FormsModule, NgIf, NgFor],
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

    // MessageCreateDto létrehozása
    const messageDto: MessageCreateDto = {
      content: this.content.trim(),
      user: this.user.trim(),
    };

    // Service hívás
    this.chatService.createMessage(messageDto).subscribe({
      next: (newMessage: MessageResponseDto) => {
        // Sikeres létrehozás
        this.messages.push(newMessage);

        // Form mezők tisztítása
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
