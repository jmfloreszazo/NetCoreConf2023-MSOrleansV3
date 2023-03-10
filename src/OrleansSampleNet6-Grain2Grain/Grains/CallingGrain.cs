using GrainInterface;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Grains;

public class CallingGrain : Grain, ICallingGrain
{
    private int _latest;

    private readonly ILogger _logger;

    public CallingGrain(ILogger<CallingGrain> logger)
    {
        _logger = logger;
    }

    public Task<int> Increment(int number)
    {
        _latest += number;
        _logger.LogInformation($"\n Add increment: '{_latest}'");
        return Task.FromResult(_latest);
    }

    public Task<string> ReturnStringMessage(int number)
    {
        _logger.LogInformation($"\n Return string message: '{_latest}'");
        var grain = GrainFactory.GetGrain<ITestGrain>(1);
        return grain.ResponseTest(Increment(number).Result.ToString());
    }
}