using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;

namespace RightCrowd.CompareTool.HelperClasses.Builders.TaskBuffer
{
    /// <summary>
    /// This class creates a list task buffer from all the tasks stored.
    /// </summary>
    internal class ListTaskBufferBuilder : ICompareTaskBufferBuilder
    {
        #region Fields

        private List<ICompareTask> _tasks;

        #endregion // Fields

        #region Constructor

        public ListTaskBufferBuilder()
        {
            _tasks = new List<ICompareTask>();
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Builds the list buffer from the tasks stored.
        /// </summary>
        public ICompareTaskBuffer Buffer
        {
            get
            {
                // Create the task buffer...
                ICompareTaskBuffer buffer = new ListTaskBuffer();
                buffer.Tasks = _tasks.ToArray();
                // Return it
                return buffer;
            }
        }

        public void Add(ICompareTask task)
        {
            _tasks.Add(task);
        }

        #endregion // Properties
    }
}
