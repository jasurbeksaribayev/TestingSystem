using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.SolvedQuestions;
using AspnetApp.Service.IServices.SolvedQuestions;
using AspnetApp.Service.Services.Answers;
using AspnetApp.Service.Services.SolvedQuestions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolvedQuestionController : ControllerBase
    {
        private readonly ISolvedQuestionService solvedQuestionService;

        public SolvedQuestionController(ISolvedQuestionService solvedQuestionService)
        {
            this.solvedQuestionService = solvedQuestionService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(SolvedQuestionForCreationDTO solvedQuestionForCreationDTO)
           => Ok(await solvedQuestionService.CreateAsync(solvedQuestionForCreationDTO));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await solvedQuestionService.DeleteAsync(id));

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, SolvedQuestionForUpdateDTO solvedQuestionForUpdateDTO)
            => Ok(await solvedQuestionService.UpdateAsync(id, solvedQuestionForUpdateDTO));
    }
}

