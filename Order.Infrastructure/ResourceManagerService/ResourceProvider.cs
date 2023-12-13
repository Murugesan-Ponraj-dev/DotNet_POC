using Order.Domain.Common;
using Order.Domain.Services;
using System.Resources;

namespace Order.Infrastructure.ResourceManagerService
{
    public class ResourceProvider : IResourceManager
    {
        /// <summary>
        /// Retrieve the value from resourcefile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceName"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetResourceValue<T>(string resourceName, string keyName) where T : class
        {
            ResourceManager resourceManager = new(resourceName, typeof(T).Assembly);
            string? value = resourceManager?.GetString(keyName);
            return (value is null) ? ResourceKeys.ResourceManagerFileError : value;

        }
    }
}
