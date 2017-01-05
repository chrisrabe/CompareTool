using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;

namespace RightCrowd.CompareTool.HelperClasses.Readers.XML
{
    /// <summary>
    /// Reads through the XML file and returns a data node.
    /// </summary>
    public class XMLReader : IXMLReader
    {
        private IMetaData _metaData;

        public XMLReader()
        {
            _metaData = new MetaData();
        }

        public IEnumerable<IDataNode> ReadXMLFile(string filename)
        {
            string nodeName = Path.GetFileName(filename);
            XDocument doc = XDocument.Load(filename);
            ObservableCollection<IField> fields = new ObservableCollection<IField>();
            XElement root = doc.Elements().First();

            if (root.Elements().Count() == 0)
                return null; // no elements to parse, no need to make a node.

            // If it only has one child, replace the root with its only child.
            if (root.Elements().Count() == 1)
                root = root.Elements().First();

            var mapping = _metaData.KeyFields.FirstOrDefault(x => x.Name.Equals(root.Name.ToString()));
            if (mapping != null)
            {
                List<IDataNode> nodes = new List<IDataNode>();
                foreach(XElement x in root.Elements())
                {
                    string nodeKey = null;
                    foreach(string key in mapping.Keys)
                    {
                        foreach(XElement child in x.Elements())
                        {
                            if (key.Equals(child.Name.ToString()))
                            {
                                nodeKey = key;
                                break;
                            }
                        }
                        if (nodeKey != null)
                            break;
                    }
                    nodes.Add(new DataNode($"{x.Name}.{x.Element(nodeKey).Value}", new ObservableCollection<IField>(x.Elements().Select(Parse))));
                }
                return nodes;
            }
            else
            {
                return new List<IDataNode>() { new DataNode(nodeName, new ObservableCollection<IField>(root.Elements().Select(Parse))) };
            }
        }

        /// <summary>
        /// Parses through through the element. If the element has at least one child, 
        /// it returns a CompositeField, otherwise it will return a raw field.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private IField Parse(XElement element)
        {
            IField field = null;
            string fieldName = element.Name.ToString();

            if (element.Elements().Count() > 0)
            {
                field = new CompositeField(fieldName);
                // Parse each children and add them to the fields collection
                foreach (XElement child in element.Elements())
                {
                    IField childField = Parse(child);
                    ((CompositeField)field).Fields.Add(childField);
                }
            }
            else
            {
                var value = element.Value == null ? "" : element.Value;
                field = new RawField(fieldName, value);
            }
            return field;
        }
    }
}
