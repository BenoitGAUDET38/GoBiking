using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools.JCDecaux
{
	class Station
    {
        public int number { get; set; }
		public string name { get; set; }
		public string adress { get; set; }
        public Coordinate position { get; set; }
		public TotalStands totalStands { get; set; }

		public override string ToString()
		{
			return name + " " + adress;
		}
	}

	class TotalStands
	{
		public Availabilities availabilities { get; set; }
	}

	class Availabilities
	{
		public int bikes { get; set; }
		public int stands { get; set; }
	}
}
