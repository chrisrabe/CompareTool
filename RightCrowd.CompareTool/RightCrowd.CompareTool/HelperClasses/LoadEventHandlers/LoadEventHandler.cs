using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightCrowd.CompareTool.Models.DataModels.Database;
using System.Windows.Forms;
using RightCrowd.CompareTool.HelperClasses.Readers.DirectoryReaders;

namespace RightCrowd.CompareTool.HelperClasses.LoadEventHandlers
{
    public class LoadEventHandler : ILoadEventHandler
    {
        /// <summary>
        /// Opens a folder browser dialog and then creates a 
        /// background worker thread which reads the directory.
        /// </summary>
        /// <returns></returns>
        public IDatabase LoadDirectory()
        {
            FolderBrowserDialog browser = new FolderBrowserDialog(); // open dialog

            // Should create a background worker for this...
            if(browser.ShowDialog() == DialogResult.OK)
            {
                return new DirectoryReader().ReadDirectory(browser.SelectedPath);
            }

            return null;
        }
    }
}
