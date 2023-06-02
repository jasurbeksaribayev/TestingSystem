using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.IServices.Quizes;
using AspnetApp.Service.Services.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizeService quizService;

        public QuizController(IQuizeService quizService)
        {
            this.quizService = quizService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(QuizForCreationDTO quizForCreationDTO)
           => Ok(await quizService.CreateAsync(quizForCreationDTO));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await quizService.DeleteAsync(id));

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, QuizForCreationDTO quizForCreationDTO)
            => Ok(await quizService.UpdateAsync(id, quizForCreationDTO));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(int id)
            => Ok(await quizService.GetAsync(c => c.Id.Equals(id)));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await quizService.GetAllAsync());
    }
}
