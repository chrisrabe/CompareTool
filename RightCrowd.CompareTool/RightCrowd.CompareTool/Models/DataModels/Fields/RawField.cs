using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.DataModels.Fields
{
    /// <summary>
    /// Represents a field which can hold a single value.
    /// </summary>
    internal class RawField : ObservableObject, IField
    {
        #region Fields

        private string _name;
        private string _value;

        #endregion // Fields

        #region Constructor

        public RawField(string name, string value)
        {
            Name = name;
            Value = value;
        }

        #endregion // Constructor


        #region Properties

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

        #endregion // Properties
    }
}
