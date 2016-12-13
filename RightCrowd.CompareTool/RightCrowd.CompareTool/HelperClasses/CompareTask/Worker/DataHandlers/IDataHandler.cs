using RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.List;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers
{
    /// <summary>
    /// Handles all the recording logic for the CompareTaskWorker.
    /// </summary>
    public interface IDataHandler
    {
        /// <summary>
        /// Returns the builder which stored all the nodes which
        /// are different in all compared databases.
        /// </summary>
        IListDatabaseStorageBuilder Differences { get; }

        /// <summary>
        /// Returns the builder which stored all the nodes which
        /// are the same in all compared databases.
        /// </summary>
        IListDatabaseStorageBuilder Similarities { get; }

        /// <summary>
        /// Marks the data node as different and stores it inside the Differences
        /// Storage Builder. If the allDifferent flag is true, it should mark all
        /// of its fields as different.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="node"></param>
        /// <param name="allDifferent"></param>
        void RecordAsDifferent(int databaseIndex, IDataNode node, bool allDifferent);

        /// <summary>
        /// Marks the field as different. If the allDifferent flag is true, it
        /// should mark all of the composite field's children.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="allDifferent"></param>
        void RecordAsDifferent(IField field, bool allDifferent);

        /// <summary>
        /// Stores the data node into the similarities data storage builder.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="node"></param>
        void RecordAsSimilar(int databaseIndex, IDataNode node);
    }
}
