using RoutingServer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RoutingServer
{
    /**
     * Class to get informations from the OpenRouteMap API
     */
	class OpenStreetMapTools
	{
        private static string _openRouteMapApiKey = "5b3ce3597851110001cf624877fc518d67ed45a4a2abc0dfb0851937";
        private string _baseUrl = "https://api.openrouteservice.org/";

        /**
         * Return the coordinate refering to the given adress
         */
        public async Task<Coordinate> GetCoordinateFromAdressAsync(string adress)
		{
            string positionUrl = _baseUrl + "geocode/search" + "?api_key=" + _openRouteMapApiKey + "&text=" + adress;
            string positionJson = await RequestTools.GetRequest(positionUrl);
            Geocode geocode = JsonSerializer.Deserialize<Geocode>(positionJson);


            Coordinate position = null;
            if (geocode.features.Count > 0)
			{
                double latitude = geocode.features[0].geometry.coordinates[1];
                double longitude = geocode.features[0].geometry.coordinates[0];
                position = new Coordinate(latitude, longitude);
            }

            return position;
		}

        /**
         * Return the city name refering to the given adress
         */
        public async Task<string> GetCityFromCoordinateAsync(Coordinate coordinate)
		{
            string geocodeUrl = _baseUrl + "geocode/reverse" + "?api_key=" + _openRouteMapApiKey
                + "&point.lat=" + coordinate.GetLatitudeString()
                + "&point.lon=" + coordinate.GetLongitudeString();
            string geocodeJson = await RequestTools.GetRequest(geocodeUrl);
            Geocode geocode = JsonSerializer.Deserialize<Geocode>(geocodeJson);

            string cityName = "";
			try
			{
                cityName = geocode.features[0].properties.locality;
            }
			catch (Exception)
			{
                Console.WriteLine("Unreachable city name");
			}
            return cityName;
        }

        /**
         * Return a string corresponding to the directions to take between 2 coordinates in JSON format (WITH BIKE MOVE SPEED)
         */
        public async Task<string> GetDirectionsAsync(Coordinate startCoordinate, Coordinate endCoordinate)
		{
            string directionsUrl = _baseUrl + "v2/directions/cycling-regular" + "?api_key=" + _openRouteMapApiKey
                + "&start=" + startCoordinate.GetLongitudeString() + "," + startCoordinate.GetLatitudeString()
                + "&end=" + endCoordinate.GetLongitudeString() + "," + endCoordinate.GetLatitudeString();
            string directionsJson = await RequestTools.GetRequest(directionsUrl);
            return directionsJson;
        }
    }
}
