using DTO;
using IBL;
using IDAL;

namespace BL
{
    public class QuestionBL : IQuestionBL
    {
        private readonly IQuestionDAL _question;
        public QuestionBL(IQuestionDAL question)
        {
            _question = question;
        }

        public List<QuestionDTO> GetQuestions()
        {
            return _question.GetQuestions();
        }
        public bool AddQuestion(string question, string attribute)
        {
            return _question.AddQuestion(question, attribute);
        }

        public bool UpdateQuestion(string questionText, string newQuestionText, string attributeName, string newAttributeName)
        {
            return _question.UpdateQuestion(questionText, newQuestionText, attributeName, newAttributeName);
        }

        public bool DeleteQuestion(string questionText)
        {
            return _question.DeleteQuestion(questionText);
        }

        public List<string> GetQuestionsByAttribute(string attributeName)
        {
            return _question.GetQuestionsByAttribute(attributeName);
        }

        public bool AddAttributeToQuestion(string questionText, string attributeName)
        {
            return _question.AddAttributeToQuestion(questionText, attributeName);
        }
    }
}
