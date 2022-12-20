using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BEDefecto.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Products { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public byte[]  Imagendata { get; set; }
        public Image()
        {
            ProductId = 0;
            ImageName = "";
            ImageId = 0;
        }
    }
}