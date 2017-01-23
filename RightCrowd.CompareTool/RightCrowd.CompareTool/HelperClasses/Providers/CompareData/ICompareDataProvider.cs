using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Partition;

namespace RightCrowd.CompareTool.HelperClasses.Providers.CompareData
{
    /// <summary>
    /// This class is responsible for storage the comparison data.
    /// </summary>
    public interface ICompareDataProvider
    {
        IDisplayPartition DatabaseOne { get; }

        IDisplayPartition DatabaseTwo { get; }

        /// <summary>
        /// Gets or sets the comparison storage inside the data provider.
        /// </summary>
        IComparisonDataStorage ComparisonStorage { get; set; }
    }
}
