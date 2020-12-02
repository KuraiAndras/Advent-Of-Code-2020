using System.Threading.Tasks;
using CliFx;

namespace Advent
{
    public static class Program
    {
        public static async Task Main() =>
            await new CliApplicationBuilder()
                .AddCommandsFromThisAssembly()
                .Build()
                .RunAsync();
    }
}
