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

			// get the closests stations
			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Station closestOriginStation = await jCDecauxTools.GetNearestStationWithAvailableBikeAsync(originCityName, originCoordinate);
			Station closestDestinationStation = await jCDecauxTools.GetNearestStationWithAvailableStandAsync(destinationCityName, destinationCoordinate);

			// get the 3 directions corresponding to the bike traject
			Direction originToStationDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, closestOriginStation.position, false);
			Direction stationToStationDirection = await openStreetMapTools.GetDirectionsAsync(closestOriginStation.position, closestDestinationStation.position, true);
			Direction stationToDestionationDirection = await openStreetMapTools.GetDirectionsAsync(closestDestinationStation.position, destinationCoordinate, false);

			// get direction corresponding to the walk traject
			Direction walkDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, destinationCoordinate, false);

			double totalDurationWalking = walkDirection.GetFirstSegmentDuration();
			double totalDurationWithBike = originToStationDirection.GetFirstSegmentDuration() + stationToStationDirection.GetFirstSegmentDuration() + stationToDestionationDirection.GetFirstSegmentDuration();

			if (totalDurationWalking < totalDurationWithBike)
			{
				double totalDistance = walkDirection.GetFirstSegmentDuration();

				return "Total distance : " + totalDistance + "m" + Environment.NewLine
					+ "Total duration : " + totalDurationWalking + "s" + Environment.NewLine
					+ walkDirection.ToString();
			}
			else
			{
				// INFORMATIONS TO RETURN
				// put the 3 direction in a string
				double totalDistance = originToStationDirection.GetFirstSegmentDistance() + stationToStationDirection.GetFirstSegmentDistance() + stationToDestionationDirection.GetFirstSegmentDistance();

				return "Total distance : " + totalDistance + "m" + Environment.NewLine
					+ "Total duration : " + totalDurationWithBike + "s" + Environment.NewLine
					+ originToStationDirection.ToString() + Environment.NewLine
					+ stationToStationDirection.ToString() + Environment.NewLine
					+ stationToDestionationDirection.ToString();
			}
		}



	}



}
