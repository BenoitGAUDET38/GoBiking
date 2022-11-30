using ProxyCache.JCDecauxItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyCache
{
	internal class JCDecauxContracts
	{
		private static HttpClient _client = new HttpClient();
		private static string _JCDecauxApiKey = "cf5d33d4dcc71a48c9441fbcc978ea6604c74177";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";

		public List<Contract> contracts { get; set; }

		public JCDecauxContracts() {
			contracts = GetContracts().Result;
			
		}


		/**
		 * Return the list of contracts from JCDecaux
		 */
		public async Task<List<Contract>> GetContracts()
		{
			string contractsUrl = _baseUrl + "contracts" + "?apiKey=" + _JCDecauxApiKey;
			string contractsJson = await GetRequest(contractsUrl);
			return JsonSerializer.Deserialize<List<Contract>>(contractsJson);
		}

		/**
		 * Return the list of stations matching with the given contract name
		 */
		public async Task<string> GetStations(string contractName)
		{
			string stationsUrl = _baseUrl + "stations" + "?apiKey=" + _JCDecauxApiKey
				+ "&contract=" + contractName;
			return await GetRequest(stationsUrl);
		}

		/**
		 * Get a request from an URL
		 */
		public static async Task<string> GetRequest(string url)
		{
			string responseBody = "";
			try
			{
				HttpResponseMessage response = await _client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				responseBody = await response.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("\nRequest error...");
				Console.WriteLine("Message :{0} ", e.Message);
			}

			return responseBody;
		}
	}
}
