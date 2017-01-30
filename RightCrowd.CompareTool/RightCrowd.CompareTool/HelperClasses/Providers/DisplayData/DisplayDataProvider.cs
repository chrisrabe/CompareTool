using RightCrowd.CompareTool.HelperClasses.CompareTask.Filter;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.HelperClasses.Providers.DisplayData
{
    public class DisplayDataProvider : ObservableObject, IDisplayDataProvider
    {
        private IDisplayData _db1Results;
        private IDisplayData _db2Results;
        private IComparisonDataStorage _comparisonStorage;
        private IComparisonFilter _filter;


        public DisplayDataProvider()
        {
            _filter = new ComparisonFilter();
        }

        public IComparisonDataStorage ComparisonStorage
        {
            set
            {
                if(_comparisonStorage != value)
                {
                    _comparisonStorage = value;
                    if(ViewModel != null)
                    {
                        DB1Results = _filter.FilterStorage(0, value);
                        DB2Results = _filter.FilterStorage(1, value);
                        ViewModel.DB1Differences = DB1Results.Differences;
                        ViewModel.DB1Similarities = DB1Results.Similarities;
                        ViewModel.DB2Differences = DB2Results.Differences;
                        ViewModel.DB2Similarities = DB2Results.Similarities;
                    }
                }
            }
        }

        public CompareViewModel ViewModel { get; set; }

        public IDisplayData DB1Results
        {
            get { return _db1Results; }
            set
            {
                if(_db1Results != value)
                {
                    _db1Results = value;
                    OnPropertyChanged("DB1Results");
                }
            }
        }

        public IDisplayData DB2Results
        {
            get { return _db2Results; }
            set
            {
                if(_db2Results != value)
                {
                    _db2Results = value;
                    OnPropertyChanged("DB2Results");
                }
            }
        }
    }
}
