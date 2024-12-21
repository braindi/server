using DTO;
using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBL categ;

        public CategoryController(ICategoryBL cat)
        {
            categ = cat;
        }


        // GET: api/<CategoryController>
        [HttpGet]
        public List<CategoryDTO> Get()
        {
            return categ.GetCategories();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return BadRequest("Category name cannot be empty");
            }

            var category = new CategoryDTO { CategoryName = value };
            bool isAdded = categ.AddCategory(category);

            if (isAdded)
            {
                return Ok("Category added successfully");
            }
            else
            {
                return Conflict("Category already exists");
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{categoryName}")]
        public bool Put(string categoryName, [FromBody] string newCategoryName)
        {
            return categ.UpdateCategory(categoryName, newCategoryName);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{categoryName}")]
        public bool Delete(string categoryName)
        {
            return categ.DeleteCategory(categoryName);
        }

        // GET api/Category/attributes/{categoryName}
        [HttpGet("categoriess/{attributeName}")]
        public IActionResult GetCategoryNamesByAttribute(string attributeName)
        {
            List<string> categoryNames = categ.GetCategoriesByAttribute(attributeName);

            if (categoryNames == null)
            {
                return NotFound($"Attribute '{attributeName}' not found.");
            }

            return Ok(categoryNames);
        }
    }
}
