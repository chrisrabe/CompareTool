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

        public CompositeField(string name, ObservableCollection<IField> fields)
        {
            Name = name;
            Fields = fields;
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
    }
}
