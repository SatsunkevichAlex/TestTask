using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UserService.Configuration;
using UserService.Models;

namespace UserService.Data
{
    public class DbHelper
    {
        private readonly string connectionString =
            AppSettingsJson.GetAppSettings()["TestTaskDatabase"];

        public async Task<int> CreateUser(User user)
        {
            if (IsExisted(user))
            {
                return -1;
            }

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.ExecuteAsync(Queries.InsertUser, new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Status = user.Status.ToString()
                });
            }
        }

        public void DelteUser(User user)
        {
            throw new Exception();
        }

        public List<User> GetAllUsers()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>(Queries.SelectAllUsers).ToList();
            }
        }

        public bool IsExisted(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>(Queries.IsExists, new { Id = user.Id }).Any();
            }
        }
    }
}
