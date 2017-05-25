using System;
using InstagramTools.Api.Classes.Models;

namespace InstagramTools.ConsoleClient.Utils
{
    public static class ConsoleUtils
    {
        public static void PrintMedia(string header, InstaMedia media, int maxDescriptionLength)
        {
            Console.WriteLine($"{header.ToUpper()}:");

            Console.WriteLine($"AUTHOR: [{media.User.UserName}]");
            Console.WriteLine();

            Console.WriteLine($"TEXT:");
            Console.WriteLine(string.IsNullOrWhiteSpace(media.Caption?.Text) ? "null" : $"{media.Caption?.Text.Truncate(maxDescriptionLength)}");
            Console.WriteLine();

            Console.WriteLine($"MEDIA CODE:");
            Console.WriteLine("media.Code");
            Console.WriteLine();

            Console.WriteLine($"LIKES: {media.LikesCount}\t COMMENTS: {media.CommentsCount}");
            Console.WriteLine();

            Console.WriteLine($"IS MULTIPOST: {media.IsMultiPost}");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}