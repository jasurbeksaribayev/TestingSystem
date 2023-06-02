using AspnetApp.Data.DbContexts;
using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.IServices.Courses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public CourseService(IGenericRepository<Course> courseRepository,IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }
        
        public async ValueTask<CourseForViewDTO> CreateAsync(CourseForCreationDTO courseForCreationDTO)
        {
            var exisCourse = await courseRepository.GetAsync(c => c.Name.Equals(courseForCreationDTO.Name));

            if (exisCourse != null)
                throw new AspnetAppException(404, "Course already exist with this name. Change course name.");

            var course = await courseRepository.CreateAsync(mapper.Map<Course>(courseForCreationDTO));

            await courseRepository.SaveChangesasync();

            return mapper.Map<CourseForViewDTO>(course);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var existCourse = await courseRepository.DeleteAsync(id);

            if (!existCourse)
                throw new AspnetAppException(404, "Course not found");

            await courseRepository.SaveChangesasync();

            return true;            
        }

        public async ValueTask<IEnumerable<CourseForViewDTO>> GetAllAsync(Expression<Func<Course, bool>> expression = null)
        {
            var quizzes = await courseRepository.GetAll()
                .Include(c => c.Quizes).ThenInclude(q => q.Questions).ToListAsync();

            if (quizzes == null)
                throw new AspnetAppException(404, "nothing not found");

            return mapper.Map<IEnumerable<CourseForViewDTO>>(quizzes);
        }

        public async ValueTask<CourseForViewDTO> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var quiz = await courseRepository.GetAll()
                .Include(c => c.Quizes)
                .ThenInclude(q => q.Questions)
                .FirstOrDefaultAsync(expression);
            
            if (quiz == null)
                throw new AspnetAppException(404, "not found");

            return mapper.Map<CourseForViewDTO>(quiz);
        }

        public async ValueTask<CourseForViewDTO> UpdateAsync(int id, CourseForUpdateDTO courseForUpdateDTO)
        {
            var existCourse = await courseRepository.GetAsync(c => c.Id.Equals(id));

            if (existCourse == null)
                throw new AspnetAppException(404, "Course not found");

            if(existCourse.Name==courseForUpdateDTO.Name && existCourse.Id != id)
                throw new AspnetAppException(404, "Course already exist with this name. Change course name.");

            existCourse.LastModifiedTime = DateTime.UtcNow;

            var course = courseRepository.Update(mapper.Map(courseForUpdateDTO, existCourse));

            await courseRepository.SaveChangesasync();

            return mapper.Map<CourseForViewDTO>(course);
        }
    }
}
