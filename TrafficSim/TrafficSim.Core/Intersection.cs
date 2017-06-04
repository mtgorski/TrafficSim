using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace TrafficSim.Core
{
    public class Intersection
    {
        private int _time;
        private bool _letLeft;

        public Intersection(int x, int y)
        {
            Location = new Location {X = x, Y = y};
            Lights = new Dictionary<Direction, Color>
            {
                {Direction.North, Color.Red},
                {Direction.South, Color.Red},
                {Direction.West, Color.Red},
                {Direction.East, Color.Red}
            };
        }

        public Location Location { get; set; }
        public Dictionary<Direction, Color> Lights { get; }

        public void Tick()
        {
            _time++;

            if (_time % 20 == 0)
            {
                if (Lights[Direction.West] == Color.Green)
                {
                    Lights[Direction.West] = Lights[Direction.East] = Color.Yellow;
                }                    
                else
                {
                    Lights[Direction.North] = Lights[Direction.South] = Color.Yellow;
                }
            }

            if (_time % 20 == 1)
            {
                if (Lights[Direction.West] == Color.Yellow)
                {
                    Lights[Direction.West] = Lights[Direction.East] = Color.Red;
                }
                else
                {
                    Lights[Direction.North] = Lights[Direction.South] = Color.Red;
                }
            }

            if (_time % 20 == 2)
            {
                if (_letLeft)
                {
                    _letLeft = false;
                    Lights[Direction.West] = Lights[Direction.East] = Color.Green;
                }
                else
                {
                    _letLeft = true;
                    Lights[Direction.North] = Lights[Direction.South] = Color.Green;
                }
            }
        }
    }
}

