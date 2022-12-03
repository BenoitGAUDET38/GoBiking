using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Tools.OpenStreetMap
{
    public class Direction
    {
        private static readonly string WALK_PROFIL = "foot-walking";
        private static readonly string BIKE_PROFIL = "cycling-regular";

		public List<Feature> features { get; set; }
        public Metadata metadata { get; set; }

        public double GetFirstSegmentDuration()
        {
            if (features.Count() == 0 || features[0].properties.segments.Count() == 0)
                return 0;

            return features[0].properties.segments[0].duration;
        }

        public double GetFirstSegmentDistance()
        {
            if (features.Count() == 0 || features[0].properties.segments.Count() == 0)
                return 0;

            return features[0].properties.segments[0].distance;
        }

        public override string ToString()
		{
            string res;
            if (metadata.query.profile == WALK_PROFIL)
                res = "-------------------------------- WALK --------------------------------" + Environment.NewLine;
			else if (metadata.query.profile == BIKE_PROFIL)
				res = "-------------------------------- BIKE --------------------------------" + Environment.NewLine;
			else
			    res = metadata.query.profile + " :" + Environment.NewLine;
            // TODO : check the meaning of the array of features and segments
            if (features.Count() == 0 && features[0].properties.segments.Count() == 0)
                return res;

            Segment segment = features[0].properties.segments[0];
            res += " -> " + "Segment distance : " + segment.distance + "m" + Environment.NewLine; // add total distance
            res += "    " + "Segment duration : " + segment.duration + "s" + Environment.NewLine; // add total duration

            int i = 0;
            foreach (Step step in segment.steps)
            {
                res += "    " + i + " -> " + "Instruction : " + step.instruction + Environment.NewLine;
                res += "    " + "     " + "Name : " + step.name + Environment.NewLine;
                i++;
            }

            return res;
		}
	}
    public class Engine
    {
        public string version { get; set; }
        public DateTime build_date { get; set; }
        public DateTime graph_date { get; set; }
    }

    public class Feature
    {
        public Properties properties { get; set; }
    }

    public class Metadata
    {
        public Query query { get; set; }
    }

    public class Properties
    {
        public List<Segment> segments { get; set; }
        public Summary summary { get; set; }
    }

    public class Query
    {
        public string profile { get; set; }
    }

    public class Segment
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public List<Step> steps { get; set; }
    }

    public class Step
    {
        public double distance { get; set; }
        public double duration { get; set; }
        public string instruction { get; set; }
        public string name { get; set; }
        public List<int> way_points { get; set; }
        public int? exit_number { get; set; }
    }

    public class Summary
    {
        public double distance { get; set; }
        public double duration { get; set; }
    }


}
