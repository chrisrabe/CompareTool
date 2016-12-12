using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Comparison.Data;

namespace RightCrowd.CompareTool.Models.Comparison.DataStorage
{
    /// <summary>
    /// Stores all the comparison data.
    /// </summary>
    public class ComparisonDataStorage : IComparisonDataStorage
    {
        #region Fields
        private ObservableCollection<IComparisonData> _comparisonData;

        #endregion

        #region IComparisonDataStorage Properties

        /// <summary>
        /// Gets or sets the comparison data.
        /// </summary>
        public ObservableCollection<IComparisonData> ComparisonData
        {
            get
            {
                if (_comparisonData == null)
                    _comparisonData = new ObservableCollection<IComparisonData>();

                return _comparisonData;
            }

            set
            {
                if (_comparisonData != value)
                    _comparisonData = value;
            }
        }

        #endregion
    }
}
