using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafficSim.Core
{
    public class Simulator
    {
        private readonly CarCollection _carCollection;
        private readonly CarInjector _carinjector;
        private readonly IntersectionCollection _intersections;
        private readonly CarDeleter _deleter;

        public Simulator(StreetDescription description, 
            IntersectionCollection intersections, 
            CarCollection carCollection, 
            CarInjector injector,
            CarDeleter deleter
           )
        {
            Streets = description;
            _intersections = intersections;
            _carCollection = carCollection;
            _carinjector = injector;
            _deleter = deleter;
        }

        public StreetDescription Streets { get; }

        public void Tick()
        {
            _carinjector.Tick(this);
            _carCollection.Tick(this);
            _intersections.Tick(this);
            _deleter.Tick(this);
        }

        public bool WillAllowPhase(Car car, Phase intendedPhase)
        {
            return _carCollection.WillAllowPhase(car, intendedPhase) && _intersections.WillAllowPhase(intendedPhase);
        }

        public bool TryAddCar(Car car)
        {
            if (WillAllowPhase(car, car.Phase))
            {
                _carCollection.Add(car);
                return true;
            }

            return false;
        }

        public IEnumerable<Car> CarsWhere(Func<Car, bool> func)
        {
            return _carCollection.Where(func);
        }

        public void DeleteCar(Car car)
        {
            _carCollection.Remove(car);
        }
    }
}
