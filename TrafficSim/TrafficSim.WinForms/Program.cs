using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficSim.Core;

namespace TrafficSim.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var streets = new StreetDescription(horizontalRoads: new int[] {200, 500, 600}, verticalRoads: new int[] {200, 300, 400, 600, 800});
             
            var intersections = new IntersectionCollection(streets.GetIntersections().ToList());
            var carCollection = new CarCollection(streets);
            var form = new MainForm(streets, intersections, carCollection);
            Task.Run(() => MainLoop(streets, intersections, carCollection, form));
            Application.Run(form);
        }

        private static async Task MainLoop(StreetDescription streets, IntersectionCollection intersections, CarCollection carCollection, MainForm sim)
        {
            var simulator = new Simulator(streets, intersections, carCollection);

            while (true)
            {
                await Task.Delay(1000);
                simulator.MoveNext();
                var tcs = new TaskCompletionSource<int>();
                sim.Draw(tcs);
                await tcs.Task;
            }
        }
        
    }
}
