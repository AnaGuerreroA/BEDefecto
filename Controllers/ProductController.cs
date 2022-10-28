using System.Net.Mime;
using BEDefecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            return Ok(product);
        }

        //httppost products
        [HttpPost]
        [Route("api/products")]
        public IActionResult PostProduct([FromBody] Product product)
        {
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
        public IActionResult PutProduct(int id, [FromBody] Product product)
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
    }
}