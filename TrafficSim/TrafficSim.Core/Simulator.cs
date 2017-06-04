using System.Collections.Generic;

namespace TrafficSim.Core
{
    public class Simulator
    {
        private CarCollection _carCollection;
        private IntersectionCollection _intersections;
        private StreetDescription _streets;

        public Simulator(StreetDescription streets, IntersectionCollection intersections, CarCollection carCollection)
        {
            _streets = streets;
            _intersections = intersections;
            _carCollection = carCollection;
        }

        public void MoveNext()
        {
            _carCollection.InjectCars();
            _carCollection.MoveNext(_intersections);
            _intersections.MoveNext();
        }
    }
}
