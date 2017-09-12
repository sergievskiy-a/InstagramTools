using System;

namespace InstagramTools.Common.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string name) :
            base($"Role name:{name} doesn't exist!")
        { }
    }
}
