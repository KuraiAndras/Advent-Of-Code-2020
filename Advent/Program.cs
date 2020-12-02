using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var filePath = args[0];

            var numbers = (await File.ReadAllLinesAsync(filePath))
                .Select(int.Parse)
                .ToImmutableArray();

            var stopwatch = Stopwatch.StartNew();

            var message = GetPair(numbers, 2020) switch
            {
                null => "Did not find solution",
                var (first, second) => $"{first}*{second}={first * second}",
            };

            stopwatch.Stop();

            Console.WriteLine(message);
            Console.WriteLine($"Time to complete: {stopwatch.Elapsed}");
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
