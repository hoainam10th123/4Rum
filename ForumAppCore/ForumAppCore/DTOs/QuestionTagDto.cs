using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class QuestionTagDto
    {
        public Guid QuestionId { get; set; }
        public int TagId { get; set; }
        public string LaguageName { get; set; }
    }
}
