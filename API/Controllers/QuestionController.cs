using DTO;
using IBL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionBL ques;
        public QuestionController(IQuestionBL q)
        {
            ques = q;
        }
        // GET: api/<QuestionController>
        [HttpGet]
        public List<QuestionDTO> Get()
        {
            return ques.GetQuestions();
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        public class QuestionRequest
        {
            public string QuestionText { get; set; } = null!;
            public string AttributeName { get; set; } = null!;
        }

        // POST api/<QuestionController>
        [HttpPost]
        public IActionResult Post([FromBody] QuestionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.QuestionText))
            {
                return BadRequest("Question name cannot be empty");
            }

            bool isAdded = ques.AddQuestion(request.QuestionText, request.AttributeName);

            if (isAdded)
            {
                return Ok("Question added successfully");
            }
            else
            {
                return Conflict("Question already exists");
            }
        }

        // PUT api/<QuestionController>/5
        [HttpPut("{questionText}")]
        public bool Put(string questionText, [FromBody] UpdateQuestionModel model)
        {
            return ques.UpdateQuestion(questionText, model.NewQuestionText, model.AttributeName, model.NewAttributeName);
        }

        public class UpdateQuestionModel
        {
            public string NewQuestionText { get; set; }
            public string AttributeName { get; set; }
            public string NewAttributeName { get; set; }
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{questionText}")]
        public bool Delete(string questionText)
        {
            return ques.DeleteQuestion(questionText);
        }

        // GET api/Attribute/questions/{attributeName}
        [HttpGet("questions/{attributeName}")]
        public IActionResult GetQuestionsByAttribute(string attributeName)
        {
            List<string> questions = ques.GetQuestionsByAttribute(attributeName);

            if (questions == null)
            {
                return NotFound($"Attribute '{attributeName}' not found.");
            }

            return Ok(questions);
        }

        // POST api/Question/addAttribute
        [HttpPost("addAttribute")]
        public IActionResult AddAttributeToQuestion(string questionText, string attributeName)
        {
            bool result = ques.AddAttributeToQuestion(questionText, attributeName);

            if (!result)
            {
                return NotFound($"Either the question '{questionText}' or the attribute '{attributeName}' was not found.");
            }

            return Ok($"Attribute '{attributeName}' successfully added to question '{questionText}'.");
        }
    }
}
