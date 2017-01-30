using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Export.Node;

namespace RightCrowd.CompareTool.Models.Export.Data
{
    public class ExportData : IExportData
    {
        private ObservableCollection<IExportNode> _databaseOneData;
        private ObservableCollection<IExportNode> _databaseTwoData;

        public ExportData(ObservableCollection<IExportNode> databaseOneData, ObservableCollection<IExportNode> databaseTwoData)
        {
            DatabaseOneData = databaseOneData;
            DatabaseTwoData = databaseTwoData;
        }

        #region Properties

        public ObservableCollection<IExportNode> DatabaseOneData
        {
            get
            {
                if (_databaseOneData == null)
                    DatabaseOneData = new ObservableCollection<IExportNode>();
                return _databaseOneData;
            }
            set
            {
                if (_databaseOneData != value)
                    _databaseOneData = value;
            }
        }

        public ObservableCollection<IExportNode> DatabaseTwoData
        {
            get
            {
                if (_databaseTwoData == null)
                    DatabaseTwoData = new ObservableCollection<IExportNode>();
                return _databaseTwoData;
            }

            set
            {
                if (_databaseTwoData != value)
                    _databaseTwoData = value;
            }
        }

        #endregion // Properties
    }
}
