using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace TestTaskTests
{
    [TestClass]
    public class DbHelperTests : BaseTest
    {
        private DbHelper _db;

        [TestInitialize]
        public void Init()
        {
            _db = new DbHelper();
        }

        [TestMethod]
        public async Task DbHelper_CreateUser_Test()
        {
            var user = CreateRandomUser();

            var status = await _db.CreateUser(user);

            Assert.IsTrue(status == 1);
        }

        [TestMethod]
        public async Task DbHelper_CreateExistedUser_Test()
        {
            var user = CreateRandomUser();

            await _db.CreateUser(user);
            var status = await _db.CreateUser(user);

            Assert.IsTrue(status == -1);
        }

        [TestMethod]
        public async Task DbHelper_CreateNullUser_Test()
        {
            User user = null;

            var status = await _db.CreateUser(user);

            Assert.IsTrue(status == -2);
        }

        [TestMethod]
        public async Task DbHelper_SetDeleted_Test()
        {
            User user = CreateRandomUser();

            await _db.CreateUser(user);
            var removed = _db.SetDeleted(user.Id);

            Assert.IsNotNull(removed);
        }

        [TestMethod]
        public async Task DbHelper_RemoveSetDeletedUser_Test()
        {
            User user = CreateRandomUser();

            await _db.CreateUser(user);
            await _db.SetDeleted(user.Id);
            var status = await _db.IsDeleted(user.Id);

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task DbHelper_SetDeletedNotExistingUser_Test()
        {
            User user = CreateRandomUser();

            var removed = await _db.SetDeleted(user.Id);

            Assert.IsNull(removed);
        }

        [TestMethod]
        public async Task DbHelper_IsDeleted_Test()
        {
            User user = CreateRandomUser();

            await _db.CreateUser(user);
            await _db.SetDeleted(user.Id);
            var status = await _db.IsDeleted(user.Id);

            Assert.IsTrue(status);
        }

        [TestMethod]
        public async Task DbHelper_IsNotDeleted_Test()
        {
            User user = CreateRandomUser();
                       
            var status = await _db.IsDeleted(user.Id);

            Assert.IsFalse(status);
        }

        [TestMethod]
        public async Task DbHelper_GetAllUsers_Test()
        {
            User user = CreateRandomUser();

            await _db.CreateUser(user);
            var users = _db.GetAllUsers();

            Assert.IsTrue(users.Any());
        }
    }
}
