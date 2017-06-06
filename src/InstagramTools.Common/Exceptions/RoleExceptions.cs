using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramTools.Common.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string name) :
            base($"Role name:{name} doesn't exist!")
        { }
    }
}
