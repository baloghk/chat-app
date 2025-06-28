export interface MessageCreateDto {
  content: string;
  user: string;
}

export interface MessageResponseDto {
  publicId: string;
  content: string;
  user: string;
  timestamp: string;
}
