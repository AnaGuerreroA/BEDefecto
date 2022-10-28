using System.ComponentModel.DataAnnotations;

namespace BEDefecto.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public User(int userId, string email, string name, string password)
        {
            UserId = userId;
            Email = email;
            Name = name;
            Password = password;
        }
    }
}