using AspnetApp.Domain.Entities;
using AspnetApp.Domain.Enums;
using AspnetApp.Service.DTOs.QuizResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Users
{
    public class UserForViewDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public ICollection<QuizResultForViewDTO> QuizRezults { get; set; }

    }
}
