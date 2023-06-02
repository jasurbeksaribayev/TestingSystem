using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.IServices.Answers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspnetApp.Service.Services.Answers
{
    public class AnswerService : IAnswerService
    {
        private readonly IGenericRepository<Answer> answerRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;

        public AnswerService(IGenericRepository<Answer> answerRepository, IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            this.answerRepository = answerRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async ValueTask<AnswerForViewDTO> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
        {
            var questionAnswers = await questionRepository.GetAll().Include(a => a.Answers).FirstOrDefaultAsync(a=>a.Id==answerForCreationDTO.QuestionId);

            if (questionAnswers == null)
                throw new AspnetAppException(404, "Question not found");

            foreach (var answer in questionAnswers.Answers)
            {
                if (answer.Content == answerForCreationDTO.Content || (answer.IsCorrect == true && answer.IsCorrect == answerForCreationDTO.IsCorrect))
                    throw new AspnetAppException(400, "Change content or change \"isCorrect\"");
            }

            var answers = await answerRepository.CreateAsync(mapper.Map<Answer>(answerForCreationDTO));

            await questionRepository.SaveChangesasync();

            return mapper.Map<AnswerForViewDTO>(answers);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var existAnswer = await answerRepository.DeleteAsync(id);

            if (!existAnswer)
                throw new AspnetAppException(404, "Answer not found");

            await questionRepository.SaveChangesasync();

            return true;
        }

        public async ValueTask<IEnumerable<AnswerForViewDTO>> GetAllAsync(Expression<Func<SolvedQuestion, bool>> expression = null)
        {
            var answers = answerRepository.GetAll();
            
            return mapper.Map<IEnumerable<AnswerForViewDTO>>(answers);

        }

        public async ValueTask<AnswerForViewDTO> UpdateAsync(int id, AnswerForUpdateDTO answerForUpdateDTO)
        {
            var existAnswer = await answerRepository.GetAsync(q => q.Id.Equals(id));

            if (existAnswer == null)
                throw new AspnetAppException(404, "Answer not found");

            var questionAnswers = await questionRepository.GetAll().Include(a => a.Answers).FirstOrDefaultAsync(a => a.Id == answerForUpdateDTO.QuestionId);

            if (questionAnswers == null)
                throw new AspnetAppException(404, "Question not found");

            foreach (var answer in questionAnswers.Answers)
            {
                if (answer.Content == answerForUpdateDTO.Content && answer.Id != id || (answer.IsCorrect == true && answer.IsCorrect == answerForUpdateDTO.IsCorrect))
                    throw new AspnetAppException(400, "Change content or change \"isCorrect\"");
            }

            existAnswer.LastModifiedTime = DateTime.UtcNow;

            var answers = answerRepository.Update(mapper.Map(answerForUpdateDTO, existAnswer));

            await questionRepository.SaveChangesasync();

            return mapper.Map<AnswerForViewDTO>(answers);
        }
    }
}
