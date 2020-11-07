using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UserService.Enums;
using UserService.Models;

namespace TestTaskTests
{
    [TestClass]
    public class BaseTest
    {
        protected User CreateRandomUser()
        {
            var random = new Random();
            return new User
            {
                Id = random.Next(),
                Name = "testName" + random.Next(),
                Status = UserStatus.New
            };
        }
    }
}
