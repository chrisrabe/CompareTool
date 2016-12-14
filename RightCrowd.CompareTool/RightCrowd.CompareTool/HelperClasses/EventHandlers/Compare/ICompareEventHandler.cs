using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare
{
    /// <summary>
    /// This class is responsible for handling comparison events.
    /// </summary>
    public interface ICompareEventHandler
    {
        /// <summary>
        /// Compares the databases inside the list storage and then
        /// returns some comparison data. Each comparison data
        /// should contain a database storage containing all the nodes
        /// which are the same in both databases and another database
        /// storage containing all the nodes which are different from
        /// both databases.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        IComparisonDataStorage Compare(IListDatabaseStorage storage);

        /// <summary>
        /// Reports its comparison progress to the view model.
        /// </summary>
        void ReportProgress(int progress);
    }
}
