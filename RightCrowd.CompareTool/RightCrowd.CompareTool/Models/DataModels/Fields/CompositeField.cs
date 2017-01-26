using RightCrowd.CompareTool.HelperClasses;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace RightCrowd.CompareTool.Models.DataModels.Fields
{
    /// <summary>
    /// This is a field type which holds multiple field values.
    /// </summary>
    public class CompositeField : ObservableObject, IField
    {
        #region Fields

        private string _name;
        private ObservableCollection<IField> _fields;
        private bool _different;

        #endregion // Fields

        #region Constructors

        public CompositeField(string name, ObservableCollection<IField> fields) : this(name)
        {
            Fields = fields;
        }

        public CompositeField(string name)
        {
            Name = name;
            Different = false;
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the name of this field.
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
        /// Gets or sets the fields.
        /// </summary>
        public ObservableCollection<IField> Fields
        {
            get
            {
                if (_fields == null)
                    _fields = new ObservableCollection<IField>();
                return _fields;
            }
            set
            {
                if (_fields != value)
                {
                    _fields = value;
                    OnPropertyChanged("Fields");
                }
            }
        }

        /// <summary>
        /// Iterates through all the fields in this composite field.
        /// If it detects one different field, then it updates the flag to true.
        /// </summary>
        public bool Different
        {
            get
            {
                if (HasDifferentFields())
                    Different = true;
                return _different;
            }

            set
            {
                if (_different != value)
                {
                    _different = value;
                    Fields.ToList().ForEach(field => field.Different = value);
                    OnPropertyChanged("Different");
                }
            }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// This method iterates through the fields of
        /// the composite field and if it detects any
        /// fields which are different, then it returns
        /// true.
        /// </summary>
        /// <returns></returns>
        private bool HasDifferentFields()
        {
            foreach (IField field in Fields)
            {
                if (field.Different)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the name of this composite field.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
                return false;

            CompositeField other = (CompositeField)obj;

            if (other.Name != Name)
                return false;

            // All fields must exist in the other's fields
            return Fields.All(field => other.Fields.Any(x => x.Equals(field)));
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Name != null ? Name.GetHashCode() : 0);
                Fields.ToList().ForEach(field =>
                {
                    result = (result * 397) ^ (field != null ? field.GetHashCode() : 0);
                });
                return result;
            }
        }

        #endregion // Methods
    }
}
