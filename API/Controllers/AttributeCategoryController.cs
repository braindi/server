using Microsoft.AspNetCore.Mvc;
using DTO;
using IBL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeCategoryController : ControllerBase
    {
        private readonly IAttributeCategoryBL attcat;

        public AttributeCategoryController(IAttributeCategoryBL attcat)
        {
            this.attcat = attcat;
        }


        // GET: api/<AttributeCategoryController>
        [HttpGet]
        public List<AttributeCategotyClassDTO> Get()
        {
            return attcat.GetAttributeCategoty();

        }

        // GET api/<AttributeCategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttributeCategoryController>
        [HttpPost]
        public IActionResult Post(string attributeName, string categoryName, int isUnique)
        {
            var result = attcat.AddAttributeCategory(attributeName, categoryName, isUnique);
            if (result)
            {
                return Ok("Category added successfully.");
            }
            else
            {
                return BadRequest("Failed to add category.");
            }
        }

        // DELETE api/<AttributeCategoryController>/5
        [HttpDelete]
        public bool Delete(string attributeName, string categoryName)
        {
            return attcat.DeleteAttributeCategory(attributeName, categoryName);
        }
    }
}
