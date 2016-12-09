using RightCrowd.CompareTool.HelperClasses;
using System;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.Fields
{
    /// <summary>
    /// This is a field type which holds multiple field values.
    /// </summary>
    internal class CompositeField : ObservableObject, IField
    {
        #region Fields

        private string _name;
        private ObservableCollection<IField> _fields;

        #endregion // Fields

        #region Constructors

        public CompositeField(string name)
        {
            Name = name;
        }

        #endregion // Constructors

        #region Properties

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
                if(_fields != value)
                {
                    _fields = value;
                    OnPropertyChanged("Fields");
                }
            }
        }

        #endregion // Properties

        public override string ToString()
        {
            return Name;
        }
    }
}
