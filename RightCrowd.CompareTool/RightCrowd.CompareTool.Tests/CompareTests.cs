using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare;
using RightCrowd.CompareTool.Models.Comparison.Data;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.Tests.SetupHelpers.TestSetup;

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

        #region Single Node Tests

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

        #endregion // Single Node Tests

        #region Multiple Nodes Tests

        [TestMethod]
        public void Test07_CompareSimilarDatabases_NoComposite_MultiNode()
        {
            int database1Size = 10;
            int database2Size = 10;
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(database1Size, database2Size);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                int numDifferenceDB1 = 0;
                int numDifferenceDB2 = 0;
                int numSimilaritiesDB1 = 0;
                int numSimilaritiesDB2 = 0;
                // Count how many nodes are different and similar for each data
                foreach(IComparisonData data in provider.ComparisonStorage.ComparisonData)
                {
                    IListDatabaseStorage difference = data.Difference;
                    IListDatabaseStorage similarities = data.Similarities;
                    numDifferenceDB1 += difference.Databases[0].Data.Count;
                    numSimilaritiesDB1 += similarities.Databases[0].Data.Count;
                    numDifferenceDB2 += difference.Databases[1].Data.Count;
                    numSimilaritiesDB2 += similarities.Databases[1].Data.Count;
                }
                // Check differences and similarities in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, numDifferenceDB1);
                Assert.AreEqual(database1Size, numSimilaritiesDB1);
                // Check differences and similarities in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, numDifferenceDB2);
                Assert.AreEqual(database2Size, numSimilaritiesDB2);
            }
        }

        [TestMethod]
        public void Test08_CompareSimilarDatabases_Composite_NoNested_MultiNode()
        {
            int database1Size = 10;
            int database2Size = 10;
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 2;
            setup.NumberOfCompositeChild = 0;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(database1Size, database2Size);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                int numDifferenceDB1 = 0;
                int numDifferenceDB2 = 0;
                int numSimilaritiesDB1 = 0;
                int numSimilaritiesDB2 = 0;
                // Count how many nodes are different and similar for each data
                foreach (IComparisonData data in provider.ComparisonStorage.ComparisonData)
                {
                    IListDatabaseStorage difference = data.Difference;
                    IListDatabaseStorage similarities = data.Similarities;
                    numDifferenceDB1 += difference.Databases[0].Data.Count;
                    numSimilaritiesDB1 += similarities.Databases[0].Data.Count;
                    numDifferenceDB2 += difference.Databases[1].Data.Count;
                    numSimilaritiesDB2 += similarities.Databases[1].Data.Count;
                }
                // Check differences and similarities in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, numDifferenceDB1);
                Assert.AreEqual(database1Size, numSimilaritiesDB1);
                // Check differences and similarities in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, numDifferenceDB2);
                Assert.AreEqual(database2Size, numSimilaritiesDB2);
            }
        }

        [TestMethod]
        public void Test09_CompareSimilarDatabases_Composite_Nested_MultiNode()
        {
            int database1Size = 10;
            int database2Size = 10;
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = 0;
            setup.DB1ExpectedFieldDifference = 0;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = 0;
            setup.DB2ExpectedFieldDifference = 0;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 2;
            setup.NumberOfCompositeChild = 2;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateSimilarDatabases(database1Size, database2Size);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                int numDifferenceDB1 = 0;
                int numDifferenceDB2 = 0;
                int numSimilaritiesDB1 = 0;
                int numSimilaritiesDB2 = 0;
                // Count how many nodes are different and similar for each data
                foreach (IComparisonData data in provider.ComparisonStorage.ComparisonData)
                {
                    IListDatabaseStorage difference = data.Difference;
                    IListDatabaseStorage similarities = data.Similarities;
                    numDifferenceDB1 += difference.Databases[0].Data.Count;
                    numSimilaritiesDB1 += similarities.Databases[0].Data.Count;
                    numDifferenceDB2 += difference.Databases[1].Data.Count;
                    numSimilaritiesDB2 += similarities.Databases[1].Data.Count;
                }
                // Check differences and similarities in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, numDifferenceDB1);
                Assert.AreEqual(database1Size, numSimilaritiesDB1);
                // Check differences and similarities in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, numDifferenceDB2);
                Assert.AreEqual(database2Size, numSimilaritiesDB2);
            }
        }

        [TestMethod]
        public void Test10_CompareDifferentDatabases_NoComposite_MultiNode()
        {
            int database1Size = 10;
            int database2Size = 10;
            // Set up the test
            CompareTestSetup setup = new CompareTestSetup();
            setup.DB1ExpectedDifferences = database1Size;
            setup.DB1ExpectedFieldDifference = 2;
            setup.DB1NumberOfRawFields = 5;
            setup.DB2ExpectedDifferences = database2Size;
            setup.DB2ExpectedFieldDifference = 2;
            setup.DB2NumberOfRawFields = 5;
            setup.NumberOfCompositeFields = 0;
            setup.NumberOfCompositeChild = 0;
            // Create the things needed for the test
            IListDatabaseStorage storage = setup.CreateDifferentDatabases(database1Size, database2Size);
            ICompareDataProvider provider = new MockCompareDataProvider();
            ICompareEventHandler handler = CreateEventHandler(provider);
            handler.Compare(storage);

            // wait for handler to finish
            while (provider.ComparisonStorage == null)
                Task.Delay(25);

            Assert.IsNotNull(provider.ComparisonStorage, "Compare Results must not be null");

            if (provider.ComparisonStorage != null)
            {
                int numDifferenceDB1 = 0;
                int numDifferenceDB2 = 0;
                int numSimilaritiesDB1 = 0;
                int numSimilaritiesDB2 = 0;
                // Count how many nodes are different and similar for each data
                foreach (IComparisonData data in provider.ComparisonStorage.ComparisonData)
                {
                    IListDatabaseStorage difference = data.Difference;
                    IListDatabaseStorage similarities = data.Similarities;
                    numDifferenceDB1 += difference.Databases[0].Data.Count;
                    numSimilaritiesDB1 += similarities.Databases[0].Data.Count;
                    numDifferenceDB2 += difference.Databases[1].Data.Count;
                    numSimilaritiesDB2 += similarities.Databases[1].Data.Count;
                }
                // Check differences and similarities in DB1
                Assert.AreEqual(setup.DB1ExpectedDifferences, numDifferenceDB1);
                Assert.AreEqual(0, numSimilaritiesDB1);
                // Check differences and similarities in DB2
                Assert.AreEqual(setup.DB2ExpectedDifferences, numDifferenceDB2);
                Assert.AreEqual(0, numSimilaritiesDB2);
            }
        }
        #endregion // Multiple Nodes Test
    }
}
