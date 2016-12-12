using RightCrowd.CompareTool.Models.Compare.Results;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare
{
    /// <summary>
    /// This class is responsible for handling comparison events.
    /// </summary>
    public interface ICompareEventHandler
    {
        /// <summary>
        /// Compares the databases stored inside the list database storage.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        ICompareResults Compare(IListDatabaseStorage storage);
    }
}
