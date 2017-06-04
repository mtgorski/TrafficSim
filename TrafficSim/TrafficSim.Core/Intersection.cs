using System;
using System.Diagnostics;
using System.Drawing;

namespace TrafficSim.Core
{
    public class Intersection
    {
        private static Random _rng = new Random();
        private int _time;
        private bool _letLeft;

        public Intersection(int x, int y)
        {
            X = x;
            Y = y;
            Left = Color.Red;
            Right = Color.Red;
            Top = Color.Red;
            Bottom = Color.Red;
        }

        public Color Bottom { get; set; }

        public Color Top { get; set; }

        public Color Right { get; set; }

        public Color Left { get; set; }

        public int X { get; }
        public int Y { get; }

        public void MoveNext()
        {
            _time++;

            if (_time % 20 == 0)
            {
                if (Left == Color.Green)
                {
                    Left = Right = Color.Red;
                }                    
                else
                {
                    Top = Bottom = Color.Red;
                }
            }

            if (_time % 20 == 1)
            {
                if (_letLeft)
                {
                    _letLeft = false;   
                    Left = Right = Color.Green;
                }
                else
                {
                    _letLeft = true;
                    Top = Bottom = Color.Green;
                }
            }
        }
    }
}

