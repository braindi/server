using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Attribute
    {
        public Attribute()
        {
            AttributeCategories = new HashSet<AttributeCategory>();
            Questions = new HashSet<Question>();
        }

        public int AttributeId { get; set; }
        public string AttributeName { get; set; } = null!;

        public virtual ICollection<AttributeCategory> AttributeCategories { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
