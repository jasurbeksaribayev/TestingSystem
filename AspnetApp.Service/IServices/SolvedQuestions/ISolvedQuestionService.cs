using AspnetApp.Service.DTOs.SolvedQuestions;

namespace AspnetApp.Service.IServices.SolvedQuestions
{
    public interface ISolvedQuestionService
    {
        ValueTask<SolvedQuestionForViewDTO> CreateAsync(SolvedQuestionForCreationDTO solvedQuestionForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
        ValueTask<SolvedQuestionForViewDTO> UpdateAsync(int id,SolvedQuestionForUpdateDTO solvedQuestionForUpdateDTO);
    }
}
