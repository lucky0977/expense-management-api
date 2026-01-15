namespace ExpenseManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }   // ✅ INT (matches DB)

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
