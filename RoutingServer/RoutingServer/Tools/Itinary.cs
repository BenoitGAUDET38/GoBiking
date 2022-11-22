using RoutingServer.Tools.JCDecaux;
using RoutingServer.Tools.OpenStreetMap;
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
			string originCityName = await openStreetMapTools.GetCityFromCoordinateAsync(originCoordinate);
			string destinationCityName = await openStreetMapTools.GetCityFromCoordinateAsync(destinationCoordinate);

			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Station closestOriginStation = await jCDecauxTools.GetNearestStationAsync(originCityName, originCoordinate);
			Station closestDestinationStation = await jCDecauxTools.GetNearestStationAsync(destinationCityName, destinationCoordinate);

			// get the 3 directions
			Direction originToStationDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, closestOriginStation.position, false);
			Direction stationToStationDirection = await openStreetMapTools.GetDirectionsAsync(closestOriginStation.position, closestDestinationStation.position, true);
			Direction stationToDestionationDirection = await openStreetMapTools.GetDirectionsAsync(closestDestinationStation.position, destinationCoordinate, false);

			// INFORMATIONS TO RETURN
			// put the 3 direction in a string
			double totalDistance = originToStationDirection.GetFirstSegmentDistance() + stationToStationDirection.GetFirstSegmentDistance() + stationToDestionationDirection.GetFirstSegmentDistance();
			double totalDuration = originToStationDirection.GetFirstSegmentDuration() + stationToStationDirection.GetFirstSegmentDuration() + stationToDestionationDirection.GetFirstSegmentDuration();

			string res = "Total distance : " + totalDistance + "m" + Environment.NewLine
				+ "Total duration : " + totalDuration + "s" + Environment.NewLine
				+ originToStationDirection.ToString() + Environment.NewLine
				+ stationToStationDirection.ToString() + Environment.NewLine
				+ stationToDestionationDirection.ToString() + Environment.NewLine;

			return res;
		}

	}
}
