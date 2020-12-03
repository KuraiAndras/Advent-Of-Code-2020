using CliFx;
using CliFx.Attributes;
using MediatR;
using System.Threading.Tasks;

namespace Advent
{
    [Command("Day2")]
    public sealed class Day2Command : ICommand
    {
        private readonly ISender _sender;

        public Day2Command(ISender sender) => _sender = sender;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var response = await _sender.Send(new Day2());

            await console.Output.WriteLineAsync($"Found: {response.ValidCount}");
        }
    }
}
