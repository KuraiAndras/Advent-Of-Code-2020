using FluentAssertions;
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
            response.Pair.HasValue.Should().BeTrue();
            response.Pair.MatchSome(CheckResponse);

            response.Terc.HasValue.Should().BeTrue();
            response.Terc.MatchSome(CheckResponse);

            void CheckResponse(Day1Command.Day1.IDay1Data data)
            {
                data.Sum.Should().Be(2020);
                _output.WriteLine($"Second Half: {data.Product}");
            }
        }
    }
}
