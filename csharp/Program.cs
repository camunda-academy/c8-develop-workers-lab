using Zeebe.Client;
using Zeebe.Client.Impl.Builder;

namespace Camunda.Training.CSharp.Services;

public class Program
{

    public static async Task Main(string[] args)
    {
        var configuration = BuildConfiguration();
        var clientId = configuration["ZeebeClientConfig:ClientId"];
        var clientSecret = configuration["ZeebeClientConfig:ClientSecret"];
        var contactPoint = configuration["ZeebeClientConfig:ContactPoint"];

        var client = CamundaCloudClientBuilder
        .Builder()
        .UseClientId(clientId)
        .UseClientSecret(clientSecret)
        .UseContactPoint(contactPoint)
        .Build();

        try
        {
            Console.WriteLine("Connecting to Camunda...");
            var topology = await client.TopologyRequest().Send();
            Console.WriteLine($"Connected! {topology}");
            using var signal = new EventWaitHandle(false, EventResetMode.AutoReset);
            signal.WaitOne();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to Camunda: {ex.Message}");
        }
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();
    }
}
