using Camunda.Orchestration.Sdk;

namespace Camunda.Training.CSharp;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var client = CreateClient();

        Console.WriteLine("Connecting to Camunda...");
        var topology = await client.GetTopologyAsync();
        Console.WriteLine($"Connected! Brokers: {topology.Brokers?.Count ?? 0}");

        // TODO: Register your job workers here

        Console.WriteLine("Workers started. Press Ctrl+C to exit.");

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        await client.RunWorkersAsync(ct: cts.Token);
    }

    /// <summary>
    /// Option 1 (recommended): zero-config via CAMUNDA_* environment variables.
    ///   export CAMUNDA_REST_ADDRESS=https://YOUR_REGION.zeebe.camunda.io/YOUR_CLUSTER_ID
    ///   export CAMUNDA_AUTH_STRATEGY=OAUTH
    ///   export CAMUNDA_CLIENT_ID=...
    ///   export CAMUNDA_CLIENT_SECRET=...
    ///   export CAMUNDA_OAUTH_URL=https://login.cloud.camunda.io/oauth/token
    ///   export CAMUNDA_TOKEN_AUDIENCE=zeebe.camunda.io
    ///
    /// Option 2 (fallback): "Camunda" section in appsettings.json.
    /// </summary>
    private static CamundaClient CreateClient()
    {
        if (HasCamundaEnvVars())
        {
            Console.WriteLine("Using CAMUNDA_* environment variables...");
            return CamundaClient.Create();
        }

        Console.WriteLine("No CAMUNDA_* env vars found; reading appsettings.json...");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        return CamundaClient.Create(new CamundaOptions
        {
            Configuration = configuration.GetSection("Camunda"),
        });
    }

    private static bool HasCamundaEnvVars() =>
        Environment.GetEnvironmentVariables()
            .Keys
            .Cast<string>()
            .Any(k => k.StartsWith("CAMUNDA_", StringComparison.OrdinalIgnoreCase));
}
