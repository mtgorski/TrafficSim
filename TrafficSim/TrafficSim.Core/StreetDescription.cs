using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace TrafficSim.Core
{
    public class StreetDescription
    {


        public int[] HorizontalRoads { get; }
        public int[] VerticalRoads { get; }

        public StreetDescription(int[] horizontalRoads, int[] verticalRoads)
        {
            HorizontalRoads = horizontalRoads;
            VerticalRoads = verticalRoads;
        }

        public IEnumerable<Intersection> GetIntersections()
        {
            foreach (var verticalRoad in VerticalRoads)
            {
                foreach (var horizontalRoad in HorizontalRoads)
                {
                    yield return new Intersection(verticalRoad, horizontalRoad);
                }
            }
        }
    }
}
    
