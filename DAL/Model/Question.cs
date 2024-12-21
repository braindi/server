using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Question
    {
        public Question()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public int AttributeId { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
