using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.DTOs.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.IServices.Questions
{
    public interface IQuestionService
    {
        ValueTask<QuestionForViewDTO> CreateAsync(QuestionForCreationDTO questionForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<QuestionForViewDTO> UpdateAsync(int id, QuestionForUpdateDTO questionForUpdateDTO);
        ValueTask<QuestionForViewDTO> GetAsync(Expression<Func<Question, bool>> expression);
        ValueTask<IEnumerable<QuestionForViewDTO>> GetAllAsync(Expression<Func<Question, bool>> expression = null);
    }
}
