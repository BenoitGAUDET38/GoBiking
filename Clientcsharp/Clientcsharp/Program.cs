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

			string res = await goBikeServiceClient.GetItinaryAsync("Sophia antipolis", "2400 Rte des Dolines, 06560 Valbonne");
			Console.WriteLine(res);
			Console.ReadLine();
		}
	}
}
