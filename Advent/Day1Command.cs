using CliFx;
using CliFx.Attributes;
using MediatR;
using Optional;
using System.Collections.Immutable;
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

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var response = await _mediator.Send(new Day1());

            await response.Pair.Match(
                p => console.Output.WriteLineAsync($"{p.First} * {p.Second} = {p.Product}"),
                () => console.Output.WriteLineAsync("Did not find pair"));

            await response.Terc.Match(
                t => console.Output.WriteLineAsync($"{t.First} * {t.Second} * {t.Third}= {t.Product}"),
                () => console.Output.WriteLineAsync("Did not find terc"));
        }

        public record Day1 : IRequest<Day1.Response>
        {
            public int Sum { get; } = 2020;

            public record Response(Option<NumberPair> Pair, Option<NumberTerc> Terc) : IResponse;

            public interface IDay1Data
            {
                int Product { get; }
                int Sum { get; }
            }

            public record NumberPair(int First, int Second) : IDay1Data
            {
                public int Product { get; } = First * Second;
                public int Sum { get; } = First + Second;
            }

            public record NumberTerc(int First, int Second, int Third) : IDay1Data
            {
                public int Product { get; } = First * Second * Third;
                public int Sum { get; } = First + Second + Third;
            }

            public sealed class Handler : IRequestHandler<Day1, Response>
            {
                public async Task<Response> Handle(Day1 request, CancellationToken cancellationToken)
                {
                    var first = await InputHelper.GetInputFileByLines("1.1", int.Parse);
                    var second = await InputHelper.GetInputFileByLines("1.2", int.Parse);

                    var pair = GetPair(first, request.Sum)?.Some() ?? Option.None<NumberPair>();
                    var terc = GetTerc(second, request.Sum)?.Some() ?? Option.None<NumberTerc>();

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
