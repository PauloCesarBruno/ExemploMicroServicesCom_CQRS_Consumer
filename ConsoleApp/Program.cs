using Infrastructure.IoC;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public sealed class Program
    {
        private async static Task Main()
        {
            IHostBuilder host = null;

            try
            {
                host = new HostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureServices((hostContext, services) =>
                    {
                        _ = new Bootstrapper(services);
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error injecting services. Message: {ex.Message} Exception: {ex}");
            }
            finally
            {
                Console.WriteLine("Press Ctrl + C to cancel!");
                host.UseConsoleLifetime();
                await host.RunConsoleAsync();
            }
        }
    }
}