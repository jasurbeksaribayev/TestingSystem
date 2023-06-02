using AspnetApp.Domain.Enums;
using AspnetApp.Helpers;
using AspnetApp.Service.DTOs.Users;
using AspnetApp.Service.IServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(UserForCreationDTO userForCreationDTO)
        => Ok(await userService.CreateAsync(userForCreationDTO));

        [HttpDelete , Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> DeleteAsync(int id)
        => Ok(await userService.DeletesAsync(id));
        
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
        => Ok(await userService.GetAsync(u=>u.Id==id));

        // to check logger

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    logger.LogInformation("API endpoint called");
        //    return Ok();
        //}

        [HttpGet]
        public async ValueTask<IActionResult> GetAll()
        => Ok(await userService.GetAllAsync());

        [HttpPut]
        public async ValueTask<IActionResult> UpdateAsync(int id, UserForUpdateDTO userForUpdateDTO)
        => Ok(await userService.UpdatesAsync(id, userForUpdateDTO));

        [HttpPatch("{password}"), Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
        => Ok(await userService.ChangePasswordAsync(userForChangePasswordDTO));
        
        [HttpPatch("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> ChangeRoleAsync([FromRoute] int id, UserRole userRole)
           => Ok(await userService.ChangeRoleAsync(id, userRole));
    }
}
