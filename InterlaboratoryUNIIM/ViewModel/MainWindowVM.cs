using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InterlaboratoryUNIIM.Algorithm;

using Microsoft.VisualBasic;

using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
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
            DrawingPlot(DataSet, ResultALGs);
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
            DrawingPlot(DataSet, ResultALGs);
        }

        private void DataSet_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NumOfParticipant = DataSet.Count;
        }

        private void DrawingPlot(ObservableCollection<DataUNIIM> dataUNIIMs, ObservableCollection<ResultALG> resultALGs)
        {
            DrawingModel = new PlotModel { Title = "График" };
            DataUNIIM MinItem = dataUNIIMs.OrderBy(min => min.Data - min.DataStandardDeviation).First();
            DataUNIIM MaxItem = dataUNIIMs.OrderByDescending(max => max.Data + max.DataStandardDeviation).First();

            double MinValue = Mu - Math.Abs(Mu - (MinItem.Data - MinItem.DataStandardDeviation)) * 1.3;
            double MaxValue = Mu + Math.Abs(Mu - (MaxItem.Data + MaxItem.DataStandardDeviation)) * 1.3;

            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "y1",
                Title = "Номер участника",
                FontSize = 10,
                AbsoluteMinimum = -1,
                AbsoluteMaximum = dataUNIIMs.Count() + resultALGs.Count() 
            };

            var valueAxis1 = new LinearAxis
            {
                Title = "Результаты участников",
                Position = AxisPosition.Left,
                Key = "x1",
                AbsoluteMinimum = MinValue,
                AbsoluteMaximum = MaxValue,

            };

            ErrorBarSeries ErrorData = new ErrorBarSeries()
            {
                XAxisKey = "x1",
                YAxisKey = "y1",
                Font = "Arial",
                FontSize = 1,
                TextColor = OxyColors.Black,
                FillColor = OxyColors.White,
            };

            int Category = 0;
            foreach (var item in dataUNIIMs.OrderBy(x => x.Data))
            {
                ErrorData.Items.Add(new ErrorBarItem
                {
                    Value = item.Data,
                    Error = item.DataStandardDeviation,
                    CategoryIndex = Category,
                });
                Category++;
                categoryAxis.Labels.Add(item.ParticipantName);
            }

            foreach (var ResultItem in resultALGs)
            {
                ErrorData.Items.Add(new ErrorBarItem
                {
                    Value = ResultItem.KCRV,
                    Error = ResultItem.U1,
                    CategoryIndex = Category
                });
                Category++;
                categoryAxis.Labels.Add(ResultItem.Algorithm.ToString());
            }

            double X = 0.0D;
            double Y = Mu;

            LineAnnotation LineV = new LineAnnotation()
            {
                StrokeThickness = 1,
                Color = OxyColors.Gray,
                Type = LineAnnotationType.Vertical,
                Text = (Y).ToString(),
                TextColor = OxyColors.White,
                X = dataUNIIMs.Count() - 0.5,
                Y = 0
            };
            LineAnnotation LineMu = new LineAnnotation()
            {
                StrokeThickness = 1,
                Color = OxyColors.Green,
                Type = LineAnnotationType.Horizontal,
                Text = (Y).ToString(),
                TextColor = OxyColors.White,
                X = X,
                Y = Y,
            };
            LineAnnotation LineMuMin = new LineAnnotation()
            {
                StrokeThickness = 1,
                Color = OxyColors.LightBlue,
                Type = LineAnnotationType.Horizontal,
                Text = (Y).ToString(),
                TextColor = OxyColors.White,
                X = X,
                Y = Y - StandardDeviation
            };
            LineAnnotation LineMuMax = new LineAnnotation()
            {
                StrokeThickness = 1,
                Color = OxyColors.LightBlue,
                Type = LineAnnotationType.Horizontal,
                Text = (Y).ToString(),
                TextColor = OxyColors.White,
                X = X,
                Y = Y + StandardDeviation
            };

            DrawingModel.Axes.Add(categoryAxis);
            DrawingModel.Axes.Add(valueAxis1);

            DrawingModel.Series.Add(ErrorData);
            DrawingModel.Annotations.Add(LineMu);
            DrawingModel.Annotations.Add(LineMuMin);
            DrawingModel.Annotations.Add(LineMuMax);
            DrawingModel.Annotations.Add(LineV);
        }

    }
}