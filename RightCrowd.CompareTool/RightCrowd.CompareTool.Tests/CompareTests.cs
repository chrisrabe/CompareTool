using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Tests.SetupHelpers.TestSetup;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare;
using System.Linq;
using RightCrowd.CompareTool.Models.Comparison.Data;
using System.Threading.Tasks;

namespace RightCrowd.CompareTool.Tests
{
    [TestClass]
    public class CompareTests
    {
        #region Mock Compare Data Provider

        private class MockCompareDataProvider : ICompareDataProvider
        {
            IComparisonDataStorage _storage;

            public IComparisonDataStorage ComparisonStorage
            {
                get { return _storage; }
                set
                {
                    if (_storage != value)
                        _storage = value;
                }
            }
        }

        #endregion // Mock Compare Data Provider

        #region Test Setup

        private ICompareEventHandler CreateEventHandler(ICompareDataProvider dataProvider)
        {
            return new CompareEventHandler(null, dataProvider);
        }

        #endregion // Test Setup

        #region Tests
        

        #endregion // Tests
    }
}
