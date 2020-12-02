using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    public static class InputHelper
    {
        public static string CreateInputFile(string inputName) => $@"Inputs\{inputName}.txt";
        public static async Task<(ImmutableArray<int> first, ImmutableArray<int> second)> Get1()
        {
            var first = await GetInputFileByLines("1.1");
            var second = await GetInputFileByLines("1.2");

            return (first, second);
        }

        public static async Task<ImmutableArray<int>> GetInputFileByLines(string inputName) =>
            (await File.ReadAllLinesAsync(CreateInputFile(inputName)))
                .Select(int.Parse)
                .ToImmutableArray();
    }
}
