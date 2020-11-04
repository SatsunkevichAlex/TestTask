using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using UserService.Extensions;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDataService _usersData;

        public UsersController(UserDataService usersData)
        {
            _usersData = usersData;
        }

        [HttpPost]
        [Route("[controller]/Auth/create-user")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequest request
        )
        {
            var status = await _usersData.CreateUser(request.User);
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
    }
}
