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
        private IFileConverter _htmlFileConverter;
        private ExportConfiguration _configuration;
        // Commands
        private ICommand _exportToText;
        private ICommand _exportToHTML;
        #endregion // Fields

        #region Constructors

        public ExportViewModel(IDisplayDataProvider displayDataProvider)
        {
            _displayDataProvider = displayDataProvider;
            _exportFilter = new ExportFilter();
            _txtFileConverter = new TextFileConverter();
            _htmlFileConverter = new HTMLFileConverter();
            _configuration = new ExportConfiguration(false, false, false);
        }

        #endregion // Constructors

        #region ICommand Methods

        public ICommand ExportToText
        {
            get
            {
                if (_exportToText == null)
                    _exportToText = new RelayCommand(i => ExportToFile(_exportFilter, _txtFileConverter, "txt"));
                return _exportToText;
            }
        }

        public ICommand ExportToHTML
        {
            get
            {
                if (_exportToHTML == null)
                    _exportToHTML = new RelayCommand(i => ExportToFile(_exportFilter, _htmlFileConverter, "html"));
                return _exportToHTML;
            }
        }

        #endregion // ICommand Methods

        #region Export Methods

        private void ExportToFile(IExportFilter filter, IFileConverter converter, string format)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = $"(*.{format})|*.{format}";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                new ExportEventHandler(filter, converter, _configuration, _displayDataProvider).Export(dialog.FileName);
            }
        }

        #endregion // Export Methods

        #region Properties

        public bool IncludeSimilarFields
        {
            get { return _configuration.IncludeSimilarFields; }
            set
            {
                if (_configuration.IncludeSimilarFields != value)
                {
                    _configuration.IncludeSimilarFields = value;
                    OnPropertyChanged("IncludeSimilarFields");
                }
            }
        }

        public bool DB1Only
        {
            get { return _configuration.DB1Only; }
            set
            {
                if(_configuration.DB1Only != value)
                {
                    _configuration.DB1Only = value;
                    OnPropertyChanged("DB1Only");
                    if (_configuration.DB1Only)
                        DB2Only = false;
                }
            }
        }

        public bool DB2Only
        {
            get { return _configuration.DB2Only; }
            set
            {
                if(_configuration.DB2Only != value)
                {
                    _configuration.DB2Only = value;
                    OnPropertyChanged("DB2Only");
                    if (_configuration.DB2Only)
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
