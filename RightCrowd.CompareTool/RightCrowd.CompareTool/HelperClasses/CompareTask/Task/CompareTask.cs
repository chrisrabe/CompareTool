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

        #endregion // Fields

        #region Contructor

        public CompareTask(params IDatabase[] databases)
        {
            Databases = databases;
        }

        #endregion // Constructor

        #region Properties

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

        #endregion // Properties
    }
}
