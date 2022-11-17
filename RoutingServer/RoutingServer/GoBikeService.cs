using RoutingServer.Tools;
using RoutingServer.Tools.JCDecaux;
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
			Itinary itinary = new Itinary();
			return await itinary.GetItinaryAsync(originAdress, destinationAdress);
		}
	}
}
