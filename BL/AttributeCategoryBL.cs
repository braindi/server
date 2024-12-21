using DTO;
using IBL;
using IDAL;

namespace BL
{
    public class AttributeCategoryBL : IAttributeCategoryBL
    {
        private readonly IAttributeCategoryDAL _attr;
        public AttributeCategoryBL(IAttributeCategoryDAL attr)
        {
            _attr = attr;
        }

        public List<AttributeCategotyClassDTO> GetAttributeCategoty()
        {
            return _attr.GetAttributeCategoty();
        }
        public bool AddAttributeCategory(string attributeName, string categoryName, int isUnique)
        {
            return _attr.AddAttributeCategory(attributeName, categoryName, isUnique);
        }

        public bool DeleteAttributeCategory(string attributeName, string categoryName)
        {
            return _attr.DeleteAttributeCategory(attributeName, categoryName);
        }
    }
}
