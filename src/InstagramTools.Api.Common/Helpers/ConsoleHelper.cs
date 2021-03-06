﻿using System;
using InstagramTools.Api.Common.Extension;
using InstagramTools.Api.Common.Models.Models;

namespace InstagramTools.Common.Utils
{
    public static class ConsoleHelper
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