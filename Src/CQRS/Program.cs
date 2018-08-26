namespace SeLoger.Api.Streaming
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
