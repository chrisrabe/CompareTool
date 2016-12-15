using System;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Comparison.Data
{
    /// <summary>
    /// This class contains two database storages. One of the storage
    /// contain databases with nodes which are different from each other.
    /// One of the storage contain databases which are similar to each
    /// other.
    /// </summary>
    public class ComparisonData : ObservableObject, IComparisonData
    {
        #region Fields

        private IListDatabaseStorage _difference;
        private IListDatabaseStorage _similarities;
        private string _type;

        #endregion  // Fields

        #region Constructor

        public ComparisonData(string type)
        {
            Type = type;
        }

        #endregion

        #region IComparison Data Properties

        /// <summary>
        /// Gets or sets the storage containing databases which has
        /// nodes that are not present in the other databases.
        /// </summary>
        public IListDatabaseStorage Difference
        {
            get
            {
                if (_difference == null)
                    _difference = new TwoDatabaseStorage();

                return _difference;
            }

            set
            {
                if(_difference != value)
                {
                    _difference = value;
                    OnPropertyChanged("Difference");
                }
            }
        }

        /// <summary>
        /// Gets or sets the storage containing databases which has
        /// nodes that are present in all compared databases.
        /// </summary>
        public IListDatabaseStorage Similarities
        {
            get
            {
                if (_similarities == null)
                    _similarities = new TwoDatabaseStorage();

                return _similarities;
            }

            set
            {
                if(_similarities != value)
                {
                    _similarities = value;
                    OnPropertyChanged("Similarities");
                }
            }
        }

        /// <summary>
        /// Gets or sets the data node type which the databases
        /// inside the storages hold.
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                if(_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        #endregion // IComparisonData Properties

        #region Object methods

        public override string ToString()
        {
            return _type;
        }

        #endregion
    }
}
