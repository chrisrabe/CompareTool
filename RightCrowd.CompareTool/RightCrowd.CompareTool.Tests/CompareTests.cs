using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Tests.SetupHelpers.TestSetup;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare;
using System.Linq;
using RightCrowd.CompareTool.Models.Comparison.Data;
using System.Threading.Tasks;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

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

        [TestMethod]
        public void Test01_CompareSimilarDatabase_NoComposite_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if(provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[1].Data.Count);
            }
        }

        [TestMethod]
        public void Test02_CompareSimilarDatabase_Composite_NoNested_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 1;
            setup.NumberOfCompositeChild = 0;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[1].Data.Count);
            }
        }

        [TestMethod]
        public void Test03_CompareSimilarDatabases_Composite_Nested_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 1;
            setup.NumberOfCompositeChild = 3;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(1, data.Similarities.Databases[1].Data.Count);
            }
        }

        [TestMethod]
        public void Test04_CompareDifferentDatabases_NoComposite_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 1;
            setup.DB1ExpectedFieldDifference = 2;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 1;
            setup.DB2ExpectedFieldDifference = 2;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 0;
            setup.NumberOfCompositeChild = 0;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateDifferentDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[1].Data.Count);
            }
        }

        [TestMethod]
        public void Test05_CompareDifferentDatabases_Composite_NoNested_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 1;
            setup.DB1ExpectedFieldDifference = 6;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 1;
            setup.DB2ExpectedFieldDifference = 6;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 1;
            setup.NumberOfCompositeChild = 0;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateDifferentDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[1].Data.Count);
            }
        }

        [TestMethod]
        public void Test06_CompareDifferentDatabases_Composite_Nested_SingleNode()
        {
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 1;
            setup.DB1ExpectedFieldDifference = 6;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 1;
            setup.DB2ExpectedFieldDifference = 6;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 1;
            setup.NumberOfCompositeChild = 3;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateDifferentDatabases(1, 1);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                IComparisonData data = provider.ComparisonStorage.ComparisonData.First();
                // Test if there are no differences in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, data.Difference.Databases[0].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[0].Data.Count);
                // Test if there are no differences in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, data.Difference.Databases[1].Data.Count);
                Assert.AreEqual(0, data.Similarities.Databases[1].Data.Count);
            }
        }
        #endregion // Tests
    }
}
