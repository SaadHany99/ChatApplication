namespace ChatApplication.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        // Optional: Additional properties like Email, FullName, etc., if needed
        public string Email { get; set; }  // Optional
        public string FullName { get; set; }  // Optional

    }
}
