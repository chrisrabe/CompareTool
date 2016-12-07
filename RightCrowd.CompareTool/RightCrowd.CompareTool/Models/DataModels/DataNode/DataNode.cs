using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.Field;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.DataNode
{
    /// <summary>
    /// Represents the data inside a configuration file.
    /// </summary>
    internal class DataNode : ObservableObject, IDataNode
    {
        private string _type;
        private ObservableCollection<IField> _fields;

        public DataNode(string type, ObservableCollection<IField> fields)
        {
            Type = type;
            Fields = fields;
        }

        /// <summary>
        /// Gets the type of the data node.
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }

            private set
            {
                if(_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// Gets the fields of the data node.
        /// </summary>
        public ObservableCollection<IField> Fields
        {
            get
            {
                return _fields;
            }

            private set
            {
                if(_fields != value)
                {
                    _fields = value;
                    OnPropertyChanged("Fields");
                }
            }
        }
    }
}
