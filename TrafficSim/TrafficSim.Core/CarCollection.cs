using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Core
{
    public class CarCollection : IEnumerable<Car>
    {
        private readonly List<Car> _cars;

        public CarCollection()
        {
            _cars = new List<Car>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Car> GetEnumerator()
        {
            return _cars.GetEnumerator();
        }

        public void Tick(Simulator simulator)
        {

            foreach (var car in _cars)
            {
                car.Tick(simulator);
            }

            foreach (var car in _cars)
            {
                var sameLocationCars = _cars.Where(otherCar => car.Phase.Location.Equals(otherCar.Phase.Location) && otherCar != car);

                if(sameLocationCars.Any(otherCar => otherCar.Phase.Direction == car.Phase.Direction))
                    throw new InvalidOperationException("Rear Ender!");
                if(sameLocationCars.Any(otherCar => 
                    (!otherCar.Phase.Direction.IsOpposite(car.Phase.Direction)
                    )))
                    throw new InvalidOperationException("TBone!"); 
            }
        }

        public bool WillAllowPhase(Car car, Phase intentedPhase)
        {
            var matchingCars =
                _cars.Where(
                    c =>
                        c.Phase.Location.Equals(intentedPhase.Location));
            
            foreach (var otherCar in matchingCars)
            {
                if (otherCar == car)
                    continue;

                if (otherCar.Phase.Direction.IsOpposite(intentedPhase.Direction))
                    continue;

                return false;
            }

            return true; 
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Remove(Car car)
        {
            _cars.Remove(car);
        }
    }
}
