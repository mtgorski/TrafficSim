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
            var carCollection = new CarCollection();
            var form = new MainForm(streets, intersections, carCollection);
            var injector = new CarInjector();
            var deleter = new CarDeleter();
            var simulator = new Simulator(streets, intersections, carCollection, injector, deleter);
            Task.Run(() => MainLoop(form, simulator));
            Application.Run(form);
        }

        private static async Task MainLoop(MainForm form, Simulator simulator)
        {

            while (true)
            {
                await Task.Delay(1000);
                simulator.Tick();
                var tcs = new TaskCompletionSource<int>();
                form.Draw(tcs);
                await tcs.Task;
            }
        }
        
    }
}
