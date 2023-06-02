using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.IServices.Questions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IMapper mapper;

        public QuestionService(IGenericRepository<Question> questionRepository , IGenericRepository<Quiz> quizRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.quizRepository = quizRepository;
            this.mapper = mapper;
        }

        public async ValueTask<QuestionForViewDTO> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
        {
            var existQuiz = await quizRepository.GetAsync(q => q.Id.Equals(questionForCreationDTO.QuizId));

            if (existQuiz == null)
                throw new AspnetAppException(404, "Quiz not found");

            var existQuestion = await questionRepository.GetAsync(q => q.Number.Equals(questionForCreationDTO.Number) && q.QuizId.Equals(questionForCreationDTO.QuizId));

            if (existQuestion != null)
                throw new AspnetAppException(404, "Quiz already exist question with this number. Change question number.");

            var question = await questionRepository.CreateAsync(mapper.Map<Question>(questionForCreationDTO));

            await questionRepository.SaveChangesasync();

            return mapper.Map<QuestionForViewDTO>(question);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var existQuestion = await questionRepository.DeleteAsync(id);

            if (!existQuestion)
                throw new AspnetAppException(404, "Question not found");

            await questionRepository.SaveChangesasync();

            return true;
        }

        public async ValueTask<IEnumerable<QuestionForViewDTO>> GetAllAsync(Expression<Func<Question, bool>> expression = null)
        {
            var questions = await questionRepository.GetAll()
            .Include(q => q.Answers)
            .Include(q=>q.Attachments)
            .ToListAsync();

            return mapper.Map<IEnumerable<QuestionForViewDTO>>(questions);
        }

        public async ValueTask<QuestionForViewDTO> GetAsync(Expression<Func<Question, bool>> expression)
        {
            var question = await questionRepository.GetAll()
            .Include(q => q.Attachments)
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(expression);

            if (question == null)
                throw new AspnetAppException(404, "not found");

            return mapper.Map<QuestionForViewDTO>(question);
        }

        public async ValueTask<QuestionForViewDTO> UpdateAsync(int id, QuestionForUpdateDTO questionForUpdateDTO)
        {
            var existQuestion = await questionRepository.GetAsync(q => q.Id.Equals(id));

            if (existQuestion == null)
                throw new AspnetAppException(404, "Question not found");

            if (existQuestion.Number == questionForUpdateDTO.Number && existQuestion.Id != id)
                throw new AspnetAppException(404, "Quiz already exist question with this number. Change question number.");

            existQuestion.LastModifiedTime = DateTime.UtcNow;

            var question = questionRepository.Update(mapper.Map(questionForUpdateDTO, existQuestion));

            await questionRepository.SaveChangesasync();

            return mapper.Map<QuestionForViewDTO>(question);
        }
    }
}
