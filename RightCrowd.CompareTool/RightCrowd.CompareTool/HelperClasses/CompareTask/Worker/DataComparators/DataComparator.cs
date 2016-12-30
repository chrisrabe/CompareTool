using System.Linq;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.ObjectFinders;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators
{
    public class DataComparator : IDataComparator
    {
        #region Fields

        private IObjectFinder _objectFinder;

        #endregion // Fields

        #region Constructor

        public DataComparator()
        {
            _objectFinder = new ObjectFinder();
        }

        #endregion // Constructor

        #region Properties

        public IDataHandler Handler { get; set; }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// This method compares the nodes of the database at index 1 to the nodes
        /// of the database at index2. The database at index 1 must not be null.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="databases"></param>
        public void Compare(int index1, int index2, params IDatabase[] databases)
        {
            // First check if database at index 1 is null.
            if (databases[index1] == null)
                return;

            foreach (IDataNode node in databases[index1].Data)
            {
                if (node.Different || node.Visited)
                    continue; // avoids duplicate comparisons

                if (databases[index2] != null)
                {
                    // Get all the other nodes with the same name
                    var otherNodes = _objectFinder.GetOther(node, databases[index2]);

                    if (otherNodes == null || otherNodes.Count == 0)
                    {
                        // Nothing matched the node given, so record as all different.
                        node.Visited = true;
                        Handler.RecordAsDifferent(index1, node, true);
                    }
                    else
                    {
                        // Check if any nodes exactly match the given node.
                        var similar = otherNodes.Any(other =>
                        {
                            return Compare(node, other);
                        });

                        if (similar)
                            Handler.RecordAsSimilar(index1, node);
                        else
                            Handler.RecordAsDifferent(index1, node, false);
                    }
                }
                else
                    Handler.RecordAsDifferent(index1, node, true);
            }
        }

        /// <summary>
        /// Returns true if all fields are the same, otherwise returns false.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        private bool Compare(IDataNode node, IDataNode other)
        {
            bool same = true;
            node.Fields.ToList().ForEach(field =>
            {
                var otherFields = _objectFinder.GetOther(field, other);
                if (otherFields == null || otherFields.Count == 0)
                {
                    same = false;
                    Handler.RecordAsDifferent(field, true);
                }
                else
                {
                    bool similar = otherFields.Any(field2 => field.Equals(field2));
                    if (!similar)
                    {
                        same = false;
                        Handler.RecordAsDifferent(field, true);
                    }
                }
            });

            return same;
        }

        #endregion // Methods
    }
}
