using RoutingServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
	class JCDecauxTools
	{
		private static string _JCDecauxApiKey = "cf5d33d4dcc71a48c9441fbcc978ea6604c74177";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + _JCDecauxApiKey;

		/**
		 * Return the list of contracts from JCDecaux
		 */
		public async Task<List<Contract>> GetContracts()
		{
			string contractsUrl = _baseUrl;
			string contractsJson = await RequestTools.GetRequest(contractsUrl);
			return JsonSerializer.Deserialize<List<Contract>>(contractsJson);
		}

		/**
		 * Return the contract matching with the given contract name
		 */
		public async Task<Contract> GetContract(string contractName)
		{
			List<Contract> contracts = await GetContracts();
			foreach (Contract contract in contracts)
			{
				if (contract.name.Equals(contractName))
					return contract;
			}
			return null;
		}
	}
}
