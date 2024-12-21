using DTO;
using IBL;
using IDAL;

namespace BL
{
    public class CategoryBL : ICategoryBL
    {
        private readonly ICategoryDAL categ;
        public CategoryBL(ICategoryDAL cat)
        {
            categ = cat;
        }

        public List<CategoryDTO> GetCategories()
        {
            return categ.GetCategories();
        }

        public bool AddCategory(CategoryDTO category)
        {
            return categ.AddCategory(category);
        }

        public bool UpdateCategory(string categoryName, string newCategoryName)
        {
            return categ.UpdateCategory(categoryName, newCategoryName);
        }

        public List<string> GetCategoriesByAttribute(string attributeName)
        {
            return categ.GetCategoriesByAttribute(attributeName);
        }

        public bool DeleteCategory(string categoryName)
        {
            return categ.DeleteCategory(categoryName);
        }
    }
}
