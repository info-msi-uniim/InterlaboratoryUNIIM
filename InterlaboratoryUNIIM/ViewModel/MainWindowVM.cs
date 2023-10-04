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
#nullable disable
namespace InterlaboratoryUNIIM.ViewModel
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        private string _Title = "Расчет сличений УНИИМ";
        [ObservableProperty]
        private double _StandardDeviation;

        #region Начальный датасет
        public partial class DataUNIIM : ObservableObject
        {
            [ObservableProperty]
            private double _DataDelta;
            [ObservableProperty]
            private double _DataStandardDeviation;
        }

        [ObservableProperty]
        private List<DataUNIIM> _DataSet = new();
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
            int count = DataSet.Count;

            //Matrix<double> matrix;


            double[,] my = new double[num, count];
            for (int j = 0; j < count; j++)
            {
                d = DataSet[j];
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
            PopulateDataset();

            //DataUNIIM d = DataSet.First();
            //Random rnd = new Random();
            //double r = rnd.Next();
            //var n = Normal.InvCDF(r, d.DataDelta, StandardDeviation);
            //TestSKO = n;

        }

        private void PopulateDataset()
        {
            DataSet.Add(new DataUNIIM { DataDelta = 0.2188854, DataStandardDeviation = 0.345791208 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.432494344, DataStandardDeviation = 0.378945736 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.42853623, DataStandardDeviation = 0.362241872 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.006779547, DataStandardDeviation = 0.362241872 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.047697929, DataStandardDeviation = 0.391671353 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.171066582, DataStandardDeviation = 0.497205367 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.119200593, DataStandardDeviation = 0.488584016 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.331144208, DataStandardDeviation = 0.301898571 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.209797424, DataStandardDeviation = 0.445502956 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.490955443, DataStandardDeviation = 0.446114113 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.051517448, DataStandardDeviation = 0.481554809 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.224016574, DataStandardDeviation = 0.375322282 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.496617556, DataStandardDeviation = 0.423091747 });
            DataSet.Add(new DataUNIIM { DataDelta = 0.011432242, DataStandardDeviation = 0.348157542 });
            DataSet.Add(new DataUNIIM { DataDelta = -0.145809068, DataStandardDeviation = 0.384987738 });
        }

        /// <summary>
        /// Quantile function (Inverse CDF) for the normal distribution.
        /// </summary>
        /// <param name="p">Probability.</param>
        /// <param name="mu">Mean of normal distribution.</param>
        /// <param name="sigma">Standard deviation of normal distribution.</param>
        /// <param name="isLowerTail">If true, probability is P[X <= x], otherwise P[X > x].</param>
        /// <param name="isLogValues">If true, probabilities are given as log(p).</param>
        /// <returns>P[X <= x] where x ~ N(mu,sigma^2)</returns>
        /// <remarks>See https://svn.r-project.org/R/trunk/src/nmath/qnorm.c </remarks>
        public static double QNorm(double p, double mu, double sigma, bool isLowerTail, bool isLogValues)
        {
            if (double.IsNaN(p) || double.IsNaN(mu) || double.IsNaN(sigma)) return (p + mu + sigma);
            double ans;
            bool isBoundaryCase = R_Q_P01_boundaries(p, double.NegativeInfinity, double.PositiveInfinity, isLowerTail, isLogValues, out ans);
            if (isBoundaryCase) return (ans);
            if (sigma < 0) return (double.NaN);
            if (sigma == 0) return (mu);

            double p_ = R_DT_qIv(p, isLowerTail, isLogValues);
            double q = p_ - 0.5;
            double r, val;

            if (Math.Abs(q) <= 0.425)  // 0.075 <= p <= 0.925
            {
                r = .180625 - q * q;
                val = q * (((((((r * 2509.0809287301226727 +
                           33430.575583588128105) * r + 67265.770927008700853) * r +
                         45921.953931549871457) * r + 13731.693765509461125) * r +
                       1971.5909503065514427) * r + 133.14166789178437745) * r +
                     3.387132872796366608)
                / (((((((r * 5226.495278852854561 +
                         28729.085735721942674) * r + 39307.89580009271061) * r +
                       21213.794301586595867) * r + 5394.1960214247511077) * r +
                     687.1870074920579083) * r + 42.313330701600911252) * r + 1.0);
            }
            else
            {
                r = q > 0 ? R_DT_CIv(p, isLowerTail, isLogValues) : p_;
                r = Math.Sqrt(-((isLogValues && ((isLowerTail && q <= 0) || (!isLowerTail && q > 0))) ? p : Math.Log(r)));

                if (r <= 5)              // <==> min(p,1-p) >= exp(-25) ~= 1.3888e-11
                {
                    r -= 1.6;
                    val = (((((((r * 7.7454501427834140764e-4 +
                            .0227238449892691845833) * r + .24178072517745061177) *
                          r + 1.27045825245236838258) * r +
                         3.64784832476320460504) * r + 5.7694972214606914055) *
                       r + 4.6303378461565452959) * r +
                      1.42343711074968357734)
                     / (((((((r *
                              1.05075007164441684324e-9 + 5.475938084995344946e-4) *
                             r + .0151986665636164571966) * r +
                            .14810397642748007459) * r + .68976733498510000455) *
                          r + 1.6763848301838038494) * r +
                         2.05319162663775882187) * r + 1.0);
                }
                else                     // very close to  0 or 1 
                {
                    r -= 5.0;
                    val = (((((((r * 2.01033439929228813265e-7 +
                            2.71155556874348757815e-5) * r +
                           .0012426609473880784386) * r + .026532189526576123093) *
                         r + .29656057182850489123) * r +
                        1.7848265399172913358) * r + 5.4637849111641143699) *
                      r + 6.6579046435011037772)
                     / (((((((r *
                              2.04426310338993978564e-15 + 1.4215117583164458887e-7) *
                             r + 1.8463183175100546818e-5) * r +
                            7.868691311456132591e-4) * r + .0148753612908506148525)
                          * r + .13692988092273580531) * r +
                         .59983220655588793769) * r + 1.0);
                }
                if (q < 0.0) val = -val;
            }

            return (mu + sigma * val);
        }

        private static bool R_Q_P01_boundaries(double p, double left, double right, bool isLowerTail, bool isLogValues, out double ans)
        {
            if (isLogValues)
            {
                if (p > 0.0)
                {
                    ans = double.NaN;
                    return (true);
                }
                if (p == 0.0)
                {
                    ans = isLowerTail ? right : left;
                    return (true);
                }
                if (p == double.NegativeInfinity)
                {
                    ans = isLowerTail ? left : right;
                    return (true);
                }
            }
            else
            {
                if (p < 0.0 || p > 1.0)
                {
                    ans = double.NaN;
                    return (true);
                }
                if (p == 0.0)
                {
                    ans = isLowerTail ? left : right;
                    return (true);
                }
                if (p == 1.0)
                {
                    ans = isLowerTail ? right : left;
                    return (true);
                }
            }
            ans = double.NaN;
            return (false);
        }

        private static double R_DT_qIv(double p, bool isLowerTail, bool isLogValues)
        {
            return (isLogValues ? (isLowerTail ? Math.Exp(p) : -ExpM1(p)) : R_D_Lval(p, isLowerTail));
        }

        private static double R_DT_CIv(double p, bool isLowerTail, bool isLogValues)
        {
            return (isLogValues ? (isLowerTail ? -ExpM1(p) : Math.Exp(p)) : R_D_Cval(p, isLowerTail));
        }

        private static double R_D_Lval(double p, bool isLowerTail)
        {
            return isLowerTail ? p : 0.5 - p + 0.5;
        }

        private static double R_D_Cval(double p, bool isLowerTail)
        {
            return isLowerTail ? 0.5 - p + 0.5 : p;
        }
        private static double ExpM1(double x)
        {
            if (Math.Abs(x) < 1e-5)
                return x + 0.5 * x * x;
            else
                return Math.Exp(x) - 1.0;
        }
    }
}