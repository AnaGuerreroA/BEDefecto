using Microsoft.AspNetCore.Mvc;
using BEDefecto.Models.Image;
using Microsoft.EntityFrameworkCore;

namespace BEDefecto.Controllers
{
    public class ImageController : ControllerBase
    {
        private readonly DefectoContext _context;

        public ImageController(DefectoContext context)
        {
            _context = context;
        }

        //httpget images/id
        [HttpGet]
        [Route("api/images/{id}")]
        public IActionResult GetImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.ImageId == id);
            return Ok(image);
        }

        //httppost images
        [HttpPost]
        [Route("api/images")]
        public IActionResult PostImage([FromBody] ImageEntity image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return Ok(image);
        }

        //httpdelete images/id
        [HttpDelete]
        [Route("api/images/{id}")]
        public IActionResult DeleteImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.ImageId == id);
            _context.Images.Remove(image);
            _context.SaveChanges();
            return Ok(image);
        }
       
    }
    
}