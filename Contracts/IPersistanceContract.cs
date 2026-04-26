namespace GateWay;

public interface IPersistanceContract
{
    Task<T> getCachedData<T>(string key, Func<Task<T>> func);
}
