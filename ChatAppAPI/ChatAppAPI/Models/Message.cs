namespace ChatAppAPI.Models
{
    public class Message
    {
        public int Id{ get; set; }
        public Guid PublicId { get; set; } = Guid.NewGuid();
        public string User { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
