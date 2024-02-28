using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NotificationFunctions.DemoServices;
using Clean.Architecture.Core.Interfaces;

var host = new HostBuilder()
  .ConfigureFunctionsWebApplication()
  .ConfigureServices(services =>
  {
    services.AddApplicationInsightsTelemetryWorkerService();
    services.ConfigureFunctionsApplicationInsights();
    services.AddScoped<ISmsNotificationService, SmsNotificationService>();
  })
  .Build();

await host.RunAsync();
