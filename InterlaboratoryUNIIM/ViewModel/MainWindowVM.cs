using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        [RelayCommand]
        public void Test()
        {
            TestSKO = 100;
        }

        public MainWindowVM()
        {
            StandardDeviation = 0.5;
            PopulateDataset();
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
    }
}

//0.2188854   0.345791208
//- 0.432494344    0.378945736
//0.42853623  0.362241872
//0.006779547 0.4685314

//- 0.047697929    0.391671353
//0.171066582       0.497205367
//0.119200593       0.488584016
//0.331144208       0.301898571
//- 0.209797424    0.445502956
//0.490955443       0.446114113
//- 0.051517448    0.481554809
//0.224016574       0.375322282
//- 0.496617556    0.423091747
//0.011432242       0.348157542
//- 0.145809068    0.384987738
