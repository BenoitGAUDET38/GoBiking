using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ProxyCache
{
	internal class GenericProxyCache<T>
	{
		private static double DT_DEFAULT = 120; // default duration of an object in the cache
		private ObjectCache _cache;

		/**
		 * Initialization of the cache
		 */
		public GenericProxyCache()
		{
			_cache = MemoryCache.Default;
		}

		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with the default constructor.
		 * The created object will have a life duration equals to the default dt duration.
		 */
		public T GetT(string cacheItemName)
		{
			object[] args = { };
			return GetT(cacheItemName, args);
		}

		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with his constructor and the given args.
		 * The created object will have a life duration equals to the default dt duration.
		 */
		public T GetT(string cacheItemName, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T) _cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(DT_DEFAULT);

			return SetT(cacheItemName, dt, args);
		}

		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with the default constructor.
		 * The created object will have a life duration equals to the given dt duration.
		 */
		public T GetT(string cacheItemName, double dtSeconds)
		{
			object[] args = { };
			return GetT(cacheItemName, dtSeconds, args);
		}

		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with his constructor and the given args.
		 * The created object will have a life duration equals to the given dt duration.
		 */
		public T GetT(string cacheItemName, double dtSeconds, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			DateTimeOffset dt = DateTimeOffset.UtcNow;
			dt.AddSeconds(dtSeconds);

			return SetT(cacheItemName, dt, args);
		}

		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with the default constructor.
		 * The created object will have a life duration equals with the given DateTimeOffset.
		 */
		public T GetT(string cacheItemName, DateTimeOffset dt)
		{
			object[] args = { };
			return GetT(cacheItemName, dt, args);
		}


		/**
		 * Return the object corresponding with the given cache item name.
		 * If cache do not contains this item, the item will be created with his constructor and the given args.
		 * The created object will have a life duration equals with the given DateTimeOffset.
		 */
		public T GetT(string cacheItemName, DateTimeOffset dt, object[] args)
		{
			if (_cache.Contains(cacheItemName))
				return (T)_cache.Get(cacheItemName);

			return SetT(cacheItemName, dt, args);
		}

		/**
		 * Create an object with his constructor and the given args.
		 * The created object will have a life duration equals with the given DateTimeOffset.
		 */
		private T SetT(string cacheItemName, DateTimeOffset dt, object[] args)
		{
			T res = (T)Activator.CreateInstance(typeof(T), args);
			_cache.Set(cacheItemName, res, dt);

			return res;
		}
	}
}
