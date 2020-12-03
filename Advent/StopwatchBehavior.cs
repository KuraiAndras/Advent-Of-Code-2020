using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Advent
{
    public sealed class StopwatchBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<StopwatchBehavior<TRequest, TResponse>> _logger;

        public StopwatchBehavior(ILogger<StopwatchBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopwatch = Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation("Time to complete {Ellapsed}", stopwatch.Elapsed);

            return response;
        }
    }
}
