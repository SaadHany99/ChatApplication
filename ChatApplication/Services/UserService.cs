using ChatApplication.Models;
using ChatApplication.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ChatApplication.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userRepository.GetUserByIdAsync(id);
        }
        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public async Task RegisterUserAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };
            await userRepository.AddUserAsync(user);
            await userRepository.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use Rfc2898DeriveBytes to generate the hash
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Combine the salt and password hash
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                // Convert the combined salt and hash to a string for storage
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Convert the stored password hash string back to a byte array
            byte[] hashBytes = Convert.FromBase64String(passwordHash);

            // Extract the salt from the stored password hash
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the input password with the same salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                // Compare the hashed input password to the stored hash
                for (int i = 0; i < 20; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
