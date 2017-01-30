using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Export.Configurations
{
    /// <summary>
    /// This class contains properties which determines the output when
    /// exporting the results of the comparison data.
    /// </summary>
    public class ExportConfiguration : ObservableObject
    {
        #region Fields

        private bool _includeSimilarFields;
        private bool _db1Only;
        private bool _db2Only;

        #endregion // Fields

        #region Properties

        /// <summary>
        /// If true, it should export all the data under the differences tab;
        /// including the fields which are similar to the other database.
        /// If false, it should only export the fields which are marked
        /// different.
        /// </summary>
        public bool IncludeSimilarFields
        {
            get { return _includeSimilarFields; }
            set
            {
                if (_includeSimilarFields != value)
                {
                    _includeSimilarFields = value;
                    OnPropertyChanged("IncludeSimilarFields");
                }
            }
        }

        /// <summary>
        /// If true, it should export only the comparison results for database
        /// one.
        /// </summary>
        public bool DB1Only
        {
            get { return _db1Only; }
            set
            {
                if (_db1Only != value)
                {
                    _db1Only = value;
                    OnPropertyChanged("DB1Only");
                    if (_db1Only)
                        DB2Only = false; // only one flag can be set
                }
            }
        }

        /// <summary>
        /// If true, it should only export the comparison results for database
        /// two.
        /// </summary>
        public bool DB2Only
        {
            get { return _db2Only; }
            set
            {
                if (_db2Only != value)
                {
                    _db2Only = value;
                    OnPropertyChanged("DB2Only");
                    if (_db2Only)
                        DB1Only = false; // only one flag can be set
                }
            }
        }

        #endregion // Properties
    }
}
