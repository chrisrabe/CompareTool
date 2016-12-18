using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.Tests
{
    [TestClass]
    public class CompareTests
    {
        #region Set up Methods

        public IListDatabaseStorage CreateListDatabaseStorage()
        {
            IListDatabaseStorage storage = new TwoDatabaseStorage();
            // Initialise Storage Here
            return storage;
        }

        public ICompareEventHandler CreateCompareHandler()
        {
            return null;
        }

        #endregion // Set up Methods

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
