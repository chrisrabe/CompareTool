using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.DataModels.Fields
{
    /// <summary>
    /// Represents a field which can hold a single value.
    /// </summary>
    public class RawField : ObservableObject, IField
    {
        #region Fields

        private string _name;
        private string _value;
        private bool _different;

        #endregion // Fields

        #region Constructor

        public RawField(string name, string value)
        {
            Name = name;
            Value = value;
            Different = false;
        }

        #endregion // Constructor


        #region Properties

        /// <summary>
        /// Gets or sets the flag which indicates whether or
        /// not this field exists in the other database(s).
        /// </summary>
        public bool Different
        {
            get
            {
                return _different;
            }

            set
            {
                if (_different != value)
                {
                    _different = value;
                    OnPropertyChanged("Different");
                }
            }
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
                if (_name != value)
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
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion // Properties

        #region Methods

        public override string ToString()
        {
            return string.Format("{0} : {1}", Name, Value);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
                return false;

            RawField other = (RawField)obj;
            if (other.Name != this.Name)
                return false;

            if (other.Value != this.Value)
                return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                result = (result * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                return result;
            }
        }

        #endregion // Methods
    }
}
