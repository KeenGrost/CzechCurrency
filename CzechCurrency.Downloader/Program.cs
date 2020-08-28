using System;
using System.Threading.Tasks;
using Common.Utility.Extensions;
using Microsoft.Extensions.Hosting;

namespace CzechCurrency.Downloader
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunUtility();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureUtilityHostDefaults(utilityHostBuilder =>
                {
                    utilityHostBuilder.UseStartup<Startup>();

                });
    }
}
