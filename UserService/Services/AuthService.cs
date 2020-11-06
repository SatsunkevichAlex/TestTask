using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<AuthModel> _auths = new List<AuthModel>
        {
            new AuthModel
            {
                Username = "admin",
                Password = "padmin"
            }
        };

        public async Task<AuthModel> Authenticate(string username, string password)
        {
            var auth = await Task.Run(
                () => _auths.SingleOrDefault(it =>
                    it.Username == username &&
                    it.Password == password
            ));

            if (auth == null)
            {
                return null;
            }

            return new AuthModel { Username = auth.Username };
        }
    }
}
