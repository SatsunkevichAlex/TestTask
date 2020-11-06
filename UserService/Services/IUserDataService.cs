using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserDataService
    {
        public List<User> Users { get; set; }
        public Task<int> CreateUserAsync(User user);
        public Task<User> RemoveUserAsync(int id);
        public Task<bool> IsDeleted(int id);
    }
}
