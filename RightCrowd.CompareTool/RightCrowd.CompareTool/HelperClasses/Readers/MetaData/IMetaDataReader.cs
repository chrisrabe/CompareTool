using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;

namespace RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReader
{
    /// <summary>
    /// This class reads the meta data xml files.
    /// </summary>
    public interface IMetaDataReader
    {
        /// <summary>
        /// Reads the meta data and returns a meta data object.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IMetaData ReadMetaDataFile(string file);
    }
}
