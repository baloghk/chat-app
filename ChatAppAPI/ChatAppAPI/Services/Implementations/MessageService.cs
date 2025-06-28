using ChatAppAPI.DTOs.Request;
using ChatAppAPI.DTOs.Response;
using ChatAppAPI.Hubs;
using ChatAppAPI.Models;
using ChatAppAPI.Repositories.Interfaces;
using ChatAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppAPI.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageService(IMessageRepository messageRepository, IHubContext<ChatHub> hubContext)
        {
            _messageRepository = messageRepository;
            _hubContext = hubContext;
        }

        public async Task<MessageResponseDto> CreateMessageAsync(MessageCreateDto messageDto)
        {
            var message = new Message
            {
                User = messageDto.User,
                Content = messageDto.Content,
            };

            var createdMessage = await _messageRepository.CreateAsync(message);
            var responseDto = MessageResponseDto.FromEntity(createdMessage);

            await _hubContext.Clients.Group("ChatRoom")
                .SendAsync("ReceiveMessage", responseDto);

            return responseDto;
        }

        public async Task<bool> DeleteMessageAsync(Guid publicId)
        {
            return await _messageRepository.DeleteAsync(publicId);
        }

        public async Task<IEnumerable<MessageResponseDto>> GetAllMessagesAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            return messages.Select(MessageResponseDto.FromEntity);
        }

        public async Task<MessageResponseDto?> GetMessageAsync(Guid publicId)
        {
            var message = await _messageRepository.GetByGuidAsync(publicId);
            return message == null ? null : MessageResponseDto.FromEntity(message);
        }

        public async Task<MessageResponseDto?> UpdateMessageAsync(Guid publicId, MessageCreateDto messageDto)
        {
            var messageToUpdate = new Message
            {
                Content = messageDto.Content,
                User = messageDto.User
            };

            var updatedMessage = await _messageRepository.UpdateAsync(publicId, messageToUpdate);
            return updatedMessage == null ? null : MessageResponseDto.FromEntity(updatedMessage);
        }
    }
}
