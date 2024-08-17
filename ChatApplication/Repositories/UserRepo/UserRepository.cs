using ChatApplication.Models;
using ChatApplication.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatAppDbContext context;

        public UserRepository(ChatAppDbContext context)
        {
            this.context = context;
        }
        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
