using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Core
{
    public class CarCollection : IEnumerable<Car>
    {
        private StreetDescription _streets;
        private List<Car> _cars;
        private Random _rng = new Random();

        public CarCollection(StreetDescription streets)
        {
            _streets = streets;
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

        public void InjectCars()
        {
            foreach (var road in _streets.HorizontalRoads)
            {
                if (_rng.Next(10) < 2)
                {
                    var injectionPhase = new Phase {Direction = Direction.East, Location = new Location {X = 0, Y = road} };
                    if(!_cars.Any(car => car.Phase.Equals(injectionPhase)))
                        _cars.Add(new Car(0, road, Direction.East));
                }

                if (_rng.Next(10) < 2)
                {
                    var injectionPhase = new Phase
                    {
                        Direction = Direction.West,
                        Location = new Location {X = 1100, Y = road}
                    };
                    if (!_cars.Any(car => car.Phase.Equals(injectionPhase)))
                        _cars.Add(new Car(1100, road, Direction.West));
                }
            }

            foreach (var road in _streets.VerticalRoads)
            {
                if (_rng.Next(10) < 2)
                {
                    var injectionPhase = new Phase
                    {
                        Direction = Direction.South,
                        Location = new Location {X = road, Y = 0}
                    };
                    if(!_cars.Any(car => car.Phase.Equals(injectionPhase)))
                        _cars.Add(new Car(road, 0, Direction.South));
                }

                if (_rng.Next(10) < 2)
                {
                    var injectionPhase = new Phase
                    {
                        Direction = Direction.North,
                        Location = new Location {X = road, Y = 1100}
                    };
                    if (!_cars.Any(car => car.Phase.Equals(injectionPhase)))
                        _cars.Add(new Car(road, 1100, Direction.North));
                }
            }
        }

        public void MoveNext(IntersectionCollection intersections)
        {
            foreach (var car in _cars)
            {
                car.MoveNext(intersections, this);
            }

            var toDelete = new List<Car>();

            foreach (var car in _cars)
            {
                var sameLocationCars = _cars.Where(otherCar => car.Phase.Location.Equals(otherCar.Phase.Location) && otherCar != car);

                if(sameLocationCars.Any(otherCar => otherCar.Phase.Direction == car.Phase.Direction))
                    throw new InvalidOperationException("Rear Ender!");
                if(sameLocationCars.Any(otherCar => 
                    (!otherCar.Phase.Direction.IsOpposite(car.Phase.Direction)
                    )))
                    throw new InvalidOperationException("TBone!");

                if (car.Phase.Location.X < 0 || car.Phase.Location.X > 1100)
                {
                    toDelete.Add(car);
                }
                else if (car.Phase.Location.Y < 0 || car.Phase.Location.Y > 1100)
                {
                    toDelete.Add(car);
                }  
            }

            foreach (var car in toDelete)
            {
                _cars.Remove(car);
            }
        }

       

        public bool WillAllowPhase(Car car, Phase intentedPhase)
        {
            var matchingCars =
                _cars.Where(
                    c =>
                        c.Phase.Location.X == intentedPhase.Location.X && c.Phase.Location.Y == intentedPhase.Location.Y);
            
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
    }
}
