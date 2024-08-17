using ChatApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatService chatService;

        public ChatController(ChatService chatService)
        {
            this.chatService = chatService;
        }
        [HttpGet("rooms")]
        public async Task<IActionResult> GetChatRooms()
        {
            var chatRooms = await chatService.GetAllChatRoomsAsync();
            return Ok(chatRooms);
        }

        [HttpPost("rooms")]
        public async Task<IActionResult> CreateChatRoom([FromBody] string name)
        {
            await chatService.AddChatRoomAsync(name);
            return Ok();
        }

        [HttpGet("rooms/{id}/messages")]
        public async Task<IActionResult> GetMessages(int id)
        {
            var messages = await chatService.GetMessagesByChatRoomIdAsync(id);
            return Ok(messages);
        }
    }
}
