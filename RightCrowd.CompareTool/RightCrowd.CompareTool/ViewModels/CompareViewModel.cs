using RightCrowd.CompareTool.HelperClasses;
using System;

namespace RightCrowd.CompareTool.ViewModels
{
    public class CompareViewModel : ObservableObject, IPageViewModel
    {
        #region IPageViewModel Members

        public string ImagePath
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "Comparison Result";
            }
        }

        #endregion // IPageViewModel Members
    }
}
