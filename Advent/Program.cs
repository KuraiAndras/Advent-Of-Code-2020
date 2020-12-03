using CliFx;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Advent
{
    public static class Program
    {
        public static async Task Main() =>
            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(
                    Injector.CreateServiceProvider(builder =>
                        builder.AddSerilog(
                            new LoggerConfiguration()
                                .WriteTo.Console()
                                .CreateLogger()))
                    .GetThrowingService)
                .Build()
                .RunAsync();
    }

    public static class Injector
    {
        public static IServiceProvider CreateServiceProvider(Action<ILoggingBuilder> loggerBuilder)
        {
            IServiceCollection services = new ServiceCollection();

            var assemblyToScan = typeof(Program).Assembly;

            services.AddMediatR(assemblyToScan);
            services.AddCommands(assemblyToScan);

            services.AddLogging(builder => loggerBuilder(builder));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(StopwatchBehavior<,>));

            return services.BuildServiceProvider();
        }

        private static IServiceCollection AddCommands(this IServiceCollection services, Assembly assembly) =>
            services.AddRange(
                assembly
                    .GetTypes()
                    .Where(t => typeof(ICommand).IsAssignableFrom(t))
                    .Select(t => new ServiceDescriptor(t, t, ServiceLifetime.Transient)));
    }
}
