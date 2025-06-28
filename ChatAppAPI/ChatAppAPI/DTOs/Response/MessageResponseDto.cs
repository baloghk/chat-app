using ChatAppAPI.Models;

namespace ChatAppAPI.DTOs.Response
{
    public class MessageResponseDto
    {
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string User { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public static MessageResponseDto FromEntity(Message message)
        {
            return new MessageResponseDto
            {
                PublicId = message.PublicId,
                User = message.User,
                Content = message.Content,
                Timestamp = message.Timestamp
            };
        }
    }
}
