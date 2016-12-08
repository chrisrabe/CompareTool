using RightCrowd.CompareTool.Models.DataModels.Database;
using System.IO;
using RightCrowd.CompareTool.HelperClasses.Readers.XMLReaders;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;

namespace RightCrowd.CompareTool.HelperClasses.Readers.DirectoryReaders
{
    internal class DirectoryReader : IDirectoryReader
    {
        #region IDirectoryReader Members

        /// <summary>
        /// Reads each file of the directory and passes it down to the XML Reader.
        /// This requires all files in the directory to be XML files.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IDatabase ReadDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            ObservableCollection<IDataNode> data = new ObservableCollection<IDataNode>();
            IXMLReader xmlReader = new XMLReader();

            // Iterate through each file inside the directory
            foreach (string file in files) 
            {
                if (file.EndsWith(".xml")) // only parse the XML files.
                {
                    IDataNode node = xmlReader.ReadXMLFile(file);
                    if (node != null)
                        data.Add(node);
                }
            }
            return new Database(data, directoryPath);
        }

        #endregion

    }
}
