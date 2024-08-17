using ChatApplication.Models;
using ChatApplication.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories.MessageRepo
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatAppDbContext context;

        public MessageRepository(ChatAppDbContext context)
        {
            this.context = context;
        }
        public async Task AddMessageAsync(Message message)
        {
            await context.Messages.AddAsync(message);
        }

        public async Task<List<Message>> GetMessagesByChatRoomIdAsync(int chatRoomId)
        {
            return await context.Messages.Where(m => m.ChatRoomId == chatRoomId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
