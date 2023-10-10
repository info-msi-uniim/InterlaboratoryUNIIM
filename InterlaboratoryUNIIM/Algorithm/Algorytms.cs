using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MathNet.Numerics.Statistics;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class Algorithms
    {
        public double BIAS(double KCRV, double Mu)
        {
            return Math.Abs(Mu - KCRV) / Mu * 100;
        }
        public double S_Mu(double KCRV, double Mu, List<DataUNIIM> Data)
        {
            double Result;
            double Summ = 0;
            foreach (var item in Data)
            {
                Summ += Math.Pow((item.Data - Mu), 2);
            }
            Result = 1 / Mu * Math.Sqrt(Summ) / (Data.Count - 1) * 100;

            return Result;
        }

        public ResultALG Mean(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.MEAN;
            Result.KCRV = Statistics.Mean(Data.Select(d => d.Data).ToArray());
            Result.U1 = Statistics.StandardDeviation(Data.Select(d => d.Data).ToArray());
            Result.BIAS = BIAS(Result.KCRV, Mu);
            Result.S_mu = S_Mu(Result.KCRV, Mu, Data);

            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);
            //List<double> ArrayList = new List<double>(width * height);
            List<double> KCRVList = new List<double>(width);
            List<double> SDList = new List<double>(height);
            List<double> RowList = new List<double>(width);

            for (int i = 0; i < height; i++)
            {
                RowList.Clear();
                for (int j = 0; j < width; j++)
                {
                    //  ArrayList.Add(MCDataset[i, j]);
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

        public ResultALG Median(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            Result.Algorithm = Algorithm.MEDIAN;
            Result.KCRV = Statistics.Median(Data.Select(d => d.Data).ToArray());
            Result.BIAS = BIAS(Result.KCRV, Mu);
            Result.S_mu = S_Mu(Result.KCRV, Mu, Data);
            // Result.U1 = Statistics.StandardDeviation(Data.Select(d => d.Data).ToArray());


            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);
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

        public ResultALG W_Mean(List<DataUNIIM> Data, ref double[,] MCDataset, double Mu)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ResultALG Result = new ResultALG();
            int width = MCDataset.GetLength(1);
            int height = MCDataset.GetLength(0);
            Result.Algorithm = Algorithm.W_MEAN;

            //List<double> RowList = new List<double>(width);

            void Calc_W(List<double> Data, List<double> SD)
            {
                double Wi;
                double Summ;


                foreach (double Xi in Data)
                {
                    Wi = 0;
                    Summ = 0;
                    foreach (double Ui in SD)
                    {
                        Summ += 1 / Math.Pow(Ui, 2);
                    }
                    Wi = 1 /Data.IndexOf(Xi);
                }


            }


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
