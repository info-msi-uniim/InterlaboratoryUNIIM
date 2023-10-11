using System;
#nullable disable
using TridentGoalSeek;

namespace InterlaboratoryUNIIM.Algorithm
{
    public class GSExample
    {
        public decimal Result;

        public GSExample()
        {
            var myAlgorithm = new MyAlgorithm(90463.45M, 200);
            var goalSeeker = new GoalSeek(myAlgorithm);
            var seekResult = goalSeeker.SeekResult(96178.21M);
            Result = (decimal)seekResult.InputVariable;
        }

        internal class MyAlgorithm : IGoalSeekAlgorithm
        {
            public MyAlgorithm(decimal v1, int v2)
            {
                V1 = v1;
                V2 = v2;
            }

            public decimal V1 { get; }
            public int V2 { get; }

            public decimal Calculate(decimal inputVariable)
            {
                return V1 + V2 * inputVariable;
            }
        }
    }
}
