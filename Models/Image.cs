using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BEDefecto.Models.Image
{
    public class ImageEntity
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Products { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public byte[]  ImageData { get; set; }
        
        public ImageEntity()
        {
            ProductId = 0;
            ImageName = "";
            ImageId = 0;
            ImageData = new byte[0];
        }
    }
}