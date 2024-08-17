using ChatApplication.Models;
using ChatApplication.Repositories.ChatRoomRepo;
using ChatApplication.Repositories.MessageRepo;

namespace ChatApplication.Services
{
    public class ChatService
    {
        private readonly IChatRoomRepository chatRoomRepository;
        private readonly IMessageRepository messageRepository;

        public ChatService(IChatRoomRepository chatRoomRepository,IMessageRepository messageRepository)
        {
            this.chatRoomRepository = chatRoomRepository;
            this.messageRepository = messageRepository;
        }
        public async Task<IEnumerable<ChatRoom>> GetAllChatRoomsAsync()
        {
            return await chatRoomRepository.GetAllChatRoomsAsync();
        }

        public async Task<ChatRoom> GetChatRoomByIdAsync(int id)
        {
            return await chatRoomRepository.GetChatRoomByIdAsync(id);
        }

        public async Task AddChatRoomAsync(string name)
        {
            var chatRoom = new ChatRoom { Name = name };
            await chatRoomRepository.AddChatRoomAsync(chatRoom);
            await chatRoomRepository.SaveChangesAsync();
        }

        public async Task AddMessageAsync(int chatRoomId, int userId, string content)
        {
            var message = new Message
            {
                ChatRoomId = chatRoomId,
                UserId = userId,
                Content = content,
                Timestamp = DateTime.UtcNow
            };
            await messageRepository.AddMessageAsync(message);
            await messageRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatRoomIdAsync(int chatRoomId)
        {
            return await messageRepository.GetMessagesByChatRoomIdAsync(chatRoomId);
        }
    }
}
