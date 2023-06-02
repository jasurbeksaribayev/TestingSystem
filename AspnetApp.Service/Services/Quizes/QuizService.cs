using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.IServices.Quizes;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Services.Quizes
{
    public class QuizService : IQuizeService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Course> courseRepository;
        private readonly IMapper mapper;

        public QuizService(IGenericRepository<Quiz> quizRepository , IMapper mapper ,IGenericRepository<Course> courseRepository)
        {
            this.quizRepository = quizRepository;
            this.mapper = mapper;
            this.courseRepository = courseRepository;
        }

        public async ValueTask<QuizForViewDTO> CreateAsync(QuizForCreationDTO quizForCreationDTO)
        {
            var existCourse = await courseRepository.GetAsync(c => c.Id.Equals(quizForCreationDTO.CourseId));

            if (existCourse == null)
                throw new AspnetAppException(404, "Course not found");

            var existQuiz = await quizRepository.GetAsync(q=>q.Title.Equals(quizForCreationDTO.Title) && q.CourseId.Equals(quizForCreationDTO.CourseId));

            if (existQuiz!=null)
                throw new AspnetAppException(404, "Course already exist quiz with this name. Change quiz or course name.");

            var quiz = await quizRepository.CreateAsync(mapper.Map<Quiz>(quizForCreationDTO));

            await quizRepository.SaveChangesasync();

            return mapper.Map<QuizForViewDTO>(quiz);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var existQuiz = await quizRepository.DeleteAsync(id);

            if(!existQuiz)
                throw new AspnetAppException(404, "Quiz not found");

            await quizRepository.SaveChangesasync();

            return true;
        }

        public async ValueTask<IEnumerable<QuizForViewDTO>> GetAllAsync(Expression<Func<Quiz, bool>> expression=null)
        {
            var quizzes = await quizRepository.GetAll()
            .Include(q => q.QuizRezults)
            .Include(q => q.Questions).ToListAsync();

            return mapper.Map<IEnumerable<QuizForViewDTO>>(quizzes);
        }

        public async ValueTask<QuizForViewDTO> GetAsync(Expression<Func<Quiz, bool>> expression)
        {
            var quiz = await quizRepository.GetAll()
            .Include(q => q.QuizRezults)
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(expression);

            if(quiz==null)
                throw new AspnetAppException(404, "not found");

            return mapper.Map<QuizForViewDTO>(quiz);
        }

        public async ValueTask<QuizForViewDTO> UpdateAsync(int id, QuizForCreationDTO quizForCreationDTO)
        {
            var existQuiz = await quizRepository.GetAsync(q => q.Id.Equals(id));

            if (existQuiz == null)
                throw new AspnetAppException(404, "Quiz not found.");

            var existCourse = await courseRepository.GetAsync(c => c.Id.Equals(quizForCreationDTO.CourseId));

            if (existCourse == null)
                throw new AspnetAppException(404, "Course not found");

            var existQuizTitle = await quizRepository.GetAsync(q => q.Title.Equals(quizForCreationDTO.Title) && q.CourseId.Equals(quizForCreationDTO.CourseId) && q.Id!=id);

            if (existQuizTitle != null)
                throw new AspnetAppException(404, "Course already exist quiz with this name. Change quiz or course name.");

            existQuiz.LastModifiedTime = DateTime.UtcNow;

            var quiz = quizRepository.Update(mapper.Map(quizForCreationDTO, existQuiz));

            await courseRepository.SaveChangesasync();

            return mapper.Map<QuizForViewDTO>(quiz);
        }
    }
}
