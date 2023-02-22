using Orleans;

namespace GrainInterface;

public interface ISampleGrain : IGrainWithStringKey
{
    Task<string> Response(string message);
}