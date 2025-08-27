using Zeebe.Client;
using Zeebe.Client.Impl.Builder;

namespace Camunda8Training
{
    public class Program
    {
        private static IZeebeClient? _client;
        public static async Task Main(string[] args)
        {
            var program = new Program();
            await program.Run();
        }

        private async Task Run()
        {
            var configuration = BuildConfiguration();
            var clientId = configuration["ZeebeClientConfig:ClientId"];
            var clientSecret = configuration["ZeebeClientConfig:ClientSecret"];
            var contactPoint = configuration["ZeebeClientConfig:ContactPoint"];

            _client = CamundaCloudClientBuilder
            .Builder()
            .UseClientId(clientId)
            .UseClientSecret(clientSecret)
            .UseContactPoint(contactPoint)
            .Build();

            using var signal = new EventWaitHandle(false, EventResetMode.AutoReset);

            var topology = await _client.TopologyRequest().Send();
            Console.WriteLine(topology);

            signal.WaitOne();
        }

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        }
    }
}