using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.IServices.Questions;
using AspnetApp.Service.Services.Quizes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
           => Ok(await questionService.CreateAsync(questionForCreationDTO));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await questionService.DeleteAsync(id));

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, QuestionForUpdateDTO questionForUpdateDTO)
            => Ok(await questionService.UpdateAsync(id, questionForUpdateDTO));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(int id)
            => Ok(await questionService.GetAsync(c => c.Id.Equals(id)));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await questionService.GetAllAsync());
    }
}
