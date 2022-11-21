using BEDefecto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEDefecto.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly DefectoContext _context;

        public CategoryController(DefectoContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Route("api/categories")]
        public IActionResult GetCategories([FromQuery]int limit, [FromQuery]int offset)
        {
            var categories = new List<Category>();
            //if limit and offset are 0, return all categories
            if (limit == 0 && offset == 0)
            {
                categories = _context.Categories.ToList();               
            }else  {
                categories = _context.Categories.Skip(offset).Take(limit).ToList();
            }      
  
            return Ok(categories);
        }

        //httpget categories/id
        [HttpGet]
        [Route("api/categories/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            return Ok(category);
        }

        //httppost categories
        [HttpPost]
        [Route("api/categories")]
        public IActionResult PostCategory([FromBody] Category category)
        {
            if (category == null || category.Name == null || category.Name == "")
            {
                return BadRequest();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        //httpdelete categories/id
        [HttpDelete]
        [Route("api/categories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }
        
        //get product by category
        [HttpGet]
        [Route("api/categories/{id}/products")]
        public IActionResult GetProductByCategory(int id, [FromQuery]int limit, [FromQuery]int offset)
        {
            var products = _context.Products.Where(p => p.CategoryId == id).Skip(offset).Take(limit).ToList();
            return Ok(products);
        }
    }
}