using System;

namespace InstagramTools.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id):
            base($"User id:{id} doesn't exist!") { }

    }
}
