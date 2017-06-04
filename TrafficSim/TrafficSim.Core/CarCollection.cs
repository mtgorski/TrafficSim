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
                if (_rng.Next(10) < 3)
                {
                    if(!_cars.Any(car => car.X == 0 && car.Y == road && car.Direction == Direction.East))
                        _cars.Add(new Car(0, road, Direction.East));
                }

                if (_rng.Next(10) < 3)
                {
                    if (!_cars.Any(car => car.X == 1100 && car.Y == road && car.Direction == Direction.West))
                        _cars.Add(new Car(1100, road, Direction.West));
                }
            }

            foreach (var road in _streets.VerticalRoads)
            {
                if (_rng.Next(10) < 3)
                {
                    if(!_cars.Any(car => car.X == road && car.Y == 0 && car.Direction == Direction.South))
                        _cars.Add(new Car(road, 0, Direction.South));
                }

                if (_rng.Next(10) < 3)
                {
                    if (!_cars.Any(car => car.X == road && car.Y == 1100 && car.Direction == Direction.North))
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
                var sameLocationCars = _cars.Where(otherCar => car.X == otherCar.X && car.Y == otherCar.Y && otherCar != car);
                if(sameLocationCars.Any(otherCar => otherCar.Direction == car.Direction))
                    throw new InvalidOperationException("Rear Ender!");
                if(sameLocationCars.Any(otherCar => 
                    (otherCar.Direction == Direction.East && car.Direction != Direction.West)
                    || otherCar.Direction == Direction.West && car.Direction != Direction.East
                    || otherCar.Direction == Direction.North && car.Direction != Direction.South
                    || otherCar.Direction == Direction.South && car.Direction != Direction.North
                    ))
                    throw new InvalidOperationException("TBone!");

                if(car.X < 0 || car.X > 1100)
                    toDelete.Add(car);
            }

            foreach (var car in toDelete)
            {
                _cars.Remove(car);
            }
        }

       

        public bool WillAllow(Car car)
        {
            var matchingCars =
                _cars.Where(
                    c =>
                        c.X == car.IntendedLocation.Item1 && c.Y == car.IntendedLocation.Item2);

            foreach (var otherCar in matchingCars)
            {
                if (otherCar == car)
                    continue;

                if (otherCar.Direction == Direction.East && car.Direction == Direction.West)
                    continue;

                if(otherCar.Direction == Direction.South && car.Direction == Direction.North)
                    continue;

                if(otherCar.Direction == Direction.West && car.Direction == Direction.East)
                    continue;

                if(otherCar.Direction == Direction.North && car.Direction == Direction.South)
                    continue;

                return false;
            }

            return true; 
        }
    }
}
