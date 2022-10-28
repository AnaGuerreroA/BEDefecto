using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BEDefecto.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            CategoryId = 0;
            Name = "";
        }
    }
}