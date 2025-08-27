# Camunda 8 Training - C# Workers

This is the starter project for the Camunda 8 training for developers. 
Students will learn how to create and implement Zeebe workers that handle different tasks in a BPMN process.

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio Code or Visual Studio
- Access to a Camunda 8 cluster (SaaS or Self-Managed)

## Project Structure

```
camunda-8-training-csharp/
├── Program.cs                 # Main application entry point
├── appsettings.json          # Configuration file (update with your credentials)
├── Services/                 # Business logic services
│   ├── CreditService.cs      # Credit-related business operations
│   └── CreditCardService.cs  # Credit card processing operations
├── Workers/                  # Worker implementations (to be created)

```

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd camunda-8-training-csharp
```

### 2. Configure Camunda 8 Connection

Update the `appsettings.json` file with your Camunda 8 cluster credentials:

```json
{
  "ZeebeClientConfig": {
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET", 
    "ContactPoint": "YOUR_CONTACT_POINT"
  }
}
```

**Where to find these values:**
- **Camunda 8 SaaS**: Go to Console → Clusters → Your Cluster → API tab
- **Self-Managed**: Check your Zeebe gateway configuration

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Project

```bash
dotnet build
```

### 5. Run the Application

```bash
dotnet run
```

## Training Exercises

During the training, you will implement various workers in the `Workers/` folder:

### Example Worker Structure:
```csharp
using Zeebe.Client;
using Zeebe.Client.Api.Responses;

namespace Camunda8Training.Workers
{
    public class ExampleWorker
    {
        private readonly IZeebeClient _client;

        public ExampleWorker(IZeebeClient client)
        {
            _client = client;
            // Register job worker
            _client.NewWorker()
                .JobType("example-task")
                .Handler(HandleJob)
                .MaxJobsActive(5)
                .Name("example-worker")
                .Open();
        }

        private async Task HandleJob(IJobClient jobClient, IJob job)
        {
            // Implement your business logic here
            await jobClient.NewCompleteCommand(job.Key).Send();
        }
    }
}
```

## Available Services

The project includes pre-built services you can use in your workers:

### CreditService
- `GetCustomerCredit(string customerId)`: Get customer's available credit
- `DeductCredit(double credit, double amount)`: Calculate remaining amount after credit deduction

### CreditCardService  
- `ChargeAmount(string cardNumber, string cvc, string expiryDate, double amount)`: Process credit card charge

## Useful Commands

```bash
# Build the project
dotnet build

# Run the application
dotnet run

# Run with specific configuration
dotnet run --environment Development

# Clean build artifacts
dotnet clean

# Restore packages
dotnet restore
```

## Troubleshooting

### Common Issues:

1. **Connection Issues**
   - Verify your Camunda 8 credentials in `appsettings.json`
   - Check network connectivity to your cluster
   - Ensure the contact point URL is correct

2. **Build Errors**
   - Run `dotnet restore` to ensure all packages are installed
   - Check that you're using .NET 8.0 or later

3. **Worker Not Receiving Jobs**
   - Verify the job type matches what's defined in your BPMN process
   - Check that the process is deployed and instances are running
   - Ensure the worker is properly registered

### Getting Help

- Check the [Camunda 8 Documentation](https://docs.camunda.io/)
- Review the [C# Client Documentation](https://github.com/camunda-community-hub/zeebe-client-csharp)
- Ask your trainer during the session

## Next Steps

1. Examine the existing `Program.cs` to understand the basic Zeebe client setup
2. Review the business services in the `Services/` folder
3. Start implementing your first worker based on the training exercises
4. Test your workers with real process instances

Happy coding! 🚀
