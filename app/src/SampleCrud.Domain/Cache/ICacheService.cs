using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCrud.Domain.Cache
{
    public interface ICacheService
    {
        Task<T?> Get<T>(string key);
        Task Set<T>(string key, T value, TimeSpan? expiration = null);
    }
}