using RoutingServer.Tools;
using RoutingServer.Tools.JCDecaux;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer
{
	class JCDecauxTools
	{
		private static string _JCDecauxApiKey = "cf5d33d4dcc71a48c9441fbcc978ea6604c74177";
		private string _baseUrl = "https://api.jcdecaux.com/vls/v3/";

		/**
		 * Return the list of contracts from JCDecaux
		 */
		public async Task<List<Contract>> GetContracts()
		{
			string contractsUrl = _baseUrl + "contracts" + "?apiKey=" + _JCDecauxApiKey;
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

		/**
		 * Return the list of stations matching with the given contract name
		 */
		public async Task<List<Station>> GetStations(string contractName)
		{
			string stationsUrl = _baseUrl + "stations" + "?apiKey=" + _JCDecauxApiKey
				+ "&contract=" + contractName;
			string stationsJson = await RequestTools.GetRequest(stationsUrl);
			return JsonSerializer.Deserialize<List<Station>>(stationsJson);
		}

		/**
		 * Get the closest station from the given coordinate matching with the given contract name
		 */
		public async Task<Station> GetNearestStationAsync(string contractName, Coordinate currentCoordinate)
		{
			List<Station> stations = await GetStations(contractName);

			// Get the closest station from the given coordinate
			GeoCoordinate currentCoordianteGeo = new GeoCoordinate(currentCoordinate.latitude, currentCoordinate.longitude);
			double distanceClosestStation = -1;
			Station closestStation = null;
			for (int i = 0; i < stations.Count; i++)
			{
				GeoCoordinate tempCoord = new GeoCoordinate(stations[i].position.latitude, stations[i].position.longitude);
				double tempDistance = currentCoordianteGeo.GetDistanceTo(tempCoord);

				if (closestStation == null || tempDistance < distanceClosestStation)
				{
					closestStation = stations[i];
					distanceClosestStation = tempDistance;
				}
			}

			return closestStation;
		}
	}
}
