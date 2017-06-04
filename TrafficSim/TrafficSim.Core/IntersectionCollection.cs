using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

        public void MoveNext()
        {
            foreach (var intersection in _intersections)
            {
                intersection.MoveNext();
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

        public bool WillAllowPhase(Phase phase)
        {
            var x = phase.Location.X;
            var y = phase.Location.Y;
            switch (phase.Direction)
            {
                case Direction.South:
                    y += 10;
                    break;
                case Direction.East:
                    x += 10;
                    break;
                case Direction.North:
                    y -= 10;
                    break;
                case Direction.West:
                    x -= 10;
                    break;
            }
            var intersectionLocation = new Location {X = x, Y = y};

            var relevantIntersection =
                _intersections.SingleOrDefault(intersection => intersection.Location.Equals(intersectionLocation));

            if (relevantIntersection == null)
                return true;

            switch (phase.Direction)
            {
                case Direction.East:
                    return relevantIntersection.Left == Color.Green;
                case Direction.North:
                    return relevantIntersection.Bottom == Color.Green;
                case Direction.South:
                    return relevantIntersection.Top == Color.Green;
                case Direction.West:
                    return relevantIntersection.Right == Color.Green;
                default:
                    throw new ArgumentException("Unexpected direction");
            }
        }
    }
}
