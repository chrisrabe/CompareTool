using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer
{
    /// <summary>
    /// Stores all the comparison tasks inside a buffer. This class
    /// utilises a list to to keep track of all the tasks.
    /// 
    /// It keeps track of a cursor which moves forward whenever the
    /// Next property is called. The cursor doesn't move further than
    /// the end.
    /// </summary>
    public class ListTaskBuffer : ICompareTaskBuffer
    {
        #region Fields

        private ICompareTask[] _tasks;
        private int _cursor;

        #endregion // Fields

        #region Constructor

        public ListTaskBuffer()
        {
            _cursor = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns true if the cursor is not at the end of the buffer's list.
        /// </summary>
        public bool Done
        {
            get
            {
                return _tasks == null || _cursor >= _tasks.Length;
            }
        }

        /// <summary>
        /// Retrieves the next task at the cursor position. If the cursor is
        /// at the end, then it returns null.
        /// </summary>
        public ICompareTask Next
        {
            get
            {
                if (Done)
                    return null;
                else
                {
                    ICompareTask task = _tasks[_cursor];
                    _cursor++;
                    return task;
                }
            }
        }

        public ICompareTask[] Tasks
        {
            set
            {
                if (_tasks != value)
                    _tasks = value;
            }
        }

        #endregion // Properties
    }
}
