﻿using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;
using RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReaders;

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
            _metaData = new MetaDataReader().ReadMetaDataFile("RightCrowd.CompareTool.XMLMetaData.NodeMetaData.xml");
            if (_metaData == null)
                _metaData = new MetaData(); // Default meta data - no key fields
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
                // - convert all children (x) of the root node into data nodes
                // - get the first key string which exists in x's children
                // - attach the name of the node and the key together
                // - parse each children of x as a field
                return root.Elements().Select(x => 
                    new DataNode($"{x.Name}.{x.Element(mapping.Keys.FirstOrDefault(key => x.Elements().Any(child => child.Name.ToString().Equals(key)))).Value}", 
                    new ObservableCollection<IField>(x.Elements().Select(Parse))));
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
