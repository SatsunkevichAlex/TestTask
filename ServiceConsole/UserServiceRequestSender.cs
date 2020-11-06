using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using UserService.Extensions;
using UserService.Models;

namespace ServiceConsole
{
    public class UserServiceRequestSender
    {
        private readonly RestClient _client;

        public UserServiceRequestSender()
        {
            _client = new RestClient();
            _client.Authenticator = new HttpBasicAuthenticator("admin", "padmin");
        }

        public async Task<IRestResponse<CreateUserResponse>> CreateUserRequestAsync(User user)
        {
            var requestModel = new CreateUserRequest
            {
                User = user
            };
            var request = new RestRequest
            {
                Resource = "http://localhost:5000/users/Auth/create-user",
                Method = Method.POST
            };
            var t = requestModel.ToXml();
            request.AddParameter("text/xml", t, ParameterType.RequestBody);
            return await _client.ExecuteAsync<CreateUserResponse>(request);
        }

        public async Task<IRestResponse> GetUserInfoAsync(User user)
        {
            var request = new RestRequest
            {
                Resource = "http://localhost:5000/users/get-user-info",
                Method = Method.GET
            };
            request.AddParameter("Id", user.Id);
            return await _client.ExecuteAsync(request);
        }

        public async Task<IRestResponse<RemoveUserResponse>> RemoveUserAsync(User user)
        {
            var deleteRequestModel = new RemoveUserRequest
            {
                RemoveUser = new RemoveUser
                {
                    Id = user.Id
                }
            };
            var request = new RestRequest
            {
                Resource = "http://localhost:5000/users/Auth/remove-user",
                Method = Method.POST
            };
            request.AddParameter("text/xml", deleteRequestModel.ToXml(), ParameterType.RequestBody);
            return await _client.ExecuteAsync<RemoveUserResponse>(request);
        }
    }
}
