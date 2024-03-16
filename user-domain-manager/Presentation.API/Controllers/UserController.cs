using Application.Services.Core;
using Application.Services.Services.Interface;
using Azure;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Request = Application.DTO.Requests;
using Responses = Application.DTO.Responses;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly IApplicationContext context;

        public UserController(IUserServices userServices, IApplicationContext context)
        {
            this.userServices = userServices;
            this.context = context;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUSerAsync(
        [FromBody] Request.User user,
        [FromHeader] string userName)
        {
            await this.userServices.CreateUserAsync(user, userName);

            return this.Created(new Uri($"{this.context.GetRawUrl().LocalPath}/{user.userCode}", UriKind.Relative), string.Empty);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUserAsync()
        {
            List<User> users = await this.userServices.GetUserAsync();
            return this.Ok(users);
        }

        [HttpGet("{userCode}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Responses.User))]
        public async Task<ActionResult<User>> GetUserByCodeAsync([FromRoute] string userCode)
        {
            var user = await this.userServices.GetUserByCodeAsync(userCode);
            return this.Ok(user);
        }

        [HttpPut("{userCode}")]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] string userCode,
            [FromBody] Request.UserUpdate user,
            [FromHeader] string userName)
        {
            await this.userServices.UpdateUserByCodeAsync(userCode, user, userName);
            return this.NoContent();
        }

        [HttpDelete("{userCode}")]
        public async Task<ActionResult<User>> DeleteUserByCodeAsync([FromRoute] string userCode)
        {
            await this.userServices.DeleteUserByCodeAsync(userCode);
            return this.NoContent();

        }
    }
}

