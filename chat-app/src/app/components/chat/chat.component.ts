import {
  Component,
  OnInit,
  AfterViewChecked,
  ElementRef,
  ViewChild,
} from '@angular/core';
import { ChatService } from '../../services/chat/chat.service';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor } from '@angular/common';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { WelcomeDialogComponent } from '../dialogs/welcome-dialog/welcome-dialog.component'; // Path módosítása szükséges lehet
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
    MatDialogModule,
    MatIconModule,
    MatTooltipModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css',
})
export class ChatComponent implements OnInit, AfterViewChecked {
  @ViewChild('chatMessages') private chatMessagesContainer!: ElementRef;

  isDarkMode = false;
  user: string = '';
  content: string = '';
  isLoading: boolean = false;
  messages: MessageResponseDto[] = [];
  currentUserName: string = '';

  constructor(private chatService: ChatService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadTheme();
    this.openWelcomeDialog();
    this.loadAllMessages();
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  openWelcomeDialog(): void {
    const dialogRef = this.dialog.open(WelcomeDialogComponent, {
      width: '400px',
      disableClose: true,
      data: {},
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.currentUserName = result;
        this.user = result;
        console.log('Felhasználó neve beállítva:', this.currentUserName);
      } else {
        // Ha mégsem adna meg nevet, újra megnyitjuk a dialógust
        this.openWelcomeDialog();
      }
    });
  }

  onSubmit(): void {
    if (!this.content.trim() || !this.user.trim()) {
      return; // Silent fail, a dialógus biztosítja a nevet
    }

    this.isLoading = true;

    const messageDto: MessageCreateDto = {
      content: this.content.trim(),
      user: this.user.trim(),
    };

    this.chatService.createMessage(messageDto).subscribe({
      next: (newMessage: MessageResponseDto) => {
        this.messages.push(newMessage);
        this.content = ''; // Csak a tartalmat töröljük, a nevet megtartjuk
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

  isOwnMessage(messageUser: string): boolean {
    return messageUser === this.currentUserName;
  }

  changeUserName(): void {
    this.openWelcomeDialog();
  }

  private scrollToBottom(): void {
    try {
      if (this.chatMessagesContainer) {
        this.chatMessagesContainer.nativeElement.scrollTop =
          this.chatMessagesContainer.nativeElement.scrollHeight;
      }
    } catch (err) {
      console.log('Scroll error:', err);
    }
  }

  toggleTheme(): void {
    this.isDarkMode = !this.isDarkMode;
    document.body.classList.toggle('dark-theme', this.isDarkMode);
    localStorage.setItem('isDarkMode', String(this.isDarkMode));
  }

  loadTheme(): void {
    const storedTheme = localStorage.getItem('isDarkMode');
    this.isDarkMode = storedTheme === 'true';
    document.body.classList.toggle('dark-theme', this.isDarkMode);
  }
}
