using DTO;

namespace IDAL
{
    public interface ICategoryDAL
    {
        public List<CategoryDTO> GetCategories();
        public bool AddCategory(CategoryDTO category);
        public bool UpdateCategory(string categoryName, string newCategoryName);
        public List<string> GetCategoriesByAttribute(string attributeName);
        public bool DeleteCategory(string categoryName);
    }
}
