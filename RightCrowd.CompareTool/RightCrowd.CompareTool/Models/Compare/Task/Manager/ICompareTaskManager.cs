using RightCrowd.CompareTool.Models.Compare.Results;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map;
using System.ComponentModel;

namespace RightCrowd.CompareTool.Models.Compare.Task.Manager
{
    /// <summary>
    /// This class is responsible for assigning a background worker to a task.
    /// </summary>
    public interface ICompareTaskManager
    {
        /// <summary>
        /// Assigns the workers for the task manager.
        /// </summary>
        BackgroundWorker[] Workers { set; }

        /// <summary>
        /// Compares the partitions and returns the comparison results.
        /// </summary>
        /// <param name="partitions"></param>
        /// <returns></returns>
        ICompareResults Compare(IMapDatabaseStorage partitions);
    }
}
