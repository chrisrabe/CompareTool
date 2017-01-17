using RightCrowd.CompareTool.HelperClasses.ValueConversion;

namespace RightCrowd.CompareTool.HelperClasses.Readers.ConversionTableReaders
{
    /// <summary>
    /// This class is responsible for reading the XML data for the value conversion
    /// </summary>
    public interface IConversionTableReader
    {
        /// <summary>
        /// Reads the XML file indicated by the string parameter. 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        IConversionTable ReadConversionTableFile(string file);
    }
}
