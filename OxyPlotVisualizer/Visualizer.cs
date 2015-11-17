using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Reflection;
using System.Diagnostics;
using OxyPlot;


[assembly: System.Diagnostics.DebuggerVisualizer(
      typeof(OxyPlotVisualizer.Visualizer),
      Target = typeof(double[]),
      Description = "OxyPlot Visualizer")]
namespace OxyPlotVisualizer{
   
   public class Visualizer : Microsoft.VisualStudio.DebuggerVisualizers.DialogDebuggerVisualizer
   {
      public Visualizer()
      {         
      }

      protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
      {
         Trace.WriteLine(Assembly.GetCallingAssembly().FullName);
         var model = new MainViewModel(objectProvider.GetObject());
         MainView win = new MainView();
         win.DataContext = model;
         win.Title = "OxyPlot Visualizer";
         
         win.ShowDialog();
      }

      public static void TestShowVisualizer(object obj)
      {
         VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(obj, typeof(Visualizer));
         host.ShowVisualizer();
      }

   }
}