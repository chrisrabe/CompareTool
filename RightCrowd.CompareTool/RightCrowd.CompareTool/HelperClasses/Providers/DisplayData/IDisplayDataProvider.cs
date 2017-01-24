using RightCrowd.CompareTool.Models.Comparison.DataStorage;

namespace RightCrowd.CompareTool.HelperClasses.Providers.DisplayData
{
    /// <summary>
    /// Provides the display data for the given CompareViewModel
    /// </summary>
    public interface IDisplayDataProvider
    {
        CompareViewModel ViewModel { set; }
        
        IComparisonDataStorage ComparisonStorage { set; }
    }
}
