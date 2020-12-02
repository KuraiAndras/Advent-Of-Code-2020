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

            await console.Output.WriteLineAsync($"{response.Pair.First} * {response.Pair.Second} = {response.Pair.Product}");
            await console.Output.WriteLineAsync($"{response.Terc.First} * {response.Terc.Second} * {response.Terc.Third} = {response.Terc.Product}");
            await console.Output.WriteLineAsync($"Time to complete: {stopwatch.Elapsed}");
        }

        public record Day1 : IRequest<Day1.Response>
        {
            public int Sum { get; } = 2020;

            public record Response(NumberPair Pair, NumberTerc Terc) : IResponse;

            public record NumberPair(int First, int Second)
            {
                public int Product { get; } = First * Second;
                public int Sum { get; } = First + Second;
            }

            public record NumberTerc(int First, int Second, int Third)
            {
                public int Product { get; } = First * Second * Third;
                public int Sum { get; } = First + Second + Third;
            }

            public sealed class Handler : IRequestHandler<Day1, Response>
            {
                public async Task<Response> Handle(Day1 request, CancellationToken cancellationToken)
                {
                    var (first, second) = await InputHelper.Get1();

                    var pair = GetPair(first, request.Sum) ?? throw new InvalidOperationException("Did not find pair");
                    var terc = GetTerc(second, request.Sum) ?? throw new InvalidOperationException("Did not find terc");

                    return new Response(pair, terc);
                }

                private static NumberPair? GetPair(ImmutableArray<int> numbers, int sum) =>
                    numbers
                        .Select(first => numbers
                            .Select(second => new NumberPair(first, second))
                            .FirstOrDefault(p => p.First + p.Second == sum))
                        .FirstOrDefault(p => p is not null);

                private static NumberTerc? GetTerc(ImmutableArray<int> numbers, int sum) =>
                    numbers
                        .Select(first => numbers
                            .Select(second => numbers
                                .Select(third => new NumberTerc(first, second, third))
                                .FirstOrDefault(t => t.First + t.Second + t.Third == sum))
                            .FirstOrDefault(t => t is not null))
                        .FirstOrDefault(t => t is not null);
            }
        }
    }
}
