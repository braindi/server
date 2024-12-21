using DTO;

namespace IBL
{
    public interface IAttributeCategoryBL
    {
        public List<AttributeCategotyClassDTO> GetAttributeCategoty();
        public bool AddAttributeCategory(string attributeName, string categoryName, int isUnique);
        public bool DeleteAttributeCategory(string attributeName, string categoryName);
    }
}
