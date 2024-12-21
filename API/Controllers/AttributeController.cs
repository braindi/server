using DTO;
using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeBL att;
        public AttributeController(IAttributeBL attr)
        {
            att = attr;
        }


        // GET: api/<AttributeController>
        [HttpGet]
        public List<AttributeDTO> Get()
        {
            return att.GetAttributes();
        }

        // GET api/<AttributeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttributeController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return BadRequest("Attribute name cannot be empty");
            }

            var attribute = new AttributeDTO { AttributeName = value };
            bool isAdded = att.AddAttribute(attribute);

            if (isAdded)
            {
                return Ok("Attribute added successfully");
            }
            else
            {
                return Conflict("Attribute already exists");
            }
        }

        // PUT api/<AttributeController>/<attributeName>
        [HttpPut("{attributeName}")]
        public bool Put(string attributeName, [FromBody] string newAttributeName)
        {
            return att.UpdateAttribute(attributeName, newAttributeName);
        }

        // DELETE api/<AttributeController>/<attributeName>
        [HttpDelete("{attributeName}")]
        public bool Delete(string attributeName)
        {
            return att.DeleteAttribute(attributeName);
        }


        // GET api/Category/attributes/{categoryName}
        [HttpGet("attributes/{categoryName}")]
        public IActionResult GetAttributeNamesByCategory(string categoryName)
        {
            List<string> attributeNames = att.GetAttributesByCategory(categoryName);

            if (attributeNames == null)
            {
                return NotFound($"Category '{categoryName}' not found.");
            }

            return Ok(attributeNames);
        }
    }
}
