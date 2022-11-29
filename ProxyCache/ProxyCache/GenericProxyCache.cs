using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ProxyCache
{
	internal class GenericProxyCache<T> where T : new()
	{
		static double DT_DEFAULT = 60;

		private ObjectCache _cache;

		public GenericProxyCache()
		{
			_cache = MemoryCache.Default;
		}

		public T GetT(string cacheItemName)
		{
			if (_cache.Contains(cacheItemName))
				return (T) _cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(DT_DEFAULT);

			return SetT(cacheItemName, dt);
		}

		public T GetT(string cacheItemName, double dtSeconds)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(dtSeconds);

			return SetT(cacheItemName, dt);
		}

		public T GetT(string cacheItemName, DateTimeOffset dt)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			return SetT(cacheItemName, dt);
		}

		private T SetT(string cacheItemName, DateTimeOffset dt)
		{
			T res;
			if (cacheItemName.Equals(""))
				res = new T();
			else
			{
				object[] args = { cacheItemName };
				res = (T)Activator.CreateInstance(typeof(T), args);
			}
			_cache.Set(cacheItemName, res, dt);

			return res;
		}
	}
}
