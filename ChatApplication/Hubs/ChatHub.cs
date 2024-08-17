using ChatApplication.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Hubs
{
    public class ChatHub:Hub
    {
        private readonly ChatService chatService;
        private readonly UserService userService;

        public ChatHub(ChatService chatService,UserService userService)
        {
            this.chatService = chatService;
            this.userService = userService;
        }
        public async Task SendMessage(int chatRoomId, int userId, string message)
        {
            await chatService.AddMessageAsync(chatRoomId, userId, message);
            var user = await userService.GetUserByIdAsync(userId);
            await Clients.Group(chatRoomId.ToString()).SendAsync("ReceiveMessage", user.Username, message);
        }

        public async Task JoinRoom(int chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }

        public async Task LeaveRoom(int chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString());
        }
    }
}
