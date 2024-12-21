using AutoMapper;
using DAL.Model;
using DTO;
using IDAL;

namespace DAL
{
    public class AttributeDAL : IAttributeDAL
    {
        private readonly IMapper _mapper;

        public AttributeDAL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Attribute, AttributeDTO>();
                cfg.CreateMap<AttributeDTO, Model.Attribute>();
            });
            _mapper = config.CreateMapper();
        }
        public List<AttributeDTO> GetAttributes()
        {
            try
            {
                using YenteDBContext ctx = new();
                var attributes = ctx.Attributes.ToList();

                return _mapper.Map<List<AttributeDTO>>(attributes);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve attributes", ex);
            }
        }

        public bool AddAttribute(AttributeDTO attribute)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(attribute.AttributeName))
                {
                    return false;
                }
                using YenteDBContext ctx = new();
                var existAttribute = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attribute.AttributeName);
                if (existAttribute != null)
                {
                    return false;
                }

                Model.Attribute att = _mapper.Map<Model.Attribute>(attribute);
                ctx.Attributes.Add(att);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteAttribute(string attributeName)
        {
            using YenteDBContext ctx = new();

            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attributeName);

            if (attribute != null)
            {
                Model.Question question = ctx.Questions.FirstOrDefault(q => q.AttributeId == attribute.AttributeId);
                if (question != null)
                {
                    return false;
                }
                Model.AttributeCategory attributeCategory = ctx.AttributeCategories.FirstOrDefault(ac => ac.AttributeId == attribute.AttributeId);
                if (attributeCategory != null)
                {
                    return false;
                }
                ctx.Attributes.Remove(attribute);
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAttribute(string attributeName, string newAttributeName)
        {
            using YenteDBContext ctx = new();
            Model.Attribute att = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attributeName);
            if (att == null)
            {
                return false;

            }
            else
            {
                Model.Attribute newAtt = ctx.Attributes.FirstOrDefault(a => a.AttributeName == newAttributeName);

                if (newAtt == null)
                {
                    att.AttributeName = newAttributeName;
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<string> GetAttributesByCategory(string categoryName)
        {
            using YenteDBContext ctx = new();
            Category category = ctx.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (category == null)
            {
                return null;
            }
            return ctx.AttributeCategories
                 .Where(ac => ac.CategoryId == category.CategoryId)
                 .Select(ac => ac.Attribute.AttributeName)
                 .ToList();
        }
    }
}