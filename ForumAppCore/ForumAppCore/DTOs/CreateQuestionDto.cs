using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class CreateQuestionDto
    {
        public string Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        public int[] Tags { get; set; }
        [Required]
        public string NoiDung { get; set; }
    }
}
