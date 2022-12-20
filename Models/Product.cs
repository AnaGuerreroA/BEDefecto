using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BEDefecto.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }  
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }       
        public Product()
        {
            ProductId = 0;
            Title = "";
            Price = 0;
            Description = "";
            Quantity= 0;
            CategoryId = 0;
            
        }
    }
}