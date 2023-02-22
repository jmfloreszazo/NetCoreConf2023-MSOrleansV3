using System;
using System.Threading.Tasks;
using GrainInterface;
using Grains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Silo;

public class Program
{
    public static int Main()
    {
        return RunMainAsync().Result;
    }

    private static async Task<int> RunMainAsync()
    {
        try
        {
            var host = await StartSilo();
            Console.WriteLine("\n\n Press Enter to terminate...\n\n");
            Console.ReadLine();
            await host.StopAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return 1;
        }
    }

    private static async Task<ISiloHost> StartSilo()
    {
        var builder = new SiloHostBuilder()
            .UseLocalhostClustering().Configure<Orleans.Configuration.ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "Test";
            })
            .Configure<GrainCollectionOptions>(options =>
            {
                // Set the value of CollectionAge to 3 minutes for all grain
                options.CollectionAge = TimeSpan.FromMinutes(3);
                // Override the value of CollectionAge to 2 minutes for ISampleGrain
                options.ClassSpecificCollectionAge[typeof(ISampleGrain).FullName] = TimeSpan.FromMinutes(2);
            })
            .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(SampleGrain).Assembly).WithReferences())
            .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory())
            .UseDashboard(options => { })
            .ConfigureLogging(logging => logging.AddConsole());
        var host = builder.Build();
        await host.StartAsync();
        return host;
    }
}