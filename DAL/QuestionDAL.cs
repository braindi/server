using AutoMapper;
using DAL.Model;
using DTO;
using IDAL;


namespace DAL
{
    public class QuestionDAL : IQuestionDAL
    {
        private readonly IMapper _mapper;
        public QuestionDAL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Question, QuestionDTO>();
                cfg.CreateMap<QuestionDTO, Model.Question>();
            });
            _mapper = config.CreateMapper();
        }
        public List<QuestionDTO> GetQuestions()
        {
            try
            {
                using YenteDBContext ctx = new();
                var questions = ctx.Questions.ToList();
                return _mapper.Map<List<QuestionDTO>>(questions);

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve attributes", ex);
            }
        }

        public bool AddQuestion(string question, string attribute)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(question))
                {
                    return false;
                }
                using YenteDBContext ctx = new();
                var existQuestion = ctx.Questions.FirstOrDefault(q => q.QuestionText == question);

                if (existQuestion != null)
                {
                    if (!string.IsNullOrWhiteSpace(attribute))
                    {
                        var att = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attribute);
                        if (att != null)
                        {
                            existQuestion.AttributeId = att.AttributeId;
                            ctx.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }

                var questionModel = new Model.Question { QuestionText = question };

                if (!string.IsNullOrWhiteSpace(attribute))
                {
                    var att = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attribute);
                    if (att != null)
                    {
                        questionModel.AttributeId = att.AttributeId;
                    }
                    else
                    {
                        return false;
                    }
                }

                ctx.Questions.Add(questionModel);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateQuestion(string questionText, string newQuestionText, string attributeName, string newAttributeName)
        {
            using YenteDBContext ctx = new();
            Model.Question ques = ctx.Questions.FirstOrDefault(q => q.QuestionText == questionText);
            if (ques == null)
            {
                return false;
            }

            Model.Question newQues = ctx.Questions.FirstOrDefault(q => q.QuestionText == newQuestionText && q.QuestionId != ques.QuestionId);
            if (newQues != null)
            {
                return false;
            }

            var attribute = string.IsNullOrEmpty(newAttributeName)
                ? null
                : ctx.Attributes.FirstOrDefault(a => a.AttributeName == newAttributeName);

            if (attribute == null)
            {
                ques.AttributeId = 0;
            }
            else
            {
                ques.AttributeId = attribute.AttributeId;
            }

            ques.QuestionText = newQuestionText;
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteQuestion(string questionText)
        {
            using YenteDBContext ctx = new();
            Model.Question ques = ctx.Questions.FirstOrDefault(q => q.QuestionText == questionText);
            if (ques == null)
            {
                return false;
            }
            ctx.Questions.Remove(ques);
            ctx.SaveChanges();
            return true;
        }

        public List<string> GetQuestionsByAttribute(string attributeName)
        {
            using YenteDBContext ctx = new();
            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attributeName);
            if (attribute == null)
            {
                return null;
            }
            return ctx.Questions
                .Where(q => q.AttributeId == attribute.AttributeId)
                .Select(q => q.QuestionText)
                .ToList();
        }

        public bool AddAttributeToQuestion(string questionText, string attributeName)
        {
            using YenteDBContext ctx = new();
            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(ctx => ctx.AttributeName == attributeName);
            Model.Question question = ctx.Questions.FirstOrDefault(q => q.QuestionText == questionText);
            if (question == null || attribute == null)
            {
                return false;
            }
            question.AttributeId = attribute.AttributeId;
            ctx.SaveChanges();
            return true;
        }
    }
}
