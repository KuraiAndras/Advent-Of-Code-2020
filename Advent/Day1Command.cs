using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;

namespace Advent
{
    [Command]
    public sealed class Day1Command : ICommand
    {
        public int DayNumber { get; } = 1;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var numbers = await InputHelper.GetInputFileByLines(DayNumber);

            var stopwatch = Stopwatch.StartNew();

            var message = GetPair(numbers, 2020) switch
            {
                null => "Did not find solution",
                var (first, second) => $"{first}*{second}={first * second}",
            };

            stopwatch.Stop();

            await console.Output.WriteLineAsync(message);
            await console.Output.WriteLineAsync($"Time to complete: {stopwatch.Elapsed}");
        }

        private static NumberPair? GetPair(ImmutableArray<int> numbers, int sum) =>
            numbers
                .Select(first => numbers
                    .Select(second => new NumberPair(first, second))
                    .FirstOrDefault(s => s.First + s.Second == sum))
                .FirstOrDefault(p => p is not null);

        private record NumberPair(int First, int Second);
    }
}
