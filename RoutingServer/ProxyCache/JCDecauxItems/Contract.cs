using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyCache.JCDecauxItems
{
	class Contract
	{
		public string name { get; set; }
		public string commercial_name { get; set; }
		public List<string> cities { get; set; }
		public string country_code { get; set; }

		public override string ToString()
		{
			return name + " (" + commercial_name + ", " + country_code + ")";
		}
	}
}
