using RightCrowd.CompareTool.Models.Comparison.Data;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Manager
{
    /// <summary>
    /// This class is responsible for assigning tasks to the
    /// task workers.
    /// </summary>
    public interface ICompareTaskManager
    {

        /// <summary>
        /// Compares all the data partitions and returns comparison data.
        /// </summary>
        /// <param name="partitions"></param>
        /// <returns></returns>
        IComparisonDataStorage Compare(params IMapDatabaseStorage[] partitions);

        /// <summary>
        /// Reports to the event handler that it has completed its task.
        /// </summary>
        void SubmitData(IComparisonData data);
    }
}
