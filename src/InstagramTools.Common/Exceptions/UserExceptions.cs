using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramTools.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string id):
            base($"User id:{id} doesn't exist!") { }

    }
}
