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
	internal class JCDecauxStations
	{
		private static string _JCDecauxApiKey = "cf5d33d4dcc71a48c9441fbcc978ea6604c74177";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";
		private List<Station> _stations;

		/**
		 * Initialization of the list of stations
		 */
		public JCDecauxStations(string contractName)
		{
			_stations = GetStationsAsync(contractName).Result;
		}

		private async Task<string> GetStationsStringAsync(string contractName)
		{
			string stationsUrl = _baseUrl + "stations" + "?apiKey=" + _JCDecauxApiKey
				+ "&contract=" + contractName;
			string stationsJson = await RequestTools.GetRequest(stationsUrl);
			return stationsJson;
		}

		/**
		 * Return the list of stations matching with the given contract name
		 */
		private async Task<List<Station>> GetStationsAsync(string contractName)
		{
			string stationsUrl = _baseUrl + "stations" + "?apiKey=" + _JCDecauxApiKey
				+ "&contract=" + contractName;
			string stationsJson = await RequestTools.GetRequest(stationsUrl);
			return JsonSerializer.Deserialize<List<Station>>(stationsJson);
		}

		/**
		 * Return the json string corresponding to the list of stations
		 */
		public string GetStationsJson() 
		{
			if (_stations == null)
				return "No stations";
			return JsonSerializer.Serialize(_stations);
		}
	}
}
