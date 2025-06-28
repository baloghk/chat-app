using ChatAppAPI.Data;
using ChatAppAPI.Models;
using ChatAppAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<bool> DeleteAsync(Guid publicId)
        {
            var message = await GetByGuidAsync(publicId);
            if (message == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<Message?> GetByGuidAsync(Guid publicId)
        {
            return await _context.Messages
                .FirstOrDefaultAsync(m => m.PublicId == publicId);
        }

        public async Task<Message?> UpdateAsync(Guid publicId, Message message)
        {
            var existingMessage = await GetByGuidAsync(publicId);
            if (existingMessage == null)
            {
                return null;
            }

            existingMessage.Content = message.Content;
            existingMessage.User = message.User;

            await _context.SaveChangesAsync();
            return existingMessage;
        }
    }
}
