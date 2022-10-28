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

        //httpget categories
        [HttpGet]
        [Route("api/categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
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
        
        
    }
}