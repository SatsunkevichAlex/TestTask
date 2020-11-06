using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IAuthService
    {
        Task<AuthModel> Authenticate(string name, string password);        
    }
}
