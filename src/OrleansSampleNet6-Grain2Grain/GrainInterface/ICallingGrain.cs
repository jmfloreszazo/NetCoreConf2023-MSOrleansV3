using Orleans;

namespace GrainInterface;

public interface ICallingGrain : IGrainWithStringKey
{
    Task<int> Increment(int number);
    Task<string> ReturnStringMessage(int number);
}