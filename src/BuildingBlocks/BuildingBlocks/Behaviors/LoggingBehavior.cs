
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse> 
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse> 
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName} {ResponseName} with payload: {@Request}", typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;

        if(timeTaken.Seconds > 3)
            logger.LogWarning("Handled {RequestName} {ResponseName} in {ElapsedMilliseconds}ms which is longer than expected with payload: {@Request}"
                , typeof(TRequest).Name, typeof(TResponse).Name, timeTaken.TotalMilliseconds, request);



        logger.LogInformation("[END]");

        return response;
    }
}
