

using Observability.ConsoleApp.Constants;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var traceProvider = Sdk.CreateTracerProviderBuilder()
                        .ConfigureResource(conf =>
                        {
                            conf.AddService(OpenTelemetryConstants.ServiceName,
                                            OpenTelemetryConstants.ServiceVersion).AddAttributes(new List<KeyValuePair<string, object>>()
                                            {
                                                new KeyValuePair<string, object>("host.machineName",Environment.MachineName),
                                                new KeyValuePair<string, object>("host.os",Environment.OSVersion.VersionString),
                                                new KeyValuePair<string, object>("host.environment","dev"),
                                                new KeyValuePair<string, object>("dotnet.version",Environment.Version.ToString())
                                            });
                        }).Build();

Console.WriteLine("Hello, World!");
