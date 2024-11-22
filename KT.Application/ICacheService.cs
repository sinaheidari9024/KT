namespace KT.Application
{
    public interface ICacheService
    {
        bool TryToGetData<T>(string key);
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        bool RemoveData(string key);
    }
}
