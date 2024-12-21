using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class AttributeCategory
    {
        public int AttributeId { get; set; }
        public int CategoryId { get; set; }
        public bool IsUniqueToCategory { get; set; }

        public virtual Attribute Attribute { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
