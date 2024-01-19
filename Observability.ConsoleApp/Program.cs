using Observability.ConsoleApp.Constants;
using Observability.ConsoleApp.Helper;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Start Tracing...");

var traceProvider = Sdk.CreateTracerProviderBuilder()
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
                        }).AddConsoleExporter().Build();

var serviceHelper = new ServiceHelper();
await serviceHelper.Work_1();

