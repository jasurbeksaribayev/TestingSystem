using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.SolvedQuestions;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.IServices.SolvedQuestions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AspnetApp.Service.Services.SolvedQuestions
{
    public class SolvedQuestionService : ISolvedQuestionService
    {
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IGenericRepository<SolvedQuestion> solvedQuestionRepository;
        private readonly IGenericRepository<QuizRezult> quizResultRepository;
        private readonly IMapper mapper;

        public SolvedQuestionService(IGenericRepository<Question> questionRepository,IGenericRepository<SolvedQuestion> solvedQuestionRepository, IGenericRepository<QuizRezult> quizResultRepository, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.solvedQuestionRepository = solvedQuestionRepository;
            this.quizResultRepository = quizResultRepository;
            this.mapper = mapper;
        }
        public async ValueTask<SolvedQuestionForViewDTO> CreateAsync(SolvedQuestionForCreationDTO solvedQuestionForCreationDTO)
        {
            var existQuizResult = await quizResultRepository.GetAsync(q => q.Id.Equals(solvedQuestionForCreationDTO.QuizRezultId));

            if (existQuizResult is null)
                throw new AspnetAppException(404, "quizResult not found");

            var existQuestionAnswers = await questionRepository.GetAll().Include(a => a.Answers).FirstOrDefaultAsync(q => q.Id.Equals(solvedQuestionForCreationDTO.QuestionId));
            
            if(existQuestionAnswers is null)
                throw new AspnetAppException(404, "question not found");


            var answer = existQuestionAnswers.Answers.FirstOrDefault(a => a.Id == solvedQuestionForCreationDTO.AnswerId);
            
            if(answer is null)
                throw new AspnetAppException(404, "answer not found or this question not include this answer");
 
                solvedQuestionForCreationDTO.CorrectAnswer = answer.IsCorrect;
            
            var solvedQuestion = await solvedQuestionRepository.CreateAsync(mapper.Map<SolvedQuestion>(solvedQuestionForCreationDTO));

            await solvedQuestionRepository.SaveChangesasync();

            return mapper.Map<SolvedQuestionForViewDTO>(solvedQuestion);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await solvedQuestionRepository.DeleteAsync(id);

            if(!isDeleted)
                throw new AspnetAppException(404, "solvedQuestion not found");

            await solvedQuestionRepository.SaveChangesasync();

            return true;
        }

        public async ValueTask<SolvedQuestionForViewDTO> UpdateAsync(int id, SolvedQuestionForUpdateDTO solvedQuestionForUpdateDTO)
        {
            var existSolvedQuestion = await solvedQuestionRepository.GetAsync(s => s.Id.Equals(id));

            if(existSolvedQuestion is null)
                throw new AspnetAppException(404, "solvedQuestion not found");

            var existQuestionAnswer = await questionRepository.GetAll().Include(a => a.Answers).FirstOrDefaultAsync(q => q.Id.Equals(solvedQuestionForUpdateDTO.QuestionId));

            if (existQuestionAnswer is null)
                throw new AspnetAppException(404, "question not found");

            foreach (var existAnswer in existQuestionAnswer.Answers)
            {
                if (existAnswer.Id != solvedQuestionForUpdateDTO.AnswerId)
                    throw new AspnetAppException(404, "answer not found or this question not include this answer");
            }

            existSolvedQuestion.LastModifiedTime = DateTime.UtcNow;

            var solvedQuestion = solvedQuestionRepository.Update(mapper.Map(solvedQuestionForUpdateDTO, existSolvedQuestion));

            await solvedQuestionRepository.SaveChangesasync();

            return mapper.Map<SolvedQuestionForViewDTO>(solvedQuestion);
        }
    }
}
