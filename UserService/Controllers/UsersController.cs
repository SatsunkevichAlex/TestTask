using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Linq;
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

        [HttpGet]
        [Route("[controller]/get-user-info")]
        public IActionResult GetUserInfo(int Id)
        {
            var users = _usersData.Users;
            var user = _usersData.Users
                .SingleOrDefault(it => it.Id == Id);
            return Content(user.ToXml(), new MediaTypeHeaderValue("application/xml"));
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
