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
        public byte[] ImageData { get; set; }
        [NotMapped]
        public string ImageDataBase64 { get; set; }
        public Image()
        {
            ProductId = 0;
            ImageName = "";
            ImageId = 0;
            ImageData = null;
        }
    }
}