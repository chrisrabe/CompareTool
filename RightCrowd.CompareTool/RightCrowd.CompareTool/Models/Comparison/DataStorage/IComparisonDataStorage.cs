using RightCrowd.CompareTool.Models.Comparison.Data;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Comparison.DataStorage
{
    /// <summary>
    /// Stores all the compared data.
    /// </summary>
    public interface IComparisonDataStorage
    {
        /// <summary>
        /// Gets or sets a collection of comparison data.
        /// </summary>
        ObservableCollection<IComparisonData> ComparisonData { get; set; }
    }
}
