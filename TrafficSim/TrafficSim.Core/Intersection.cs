using System.Collections.Generic;

namespace TrafficSim.Core
{
    public class Intersection
    {
        private int _time;
        private bool _letLeft;

        public Intersection(int x, int y)
        {
            Location = new Location {X = x, Y = y};
            Lights = new Dictionary<Direction, LightColor>
            {
                {Direction.North, LightColor.Red},
                {Direction.South, LightColor.Red},
                {Direction.West, LightColor.Red},
                {Direction.East, LightColor.Red}
            };
        }

        public Location Location { get; }
        public Dictionary<Direction, LightColor> Lights { get; }

        public void Tick(Simulator simulator)
        {
            _time++;

            if (_time % 20 == 0)
            {
                if (Lights[Direction.West] == LightColor.Green)
                {
                    Lights[Direction.West] = Lights[Direction.East] = LightColor.Yellow;
                }                    
                else
                {
                    Lights[Direction.North] = Lights[Direction.South] = LightColor.Yellow;
                }
            }

            if (_time % 20 == 1)
            {
                if (Lights[Direction.West] == LightColor.Yellow)
                {
                    Lights[Direction.West] = Lights[Direction.East] = LightColor.Red;
                }
                else
                {
                    Lights[Direction.North] = Lights[Direction.South] = LightColor.Red;
                }
            }

            if (_time % 20 == 2)
            {
                if (_letLeft)
                {
                    _letLeft = false;
                    Lights[Direction.West] = Lights[Direction.East] = LightColor.Green;
                }
                else
                {
                    _letLeft = true;
                    Lights[Direction.North] = Lights[Direction.South] = LightColor.Green;
                }
            }
        }
    }
}

