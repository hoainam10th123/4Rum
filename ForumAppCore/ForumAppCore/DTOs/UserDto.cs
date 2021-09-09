using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumAppCore.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
