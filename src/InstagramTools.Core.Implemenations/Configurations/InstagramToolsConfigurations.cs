using Microsoft.Extensions.Configuration;

namespace InstagramTools.Core.Implemenations.Configurations
{
    public class InstagramToolsConfigurations
    {
        private IConfigurationRoot _root;

        public static string TestAccountUsername = "TestAccount:Username";
        public static string TestAccountPassword = "TestAccount:Password";

        public InstagramToolsConfigurations(IConfigurationRoot root)
        {
            this._root = root;
        }

        public string Get(string key)
        {
            return this._root[key];
        }

    }
}
