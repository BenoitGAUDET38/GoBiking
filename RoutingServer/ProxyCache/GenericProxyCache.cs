using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ProxyCache
{
	internal class GenericProxyCache<T>
	{
		static double DT_DEFAULT = 60;

		private ObjectCache _cache;

		public GenericProxyCache()
		{
			_cache = MemoryCache.Default;
		}

		// 
		public T GetT(string cacheItemName)
		{
			object[] args = { };
			return GetT(cacheItemName, args);
		}

		public T GetT(string cacheItemName, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T) _cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(DT_DEFAULT);

			return SetT(cacheItemName, dt, args);
		}

		public T GetT(string cacheItemName, double dtSeconds, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(dtSeconds);

			return SetT(cacheItemName, dt, args);
		}

		public T GetT(string cacheItemName, DateTimeOffset dt, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			return SetT(cacheItemName, dt, args);
		}

		private T SetT(string cacheItemName, DateTimeOffset dt, object[] args)
		{
			T res = (T)Activator.CreateInstance(typeof(T), args);
			_cache.Set(cacheItemName, res, dt);

			return res;
		}
	}
}
