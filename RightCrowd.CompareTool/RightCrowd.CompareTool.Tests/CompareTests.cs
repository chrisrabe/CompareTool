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

        private CompareTestSetup _setup = new CompareTestSetup();

        private ICompareEventHandler CreateEventHandler(ICompareDataProvider dataProvider)
        {
            return new CompareEventHandler(null, dataProvider);
        }

        #endregion // Test Setup

        #region Tests

        /// <summary>
        /// Checks whether it can compare two databases which contains nodes with only
        /// raw fields as their values. The two databases contain the same fields.
        /// </summary>
        [TestMethod]
        public void Test01_CompareSimilarDatabase_NoComposite()
        {
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(_setup.CreateSimilarDatabases(false, false));

            while(provider.ComparisonStorage == null) // waits for the handler to finish
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Comparison data storage is null");

            if(provider.ComparisonStorage != null)
            {
                int expectedDifferences = 0;
                int expectedSimilarities = 1;

                IComparisonData data = provider.ComparisonStorage.ComparisonData.First(); // Get the only compare data
                // Check if all nodes similar in database one
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[0].Data.Count, "Database one has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[0].Data.Count, "Database one doesn't have similarities");
                // Check if all nodes similar in database two
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[1].Data.Count, "Database two has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[1].Data.Count, "Database two doesn't have similarities");
            }
        }

        /// <summary>
        /// Checks whether it can compare two databases which contains nodes with a composite
        /// field containing raw values only. The two databases are the same.
        /// </summary>
        [TestMethod]
        public void Test02_CompareSimilarDatabase_Composite_NoNested()
        {
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(_setup.CreateSimilarDatabases(true, false));

            while (provider.ComparisonStorage == null) // wait for handler to finish
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Comparison data storage is null");

            if(provider.ComparisonStorage != null)
            {
                int expectedDifferences = 0;
                int expectedSimilarities = 1;

                IComparisonData data = provider.ComparisonStorage.ComparisonData.First(); // Get the only compare data
                // Check if all nodes similar in database one
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[0].Data.Count, "Database one has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[0].Data.Count, "Database one doesn't have similarities");
                // Check if all nodes similar in database two
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[1].Data.Count, "Database two has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[1].Data.Count, "Database two doesn't have similarities");
            }
        }

        /// <summary>
        /// Checks whether it can compare two databases which contains nodes with a composite
        /// field containing nested composite fields. Since XSLT or some form of name modification
        /// will be made during the loading time of the database to distinguish these composite
        /// fields, some modification are also made to the children of the composite field.
        /// </summary>
        [TestMethod]
        public void Test03_CompareSimilarDatabase_Composite_Nested()
        {
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(_setup.CreateSimilarDatabases(true, true));

            while (provider.ComparisonStorage == null) // wait for handler to finish
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Comparison data storage is null");

            if (provider.ComparisonStorage != null)
            {
                int expectedDifferences = 0;
                int expectedSimilarities = 1;

                IComparisonData data = provider.ComparisonStorage.ComparisonData.First(); // Get the only compare data
                // Check if all nodes similar in database one
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[0].Data.Count, "Database one has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[0].Data.Count, "Database one doesn't have similarities");
                // Check if all nodes similar in database two
                Assert.AreEqual(expectedDifferences, data.Difference.Databases[1].Data.Count, "Database two has differences");
                Assert.AreEqual(expectedSimilarities, data.Similarities.Databases[1].Data.Count, "Database two doesn't have similarities");
            }
        }

        #endregion // Tests
    }
}
