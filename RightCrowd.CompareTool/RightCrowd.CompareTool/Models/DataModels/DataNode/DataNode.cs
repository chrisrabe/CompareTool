﻿using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.ObjectModel;
using System;

namespace RightCrowd.CompareTool.Models.DataModels.DataNode
{
    /// <summary>
    /// Represents the data inside a configuration file.
    /// </summary>
    internal class DataNode : ObservableObject, IDataNode
    {
        #region Fields

        private string _filename;
        private bool _different;
        private ObservableCollection<IField> _fields;

        #endregion // Fields

        #region Constructor

        public DataNode(string filename, ObservableCollection<IField> fields)
        {
            FileName = filename;
            Fields = fields;
            Different = false;
        }

        #endregion // Constructor

        #region Properties

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
                    OnPropertyChanged("FileName");
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

        /// <summary>
        /// Gets or sets the different flag. This flag indicates whether
        /// this data node exists in the other database or not.
        /// The flag is initially set to false.
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
                if(_different != value)
                {
                    _different = value;
                    OnPropertyChanged("Different");
                }
            }
        }

        #endregion // Properties

        #region Methods

        public override string ToString()
        {
            return FileName;
        }

        /// <summary>
        /// This method iterates through the fields of
        /// the data node and if it detects any
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

        #endregion
    }
}
