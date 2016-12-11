namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Load
{
    /// <summary>
    /// Provides method contracts for load event handlers
    /// </summary>
    public interface ILoadEventHandler
    {
        /// <summary>
        /// Loads the directory in a database. The database created 
        /// will be stored at the given index inside the database storage.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="databaseIndex"></param>
        void LoadDirectory(string directoryPath, int databaseIndex);
    }
}
