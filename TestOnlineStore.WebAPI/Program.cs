namespace TestOnlineStore.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
                          .ConfigureWebHostDefaults(webHost =>
                          {
                              webHost.UseStartup<Startup>();
                          });
        return builder;
    }
}