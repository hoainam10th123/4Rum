using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public long ViewCount { get; set; }
        public string NoiDung { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<QuestionTagDto> QuestionTags { get; set; }
        public ICollection<CommentParentDto> CommentParents { get; set; }
    }

    public class QuestionAtHome
    {
        public Guid Id { get; set; }
        public string Tittle { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public long ViewCount { get; set; }
        public string NoiDung { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<QuestionTagDto> QuestionTags { get; set; }
        public int CountCommentParents { get; set; }
    }
}
