using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.DataNode
{
    /// <summary>
    /// Represents the data inside a configuration file.
    /// </summary>
    internal class DataNode : ObservableObject, IDataNode
    {
        private string _filename;
        private ObservableCollection<IField> _fields;

        public DataNode(string filename, ObservableCollection<IField> fields)
        {
            FileName = filename;
            Fields = fields;
        }

        /// <summary>
        /// Gets the type of the data node.
        /// </summary>
        public string FileName
        {
            get
            {
                return _filename;
            }

            private set
            {
                if(_filename != value)
                {
                    _filename = value;
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
