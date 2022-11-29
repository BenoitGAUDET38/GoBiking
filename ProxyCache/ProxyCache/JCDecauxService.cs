using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ProxyCache
{
	public class JCDecauxService : IJCDecauxService
	{
		private GenericProxyCache<JCDecauxItem> _proxyCache;

		public JCDecauxService() 
		{ 
			_proxyCache = new GenericProxyCache<JCDecauxItem>();
		}


		public string GetContracts()
		{
			return _proxyCache.GetT("").json;
		}

		public string GetStations(string contract)
		{
			return _proxyCache.GetT(contract).json;
		}
	}
}
