﻿using System.Xml.Linq;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Linq;

namespace RightCrowd.CompareTool.HelperClasses.Readers.XMLReaders
{
    /// <summary>
    /// Reads through the XML file and returns a data node.
    /// </summary>
    public class XMLReader : IXMLReader
    {
        public IDataNode ReadXMLFile(string filename)
        {
            XDocument doc = XDocument.Load(filename);
            ObservableCollection<IField> fields = new ObservableCollection<IField>();
            XElement root = doc.Elements().First();
            foreach (XElement element in root.Elements())
            {
                IField field = Parse(element);
                if (field != null) // discard invalid fields
                    fields.Add(field);
            }
            return new DataNode(filename, fields);
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

            if(element.Elements().Count() > 0)
            {
                field = new CompositeField(fieldName);
                // Parse each children and add them to the fields collection
                ((CompositeField)field).Fields.Concat(element.Elements().Select(child => Parse(child)));
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