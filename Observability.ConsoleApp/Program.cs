using Observability.ConsoleApp.Constants;
using Observability.ConsoleApp.Helper;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Start Tracing...");

using var traceProvider = Sdk.CreateTracerProviderBuilder()
                        .AddSource(OpenTelemetryConstants.ActivitySourceName)
                        .ConfigureResource(conf =>
                        {
                            conf.AddService(serviceName: OpenTelemetryConstants.ServiceName,
                                            serviceVersion: OpenTelemetryConstants.ServiceVersion).AddAttributes(new List<KeyValuePair<string, object>>()
                                            {
                                                new KeyValuePair<string, object>("host.machineName",Environment.MachineName),
                                                new KeyValuePair<string, object>("host.os",Environment.OSVersion.VersionString),
                                                new KeyValuePair<string, object>("host.environment","dev"),
                                                new KeyValuePair<string, object>("dotnet.version",Environment.Version.ToString())
                                            });
                        }).AddConsoleExporter().AddOtlpExporter().AddZipkinExporter(zipkinOptions =>
                        {
                            zipkinOptions.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                        }).Build();

var serviceHelper = new ServiceHelper();
await serviceHelper.Work_1();

