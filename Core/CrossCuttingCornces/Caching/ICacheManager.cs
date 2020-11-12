using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Core.CrossCuttingCornces.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        object Get(string key);

        object Get(string key, Type type);


        void Add(string key, object data, int duration);

        bool IsAdd(string key);

        void Remove(string key);

        void RemoveByPattern(string key);
    }
}
