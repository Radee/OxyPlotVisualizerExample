using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using OxyPlot;

namespace OxyPlotVisualizer
{
   public class MainViewModel : INotifyPropertyChanged
   {
      #region INotifyPropertyChanged Members

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged(string propertyName)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      #endregion INotifyPropertyChanged Members

      #region Fields

      private PlotModel plotModel;
      private OxyPlot.Axes.LinearAxis xAxis;
      private OxyPlot.Axes.LinearAxis yAxis;

      #endregion

      #region Constructor

      public MainViewModel(object data)
      {
         PlotModel = new PlotModel();

         //******************************************************************************************************************
         // Uncomment this line to remove FileNotFoundException when trying to load the OxyPlot.Wpf library from xaml file.
         // Need this to force resolve OxyPlot.Wpf assembly. You can use whatever class in the assembly.
         //
         //OxyPlot.Wpf.BarSeries toResolveWpf = new OxyPlot.Wpf.BarSeries(); 
         //
         //******************************************************************************************************************

         SetUpModel();
         FillSerie(data);
      }

      #endregion

      #region Properties

      public PlotModel PlotModel
      {
         get { return plotModel; }
         set { plotModel = value; OnPropertyChanged("PlotModel"); }
      }

      #endregion

      #region Methods

      private void SetUpModel()
      {
         PlotModel.IsLegendVisible = false;
         PlotModel.LegendOrientation = LegendOrientation.Horizontal;
         PlotModel.LegendPlacement = LegendPlacement.Outside;
         PlotModel.LegendPosition = LegendPosition.TopRight;
         PlotModel.LegendBackground = OxyColors.Transparent;
         PlotModel.LegendBorder = OxyColors.Transparent;
         PlotModel.LegendTextColor = OxyColors.White;

         //Axes
         xAxis = new OxyPlot.Axes.LinearAxis();
         xAxis.Position = OxyPlot.Axes.AxisPosition.Bottom;
         xAxis.AxislineColor = OxyColors.Black;
         xAxis.TickStyle = OxyPlot.Axes.TickStyle.Outside;
         xAxis.TicklineColor = OxyColors.Black;
         xAxis.TextColor = OxyColors.Black;

         yAxis = new OxyPlot.Axes.LinearAxis();
         yAxis.Position = OxyPlot.Axes.AxisPosition.Left;
         yAxis.AxislineColor = OxyColors.Black;
         yAxis.TickStyle = OxyPlot.Axes.TickStyle.Outside;
         yAxis.TicklineColor = OxyColors.Black;
         yAxis.TextColor = OxyColors.Black;

         PlotModel.Axes.Add(xAxis);
         PlotModel.Axes.Add(yAxis);
      }

      private void FillSerie(object data)
      {
         Task.Factory.StartNew(() =>
         {
            PlotModel.Series.Clear();
            OxyPlot.Series.LineSeries lineSerie = new OxyPlot.Series.LineSeries
            {
               StrokeThickness = 2,
               CanTrackerInterpolatePoints = true,
               Smooth = true,
            };
            PlotModel.Series.Add(lineSerie);

            double[] samples = data as double[];

            var points = new List<DataPoint>();
            double xPos = 0;
            double xStep = 1;
            for(int i = 0; i < samples.Length; i++)
            {
               double y = samples[i];
               points.Add(new DataPoint(xPos, y));
               xPos += xStep;
            }
            lineSerie.ItemsSource = points;

            PlotModel.InvalidatePlot(true);
         });
      }

      #endregion
   }
}
