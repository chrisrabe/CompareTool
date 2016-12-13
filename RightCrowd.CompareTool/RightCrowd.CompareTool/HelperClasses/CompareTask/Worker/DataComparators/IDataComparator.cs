using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers;
using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators
{
    /// <summary>
    /// This interface provides method contracts needed for comparing data models.
    /// </summary>
    public interface IDataComparator
    {
        /// <summary>
        /// Retrieves the data handler for this comparator
        /// </summary>
        IDataHandler Handler { get; set; }

        /// <summary>
        /// This method compares the nodes of the database at index 1 to the nodes
        /// of the database at index2.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="databases"></param>
        void Compare(int index1, int index2, params IDatabase[] databases);
    }
}
