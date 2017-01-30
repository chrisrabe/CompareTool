using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.HelperClasses.Providers.DisplayData
{
    /// <summary>
    /// Provides the display data for the given CompareViewModel
    /// </summary>
    public interface IDisplayDataProvider
    {
        CompareViewModel ViewModel { set; }
        
        IComparisonDataStorage ComparisonStorage { set; }

        IDisplayData DB1Results { get; set;}

        IDisplayData DB2Results { get; set; }
    }
}
