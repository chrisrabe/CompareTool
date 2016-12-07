using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.Readers.DirectoryReader
{
    /// <summary>
    /// This class is responsible for placing data from the files inside the directory into a IDatabase object.
    /// </summary>
    public interface IDirectoryReader
    {
        /// <summary>
        /// Reads through each file inside the folder specified by the directory path parameter. 
        /// It stores the information inside the files of the folder inside an IDatabase. 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        IDatabase ReadDirectory(string directoryPath);
    }
}
