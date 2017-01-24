using RightCrowd.CompareTool.HelperClasses.CompareTask.Filter;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.HelperClasses.Providers.DisplayData
{
    public class DisplayDataProvider : IDisplayDataProvider
    {
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
                        IDisplayData db1Results = _filter.FilterStorage(0, value);
                        IDisplayData db2Results = _filter.FilterStorage(1, value);
                        ViewModel.DB1Differences = db1Results.Differences;
                        ViewModel.DB1Similarities = db1Results.Similarities;
                        ViewModel.DB2Differences = db2Results.Differences;
                        ViewModel.DB2Similarities = db2Results.Similarities;
                    }
                }
            }
        }

        public CompareViewModel ViewModel { get; set; }
    }
}
