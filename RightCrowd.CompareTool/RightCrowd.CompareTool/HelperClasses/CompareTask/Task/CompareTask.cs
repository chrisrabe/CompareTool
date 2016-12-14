using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Task
{
    /// <summary>
    /// This represents a task which holds an array of databases to compare.
    /// </summary>
    public class CompareTask : ICompareTask
    {
        #region Fields

        private IDatabase[] _databases;
        private string _type;

        #endregion // Fields

        #region Contructor

        /// <summary>
        /// Creates an instance of the compare task.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="databases"></param>
        public CompareTask(string type, params IDatabase[] databases)
        {
            _type = type;
            Databases = databases;
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Databases to be compared
        /// </summary>
        public IDatabase[] Databases
        {
            get
            {
                return _databases;
            }

            set
            {
                if (_databases != value)
                    _databases = value;
            }
        }

        /// <summary>
        /// The type of nodes which this task stores.
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
        }

        #endregion // Properties
    }
}
