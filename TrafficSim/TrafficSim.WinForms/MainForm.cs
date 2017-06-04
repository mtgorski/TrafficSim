using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficSim.Core;

namespace TrafficSim.WinForms
{
    public partial class MainForm : Form
    {
        private readonly StreetDescription _streets;
        private readonly IEnumerable<Intersection> _intersections;
        private readonly IEnumerable<Car> _cars;

        public MainForm(StreetDescription streets, IEnumerable<Intersection> intersections, IEnumerable<Car> cars)
        {
            _streets = streets;
            _intersections = intersections;
            _cars = cars;
            InitializeComponent();
            Paint += OnPaint;
        }

        private void OnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            var pen = new Pen(Color.Gray, 20);
            var graphics = paintEventArgs.Graphics;
            foreach (var road in _streets.HorizontalRoads)
            {
                graphics.DrawLine(pen, 0, road, 1100, road);
            }
            foreach (var road in _streets.VerticalRoads)
            {
                graphics.DrawLine(pen, road, 0, road, 1100);
            }

            var colors = new Dictionary<Color, Pen>
            {
                {Color.Red, new Pen(Color.Red, 3)},
                {Color.Green, new Pen(Color.Green, 3)}
            };

            const int trafficLineOffset = 7;

            foreach (var intersection in _intersections)
            {
                graphics.DrawLine(colors[intersection.Left], intersection.X - trafficLineOffset, intersection.Y - trafficLineOffset, intersection.X - trafficLineOffset, intersection.Y + trafficLineOffset);
                graphics.DrawLine(colors[intersection.Top], intersection.X - trafficLineOffset, intersection.Y + trafficLineOffset, intersection.X + trafficLineOffset, intersection.Y + trafficLineOffset);
                graphics.DrawLine(colors[intersection.Right], intersection.X + trafficLineOffset, intersection.Y - trafficLineOffset, intersection.X + trafficLineOffset, intersection.Y + trafficLineOffset);
                graphics.DrawLine(colors[intersection.Bottom], intersection.X - trafficLineOffset, intersection.Y - trafficLineOffset, intersection.X + trafficLineOffset, intersection.Y - trafficLineOffset);
            }

            var carPens = new List<Tuple<float, Pen>>
            {
                new Tuple<float, Pen>(.3f, new Pen(Color.Cyan, 6)),
                new Tuple<float, Pen>(.6f, new Pen(Color.Yellow, 6)),
                new Tuple<float, Pen>(1.01f, new Pen(Color.Orange, 6))
            };
            foreach (var car in _cars)
            {
                var carPen = carPens.First(x => x.Item1 > car.Frustration).Item2;
                var leftAdjuster = car.Direction == Direction.South ? 8 : 0;
                var upAdjuster = car.Direction == Direction.West ? -8 : 0;
                graphics.DrawEllipse(carPen, car.X - leftAdjuster, car.Y + upAdjuster, 5, 5);
            }

            ( sender as TaskCompletionSource<int>)?.SetResult(1);
        }

        public void Draw(TaskCompletionSource<int> tcs)
        {
            var eventArgs = new PaintEventArgs(CreateGraphics(), Bounds);
            OnPaint(tcs, eventArgs);
        }
    }

    
}
