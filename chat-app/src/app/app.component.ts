import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ChatService } from './services/chat/chat.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'chat-app';
  messages: any[] = [];

  chatService = inject(ChatService);

  constructor() {
    this.chatService.get().subscribe((data) => {
      this.messages = data;
    });
  }
}
