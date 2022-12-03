using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientcsharp
{
	class Program
	{
		static async Task Main(string[] args)
		{

			//ProxyCacheService.JCDecauxServiceClient jc = new ProxyCacheService.JCDecauxServiceClient();
			//string res = await jc.GetStationsAsync("Lyon");


			GoBikeService.GoBikeServiceClient goBikeServiceClient = new GoBikeService.GoBikeServiceClient();
			//string res = await goBikeServiceClient.GetItinaryAsync("3 Place de la République, Mulhouse", "BOULEVARD CHARLES STOESSEL, Mulhouse");
			//string res = await goBikeServiceClient.GetItinaryAsync("3 Place de la République, Mulhouse", "23 Place de la République, Mulhouse");
			string res = await goBikeServiceClient.GetItinaryAsync("87 Rue Greuze, 69100 Villeurbanne", "21 Rue Flachet, 69100 Villeurbanne");
			Console.WriteLine(res);
			Console.ReadLine();
		}
	}
}
