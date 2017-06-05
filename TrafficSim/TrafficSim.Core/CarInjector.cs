using System;

namespace TrafficSim.Core
{
    public class CarInjector 
    {
        private static Random Rng = new Random();


        public void Tick(Simulator simulator)
        {
            var streets = simulator.Streets;
            foreach (var road in streets.HorizontalRoads)
            {
                if (Rng.Next(10) < 3)
                {                                        
                    simulator.TryAddCar(new Car(0, road, Direction.East));
                }

                if (Rng.Next(10) < 3)
                {
                    simulator.TryAddCar(new Car(1100, road, Direction.West));
                }
            }

            foreach (var road in streets.VerticalRoads)
            {
                if (Rng.Next(10) < 3)
                {
                    simulator.TryAddCar(new Car(road, 0, Direction.South));
                }

                if (Rng.Next(10) < 3)
                {
                    simulator.TryAddCar(new Car(road, 1100, Direction.North));
                }
            }
        }
    }
}
