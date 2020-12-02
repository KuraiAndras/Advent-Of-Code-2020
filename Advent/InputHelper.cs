using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    public static class InputHelper
    {
        public static string CreateInputFile(string inputName) => $@"Inputs\{inputName}.txt";

        public static async Task<ImmutableArray<T>> GetInputFileByLines<T>(string inputName, Func<string, T> parser) =>
            (await File.ReadAllLinesAsync(CreateInputFile(inputName)))
                .Select(parser)
                .ToImmutableArray();
    }
}
