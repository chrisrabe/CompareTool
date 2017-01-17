using System;
using RightCrowd.CompareTool.HelperClasses.ValueConversion;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RightCrowd.CompareTool.HelperClasses.Readers.ConversionTableReaders
{
    public class ConversionTableReader : IConversionTableReader
    {
        public IConversionTable ReadConversionTableFile(string file)
        {
            IConversionTable table = new ConversionTable();
            try
            {
                XDocument doc = XDocument.Load(Assembly.GetExecutingAssembly().GetManifestResourceStream(file));
                XElement root = doc.Elements().First();
                table.Fields = new List<IConversionField>(root.Elements().Select(Parse));
                return table;
            }
            catch (ArgumentNullException)
            {
                return table; // given file doesn't exist, return empty table
            }
        }

        private IConversionField Parse(XElement element)
        {
            string fieldName = element.Name.ToString();
            string[] values = element.Value.Split(',');
            return new ConversionField(fieldName, new List<IConversionValue>(values.Select(Parse)));
        }

        private IConversionValue Parse(string value)
        {
            // parse the dash (-) and then assign the value
            string[] values = Regex.Split(value, "~");
            if (values.Length != 2)
                throw new Exception("Error parsing value conversion table!");
            return new ConversionValue(values[0].Replace('\\',' ').Trim(), values[1].Replace('\\', ' ').Trim());
        }
    }
}
