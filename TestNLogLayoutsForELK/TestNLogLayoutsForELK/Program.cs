// See https://aka.ms/new-console-template for more information
// Test with structured logging

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;

namespace TestNLogLayoutsForELK;

public class Program
{
    private static IHostBuilder CreateDefaultBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(app => { app.AddJsonFile("appsettings.json"); })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            }).UseNLog()
            .ConfigureServices(services => { services.AddSingleton<Worker>(); });
    }

    public static void Main()
    {
        var host = CreateDefaultBuilder().Build();
        using var serviceScope = host.Services.CreateScope();
        var provider = serviceScope.ServiceProvider;
        var workerInstance = provider.GetRequiredService<Worker>();
        workerInstance.DoWork();
        host.Run();
    }
}