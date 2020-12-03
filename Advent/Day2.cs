using MediatR;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Advent
{
    public record Day2 : IRequest<Day2.Response>
    {
        public record Response(int ValidCount);

        public record Input(Range characterCount, char character, string password);

        public sealed class Handler : IRequestHandler<Day2, Response>
        {
            public async Task<Response> Handle(Day2 request, CancellationToken cancellationToken)
            {
                var input = await InputHelper.GetInputFileByLines("2", Parse);

                var count = input.Count(Count);

                return new Response(count);
            }

            private bool Count(Input input)
            {
                var charCount = input.password.Count(c => c == input.character);

                return charCount >= input.characterCount.Start.Value && charCount <= input.characterCount.End.Value;
            }

            private static Input Parse(string line)
            {
                var parts = line.Split(' ');

                var rangeString = parts[0];
                var min = int.Parse(Regex.Match(rangeString, @"\d+-").Value.TrimEnd('-'));
                var max = int.Parse(Regex.Match(rangeString, @"-\d+").Value.TrimStart('-'));

                var characterCount = new Range(min, max);

                var character = parts[1][0];

                var password = parts[2];

                return new Input(characterCount, character, password);
            }
        }
    }
}
