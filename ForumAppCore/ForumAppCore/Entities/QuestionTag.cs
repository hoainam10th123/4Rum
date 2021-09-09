using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.Entities
{
    public class QuestionTag
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        public int TagId { get; set; }
        public TagsLanguage TagsLanguage { get; set; }
    }
}
