using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class ConfigurationManager
    {
        private static IConfiguration _configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string Get(string key)
        {
            return _configuration[key];
        }

        public static string Get(string section, string key)
        {
            return _configuration.GetSection(section)[key];
        }


        public static string GetConnectionString(string key)
        {
            return _configuration.GetSection("DbConfig:DbConnectionStrings")[key];
        }

        public static string GetConnectionString()
        {
            return GetConnectionString(Get("ConnStrName"));
        }
    }
}
