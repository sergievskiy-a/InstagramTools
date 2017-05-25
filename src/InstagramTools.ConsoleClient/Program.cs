using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramTools.Api.API;
using InstagramTools.Api.API.Builder;
using InstagramTools.Api.Classes;
using InstagramTools.ConsoleClient.Samples;

namespace InstagramTools.ConsoleClient
{
    public class Program
    {
        /// <summary>
        ///     Api instance (one instance per Instagram user)
        /// </summary>
        private static IInstaApi _instaApi;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Starting demo of InstaSharper project");
            // create user session data and provide login details

            Console.WriteLine("Username:");
            var username = Console.ReadLine();

            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            var userSession = new UserSessionData
            {
                UserName = username,
                Password = password
            };
            // create new InstaApi instance using Builder
            _instaApi = new InstaApiBuilder()
                .SetUser(userSession)
                .Build();
            // login
            var logInResult = _instaApi.Login();
            if (!logInResult.Succeeded)
            {
                Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
            }
            else
            {
                Console.WriteLine("Press 1 to start basic demo samples");
                Console.WriteLine("Press 2 to start upload photo demo sample");
                Console.WriteLine("Press 3 to start comment media demo sample");

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        new Basics(_instaApi).DoShow();
                        break;
                    case ConsoleKey.D2:
                        new UploadPhoto(_instaApi).DoShow();
                        break;
                    case ConsoleKey.D3:
                        new CommentMedia(_instaApi).DoShow();
                        break;
                    default:
                        break;
                }
                var logoutResult = _instaApi.Logout();
                if (logoutResult.Value) Console.WriteLine("Logout succeed");
            }
            Console.ReadKey();
        }
    }
}
