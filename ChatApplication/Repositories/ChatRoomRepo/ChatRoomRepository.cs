using ChatApplication.Models;
using ChatApplication.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories.ChatRoomRepo
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ChatAppDbContext context;

        public ChatRoomRepository(ChatAppDbContext context)
        {
            this.context = context;
        }
        public async Task AddChatRoomAsync(ChatRoom chatRoom)
        {
            await context.ChatRooms.AddAsync(chatRoom);
        }

        public async Task<List<ChatRoom>> GetAllChatRoomsAsync()
        {
            return await context.ChatRooms.ToListAsync();
        }

        public async Task<ChatRoom> GetChatRoomByIdAsync(int id)
        {
            return await context.ChatRooms.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
