using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace UserService.Services
{
    public class UserDataService
    {
        private const int expiredAfter = 10;
        private readonly DbHelper _db;
        private DateTime _updated;
        private List<User> _users;

        public List<User> Users
        {
            get
            {
                if (IsExpired())
                {
                    return _users = GetAllUsers();
                }
                return _users;
            }
            set { }
        }

        public UserDataService()
        {
            _updated = DateTime.Now;
            _db = new DbHelper();
            _users = GetAllUsers();
        }

        public async Task<int> CreateUser(User user)
        {
            return await _db.CreateUser(user);
        }

        private bool IsExpired()
        {
            var minutesSinceLastUpdate = DateTime.Now.Subtract(_updated)
                 .TotalMinutes;
            if (minutesSinceLastUpdate > expiredAfter)
            {
                _updated = DateTime.Now;
                return true;
            }
            return false;
        }

        private List<User> GetAllUsers()
        {
            return _db.GetAllUsers();
        }
    }
}
