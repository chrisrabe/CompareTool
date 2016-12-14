using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer
{
    /// <summary>
    /// Contains a collection of tasks which could be retrieved
    /// by the manager. 
    /// </summary>
    public interface ICompareTaskBuffer
    {
        /// <summary>
        /// Sets the tasks inside the buffer.
        /// </summary>
        ICompareTask[] Tasks { set; }

        /// <summary>
        /// Indicates whether or not there are
        /// no more tasks available.
        /// </summary>
        bool Done { get; }

        /// <summary>
        /// Returns the number of tasks in the buffer.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Retrieves the next task available in the
        /// buffer.
        /// </summary>
        ICompareTask Next
        {
            get;
        }
    }
}
