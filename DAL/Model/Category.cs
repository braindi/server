using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Category
    {
        public Category()
        {
            AttributeCategories = new HashSet<AttributeCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<AttributeCategory> AttributeCategories { get; set; }
    }
}
