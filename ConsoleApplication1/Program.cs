using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizerTestApplication
{
   class Program
   {
      [STAThread]
      static void Main(string[] args)
      {
         int N = 100;
         double[] data = new double[N];

         for(int i = 0; i < N; i++)
         {
            data[i] = (i - 50) * (i - 50) / 1e3;
         }
         OxyPlotVisualizer.Visualizer.TestShowVisualizer(data);
      }
   }
}
