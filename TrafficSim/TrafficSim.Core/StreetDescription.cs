using System.Collections.Generic;

namespace TrafficSim.Core
{
    public class StreetDescription
    {
        private readonly List<Intersection> _intersections;


        public int[] HorizontalRoads { get; }
        public int[] VerticalRoads { get; }
        public IEnumerable<Intersection> Intersections => _intersections;

        public StreetDescription(int[] horizontalRoads, int[] verticalRoads)
        {
            HorizontalRoads = horizontalRoads;
            VerticalRoads = verticalRoads;
            _intersections = new List<Intersection>(GetIntersections());
        }

        private IEnumerable<Intersection> GetIntersections()
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
    
