﻿using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.DataModels.Database
{
    /// <summary>
    /// This class is responsible for caching all the configuration data of a database.
    /// </summary>
    public class Database : ObservableObject, IDatabase
    {
        private ObservableCollection<IDataNode> _data;
        private string _directoryName;

        public Database(string directoryName)
        {
            DirectoryName = directoryName;
        }

        public ObservableCollection<IDataNode> Data
        {
            get
            {
                if (_data == null)
                    _data = new ObservableCollection<IDataNode>();
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

        public override string ToString()
        {
            return _directoryName;
        }
    }
}
