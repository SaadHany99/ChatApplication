using ChatApplication.Models;

namespace ChatApplication.Repositories.ChatRoomRepo
{
    public interface IChatRoomRepository
    {
        Task<ChatRoom> GetChatRoomByIdAsync(int id);
        Task<List<ChatRoom>> GetAllChatRoomsAsync();
        Task AddChatRoomAsync(ChatRoom chatRoom);
        Task SaveChangesAsync();
    }
}
