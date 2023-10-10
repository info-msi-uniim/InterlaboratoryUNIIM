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
            //R260623 1 набор
            DataSet = new ObservableCollection<DataUNIIM>
            {
                new DataUNIIM { ParticipantName = "Lab№ 1", Data = 10.90151205, DataStandardDeviation = 0.132532719692812 },
                new DataUNIIM { ParticipantName = "Lab№ 2", Data = 9.138607881, DataStandardDeviation = 0.446764280961344 },
                new DataUNIIM { ParticipantName = "Lab№ 3", Data = 10.99768767, DataStandardDeviation = 0.245026288892717 },
                new DataUNIIM { ParticipantName = "Lab№ 4", Data = 9.257577411, DataStandardDeviation = 0.304118124651742 },
                new DataUNIIM { ParticipantName = "Lab№ 5", Data = 10.68539936, DataStandardDeviation = 0.491369076104076 },
                new DataUNIIM { ParticipantName = "Lab№ 6", Data = 9.574884192, DataStandardDeviation = 0.329503520348947 },
                new DataUNIIM { ParticipantName = "Lab№ 7", Data = 10.53315789, DataStandardDeviation = 0.447325015218289 },
                new DataUNIIM { ParticipantName = "Lab№ 8", Data = 9.814729512, DataStandardDeviation = 0.195366595921181 },
                new DataUNIIM { ParticipantName = "Lab№ 9", Data = 9.386349086, DataStandardDeviation = 0.430269353870719 },
                new DataUNIIM { ParticipantName = "Lab№ 10", Data =10.27886707, DataStandardDeviation = 0.219921984054786 },
                new DataUNIIM { ParticipantName = "Lab№ 11", Data =10.07133607, DataStandardDeviation = 0.425213464742128 },
                new DataUNIIM { ParticipantName = "Lab№ 12", Data =10.10511262, DataStandardDeviation = 0.356039948651286},
                new DataUNIIM { ParticipantName = "Lab№ 13", Data =9.565120967, DataStandardDeviation = 0.390237603089864 },
                new DataUNIIM { ParticipantName = "Lab№ 14", Data =9.411403223, DataStandardDeviation = 0.294589563710668 },
                new DataUNIIM { ParticipantName = "Lab№ 15", Data =9.702089774, DataStandardDeviation = 0.338338440218366 }
            };
        }
    }
}
