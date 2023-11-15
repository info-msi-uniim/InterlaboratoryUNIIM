using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InterlaboratoryUNIIM.Algorithm;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

#nullable disable
namespace InterlaboratoryUNIIM.ViewModel
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        private string _Title = "Расчет сличений";
        [ObservableProperty]
        private int _NumOfParticipant;
        [ObservableProperty]
        private double _StandardDeviation;
        [ObservableProperty]
        private double _Mu;
        [ObservableProperty]
        private int _NumOfIteration;

        #region Начальный датасет

        [ObservableProperty]
        private ObservableCollection<DataUNIIM> _DataSet;

        [ObservableProperty]
        private ObservableCollection<ResultALG> _ResultALGs;
        #endregion

        public PlotModel DrawingModel { get; set; }


        [RelayCommand]
        public void CalculateALL()
        {
            ResultALGs.Clear();
            int CountIteration = NumOfIteration;
            int NumParticipant = NumOfParticipant;
            double SD = StandardDeviation;
            double[,] MCDataset = new ArrayMC().Get(DataSet.ToList(), CountIteration, SD);

            ResultALGs.Add(new Algorithms().Mean(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().W_Mean(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().Median(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().DerSimonian(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().Vaf(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().HuberH15(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().MandelPaule(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().PMA1(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().PMA2(DataSet.ToList(), ref MCDataset, Mu));
            DrawingPlot();
        }

        public MainWindowVM()
        {
            StandardDeviation = 0.5;
            NumOfIteration = 10000;
            Mu = 10;
            DataSet = new DatasetUNIIM().DataSet;
            NumOfParticipant = DataSet.Count;
            ResultALGs = new();

            DataSet.CollectionChanged += DataSet_CollectionChanged;

            CalculateALL();
            DrawingPlot();

        }

        private void DataSet_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NumOfParticipant = DataSet.Count;
        }

        private void DrawingPlot()
        {
            DrawingModel = new PlotModel { Title = "Графики" };

            //DrawingModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "y1",
                Title = "Номер участника"
            };
            var valueAxis1 = new LinearAxis
            {
                Title = "Результаты участников",
                Position = AxisPosition.Left,
                MinimumPadding = 0.06,
                MaximumPadding = 0.06,
                ExtraGridlines = new[] { 0d },
                Key = "x1"
            };
            DrawingModel.Axes.Add(categoryAxis);
            DrawingModel.Axes.Add(valueAxis1);
            ErrorBarSeries ErrorData = new ErrorBarSeries()
            {
                XAxisKey = "x1",
                YAxisKey = "y1",
                Font = "Arial",
                FontSize = 1,
                TextColor = OxyColors.Black,
               FillColor = OxyColors.Aqua
            };



            foreach (var item in DataSet)
            {
                ErrorData.Items.Add(new ErrorBarItem { Value = item.Data, Error = item.DataStandardDeviation });
            }

            DrawingModel.Series.Add(ErrorData);
        }

    }
}