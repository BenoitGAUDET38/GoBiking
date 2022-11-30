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
		private GenericProxyCache<JCDecauxContracts> _contractsProxyCache;
		private GenericProxyCache<JCDecauxStations> _stationsProxyCache;

		public JCDecauxService() 
		{
			_contractsProxyCache = new GenericProxyCache<JCDecauxContracts>();
		}


		public string GetContracts()
		{
			return _contractsProxyCache.GetT("", null).json;
		}

		public string GetStations(string contract)
		{
			return _stationsProxyCache.GetT(contract, null).json;
		}
	}
}
