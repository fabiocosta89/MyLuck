using Microsoft.Extensions.Hosting;

using MyLuck.Service.Setup;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

// DI
DependencyInjection.Setup(builder);
DependencyInjection.SetupQuartz(builder);


IHost host = builder.Build();

await host.RunAsync();
