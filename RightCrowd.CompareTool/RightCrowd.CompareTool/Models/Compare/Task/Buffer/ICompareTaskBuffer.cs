using System.Runtime.CompilerServices;

namespace RightCrowd.CompareTool.Models.Compare.Task.Buffer
{
    /// <summary>
    /// Stores all the tasks inside a collection of some sort. 
    /// Worker threads can then retrieve the tasks from the 
    /// buffer using the 'Next' property.
    /// 
    /// Since multiple threads will be interacting with the
    /// 'Next' property, the method for retrieving the value
    /// is synchronized to avoid unintended errors or unexpected
    /// results caused by threads calling that method at the same time.
    /// </summary>
    public interface ICompareTaskBuffer
    {
        /// <summary>
        /// Adds a new task into the buffer
        /// </summary>
        /// <param name="task"></param>
        void Add(ICompareTask task);

        /// <summary>
        /// Stores an array of tasks in the buffer.
        /// </summary>
        ICompareTask[] Tasks { set; }
        
        /// <summary>
        /// Retrieves the next task inside the buffer.
        /// </summary>
        ICompareTask Next {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get; // Stops threads from grabbing tasks at the same time.
        }
    }
}
