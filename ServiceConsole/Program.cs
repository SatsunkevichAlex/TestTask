using System;
using UserService.Enums;
using UserService.Models;

namespace ServiceConsole
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Test task console app.");
            Console.WriteLine("For exit press Esc");
            Console.WriteLine("For send create user request press 1");
            Console.WriteLine("For send remove request press 2");
            Console.WriteLine("For send user info request press 3");

            while (true)
            {
                var key = Console.ReadKey().Key;
                Console.WriteLine();

                switch (key)
                {
                    case ConsoleKey.D1:
                        CreateRequestFlow();
                        break;
                    case ConsoleKey.D2:
                        RemoveUserFlow();
                        break;
                    case ConsoleKey.D3:
                        UserInfoFlow();
                        break;
                    default:
                        Console.WriteLine("Key is not recognized");
                        break;
                }
            }
        }

        private static User CreateRandomUser()
        {
            var random = new Random();
            return new User
            {
                Id = random.Next(),
                Name = "testName" + random.Next(),
                Status = UserStatus.New
            };
        }

        private async static void UserInfoFlow()
        {
            var user = CreateRandomUser();
            var requestSender = new UserServiceRequestSender();

            var createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);

            var infoResponse = await requestSender.GetUserInfoAsync(user);
            Console.WriteLine(infoResponse.Content);
        }

        private async static void RemoveUserFlow()
        {
            var user = CreateRandomUser();
            var requestSender = new UserServiceRequestSender();

            var createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);

            var removeRespopnse = await requestSender.RemoveUserAsync(user);
            Console.WriteLine(removeRespopnse.Content);
        }

        private static async void CreateRequestFlow()
        {
            var user = CreateRandomUser();
            var requestSender = new UserServiceRequestSender();

            var createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);
        }
    }
}
