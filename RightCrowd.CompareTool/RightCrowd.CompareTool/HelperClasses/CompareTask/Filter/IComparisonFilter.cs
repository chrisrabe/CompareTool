using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Filter
{
    /// <summary>
    /// Filters the data so that the differences and similarities are separated.
    /// </summary>
    internal interface IComparisonFilter
    {
        /// <summary>
        /// Separates all of the differences from the similarities. Also ignores
        /// the collections with no elements inside it.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="parseDifference"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        IDisplayData FilterStorage(int database, IComparisonDataStorage storage);
    }
}
