using RightCrowd.CompareTool.Models.Export.Configurations;
using RightCrowd.CompareTool.Models.Export.Data;

namespace RightCrowd.CompareTool.HelperClasses.ExportHelpers.FileConverter
{
    /// <summary>
    /// This class converts the export data into another file format.
    /// E.g txt, html, xml etc...
    /// </summary>
    public interface IFileConverter
    {
        /// <summary>
        /// Writes the export data to another file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="data"></param>
        void ToFile(string filePath, IExportData data);
    }
}
