using AutoMapper;
using DAL.Model;
using DTO;
using IDAL;

namespace DAL
{
    public class CategoryDAL : ICategoryDAL
    {
        private readonly IMapper _mapper;
        public CategoryDAL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Model.Category>();
            });
            _mapper = config.CreateMapper();
        }
        public List<CategoryDTO> GetCategories()
        {
            try
            {
                using YenteDBContext ctx = new();
                var categories = ctx.Categories.ToList();
                return _mapper.Map<List<CategoryDTO>>(categories);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve attributes", ex);
            }
        }

        public bool AddCategory(CategoryDTO category)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(category.CategoryName))
                {
                    return false;
                }
                using YenteDBContext ctx = new();
                var existCategory = ctx.Categories.FirstOrDefault(c => c.CategoryName == category.CategoryName);
                if (existCategory != null)
                {
                    return false;
                }
                Category c = _mapper.Map<Category>(category);
                ctx.Categories.Add(c);
                ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCategory(string categoryName, string newCategoryName)
        {
            using YenteDBContext ctx = new();
            Model.Category cat = ctx.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (cat == null)
            {
                return false;
            }
            else
            {
                Model.Category newCat = ctx.Categories.FirstOrDefault(c => c.CategoryName == newCategoryName);
                if (newCat == null)
                {
                    cat.CategoryName = newCategoryName;
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<string> GetCategoriesByAttribute(string attributeName)
        {
            using YenteDBContext ctx = new();
            Model.Attribute attribute = ctx.Attributes.FirstOrDefault(a => a.AttributeName == attributeName);
            if (attribute == null)
            {
                return null;
            }
            return ctx.AttributeCategories
                 .Where(ac => ac.AttributeId == attribute.AttributeId)
                 .Select(ac => ac.Category.CategoryName)
                 .ToList();
        }

        public bool DeleteCategory(string categoryName)
        {
            using YenteDBContext ctx = new();
            Model.Category category = ctx.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (category == null)
            {
                return false;
            }

            Model.AttributeCategory attributeCategory = ctx.AttributeCategories.FirstOrDefault(at => at.CategoryId == category.CategoryId);
            if (attributeCategory != null)
            {
                return false;
            }
            ctx.Categories.Remove(category);
            ctx.SaveChanges();
            return true;

        }
    }
}
