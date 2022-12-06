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
			if (originAdress.Length == 0 || destinationAdress.Length == 0)
				return "No adress found.";

			OpenStreetMapTools openStreetMapTools = new OpenStreetMapTools();

			// get the coordinates corresponding with the givens adresses and manage exceptions
			Coordinate originCoordinate;
			Coordinate destinationCoordinate;
			try
			{
				originCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(originAdress);
				destinationCoordinate = await openStreetMapTools.GetCoordinateFromAdressAsync(destinationAdress);
			}
			catch (IncorrectAdressException)
			{
				return "Origine or destination adress hasn't been found.";
			}

			// get the cities names corresponding with the coordinates
			string origineCityName = await openStreetMapTools.GetCityFromCoordinateAsync(originCoordinate);
			string destinationCityName = await openStreetMapTools.GetCityFromCoordinateAsync(destinationCoordinate);

			// get the contract name corresponding with the itinary cities and manage exceptions
			string contractName;
			try
			{
				contractName = await GetContractFromItinaryCities(origineCityName, destinationCityName);
			}
			catch (MultipleCitiesItinaryException)
			{
				return "The itinary must be contained within a single contract area.";
			}
			catch (ContractNotCoveredException)
			{
				return "The origine and destination adresses must be contained within a single contract area.";
			}

			// get the closests stations
			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Station closestOriginStation = await jCDecauxTools.GetNearestStationWithAvailableBikeAsync(contractName, originCoordinate);
			Station closestDestinationStation = await jCDecauxTools.GetNearestStationWithAvailableStandAsync(contractName, destinationCoordinate);

			// get the 3 directions corresponding to the bike traject
			Direction originToStationDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, closestOriginStation.position, false);
			Direction stationToStationDirection = await openStreetMapTools.GetDirectionsAsync(closestOriginStation.position, closestDestinationStation.position, true);
			Direction stationToDestionationDirection = await openStreetMapTools.GetDirectionsAsync(closestDestinationStation.position, destinationCoordinate, false);

			// get direction corresponding to the walk traject
			Direction walkDirection = await openStreetMapTools.GetDirectionsAsync(originCoordinate, destinationCoordinate, false);

			// calculate the total durations for the 2 itinaries
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

		/**
		 * Return the contract corresponding with the given origine and destination city names.
		 * -> if origine and destination contracts are different : Throw MultipleCitiesItinaryException
		 * -> if origine or destination arn't in a covered contract : Throw ContractNotCoveredException
		 */
		private async Task<string> GetContractFromItinaryCities(string origineCityName, string destinationCityName)
		{
			JCDecauxTools jCDecauxTools = new JCDecauxTools();
			Contract origineContract = await jCDecauxTools.GetContract(origineCityName);
			Contract destinationContract = await jCDecauxTools.GetContract(destinationCityName);

			if (origineContract.name.Equals(destinationContract.name))
				return origineContract.name;

			throw new MultipleCitiesItinaryException();
		}



	}



}
