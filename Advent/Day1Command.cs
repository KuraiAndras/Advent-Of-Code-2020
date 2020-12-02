using CliFx;
using CliFx.Attributes;
using MediatR;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Advent
{
    [Command("Day1")]
    public sealed class Day1Command : ICommand
    {
        private readonly IMediator _mediator;

        public Day1Command(IMediator mediator) => _mediator = mediator;

        public int DayNumber { get; } = 1;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var stopwatch = Stopwatch.StartNew();

            var response = await _mediator.Send(new Day1());

            stopwatch.Stop();

            await console.Output.WriteLineAsync($"{response.Numbers.First}*{response.Numbers.Second}={response.Product}");
            await console.Output.WriteLineAsync($"Time to complete: {stopwatch.Elapsed}");
        }

        public record Day1 : IRequest<Day1.Response>
        {
            public int DayNumber { get; } = 1;

            public int Sum { get; } = 2020;

            public record Response(NumberPair Numbers, int Product) : IResponse;

            public record NumberPair(int First, int Second);

            public sealed class Handler : IRequestHandler<Day1, Response>
            {
                public async Task<Response> Handle(Day1 request, CancellationToken cancellationToken)
                {
                    var numbers = await InputHelper.GetInputFileByLines(request.DayNumber);

                    var pair = GetPair(numbers, request.Sum) ?? throw new InvalidOperationException("Did not find");

                    return new Response(pair, pair.First * pair.Second);
                }

                private static NumberPair? GetPair(ImmutableArray<int> numbers, int sum) =>
                    numbers
                        .Select(first => numbers
                            .Select(second => new NumberPair(first, second))
                            .FirstOrDefault(s => s.First + s.Second == sum))
                        .FirstOrDefault(p => p is not null);
            }
        }
    }
}
