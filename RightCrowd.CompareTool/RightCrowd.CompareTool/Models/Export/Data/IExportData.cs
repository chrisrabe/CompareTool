using RightCrowd.CompareTool.Models.Export.Node;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Export.Data
{
    /// <summary>
    /// Contains the filtered data for database one and database two.
    /// </summary>
    public interface IExportData
    {
        /// <summary>
        /// Filtered data of the comparison result for database one.
        /// </summary>
        ObservableCollection<IExportNode> DatabaseOneData { get; set; }

        /// <summary>
        /// Filtered data of the comparison result for database two.
        /// </summary>
        ObservableCollection<IExportNode> DatabaseTwoData { get; set; }
    }
}
