namespace ChatApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Store hashed password
        public List<Message>? Messages { get; set; }
    }
}
