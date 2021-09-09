using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public string NoiDung { get; set; }
        public string QuestionId { get; set; }
        public string ParentId { get; set; }
        public string UserCommentTo { get; set; }
    }
}
