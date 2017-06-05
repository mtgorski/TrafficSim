namespace TrafficSim.Core
{
    public class CarDeleter
    {
        public void Tick(Simulator simulator)
        {
            var toDelete =
                simulator.CarsWhere(
                    x =>
                        x.Phase.Location.X < 0 || x.Phase.Location.Y < 0 || x.Phase.Location.Y > 1100 ||
                        x.Phase.Location.X > 1100);

            foreach (var car in toDelete)
            {
                simulator.DeleteCar(car);
            }
        }
    }
}
