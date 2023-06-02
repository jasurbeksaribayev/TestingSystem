using System.ComponentModel.DataAnnotations;

namespace AspnetApp.Service.DTOs.Courses
{
    public class CourseForCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
