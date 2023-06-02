using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.DTOs.QuizResults;
using System.Linq.Expressions;

namespace AspnetApp.Service.IServices.QuizResults
{
    public interface IQuizResultService
    {
        ValueTask<QuizResultForViewDTO> StartQuiz(QuizResultForCreationDTO quizResultForCreationDTO);
        ValueTask<QuizResultForViewDTO> CreateAsync(int quizResultId);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizRezult, bool>> expression);
        ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(Expression<Func<QuizRezult, bool>> expression = null);
        ValueTask<string> GetAllInExcel(int quizId);
    }
}
