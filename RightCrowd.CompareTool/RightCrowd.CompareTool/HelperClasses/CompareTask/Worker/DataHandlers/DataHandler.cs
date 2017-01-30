using System.Linq;

using RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.List;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers
{
    internal class DataHandler : IDataHandler
    {
        #region Fields

        private readonly IListDatabaseStorageBuilder _differences;
        private readonly IListDatabaseStorageBuilder _similarities;

        #endregion // Fields

        #region Constructor

        public DataHandler(IDatabase[] databases)
        {
            _differences = CreateBuilder(databases);
            _similarities = CreateBuilder(databases);
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Returns the builder which stored all the nodes which
        /// are different in all compared databases.
        /// </summary>
        public IListDatabaseStorageBuilder Differences
        {
            get
            {
                return _differences;
            }
        }

        /// <summary>
        /// Returns the builder which stored all the nodes which
        /// are the same in all compared databases.
        /// </summary>
        public IListDatabaseStorageBuilder Similarities
        {
            get
            {
                return _similarities;
            }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Marks the data node as different and stores it inside the Differences
        /// Storage Builder. If the allDifferent flag is true, it should mark all
        /// of its fields as different.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="node"></param>
        /// <param name="allDifferent"></param>
        public void RecordAsDifferent(IField field, bool allDifferent)
        {
            field.Different = true;
            if (allDifferent)
            {
                if (field is CompositeField)
                    ((CompositeField)field).Fields.ToList().ForEach(child => RecordAsDifferent(child, allDifferent));
            }
        }

        /// <summary>
        /// Marks the field as different. If the allDifferent flag is true, it
        /// should mark all of the composite field's children.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="allDifferent"></param>
        public void RecordAsDifferent(int databaseIndex, IDataNode node, bool allDifferent)
        {
            node.Different = true;
            if (allDifferent)
            {
                node.New = true;
                node.Fields.ToList().ForEach(field => RecordAsDifferent(field, allDifferent));
            }
            Differences.AddNode(node, databaseIndex);
        }

        /// <summary>
        /// Stores the data node into the similarities data storage builder.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="node"></param>
        public void RecordAsSimilar(int databaseIndex, IDataNode node)
        {
            Similarities.AddNode(node, databaseIndex);
        }

        #endregion // Methods

        #region Helper Methods

        /// <summary>
        /// Creates a database storage builder containing databases with the same
        /// directory name as the databases passed.
        /// </summary>
        /// <param name="databases"></param>
        /// <returns></returns>
        private IListDatabaseStorageBuilder CreateBuilder(IDatabase[] databases)
        {
            IListDatabaseStorageBuilder builder = new ListDatabaseStorageBuilder();
            databases.ToList().ForEach(database => 
            {
                if(database == null)
                    builder.Add(null);
                else
                    builder.Add(new Database(database.DirectoryName));
            });
            return builder;
        }

        #endregion // Helper Methods
    }
}
