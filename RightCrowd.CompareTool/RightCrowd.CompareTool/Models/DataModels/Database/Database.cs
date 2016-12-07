using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.DataModels.Database
{
    /// <summary>
    /// This class is responsible for caching all the configuration data of a database.
    /// </summary>
    internal class Database : ObservableObject, IDatabase
    {
        private ObservableCollection<IDataNode> _data;
        private string _directoryName;

        public Database(ObservableCollection<IDataNode> data, string directoryName)
        {
            _data = data;
            _directoryName = directoryName;
        }

        public ObservableCollection<IDataNode> Data
        {
            get
            {
                return _data;
            }

            set
            {
                if(_data != value)
                {
                    _data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        public string DirectoryName
        {
            get
            {
                return _directoryName;
            }

            set
            {
                if(_directoryName != value)
                {
                    _directoryName = value;
                    OnPropertyChanged("DirectoryName");
                }
            }
        }
    }
}
