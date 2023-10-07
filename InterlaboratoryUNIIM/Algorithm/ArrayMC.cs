using System;
using System.Collections.Generic;

using MathNet.Numerics.Distributions;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class ArrayMC
    {
        public double[,] Get(List<DataUNIIM> DataSet, int CountIteration, double StandardDeviation)
        {
            Random rnd = new Random();
            int count = DataSet.Count;
            double[,] MCDataset = new double[CountIteration, count];

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < CountIteration; i++)
                {
                    MCDataset[i, j] = Normal.InvCDF(DataSet[j].Data, StandardDeviation, rnd.NextDouble());
                }
            }
            return MCDataset;
        }
    }
}
