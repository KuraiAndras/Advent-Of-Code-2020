using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Advent.Tests
{
    public class UnitTest1
    {
        private readonly IMediator _mediator = Program.CreateServiceProvider().GetRequiredService<IMediator>();

        [Fact]
        public async Task Day1()
        {
            // Arrange
            var request = new Day1Command.Day1();

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Equal(2020, response.Sum);
        }
    }
}
