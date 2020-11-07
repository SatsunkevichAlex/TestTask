using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using UserService.Models;
using UserService.Services;

namespace TestTaskTests
{
    [TestClass]
    public class UserDataServiceTests : BaseTest
    {
        private User _testUser;
        private UserDataService _service;

        [TestInitialize]
        public void Init()
        {
            _testUser = CreateRandomUser();
            _service = new UserDataService();
        }

        [TestMethod]
        public async Task UserDataService_CreateUserAsync_Test()
        {
            var status = await _service.CreateUserAsync(_testUser);

            Assert.AreEqual(status, 1);
        }

        [TestMethod]
        public async Task UserDataService_CreateNullUserAsync_Test()
        {
            _testUser = null;

            var status = await _service.CreateUserAsync(_testUser);

            Assert.AreEqual(status, -2);
        }

        [TestMethod]
        public async Task UserDataService_CreateExistedUserAsync_Test()
        {
            await _service.CreateUserAsync(_testUser);
            var status = await _service.CreateUserAsync(_testUser);

            Assert.AreEqual(status, -1);
        }

        [TestMethod]
        public async Task UserDataService_SetDeletedUserAsync_Test()
        {
            await _service.CreateUserAsync(_testUser);
            var removedUser = await _service.SetDeletedUserAsync(_testUser.Id);

            Assert.IsNotNull(removedUser);
        }

        [TestMethod]
        public async Task UserDataService_RemoveSetDeletedUserAsync_Test()
        {
            await _service.CreateUserAsync(_testUser);
            await _service.SetDeletedUserAsync(_testUser.Id);
            var status = await _service.IsDeleted(_testUser.Id);

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task UserDataService_IsDeleted_Test()
        {
            await _service.CreateUserAsync(_testUser);
            await _service.SetDeletedUserAsync(_testUser.Id);
            var status = await _service.IsDeleted(_testUser.Id);

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task UserDataService_IsNotDeleted_Test()
        {
            var status = await _service.IsDeleted(_testUser.Id);

            Assert.IsFalse(status);
        }
    }
}
