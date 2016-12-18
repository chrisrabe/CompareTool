using RightCrowd.CompareTool.Models.Comparison.DataStorage;

namespace RightCrowd.CompareTool.HelperClasses.Providers.CompareData
{
    /// <summary>
    /// This class is responsible for caching the compare data.
    /// </summary>
    internal class CompareDataProvider : ICompareDataProvider
    {
        #region  Fields

        private IComparisonDataStorage _storage;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// Gets or sets the comparison data storage.
        /// </summary>
        public IComparisonDataStorage ComparisonStorage
        {
            get
            {
                return _storage;
            }

            set
            {
                if (_storage != value)
                    _storage = value;
            }
        }

        #endregion // Properties
    }
}
