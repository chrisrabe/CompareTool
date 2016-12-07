using System;
using System.Xml.Linq;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Fields;

namespace RightCrowd.CompareTool.HelperClasses.Readers.XMLReaders
{
    public class XMLReader : IXMLReader
    {
        public IDataNode ReadXMLFile(XDocument doc)
        {
            ObservableCollection<IField> fields = new ObservableCollection<IField>();
            return null;
        }
    }
}
