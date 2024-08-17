using ChatApplication.Models;

namespace ChatApplication.Repositories.MessageRepo
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesByChatRoomIdAsync(int chatRoomId);
        Task AddMessageAsync(Message message);
        Task SaveChangesAsync();
    }
}
