using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.IServices.Courses
{
    public interface ICourseService
    {
        ValueTask<CourseForViewDTO> CreateAsync(CourseForCreationDTO courseForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<CourseForViewDTO> UpdateAsync(int id, CourseForUpdateDTO courseForUpdateDTO);
        ValueTask<CourseForViewDTO> GetAsync(Expression<Func<Course, bool>> expression);
        ValueTask<IEnumerable<CourseForViewDTO>> GetAllAsync(Expression<Func<Course, bool>> expression=null);
    }
}
