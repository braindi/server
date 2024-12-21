using DAL.Model;
using IDAL;
using DTO;
using AutoMapper;

namespace DAL
{
    public class AttributeCategoryDAL : IAttributeCategoryDAL
    {
        private readonly IMapper _mapper;

        public AttributeCategoryDAL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.AttributeCategory, AttributeCategoryDTO>();
                cfg.CreateMap<AttributeCategoryDTO, Model.AttributeCategory>();
                cfg.CreateMap<AttributeCategotyClass, AttributeCategotyClassDTO>();
                cfg.CreateMap<AttributeCategotyClassDTO, AttributeCategotyClass>();
            });
            _mapper = config.CreateMapper();
        }
        public List<AttributeCategotyClassDTO> GetAttributeCategoty()
        {
            using YenteDBContext ctx = new();
            var result = ctx.AttributeCategories
           .Select(x => new AttributeCategotyClass
           {
               AttributeName = x.Attribute.AttributeName,
               categoryName = x.Category.CategoryName,
               isUnique = x.IsUniqueToCategory == true ? "כן" : "לא"
           })
           .ToList();
            return _mapper.Map<List<AttributeCategotyClassDTO>>(result);
        }

        public bool AddAttributeCategory(string attributeName, string categoryName, int isUnique)
        {
            using YenteDBContext ctx = new();

            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attributeName);
            Model.Category category = ctx.Categories.FirstOrDefault(c => c.CategoryName == categoryName);

            if (attribute == null || category == null || (isUnique != 0 && isUnique != 1))
            {
                return false;
            }

            if (ctx.AttributeCategories.Any(ac =>
                ac.AttributeId == attribute.AttributeId &&
                ac.CategoryId == category.CategoryId &&
                ac.IsUniqueToCategory == (isUnique == 1)))
            {
                return false;
            }

            if (isUnique == 1)
            {
                if (ctx.AttributeCategories.Any(ac => ac.AttributeId == attribute.AttributeId))
                {
                    return false;
                }

                if (ctx.AttributeCategories.Any(ac => ac.CategoryId == category.CategoryId && ac.IsUniqueToCategory == true))
                {
                    return false;
                }
            }
            else
            {

                if (ctx.AttributeCategories.Any(ac => ac.AttributeId == attribute.AttributeId && ac.IsUniqueToCategory == true))
                {
                    return false;
                }
            }

            AttributeCategory ac = new AttributeCategory
            {
                AttributeId = attribute.AttributeId,
                CategoryId = category.CategoryId,
                IsUniqueToCategory = (isUnique == 1)
            };

            ctx.AttributeCategories.Add(ac);
            ctx.SaveChanges();
            return true;
        }

        public bool DeleteAttributeCategory(string attributeName, string categoryName)
        {
            using YenteDBContext ctx = new();

            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(ac => ac.AttributeName == attributeName);
            Model.Category category = ctx.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (attribute == null || category == null)
            {
                return false;
            }

            Model.AttributeCategory ac = new AttributeCategory();
            ac.AttributeId = attribute.AttributeId;
            ac.CategoryId = category.CategoryId;
            AttributeCategory attCar = ctx.AttributeCategories.FirstOrDefault(x => x.AttributeId == ac.AttributeId && x.CategoryId == ac.CategoryId);
            if (attCar == null)
            {
                return false;
            }
            else
            {
                ctx.AttributeCategories.Remove(attCar);
                ctx.SaveChanges();
                return true;
            }
        }
    }
}
