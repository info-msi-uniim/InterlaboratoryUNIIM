#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;

using TridentGoalSeek;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class GSExample
    {
        public decimal? Result;

        public GSExample()
        {
            var myAlgorithm = new MyAlgorithm(new DatasetUNIIM().DataSet.ToList());
            var GoalOption = new GoalSeekOptions(100, 90, 5, 0, 1, 1000, false);
            var goalSeeker = new GoalSeek(myAlgorithm);
            var seekResult = goalSeeker.SeekResult(14, GoalOption);
            Result = seekResult.OutputValue;

        }

        internal class MyAlgorithm : IGoalSeekAlgorithm
        {
            public MyAlgorithm(List<DataUNIIM> Data)
            {
                ExternalData = Data;
            }
            public List<DataUNIIM> ExternalData;

            public decimal Calculate(decimal inputVariable)
            {
                return (decimal)FlCalculate(ExternalData, inputVariable);
            }

            public double FlCalculate(List<DataUNIIM> Data, decimal Lambda)
            {
                int m = Data.Count;
                double l = Decimal.ToDouble(Lambda);

                double Fl = 0d;
                double Xi = 0d;
                double Xj = 0d;
                double Ui = 0d;
                double Uj = 0d;
                double Uk = 0d;
                double S1 = 0d;
                double S2 = 0d;

                for (int i = 0; i < m; i++)
                {
                    Xi = Data[i].Data;
                    Ui = Data[i].DataStandardDeviation;
                    S1 = 0d;
                    for (int j = 0; j < m; j++)
                    {
                        Xj = Data[j].Data;
                        Uj = Data[j].DataStandardDeviation;

                        S2 = 0d;
                        for (int k = 0; k < m; k++)
                        {
                            Uk = Data[k].DataStandardDeviation;
                            S2 += Math.Pow((Uk * Uk - l), -1);
                        }

                        S1 += Math.Pow(Uj * Uj + l, -1) * Xj / S2;
                    }

                    Fl += Math.Pow((S1 - Xi), 2) / (Ui * Ui + l);
                }
                //  Debug.WriteLine(Fl.ToString());
                return Fl;
            }
        }
    }
}
