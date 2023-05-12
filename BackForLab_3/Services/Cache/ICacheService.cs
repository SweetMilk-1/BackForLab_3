namespace BackForLab_3.Services.Cache
{
    public interface ICacheService
    {
        Task<T?> Get<T>(string key);
        Task<T> Set<T>(string key, T obj);
    }
}
