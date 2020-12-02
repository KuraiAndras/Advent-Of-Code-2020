using CliFx;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
                .UseTypeActivator(CreateServiceProvider().GetThrowingService)
                .Build()
                .RunAsync();

        public static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            var assemblyToScan = typeof(Program).Assembly;

            services.AddMediatR(assemblyToScan);
            services.AddCommands(assemblyToScan);


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
