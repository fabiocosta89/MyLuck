using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MyLuck.Service.Features.High5;
using MyLuck.Service.Features.Lotto;
using MyLuck.Service.Setup;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

// DI
DependencyInjection.Setup(builder);

IHost host = builder.Build();

// High 5
var high5 = host.Services.GetRequiredService<IHigh5Service>();
await high5.RunAsync();

// Lotto
var lotto = host.Services.GetRequiredService<ILottoService>();
await lotto.RunAsync();

