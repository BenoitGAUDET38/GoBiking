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
        private string _baseUrl = "https://api.openrouteservice.org/geocode/search?api_key=" + _openRouteMapApiKey;

        /**
         * Return the 
         */
        public async Task<Coordinate> GetPositionFromAdressAsync(string adress)
		{
            string positionUrl = _baseUrl + "&text=" + adress;
            string positionJson = await RequestTools.GetRequest(positionUrl);
            Geocode geocodes = JsonSerializer.Deserialize<Geocode>(positionJson);


            Coordinate position = null;
            if (geocodes.features.Count > 0)
			{
                double latitude = geocodes.features[0].geometry.coordinates[1];
                double longitude = geocodes.features[0].geometry.coordinates[0];
                position = new Coordinate(latitude, longitude);
            }

            return position;
		}
    }
}
