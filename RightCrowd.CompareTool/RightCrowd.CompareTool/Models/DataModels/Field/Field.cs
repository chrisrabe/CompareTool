using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.DataModels.Field
{
    /// <summary>
    /// Represents a field inside a data node.
    /// </summary>
    internal class Field : ObservableObject, IField
    {
        private string _name;
        private string _value;

        public Field(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// gets or sets the name of the field.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the field. 
        /// The field uses a look-up table for specific names to make the
        /// values human readable.
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                // Use look up table to convert the value.
                if(_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
    }
}
