using System.Collections.Generic;

#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace InterlaboratoryUNIIM.Algorithm
{
    public partial class DataUNIIM : ObservableObject
    {
        [ObservableProperty]
        private double _DataDelta;
        [ObservableProperty]
        private double _DataStandardDeviation;
    }


    public partial class DatasetUNIIM : ObservableObject
    {
        public List<DataUNIIM> DataSet;

        public DatasetUNIIM()
        {
            DataSet = new List<DataUNIIM>
            {
                new DataUNIIM { DataDelta = 0.2188854, DataStandardDeviation = 0.345791208 },
                new DataUNIIM { DataDelta = -0.432494344, DataStandardDeviation = 0.378945736 },
                new DataUNIIM { DataDelta = 0.42853623, DataStandardDeviation = 0.362241872 },
                new DataUNIIM { DataDelta = 0.006779547, DataStandardDeviation = 0.362241872 },
                new DataUNIIM { DataDelta = -0.047697929, DataStandardDeviation = 0.391671353 },
                new DataUNIIM { DataDelta = 0.171066582, DataStandardDeviation = 0.497205367 },
                new DataUNIIM { DataDelta = 0.119200593, DataStandardDeviation = 0.488584016 },
                new DataUNIIM { DataDelta = 0.331144208, DataStandardDeviation = 0.301898571 },
                new DataUNIIM { DataDelta = -0.209797424, DataStandardDeviation = 0.445502956 },
                new DataUNIIM { DataDelta = 0.490955443, DataStandardDeviation = 0.446114113 },
                new DataUNIIM { DataDelta = -0.051517448, DataStandardDeviation = 0.481554809 },
                new DataUNIIM { DataDelta = 0.224016574, DataStandardDeviation = 0.375322282 },
                new DataUNIIM { DataDelta = -0.496617556, DataStandardDeviation = 0.423091747 },
                new DataUNIIM { DataDelta = 0.011432242, DataStandardDeviation = 0.348157542 },
                new DataUNIIM { DataDelta = -0.145809068, DataStandardDeviation = 0.384987738 }
            };
        }

    }

    public partial class ResultALG : ObservableObject
    {
        [ObservableProperty]
        private double _KCRV;
        [ObservableProperty]
        private double _U;
    }
}
