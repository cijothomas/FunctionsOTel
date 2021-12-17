using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

[assembly: FunctionsStartup(typeof(FunctionsOTel.Startup))]
namespace FunctionsOTel
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging((loggingBuilder) =>
            {
                // Configure logging filter here in code
                // or in host.json
                // loggingBuilder.AddFilter<OpenTelemetryLoggerProvider>("*", LogLevel.Warning);
                loggingBuilder.AddOpenTelemetry(
                    (otelOptions) =>
                        {
                            otelOptions.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("testservice"));
                            otelOptions.IncludeFormattedMessage = true;
                            otelOptions.AddConsoleExporter();
                        }
                    );
            }
            );
        }
    }
}
