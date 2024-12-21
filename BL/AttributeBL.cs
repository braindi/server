using DAL;
using DTO;
using IBL;
using IDAL;

namespace BL
{
    public class AttributeBL : IAttributeBL
    {
        private readonly IAttributeDAL _attr;
        public AttributeBL(IAttributeDAL attr)
        {
            _attr = attr;
        }

        public List<AttributeDTO> GetAttributes()
        {
            return _attr.GetAttributes();
        }
        public bool AddAttribute(AttributeDTO attribute)
        {
            return _attr.AddAttribute(attribute);
        }

        public bool DeleteAttribute(string attributeName)
        {
            return _attr.DeleteAttribute(attributeName);
        }

        public bool UpdateAttribute(string attributeName, string newAttributeName)
        {
            return _attr.UpdateAttribute(attributeName, newAttributeName);
        }

        public List<string> GetAttributesByCategory(string categoryName)
        {
            return _attr.GetAttributesByCategory(categoryName);
        }
    }
}