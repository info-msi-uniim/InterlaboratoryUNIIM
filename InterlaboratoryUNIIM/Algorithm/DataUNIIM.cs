using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
using CommunityToolkit.Mvvm.ComponentModel;

namespace InterlaboratoryUNIIM.Algorithm
{
    public enum Algorithm
    {
        MEAN,
        MEDIAN,
        W_MEAN,
        DERSIMONIAN,
        VAF,
        HUBER_H15,
        MANDEL_PAULE,
        PMA1,
        PMA2
    }

    public partial class DataUNIIM : ObservableValidator
    {
        [ObservableProperty]
        [Required]
        private string _ParticipantName = "Lab.";
        [ObservableProperty]
        [Required]
        private double _Data;
        [ObservableProperty]
        [Required]
        private double _DataStandardDeviation;
    }

    public partial class ResultALG : ObservableObject
    {
        [ObservableProperty]
        private Algorithm _Algorithm;
        [ObservableProperty]
        private double _S_mu;
        [ObservableProperty]
        private double _KCRV;
        [ObservableProperty]
        private double _U1;
        [ObservableProperty]
        private double _BIAS;
        [ObservableProperty]
        private double _KCRVMC;
        [ObservableProperty]
        private double _U1MC;
        [ObservableProperty]
        private long _Duration;
    }


    public partial class DatasetUNIIM : ObservableObject
    {
        public ObservableCollection<DataUNIIM> DataSet;

        public DatasetUNIIM()
        {
            DataSet = new ObservableCollection<DataUNIIM>
            {
                new DataUNIIM { ParticipantName = "Lab№ 1", Data = 10.90151205, DataStandardDeviation = 0.345791208 },
                new DataUNIIM { ParticipantName = "Lab№ 2", Data = 9.138607881, DataStandardDeviation = 0.378945736 },
                new DataUNIIM { ParticipantName = "Lab№ 3", Data = 10.99768767, DataStandardDeviation = 0.362241872 },
                new DataUNIIM { ParticipantName = "Lab№ 4", Data = 9.257577411, DataStandardDeviation = 0.362241872 },
                new DataUNIIM { ParticipantName = "Lab№ 5", Data = 10.68539936, DataStandardDeviation = 0.391671353 },
                new DataUNIIM { ParticipantName = "Lab№ 6", Data = 9.574884192, DataStandardDeviation = 0.497205367 },
                new DataUNIIM { ParticipantName = "Lab№ 7", Data = 10.53315789, DataStandardDeviation = 0.488584016 },
                new DataUNIIM { ParticipantName = "Lab№ 8", Data = 9.814729512, DataStandardDeviation = 0.301898571 },
                new DataUNIIM { ParticipantName = "Lab№ 9", Data = 9.386349086, DataStandardDeviation = 0.445502956 },
                new DataUNIIM { ParticipantName = "Lab№ 10", Data =10.27886707, DataStandardDeviation = 0.446114113 },
                new DataUNIIM { ParticipantName = "Lab№ 11", Data =10.07133607, DataStandardDeviation = 0.481554809 },
                new DataUNIIM { ParticipantName = "Lab№ 12", Data =10.10511262, DataStandardDeviation = 0.375322282 },
                new DataUNIIM { ParticipantName = "Lab№ 13", Data =9.565120967, DataStandardDeviation = 0.423091747 },
                new DataUNIIM { ParticipantName = "Lab№ 14", Data =9.411403223, DataStandardDeviation = 0.348157542 },
                new DataUNIIM { ParticipantName = "Lab№ 15", Data =9.702089774, DataStandardDeviation = 0.384987738 }
            };
        }
    }
}
