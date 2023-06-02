using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.Quetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Service.IServices.Answers
{
    public interface IAnswerService
    {
        ValueTask<AnswerForViewDTO> CreateAsync(AnswerForCreationDTO answerForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<AnswerForViewDTO> UpdateAsync(int id, AnswerForUpdateDTO answerForUpdateDTO);
        ValueTask<IEnumerable<AnswerForViewDTO>> GetAllAsync(Expression<Func<SolvedQuestion, bool>> expression = null);
    }
}
