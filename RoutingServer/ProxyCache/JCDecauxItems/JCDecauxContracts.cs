using ProxyCache.JCDecauxItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyCache.JCDecauxItems
{
	internal class JCDecauxContracts
	{
		private static string _JCDecauxApiKey = "cf5d33d4dcc71a48c9441fbcc978ea6604c74177";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";
		private List<Contract> _contracts;

		/**
		 * Initialization of the list of contracts
		 */
		public JCDecauxContracts() {
			_contracts = GetContracts().Result;
		}

		/**
		 * Return the list of contracts from JCDecaux
		 */
		private async Task<List<Contract>> GetContracts()
		{
			string contractsUrl = _baseUrl + "contracts" + "?apiKey=" + _JCDecauxApiKey;
			string contractsJson = await RequestTools.GetRequest(contractsUrl);
			return JsonSerializer.Deserialize<List<Contract>>(contractsJson);
		}

		/**
		 * Return the json string corresponding to the list of stations
		 */
		public string GetContractsJson()
		{
			if (_contracts == null)
				return "No contracts";
			return JsonSerializer.Serialize(_contracts);
		}
	}
}
