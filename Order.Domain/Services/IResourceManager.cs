namespace Order.Domain.Services;
public interface IResourceManager
{
    string GetResourceValue<T>(string resourceName, string keyName) where T : class;
}

