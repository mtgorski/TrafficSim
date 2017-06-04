using System;

namespace TrafficSim.Core
{
    public class Car
    {
        private int _timesWaiting = 0;
        private int _timesMoving = 0;
        public Car(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public Direction Direction { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void MoveNext(IntersectionCollection intersections, CarCollection cars)
        {
            var x = X;
            var y = Y;
            switch (Direction)
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

            IntendedLocation = new Tuple<int, int>(x, y);

            if (intersections.WillAllow(x, y, Direction) && cars.WillAllow(this))
            {
                X = x;
                Y = y;
                _timesMoving++;
            }
            else
            {
                _timesWaiting++;
            }

            Frustration = (float)_timesWaiting / (_timesMoving + _timesWaiting);
        }

        public float Frustration { get; private set; }

        public Tuple<int, int> IntendedLocation { get; set; }
    }
}
