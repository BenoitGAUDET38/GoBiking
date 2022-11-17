using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
	class Coordinate
	{
		public double latitude { get; set; }
		public double longitude { get; set; }

		public Coordinate(double latitude, double longitude)
		{
			this.latitude = latitude;
			this.longitude = longitude;
		}

		/**
		 * Return the latitude value with a '.' instead of a ',' to write the number in a correct format.
		 */
		public string GetLatitudeString()
		{
			return latitude.ToString().Replace(',', '.');
		}

		/**
		 * Return the longitude value with a '.' instead of a ',' to write the number in a correct format.
		 */
		public string GetLongitudeString()
		{
			return longitude.ToString().Replace(',', '.');
		}

		public override string ToString()
		{
			return GetLatitudeString() + ", " + GetLongitudeString();
		}
	}
}
