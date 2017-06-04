namespace TrafficSim.Core
{
    public class Car
    {
        private int _timesWaiting;
        private int _timesMoving;
        public Car(int x, int y, Direction direction)
        {
            Phase = new Phase {Direction = direction, Location = new Location {X = x, Y = y} };
        }

        public float Frustration { get; private set; }

        public Phase Phase { get; private set; }

        public void Tick(IntersectionCollection intersections, CarCollection cars)
        {
            var intendedPhase = Phase.GetPhasedAdvancedBy(10);

            if (intersections.WillAllowPhase(intendedPhase) && cars.WillAllowPhase(this, intendedPhase))
            {
                Phase = intendedPhase;
                _timesMoving++;
            }
            else
            {
                _timesWaiting++;
            }

            Frustration = (float)_timesWaiting / (_timesMoving + _timesWaiting);
        }

        

    }
}
