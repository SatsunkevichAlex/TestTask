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
            Console.WriteLine("1: create user request");
            Console.WriteLine("2: remove request press");
            Console.WriteLine("3: user info request");
            Console.WriteLine("4: create user error");
            Console.WriteLine("5: remove user error");
            Console.WriteLine("ESC: exit");

            bool flag = true;
            while (flag)
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
                    case ConsoleKey.D4:
                        CreateUserErrorFlow();
                        break;
                    case ConsoleKey.D5:
                        DeleteUserErrorFlow();
                        break;
                    case ConsoleKey.Escape:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Key is not recognized");
                        break;
                }
            }
        }

        private async static void CreateUserErrorFlow()
        {
            var user = CreateRandomUser();
            var requestSender = new UserServiceRequestSender();

            var createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);
            createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);
        }
        
        private async static void DeleteUserErrorFlow()
        {
            var user = CreateRandomUser();
            var requestSender = new UserServiceRequestSender();

            var createResponse = await requestSender.CreateUserRequestAsync(user);
            Console.WriteLine(createResponse.Content);

            var removeRespopnse = await requestSender.RemoveUserAsync(user);
            Console.WriteLine(removeRespopnse.Content);
            removeRespopnse = await requestSender.RemoveUserAsync(user);
            Console.WriteLine(removeRespopnse.Content);
        }

        private async static void UserInfoFlow()
        {
            var requestSender = new UserServiceRequestSender();
            var infoResponse = await requestSender.GetUserInfoAsync();
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
    }
}
