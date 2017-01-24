using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Filter;

namespace RightCrowd.CompareTool.HelperClasses.Providers.CompareData
{
    /// <summary>
    /// This class is responsible for caching the compare data.
    /// </summary>
    internal class CompareDataProvider : ObservableObject, ICompareDataProvider
    {
        #region  Fields

        private IComparisonDataStorage _storage;

        private IComparisonFilter _filter;

        #endregion // Fields

        #region Constructors

        public CompareDataProvider()
        {
            _filter = new ComparisonFilter();
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the comparison data storage.
        /// </summary>
        public IComparisonDataStorage ComparisonStorage
        {
            get
            {
                if (_storage == null)
                    ComparisonStorage = new ComparisonDataStorage();

                return _storage;
            }

            set
            {
                if (_storage != value)
                {
                    _storage = value;
                    OnPropertyChanged("ComparisonStorage");
                }
            }
        }
        #endregion // Properties
    }
}
