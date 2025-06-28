using ChatAppAPI.DTOs.Request;
using ChatAppAPI.DTOs.Response;

namespace ChatAppAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResponseDto>> GetAllMessagesAsync();
        Task<MessageResponseDto?> GetMessageAsync(Guid publicId);
        Task<MessageResponseDto> CreateMessageAsync(MessageCreateDto messageDto);
        Task<MessageResponseDto?> UpdateMessageAsync(Guid publicId, MessageCreateDto messageDto);
        Task<bool> DeleteMessageAsync(Guid publicId);  
    }
}
    