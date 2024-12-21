﻿using DTO;

namespace IDAL
{
    public interface IAttributeDAL
    {
        public List<AttributeDTO> GetAttributes();
        public bool AddAttribute(AttributeDTO attribute);
        public bool DeleteAttribute(string attributeName);
        public bool UpdateAttribute(string attributeName, string newAttributeName);
        public List<string> GetAttributesByCategory(string categoryName);
    }
}