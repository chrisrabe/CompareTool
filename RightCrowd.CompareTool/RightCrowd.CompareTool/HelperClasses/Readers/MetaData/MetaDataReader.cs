using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields;
using System;

namespace RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReaders
{
    public class MetaDataReader : IMetaDataReader
    {
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
                return null; // given file doesn't exist, return null.
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
            string[] values = element.Value.Split(',');
            keyField.Keys = new List<string>(values.Select(x => x.Trim()));
            return keyField;
        }
    }
}
