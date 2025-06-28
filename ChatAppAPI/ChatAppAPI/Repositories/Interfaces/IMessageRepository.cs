using ChatAppAPI.Models;

namespace ChatAppAPI.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message?> GetByGuidAsync(Guid publicId);
        Task<Message> CreateAsync(Message message);
        Task<Message?> UpdateAsync(Guid publicId, Message message);
        Task<bool> DeleteAsync(Guid publicId);
    }
}
