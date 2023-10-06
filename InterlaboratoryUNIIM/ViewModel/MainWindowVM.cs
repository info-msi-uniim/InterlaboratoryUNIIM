using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MathNet.Numerics.Distributions;

using MathNet.Numerics.Statistics;
using MathNet.Numerics.Random;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using MathNet.Numerics.LinearAlgebra;
using InterlaboratoryUNIIM.Algorithm;
#nullable disable
namespace InterlaboratoryUNIIM.ViewModel
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        private string _Title = "Расчет сличений УНИИМ";
        [ObservableProperty]
        private double _StandardDeviation;
        [ObservableProperty]
        private double _NumOfIteration;

        #region Начальный датасет

        [ObservableProperty]
        private List<DataUNIIM> _DataSet = new DatasetUNIIM().DataSet;

        #endregion

        [ObservableProperty]
        private double _TestSKO;
        [ObservableProperty]
        private string _Duration;
        [RelayCommand]
        public void Test()
        {
            //var samples = DataSet.Select(c => c.DataDelta);
            //var statistics = new DescriptiveStatistics(samples);
            //TestSKO = statistics.StandardDeviation;
        }

        [RelayCommand]
        public void TestMean()
        {
            //var samples = DataSet.Select(c => c.DataDelta);
            //var statistics = new DescriptiveStatistics(samples);
            //TestSKO = statistics.StandardDeviation;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DataUNIIM d;
            Random rnd = new Random();
            //double r = rnd.NextDouble();
            // double n;
            // n = Normal.InvCDF( d.DataDelta,StandardDeviation, r);
            int num = 10000;
            int count = new DatasetUNIIM().DataSet.Count;


            //Matrix<double> matrix;


            double[,] my = new double[num, count];
            for (int j = 0; j < count; j++)
            {
                d = new DatasetUNIIM().DataSet[j];
                for (int i = 0; i < num; i++)
                {
                    my[i, j] = Normal.InvCDF(d.DataDelta, StandardDeviation, rnd.NextDouble());
                }
            }

            // var n = QNorm(r, d.DataDelta, StandardDeviation, true, false);
            //TestSKO = n;
            //TestSKO = my.Mean();
            stopwatch.Stop();
            Duration = stopwatch.ElapsedTicks.ToString() + " (" + stopwatch.ElapsedMilliseconds.ToString() + " ms)";
        }

        public MainWindowVM()
        {
            StandardDeviation = 0.5;
            NumOfIteration = 10000;
          // Data

        }
    }
}