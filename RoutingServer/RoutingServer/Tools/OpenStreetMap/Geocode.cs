using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools.OpenStreetMap
{
    public class GeocodeFeature
    {
        public Geometry geometry { get; set; }
        public GeocodeProperties properties { get; set; }
    }

    public class Geometry
    {
        public List<double> coordinates { get; set; }
    }

    public class GeocodeProperties
    {
        public string country { get; set; }
        public string locality { get; set; }
    }

    public class Geocode
    {
        public List<GeocodeFeature> features { get; set; }
    }
}
