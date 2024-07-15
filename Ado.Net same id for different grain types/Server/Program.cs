using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Grains;

namespace Server;

internal class Program
{
    static async Task Main()
    {
        Console.WriteLine("\n\n Starting Silo server \n\n");

        try
        {
            var host = await StartSiloAsync();

            var client = host.Services.GetService<IClusterClient>()!;

            var commonIdForDifferentTypes = Guid.Empty.ToString();

            var personGrain = client.GetGrain<IPersonGrain>(commonIdForDifferentTypes);
            var cityGrain = client.GetGrain<ICityGrain>(commonIdForDifferentTypes);

            await personGrain.SavePersonAsync("Joe");
            await cityGrain.SaveCityAsync("Lisbona");

            await host.StopAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static async Task<IHost> StartSiloAsync()
    {
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder.AddUserSecrets<Program>().Build();
        var connectionString = configuration.GetSection("orleans")["storageConnectionString"] ??
                               throw new InvalidOperationException();

        var builder = Host.CreateDefaultBuilder()
            .UseOrleans(c =>
            {
                c.AddAdoNetGrainStorageAsDefault(options =>
                {
                    options.Invariant = "Npgsql";
                    options.ConnectionString = connectionString;
                });
                c.UseLocalhostClustering()
                    .ConfigureLogging(logging => logging.AddConsole());
            })
            .UseConsoleLifetime();

        var host = builder.Build();
        await host.StartAsync();

        return host;
    }
}
