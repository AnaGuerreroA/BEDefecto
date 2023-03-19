using System.Net.Mime;
using BEDefecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BEDefecto.Controllers
{
    public class ProductController: ControllerBase
    {
        private readonly DefectoContext _context;
 
        public ProductController(DefectoContext context)
        {
            _context = context;           
        }
     
        //get products by prams limit and offset  
        [HttpGet]
        [Route("api/products")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult GetProducts([FromQuery]int limit, [FromQuery]int offset)
        {
            var products = _context.Products.Include(p => p.Category).Include(p => p.Images).Skip(offset).Take(limit).ToList();
            return Ok(products);
        }   

        //httpget products/id
        [HttpGet]
        [Route("api/products/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Include(p => p.Category).Include(p => p.Images).FirstOrDefault(p => p.ProductId == id);
            return Ok(product);
        }

        [HttpPost]
        [Route("api/products")]
        public IActionResult PostProduct([FromBody] Product product)
        {        
            if (product == null || product.Title == null || product.Title == "")
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);          
        }

        //httpdelete products/id
        [HttpDelete]
        [Route("api/products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if  (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }

        //httpput products/id
        [HttpPut]
        [Route("api/products/{id}")]
        public IActionResult PutProduct(int id, [FromBody] Product product, IFormFile file)
        {
            var productUpdate = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if  (productUpdate == null)
            {
                return NotFound();
            }
            productUpdate.Title = product.Title;
            productUpdate.Price = product.Price;
            productUpdate.Description = product.Description;
            productUpdate.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return Ok(productUpdate);
        }

        //upload file
        [HttpPost]
        [Route("api/upload")]
        public IActionResult UploadFile()
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        // get the first file
                        var file = formFile;

                        // save the file to a folder
                        //targetPath is the path to the folder where you want to save the file as imagenes

                        var targetPath = "./imagenes/" + file.Name;
                        using (var stream = new FileStream(targetPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        
                        // convert the image data to base64 encoding
                        var fileContents = System.IO.File.ReadAllBytes(targetPath);
                        
                        var image = new Image();
                        //get last product id as int
                        var lastProduct = _context.Products.Max(p => p.ProductId);
                        image.ProductId = lastProduct;
                        image.ImageName = file.Name;
                        image.ImageData = fileContents; // set the base64-encoded image data to the ImageData property
                        _context.Images.Add(image);
                        _context.SaveChanges();
                    }
                } 
            }

            return Ok();
        }
       
    }
}