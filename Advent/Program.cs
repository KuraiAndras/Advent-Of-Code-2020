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

            var message = GetNumbers(numbers) switch
            {
                null => "Did not find solution",
                ({ } first, { } second) => $"{first}*{second}={first * second}",
            };

            stopwatch.Stop();

            Console.WriteLine(message);
            Console.WriteLine($"Time to complete: {stopwatch.Elapsed}");
        }

        private static (int first, int second)? GetNumbers(ImmutableArray<int> numbers)
        {
            foreach (var number1 in numbers)
            {
                foreach (var number2 in numbers.Where(number2 => number1 + number2 == 2020))
                {
                    return (number1, number2);
                }
            }

            return null;
        }
    }
}
