using CliFx;
using CliFx.Attributes;
using MediatR;
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
    }
}
