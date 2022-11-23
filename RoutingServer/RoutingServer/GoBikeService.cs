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
		public string GetItinary(string originAdress, string destinationAdress)
		{
			try
			{
				Itinary itinary = new Itinary();
				return itinary.GetItinaryAsync(originAdress, destinationAdress).Result;
			}
			catch (Exception)
			{
				return "Des problèmes ont été rencontré par le serveur...";
			}
		}
	}
}
