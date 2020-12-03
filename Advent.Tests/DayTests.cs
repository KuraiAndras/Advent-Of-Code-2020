using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Advent.Tests
{
    public class DayTests
    {
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper _output;

        public DayTests(ITestOutputHelper output)
        {
            _output = output;
            _mediator = Injector
                .CreateServiceProvider(builder =>
                    builder.AddSerilog(
                        new LoggerConfiguration()
                            .WriteTo.TestOutput(_output)
                            .CreateLogger()))
                .GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Day1()
        {
            // Arrange
            var request = new Day1Command.Day1();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Equal(2020, response.Pair.Sum);
            _output.WriteLine($"First Half: {response.Pair.Product}");

            Assert.Equal(2020, response.Terc.Sum);
            _output.WriteLine($"Second Half: {response.Terc.Product}");
        }
    }
}
