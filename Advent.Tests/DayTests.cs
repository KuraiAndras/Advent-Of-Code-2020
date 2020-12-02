using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Advent.Tests
{
    public class DayTests
    {
        private readonly IMediator _mediator = Program.CreateServiceProvider().GetRequiredService<IMediator>();
        private readonly ITestOutputHelper _output;

        public DayTests(ITestOutputHelper output) => _output = output;

        [Fact]
        public async Task Day1()
        {
            // Arrange
            var request = new Day1Command.Day1();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Equal(2020, response.Pair.Sum);
            Assert.Equal(2020, response.Terc.Sum);
            _output.WriteLine($"First Half: {response.Pair.Product}");
            _output.WriteLine($"Second Half: {response.Terc.Product}");
        }
    }
}
