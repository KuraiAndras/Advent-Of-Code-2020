using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    public static class InputHelper
    {
        public static string CreateInputFile(int dayNumber) => $@"Inputs\{dayNumber}.txt";

        public static async Task<ImmutableArray<int>> GetInputFileByLines(int dayNumber) =>
            (await File.ReadAllLinesAsync(CreateInputFile(dayNumber)))
                .Select(int.Parse)
                .ToImmutableArray();
    }
}
