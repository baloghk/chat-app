using Microsoft.AspNetCore.Mvc;
using ChatAppAPI.Models;
using ChatAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using ChatAppAPI.Services.Interfaces;
using ChatAppAPI.DTOs.Response;
using ChatAppAPI.DTOs.Request;

namespace ChatAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public ChatController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{publicId:guid}")]
        public async Task<ActionResult<MessageResponseDto>> GetMessage(Guid publicId)
        {
            var message = await _messageService.GetMessageAsync(publicId);
            if (message == null)
                return NotFound($"Message with ID {publicId} not found.");

            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponseDto>> CreateMessage([FromBody] MessageCreateDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdMessage = await _messageService.CreateMessageAsync(messageDto);
            return CreatedAtAction(
                nameof(GetMessage),
                new { publicId = createdMessage.PublicId },
                createdMessage);
        }

        [HttpPut("{publicId:guid}")]
        public async Task<ActionResult<MessageResponseDto>> UpdateMessage(
            Guid publicId,
            [FromBody] MessageCreateDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedMessage = await _messageService.UpdateMessageAsync(publicId, messageDto);
            if (updatedMessage == null)
                return NotFound($"Message with ID {publicId} not found.");

            return Ok(updatedMessage);
        }

        [HttpDelete("{publicId:guid}")]
        public async Task<IActionResult> DeleteMessage(Guid publicId)
        {
            var deleted = await _messageService.DeleteMessageAsync(publicId);
            if (!deleted)
                return NotFound($"Message with ID {publicId} not found.");

            return NoContent();
        }
    }
}