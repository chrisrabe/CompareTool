using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Export;
using RightCrowd.CompareTool.HelperClasses.ExportHelpers.FileConverter;
using RightCrowd.CompareTool.HelperClasses.ExportHelpers.Filter;
using RightCrowd.CompareTool.HelperClasses.Providers.DisplayData;
using RightCrowd.CompareTool.Models.Export.Configurations;
using System.Windows.Forms;
using System.Windows.Input;

namespace RightCrowd.CompareTool
{
    public class ExportViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        // Export classes
        private IDisplayDataProvider _displayDataProvider;
        private IExportFilter _exportFilter;
        private IFileConverter _txtFileConverter;
        // Flags
        private bool _includeSimilarFields;
        private bool _db1Only;
        private bool _db2Only;
        // Commands
        private ICommand _exportToText;
        #endregion // Fields

        #region Constructors

        public ExportViewModel(IDisplayDataProvider displayDataProvider)
        {
            _displayDataProvider = displayDataProvider;
            _exportFilter = new ExportFilter();
            _txtFileConverter = new TextFileConverter();
        }

        #endregion // Constructors

        #region ICommand Methods

        public ICommand ExportToText
        {
            get
            {
                if (_exportToText == null)
                    _exportToText = new RelayCommand(i => ExportToFile("txt"));
                return _exportToText;
            }
        }

        #endregion // ICommand Methods

        #region Export Methods

        private void ExportToFile(string format)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = $"(*.{format})|*.{format}";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                new ExportEventHandler(_exportFilter, _txtFileConverter, new ExportConfiguration(IncludeSimilarFields, DB1Only, DB2Only), _displayDataProvider).Export(dialog.FileName);
            }
        }

        #endregion // Export Methods

        #region Properties

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

        public bool DB1Only
        {
            get { return _db1Only; }
            set
            {
                if(_db1Only != value)
                {
                    _db1Only = value;
                    OnPropertyChanged("DB1Only");
                    if (_db1Only)
                        DB2Only = false;
                }
            }
        }

        public bool DB2Only
        {
            get { return _db2Only; }
            set
            {
                if(_db2Only != value)
                {
                    _db2Only = value;
                    OnPropertyChanged("DB2Only");
                    if (_db2Only)
                        DB1Only = false;
                }
            }
        }

        #endregion // Properties

        #region IPageViewModel Members

        public string ImagePath
        {
            get
            {
                return "pack://application:,,,/Resources/save_icon.png";
            }
        }

        public string Name
        {
            get
            {
                return "Export Results";
            }
        }

        #endregion // IPageViewmodel Members
    }
}
