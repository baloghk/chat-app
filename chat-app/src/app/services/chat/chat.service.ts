import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import {
  MessageCreateDto,
  MessageResponseDto,
} from '../../shared/DTOs/message.dto';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private apiUrl = environment.apiURL + '/chat';

  constructor(private http: HttpClient) {}

  createMessage(messageDto: MessageCreateDto): Observable<MessageResponseDto> {
    return this.http.post<MessageResponseDto>(this.apiUrl, messageDto);
  }

  getAllMessages(): Observable<MessageResponseDto[]> {
    return this.http.get<MessageResponseDto[]>(this.apiUrl);
  }

  getMessageById(publicId: string): Observable<MessageResponseDto> {
    return this.http.get<MessageResponseDto>(`${this.apiUrl}/${publicId}`);
  }

  updateMessage(
    publicId: string,
    messageDto: MessageCreateDto
  ): Observable<MessageResponseDto> {
    return this.http.put<MessageResponseDto>(
      `${this.apiUrl}/${publicId}`,
      messageDto
    );
  }

  deleteMessage(publicId: string): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${publicId}`);
  }
}
