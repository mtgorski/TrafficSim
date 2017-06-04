namespace TrafficSim.Core
{
    public class Simulator
    {
        private readonly CarCollection _carCollection;
        private readonly IntersectionCollection _intersections;

        public Simulator(IntersectionCollection intersections, CarCollection carCollection)
        {
            _intersections = intersections;
            _carCollection = carCollection;
        }

        public void Tick()
        {
            _carCollection.Tick(_intersections);
            _intersections.Tick();
        }
    }
}
