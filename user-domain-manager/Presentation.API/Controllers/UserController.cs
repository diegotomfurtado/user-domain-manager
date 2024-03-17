using System.ComponentModel.DataAnnotations;
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
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateUSerAsync(
        [FromBody] Request.User user,
        [FromHeader] string userName)
        {
            try
            {
                await this.userServices.CreateUserAsync(user, userName);
                return this.Created(new Uri($"{this.context.GetRawUrl().LocalPath}/{user.userCode}", UriKind.Relative), string.Empty);

            }catch (AggregateException ex)
            {
                ModelState.AddModelError("ValidationError", ex.Message);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUserAsync()
        {
            List<User> users = await this.userServices.GetUserAsync();
            return this.Ok(users);
        }

        [HttpGet("{userCode}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Responses.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Responses.User))]
        public async Task<IActionResult> GetUserByCodeAsync([FromRoute] string userCode)
        {
            var user = await this.userServices.GetUserByCodeAsync(userCode);
            return user == null ? NotFound("The user was not found.") : this.Ok(user);
        }

        [HttpPut("{userCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> UpdateUser(
            [FromRoute] string userCode,
            [FromBody] Request.UserUpdate userDto,
            [FromHeader] string userName)
        {
            await this.userServices.UpdateUserByCodeAsync(userCode, userDto, userName);
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

