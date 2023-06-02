using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.IServices.Quizes
{
    public interface IQuizeService
    {
        ValueTask<QuizForViewDTO> CreateAsync(QuizForCreationDTO quizForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<QuizForViewDTO> UpdateAsync(int id, QuizForCreationDTO quizForCreationDTO);
        ValueTask<QuizForViewDTO> GetAsync(Expression<Func<Quiz, bool>> expression);
        ValueTask<IEnumerable<QuizForViewDTO>> GetAllAsync(Expression<Func<Quiz, bool>> expression=null);

    }
}
