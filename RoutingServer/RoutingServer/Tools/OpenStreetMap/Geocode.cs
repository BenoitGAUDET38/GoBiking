using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools
{
    public class Feature
    {
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }

    public class Geometry
    {
        public List<double> coordinates { get; set; }
    }

    public class Properties
    {
        public string country { get; set; }
        public string locality { get; set; }
    }

    public class Geocode
    {
        public List<Feature> features { get; set; }
    }
}
