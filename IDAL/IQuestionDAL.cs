using DTO;

namespace IDAL
{
    public interface IQuestionDAL
    {
        public List<QuestionDTO> GetQuestions();
        public bool AddQuestion(string question, string attribute);
        public bool UpdateQuestion(string questionText, string newQuestionText, string attributeName, string newAttributeName);
        public bool DeleteQuestion(string questionText);
        public List<string> GetQuestionsByAttribute(string attributeName);
        public bool AddAttributeToQuestion(string questionText, string attributeName);

    }
}
