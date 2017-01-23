using System;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Partition;
using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Filter;

namespace RightCrowd.CompareTool.HelperClasses.Providers.CompareData
{
    /// <summary>
    /// This class is responsible for caching the compare data.
    /// </summary>
    internal class CompareDataProvider : ObservableObject, ICompareDataProvider
    {
        #region  Fields

        private IDisplayPartition _database1;
        private IDisplayPartition _database2;
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
                    DatabaseOne = _filter.FilterStorage(0, value);
                    DatabaseTwo = _filter.FilterStorage(1, value);
                    OnPropertyChanged("ComparisonStorage");
                }
            }
        }

        public IDisplayPartition DatabaseOne
        {
            get
            {
                if (_database1 == null)
                    _database1 = new DisplayPartition(new DisplayData(null), new DisplayData(null));

                return _database1;
            }
            private set
            {
                if (_database1 != value)
                {
                    _database1 = value;
                    OnPropertyChanged("DatabaseOne");
                }
            }
        }

        public IDisplayPartition DatabaseTwo
        {
            get
            {
                if (_database2 == null)
                    _database2 = new DisplayPartition(new DisplayData(null), new DisplayData(null));

                return _database2;
            }
            private set
            {
                if (_database2 != value)
                {
                    _database2 = value;
                    OnPropertyChanged("DatabaseTwo");
                }
            }
        }

        #endregion // Properties
    }
}
