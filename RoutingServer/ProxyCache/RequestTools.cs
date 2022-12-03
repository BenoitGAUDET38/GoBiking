using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache
{
	class RequestTools
	{
        static HttpClient client = new HttpClient();

		/**
         * Get a request from an URL
         */
		public static async Task<string> GetRequest(string url)
        {
			string responseBody = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            return responseBody;
        }
    }
}
