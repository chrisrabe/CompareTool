using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;
using RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReaders;
using RightCrowd.CompareTool.HelperClasses.ValueConversion;
using RightCrowd.CompareTool.HelperClasses.Readers.ConversionTableReaders;

namespace RightCrowd.CompareTool.HelperClasses.Readers.XML
{
    /// <summary>
    /// Reads through the XML file and returns a data node.
    /// </summary>
    public class XMLReader : IXMLReader
    {
        private IMetaData _nodeMetaData;
        private IMetaData _fieldMetaData;
        private IConversionTable _conversionTable;

        /// <summary>
        /// Creates an XMLReader which uses the internal resources in this tool.
        /// 
        /// The three resources are:
        /// - NodeMetaData.xml
        /// - FieldMetaData.xml
        /// - ConversionTable.xml
        /// 
        /// All of these resources are located in the XMLMetaData folder.
        /// 
        /// </summary>
        public XMLReader() : this(new MetaDataReader().ReadMetaDataFile("RightCrowd.CompareTool.XMLMetaData.NodeMetaData.xml"),
                                  new MetaDataReader().ReadMetaDataFile("RightCrowd.CompareTool.XMLMetaData.FieldMetaData.xml"),
                                  new ConversionTableReader().ReadConversionTableFile("RightCrowd.CompareTool.XMLMetaData.ConversionTable.xml")){ }

        /// <summary>
        /// Creates an XMLReader with a custom meta data for fields and nodes and a custom converstion table
        /// </summary>
        /// <param name="nodeMetaData"></param>
        /// <param name="fieldMetaData"></param>
        /// <param name="conversionTable"></param>
        public XMLReader(IMetaData nodeMetaData, IMetaData fieldMetaData, IConversionTable conversionTable)
        {
            _nodeMetaData = nodeMetaData == null ? new MetaData() : nodeMetaData;
            _fieldMetaData = fieldMetaData == null ? new MetaData() : fieldMetaData;
            _conversionTable = conversionTable == null ? new ConversionTable() : conversionTable;
        }

        #region Methods

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

            var mapping = _nodeMetaData.KeyFields.FirstOrDefault(x => x.Name.Equals(root.Name.ToString()));
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

        #endregion // Methods

        #region Helper Methods

        /// <summary>
        /// Parses through through the element. If the element has at least one child, 
        /// it returns a CompositeField, otherwise it will return a raw field.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private IField Parse(XElement element)
        {
            string fieldName = element.Name.ToString();
            return element.Elements().Count() > 0 ? Parse(element, fieldName) : Parse(fieldName, element.Value == null ? "" : element.Value);
        }

        /// <summary>
        /// Parses the raw field. 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private IField Parse(string fieldName, string value)
        {
            IConversionField conversionField = _conversionTable.Fields.FirstOrDefault(field => field.Name.Equals(fieldName));
            // if the field name is inside the conversion table,
            if (conversionField != null)
            {
                // modify the value by finding the value which it converts to
                IConversionValue conversionValue = conversionField.Values.FirstOrDefault(conversion => conversion.Value.Equals(value));
                // if there's no conversion found, leave it as it is
                string newValue = conversionValue != null ? conversionValue.Conversion : value;
                return new RawField(fieldName, newValue);
            }
            else // return a raw field with no modifications
            {
                return new RawField(fieldName, value);
            }
        }

        /// <summary>
        /// Parses the composite field. If the key exists inside the mapping, then it attaches the value of
        /// the field key onto the field name, otherwise it uses the default field name.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private IField Parse(XElement field, string fieldName)
        {
            var mapping = _fieldMetaData.KeyFields.FirstOrDefault(x => x.Name.Equals(fieldName));
            if (mapping != null)
            {
                string fieldKey = mapping.Keys.FirstOrDefault(key => field.Elements().Any(child => child.Name.ToString().Equals(key)));
                return new CompositeField((fieldKey != null) ? $"{fieldName}.{field.Element(fieldKey).Value}" : fieldName,
                       new ObservableCollection<IField>(field.Elements().Select(Parse)));
            }
            else
                return new CompositeField(fieldName, new ObservableCollection<IField>(field.Elements().Select(Parse)));
        }
    }

    #endregion // Helper Methods
}
