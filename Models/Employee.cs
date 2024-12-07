using BCrypt.Net;

namespace ErpCalciolari.Models
{
    public class Employee
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public Employee(string name, string username, string email, string password)
        {
            Name = name;
            Username = username;
            Email = email;
            PasswordHash = HashPassword(password);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
