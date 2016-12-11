using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Xml.Linq;

namespace RightCrowd.CompareTool.HelperClasses.Readers.XML
{
    /// <summary>
    /// This class is responsible for parsing and reading XML files.
    /// </summary>
    public interface IXMLReader
    {
        /// <summary>
        /// Parses the XML file and returns the contents inside a data node. 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        IDataNode ReadXMLFile(string filename);
    }
}
