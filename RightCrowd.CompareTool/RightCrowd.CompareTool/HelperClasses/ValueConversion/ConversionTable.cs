using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.ValueConversion
{
    public class ConversionTable : IConversionTable
    {
        private ICollection<IConversionField> _fields;

        public ICollection<IConversionField> Fields
        {
            get
            {
                if (_fields == null)
                    _fields = new List<IConversionField>();
                return _fields;
            }

            set { _fields = value; }
        }
    }

    public class ConversionField : IConversionField
    {
        private ICollection<IConversionValue> _values;

        public ConversionField(string name, ICollection<IConversionValue> values)
        {
            Name = name;
            Values = values;
        }

        public string Name { get; set; }

        public ICollection<IConversionValue> Values
        {
            get
            {
                if (_values == null)
                    _values = new List<IConversionValue>();
                return _values;
            }

            set { _values = value; }
        }
    }

    public class ConversionValue : IConversionValue
    {
        public ConversionValue(string value, string conversion)
        {
            Value = value;
            Conversion = conversion;
        }

        public string Conversion { get; set; }

        public string Value { get; set; }
    }
}
