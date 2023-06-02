using AspnetApp.Helpers;
using AspnetApp.Service.DTOs.QuizResults;
using AspnetApp.Service.IServices.QuizResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService quizResultService;

        public QuizResultController(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }

        [HttpPost("start")]
        public async ValueTask<IActionResult> StartAsync(QuizResultForCreationDTO quizResultForCreationDTO)
            => Ok(await quizResultService.StartQuiz(quizResultForCreationDTO));

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(int quizResultId)
           => Ok(await quizResultService.CreateAsync(quizResultId));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await quizResultService.DeleteAsync(id));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(int id)
            => Ok(await quizResultService.GetAsync(c => c.Id.Equals(id)));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await quizResultService.GetAllAsync());

        [HttpGet("{quizId}/Excel")]
        public async ValueTask<IActionResult> GetAllInExcel([FromRoute] int quizId)
                    => Ok(await quizResultService.GetAllInExcel(quizId));
    }
}
