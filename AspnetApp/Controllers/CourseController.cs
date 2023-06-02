using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Courses;
using AspnetApp.Service.IServices.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
        /// <summary>
        /// OOO uxshadi
        /// </summary>
        /// <param name="courseForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(CourseForCreationDTO courseForCreationDTO)
            => Ok(await courseService.CreateAsync(courseForCreationDTO));

        [HttpDelete]
        public async ValueTask<IActionResult> DeleteAsync(int id)
            => Ok(await courseService.DeleteAsync(id));

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, CourseForUpdateDTO courseForUpdateDTO)
            => Ok(await courseService.UpdateAsync(id, courseForUpdateDTO));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync(int id)
            => Ok(await courseService.GetAsync(c => c.Id.Equals(id)));

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await courseService.GetAllAsync());
    }
}
