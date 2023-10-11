using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

using MathNet.Numerics.Statistics;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class Algorithms
    {
        public double BIAS(double KCRV, double Mu)
        {
            return Math.Abs(Mu - KCRV) / Mu * 100;
        }
        public double S_Mu(double M, double Mu, List<DataUNIIM> Data)
        {
            double Result;
            double Summ = 0;
            foreach (var item in Data)
            {
                Summ += Math.Pow((item.Data - Mu), 2);
            }
            Result = 1.0 / Mu * Math.Sqrt(Summ / (M - 1)) * 100;

            return Result;
        }

        public ResultALG Mean(List<DataUNIIM> DataUNIIM, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ResultALG Result = new ResultALG();
            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);
            int m = DataUNIIM.Count;


            double U1(List<DataUNIIM> DataUNIIM, double m)
            {
                return (1.0 / m) * Statistics.StandardDeviation(DataUNIIM.Select(d => d.Data).ToArray()) * 100;
            }

            Result.Algorithm = Algorithm.MEAN;
            Result.KCRV = Statistics.Mean(DataUNIIM.Select(d => d.Data).ToArray());
            Result.U1 = U1(DataUNIIM, m);

            Result.BIAS = BIAS(Result.KCRV, Mu);
            Result.S_mu = S_Mu(height, Mu, DataUNIIM);

            List<double> KCRVList = new List<double>(width);
            List<double> SDList = new List<double>(height);
            List<double> RowList = new List<double>(width);

            for (int i = 0; i < height; i++)
            {
                RowList.Clear();
                for (int j = 0; j < width; j++)
                {
                    RowList.Add(MCDataset[i, j]);
                }
                KCRVList.Add(Statistics.Mean(RowList.ToArray()));
                SDList.Add(Statistics.StandardDeviation(RowList.ToArray()) / Math.Sqrt(Mu));
            }

            Result.KCRVMC = Statistics.Mean(KCRVList);
            Result.U1MC = Statistics.Mean(SDList);

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG Median(List<DataUNIIM> DataUNIIM, ref double[,] MCDataset, double Mu)
        {

            double U1(List<DataUNIIM> DataUNIIM)
            {
                double U1 = 0;
                double MEDx = Statistics.Median(DataUNIIM.Select(d => d.Data).ToArray());
                double m = DataUNIIM.Count();
                List<double> tmpMed = new();

                foreach (DataUNIIM xi in DataUNIIM)
                {
                    tmpMed.Add(Math.Abs(xi.Data - MEDx));
                }
                U1 = Math.PI/(2*m)*1.483*Statistics.Median(tmpMed)*100.0;

                return U1;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);

            Result.Algorithm = Algorithm.MEDIAN;
            Result.KCRV = Statistics.Median(DataUNIIM.Select(d => d.Data).ToArray());
            Result.BIAS = BIAS(Result.KCRV, Mu);
            Result.S_mu = S_Mu(height, Mu, DataUNIIM);
            Result.U1 = U1(DataUNIIM);



            List<double> KCRVList = new List<double>(width);
            List<double> SDList = new List<double>(height);
            List<double> RowList = new List<double>(width);
            for (int i = 0; i < height; i++)
            {
                RowList.Clear();
                for (int j = 0; j < width; j++)
                {
                    RowList.Add(MCDataset[i, j]);
                }
                KCRVList.Add(Statistics.Median(RowList.ToArray()));
                //SDList.Add(Statistics.StandardDeviation(RowList.ToArray()) / Math.Sqrt(Mu));

            }

            Result.KCRVMC = Statistics.Mean(KCRVList);
            Result.U1MC = Statistics.Mean(SDList);



            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG W_Mean(List<DataUNIIM> DataUNIIM, ref double[,] MCDataset, double Mu)
        {

            double CalcXu(List<DataUNIIM> tmpList)
            {
                double xu = 0;
                double wi = 0;
                double summ = 0;
                foreach (DataUNIIM xi in tmpList)
                {
                    wi = 0;
                    summ = 0;
                    foreach (DataUNIIM ui in tmpList)
                    {
                        summ += 1.0 / Math.Pow(ui.DataStandardDeviation, 2);
                    }
                    wi = 1.0 / Math.Pow(xi.DataStandardDeviation, 2) / summ;
                    xu += wi * xi.Data;
                }
                return xu;
            }

            double U1(List<DataUNIIM> tmpList)
            {
                double U1;
                double summ = 0;
                foreach (DataUNIIM ui in tmpList)
                {
                    summ += 1.0 / Math.Pow(ui.DataStandardDeviation, 2);
                }
                U1 = Math.Sqrt( summ); 
                return U1;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);
            Result.Algorithm = Algorithm.W_MEAN;
            Result.KCRV = CalcXu(DataUNIIM);
            Result.BIAS = BIAS(Result.KCRV, Mu);
            Result.S_mu = S_Mu(height, Mu, DataUNIIM);
            Result.U1 = U1(DataUNIIM);


            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG Dersimonian(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.DERSIMONIAN;
            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG Vaf(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.VAF;

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG HuberH15(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.HUBER_H15;

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG MandelPaule(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.MANDEL_PAULE;

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG PMA1(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.PMA1;

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

        public ResultALG PMA2(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.PMA2;

            stopwatch.Stop();
            Result.Duration = stopwatch.ElapsedMilliseconds;
            return Result;
        }

    }
}
