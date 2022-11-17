using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
	public class GoBikeService : IGoBikeService
	{
		public async Task<string> GetItinary(string originAdress, string destinationAdress)
		{
			OpenStreetMapTools tool = new OpenStreetMapTools();
			Coordinate originCoordinate = await tool.GetPositionFromAdressAsync(originAdress);
			Coordinate destinationCoordinate = await tool.GetPositionFromAdressAsync(destinationAdress);

			string res = "Origin : " + originCoordinate.ToString() + Environment.NewLine +
				"Destination : " + destinationCoordinate.ToString();
			
			return res;
		}
	}
}
