using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Core
{
    public class IntersectionCollection : IEnumerable<Intersection>
    {
        private readonly IEnumerable<Intersection> _intersections;

        public IntersectionCollection(IEnumerable<Intersection> intersections)
        {
            _intersections = intersections.ToList();
        }

        public void Tick(Simulator simulator)
        {
            foreach (var intersection in _intersections)
            {
                intersection.Tick(simulator);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
            return _intersections.GetEnumerator();
        }

        public bool WillAllowPhase(Phase intendedPhase)
        {           
            var intersectionLocation = intendedPhase.GetPhasedAdvancedBy(10).Location;

            var relevantIntersection =
                _intersections.SingleOrDefault(intersection => intersection.Location.Equals(intersectionLocation));

            if (relevantIntersection == null)
                return true;

            return relevantIntersection.Lights[intendedPhase.Direction] == LightColor.Green;            
        }
    }
}
