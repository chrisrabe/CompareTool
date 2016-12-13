using RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;

namespace RightCrowd.CompareTool.HelperClasses.Builders.TaskBuffer
{
    /// <summary>
    /// This class stores all the compare task objects and then provides a
    /// method which returns an instance of a compare task buffer.
    /// </summary>
    public interface ICompareTaskBufferBuilder
    {
        /// <summary>
        /// Adds a task into the buffer builder
        /// </summary>
        /// <param name="task"></param>
        void Add(ICompareTask task);

        /// <summary>
        /// Builds and retrieves the buffer.
        /// </summary>
        ICompareTaskBuffer Buffer { get; }
    }
}
