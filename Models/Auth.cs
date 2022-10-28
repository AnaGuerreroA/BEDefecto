using System.ComponentModel.DataAnnotations;

namespace BEDefecto.Models
{
    public class Auth
    {
        [Key]
        public string Access_token { get; set; }

        public Auth(string access_token)
        {
            Access_token = access_token;
        }
    }
}