using RightCrowd.CompareTool.Models.Comparison.DataStorage;

namespace RightCrowd.CompareTool.HelperClasses.Providers.CompareData
{
    /// <summary>
    /// This class is responsible for storage the comparison data.
    /// </summary>
    public interface ICompareDataProvider
    {
        /// <summary>
        /// Gets or sets the comparison storage inside the data provider.
        /// </summary>
        IComparisonDataStorage ComparisonStorage { get; set; }
    }
}
