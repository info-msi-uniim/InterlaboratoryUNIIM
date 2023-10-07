using System;
using System.Collections.Generic;

using MathNet.Numerics.Distributions;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class ArrayMC
    {
        public double[,] Get(List<DataUNIIM> DataSet, int CountIteration, double StandardDeviation)
        {
            DataUNIIM d;
            Random rnd = new Random();
            int count = DataSet.Count;
            double[,] MCDataset = new double[CountIteration, count];

            for (int j = 0; j < count; j++)
            {
                d = DataSet[j];
                for (int i = 0; i < CountIteration; i++)
                {
                    MCDataset[i, j] = Normal.InvCDF(d.Data, StandardDeviation, rnd.NextDouble());
                }
            }
            return MCDataset;
        }
    }
}
