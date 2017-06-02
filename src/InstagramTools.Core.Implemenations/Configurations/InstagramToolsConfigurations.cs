using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InstagramTools.Core.Configurations
{
    public class InstagramToolsConfigurations
    {
        private IConfigurationRoot _root;

        public static string TestAccountUsername = "TestAccount:Username";
        public static string TestAccountPassword = "TestAccount:Password";

        public InstagramToolsConfigurations(IConfigurationRoot root)
        {
            _root = root;
        }

        public string Get(string key)
        {
            return _root[key];
        }

    }
}
