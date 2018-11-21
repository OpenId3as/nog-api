using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
