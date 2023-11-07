using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InterlaboratoryUNIIM.Algorithm;
#nullable disable
namespace InterlaboratoryUNIIM.ViewModel
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        private string _Title = "Расчет сличений";
        [ObservableProperty]
        private int _NumOfParticipant;
        [ObservableProperty]
        private double _StandardDeviation;
        [ObservableProperty]
        private double _Mu;
        [ObservableProperty]
        private int _NumOfIteration;

        #region Начальный датасет

        [ObservableProperty]
        private ObservableCollection<DataUNIIM> _DataSet;

        [ObservableProperty]
        private ObservableCollection<ResultALG> _ResultALGs;
        #endregion

        [RelayCommand]
        public void CalculateALL()
        {
            ResultALGs.Clear();
            int CountIteration = NumOfIteration;
            int NumParticipant = NumOfParticipant;
            double SD = StandardDeviation;
            double[,] MCDataset = new ArrayMC().Get(DataSet.ToList(), CountIteration, SD);

            ResultALGs.Add(new Algorithms().Mean(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().W_Mean(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().Median(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().DerSimonian(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().Vaf(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().HuberH15(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().MandelPaule(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().PMA1(DataSet.ToList(), ref MCDataset, Mu));
            ResultALGs.Add(new Algorithms().PMA2(DataSet.ToList(), ref MCDataset, Mu));
        }

        public MainWindowVM()
        {
            StandardDeviation = 0.5;
            NumOfIteration = 10000;
            Mu = 10;
            DataSet = new DatasetUNIIM().DataSet;
            NumOfParticipant = DataSet.Count;
            ResultALGs = new();

            DataSet.CollectionChanged += DataSet_CollectionChanged;

            CalculateALL();
        }

        private void DataSet_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NumOfParticipant = DataSet.Count;
        }
    }
}