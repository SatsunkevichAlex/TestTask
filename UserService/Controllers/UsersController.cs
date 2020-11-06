using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using UserService.Extensions;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserDataService _usersData;

        public UsersController(IUserDataService usersData)
        {
            _usersData = usersData;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-user-info")]
        public IActionResult GetUserInfo()
        {
            var user = _usersData.Users
                .FirstOrDefault();
            return Content(user.ToXml(),
                new MediaTypeHeaderValue("application/xml"));
        }

        [HttpPost]
        [Route("Auth/create-user")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequest request
        )
        {
            var status = await
                _usersData.CreateUserAsync(request.User);
            if (status == -1)
            {
                return Content(new ErrorResponse
                {
                    ErrorMessage = $"User with id {request.User.Id} already exists",
                    Success = false,
                    ErrorId = 1
                }.ToXml(), new MediaTypeHeaderValue("application/xml"));
            }
            return Content(new CreateUserResponse
            {
                User = request.User,
                Success = true,
                ErrorId = 0
            }.ToXml(), new MediaTypeHeaderValue("application/xml"));
        }

        [HttpPost]
        [Route("Auth/remove-user")]
        public async Task<IActionResult> RemoveUser(
            [FromBody] RemoveUserRequest request
        )
        {
            if (await _usersData.IsDeleted(request.RemoveUser.Id))
            {
                return Content($"User {request.RemoveUser.Id} already marked as deleted",
                    new MediaTypeHeaderValue("application/json"));
            }

            var user = await
                _usersData.RemoveUserAsync(request.RemoveUser.Id);

            if (user != null)
            {
                return Content(JsonConvert.SerializeObject(
                        new RemoveUserResponse
                        {
                            Message = "User was removed",
                            Success = true,
                            User = user
                        }), new MediaTypeHeaderValue("application/json"));
            }

            return Content(JsonConvert.SerializeObject(
                    new ErrorResponse
                    {
                        ErrorId = 2,
                        ErrorMessage = "User not found",
                        Success = false
                    }
                ),
                new MediaTypeHeaderValue("application/json"));
        }
    }
}
