using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;

using MyLuck.Functions.Settings;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// DI
DependencyInjection.Setup(builder);

await builder.Build().RunAsync();
