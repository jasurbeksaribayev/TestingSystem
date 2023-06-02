using AspnetApp.Service.DTOs.Answers;
using AspnetApp.Service.DTOs.Quetions;
using AspnetApp.Service.IServices.Answers;
using AspnetApp.Service.Services.Answers;
using AspnetApp.Service.Services.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
           => Ok(await answerService.CreateAsync(answerForCreationDTO));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await answerService.DeleteAsync(id));

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, AnswerForUpdateDTO answerForUpdateDTO)
            => Ok(await answerService.UpdateAsync(id, answerForUpdateDTO));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await answerService.GetAllAsync());
    }
}
