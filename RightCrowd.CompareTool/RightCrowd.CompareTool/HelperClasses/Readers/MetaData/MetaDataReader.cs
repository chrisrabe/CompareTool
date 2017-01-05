using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields;

namespace RightCrowd.CompareTool.HelperClasses.Readers.MetaDataReader
{
    public class MetaDataReader : IMetaDataReader
    {
        public IMetaData ReadMetaDataFile(string file)
        {
            IMetaData meta = new MetaData();
            XDocument doc = XDocument.Load(file);
            XElement root = doc.Elements().First();
            meta.KeyFields = new List<IKeyField>(root.Elements().Select(Parse));
            return meta;
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
            return null;
        }
    }
}
