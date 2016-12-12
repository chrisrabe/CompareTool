using RightCrowd.CompareTool.Models.Compare.Task;
using System.ComponentModel;

namespace RightCrowd.CompareTool.Models.Compare.Data.Collector
{
    /// <summary>
    /// Collects the data from one compare task.
    /// </summary>
    public interface ICompareDataCollector
    {
        /// <summary>
        /// Sets the worker for the data collector.
        /// </summary>
        BackgroundWorker Worker { set; }

        /// <summary>
        /// Collects the 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        ICompareData CollectData(ICompareTask task);
    }
}
