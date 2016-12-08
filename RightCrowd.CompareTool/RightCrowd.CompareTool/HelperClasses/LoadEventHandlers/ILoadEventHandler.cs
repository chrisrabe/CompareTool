using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.LoadEventHandlers
{
    /// <summary>
    /// This interface provides method for handling load events.
    /// </summary>
    public interface ILoadEventHandler
    {
        /// <summary>
        /// This class should open a directory chooser dialog 
        /// and then convert the files inside the directory into an 'IDatabase' object.
        /// </summary>
        /// <returns></returns>
        IDatabase LoadDirectory();
    }
}
