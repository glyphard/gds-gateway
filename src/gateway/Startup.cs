using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using GDS.Gateway.Adapters;
using GDS.Gateway.Adapters.DataAccess;

[assembly: FunctionsStartup(typeof(GDS.Gateway.Startup))]
namespace GDS.Gateway
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IGatewayAdapterRouter, GatewayAdapterRouter>();
            
            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
            
        }
    }
}