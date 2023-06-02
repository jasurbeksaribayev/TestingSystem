using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.DTOs.Courses
{
    public class CourseForViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuizForViewDTO> Quizes { get; set; }
    }
}
