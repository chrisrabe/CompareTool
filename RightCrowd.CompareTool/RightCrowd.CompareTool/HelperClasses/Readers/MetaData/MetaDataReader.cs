using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields;
using System;
using System.Text.RegularExpressions;

namespace RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReaders
{
    public class MetaDataReader : IMetaDataReader
    {

        /// <summary>
        /// Reads the XML file indicated by the string parameter. If the file doesn't exist,
        /// then it returns an empty meta data object.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public IMetaData ReadMetaDataFile(string file)
        {
            IMetaData meta = new MetaData();
            try
            {
                XDocument doc = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(file));
                XElement root = doc.Elements().First();
                meta.KeyFields = new List<IKeyField>(root.Elements().Select(Parse));
                return meta;
            }
            catch (ArgumentNullException)
            {
                return meta; // given file doesn't exist, return empty meta data
            }
        }

        /// <summary>
        /// Parses an element of the root node. It assumes that it follows the syntax of
        /// the keyfield name as the element name and the key as its value and are
        /// separated by commas if multiple keys are used.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private IKeyField Parse(XElement element)
        {
            string name = element.Name.ToString();
            IKeyField keyField = new KeyField(name);
            string[] values = Regex.Split(element.Value, "\\{0},"); 
            keyField.Keys = new List<string>(values.Select(x => x.Replace('\\',' ').Trim()));
            return keyField;
        }
    }
}
