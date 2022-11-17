using RoutingServer.Tools.JCDecaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools
{
	class Itinary
	{
		public async Task<string> GetItinaryAsync(string originAdress, string destinationAdress)
		{
			OpenStreetMapTools openStreetMapTools = new OpenStreetMapTools();
			Coordinate originCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(originAdress);
			Coordinate destinationCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(destinationAdress);
			string cityName = await openStreetMapTools.GetCityFromCoordinateAsync(originCoordinate);

			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Station closestOriginStation = await jCDecauxTools.GetNearestStationAsync(cityName, originCoordinate);
			Station closestDestinationStation = await jCDecauxTools.GetNearestStationAsync(cityName, destinationCoordinate);

			string res = "Origin closest station : " + closestOriginStation.ToString() + Environment.NewLine +
				"Destination closest station : " + closestDestinationStation.ToString();

			return res;
		}

	}
}
