using System;
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
             
            var intersections = new IntersectionCollection(streets.Intersections);
            var carCollection = new CarCollection(streets);
            var form = new MainForm(streets, intersections, carCollection);
            Task.Run(() => MainLoop(intersections, carCollection, form));
            Application.Run(form);
        }

        private static async Task MainLoop(IntersectionCollection intersections, CarCollection carCollection, MainForm sim)
        {
            var simulator = new Simulator(intersections, carCollection);

            while (true)
            {
                await Task.Delay(1000);
                simulator.Tick();
                var tcs = new TaskCompletionSource<int>();
                sim.Draw(tcs);
                await tcs.Task;
            }
        }
        
    }
}
