using Microsoft.AspNetCore.Mvc;
using ChatAppAPI.Models;
using ChatAppAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        {
            return await _context.Messages.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Message>> Post(Message message)
        {
            if (message == null)
            {
                return BadRequest("Invalid message data.");
            }
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }
    }
}
