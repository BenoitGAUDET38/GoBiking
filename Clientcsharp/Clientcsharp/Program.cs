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
			GoBikeService.GoBikeServiceClient goBikeServiceClient = new GoBikeService.GoBikeServiceClient();

			string res = await goBikeServiceClient.GetItinaryAsync("3 Place de la République, Mulhouse", "BOULEVARD CHARLES STOESSEL, Mulhouse");
			Console.WriteLine(res);
			Console.ReadLine();
		}
	}
}
