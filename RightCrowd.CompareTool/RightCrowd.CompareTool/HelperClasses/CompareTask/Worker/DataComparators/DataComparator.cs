using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.ObjectFinders;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators
{
    /// <summary>
    /// This class is responsible for comparing the data models.
    /// </summary>
    public class DataComparator : IDataComparator
    {
        #region Fields

        private IDataHandler _handler;
        private IObjectFinder _objectFinder;
        private int _diffCount1;
        private int _diffCount2;

        #endregion // Fields

        #region Constructor

        public DataComparator()
        {
            _objectFinder = new ObjectFinder();
        }

        #endregion

        #region IDataComparator Members

        /// <summary>
        /// Gets or sets the data handler for this data comparator.
        /// </summary>
        public IDataHandler Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }

        /// <summary>
        /// This method compares the nodes of the database at index 1 to the nodes
        /// of the database at index2. The database at index 1 must not be null.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="databases"></param>
        public void Compare(int index1, int index2, params IDatabase[] databases)
        {
            if (databases[index1] == null)
                return;

            foreach (IDataNode node in databases[index1].Data)
            {
                if (node.Different)
                    continue; // avoids duplicate comparisons
                if (databases[index2] != null)
                {
                    _diffCount1 = _diffCount2 = 0; // Reset field counters to zero
                    var otherNodes = _objectFinder.GetOther(node, databases[index2]); // Gets all the nodes with the same name
                    IDataNode other = null; // This should get the data node with the same value.

                    // Mark as different or compare fields
                    if (other == null)
                    {
                        node.Visited = true;
                        _handler.RecordAsDifferent(index1, node, true);
                        continue;
                    }
                    else
                    {
                        Compare(index1, index2, node, other);
                    }
                    RecordNode(_diffCount1, index1, node);
                    RecordNode(_diffCount2, index2, other);
                }
                else
                    _handler.RecordAsDifferent(index1, node, true);
            }
        }

        #endregion // IDataComparator Members

        #region Helper Methods

        /// <summary>
        /// If the count is '0', then it records the node as similar.
        /// Otherwise it records the node as different.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="index"></param>
        /// <param name="node"></param>
        private void RecordNode(int count, int index, IDataNode node)
        {
            if (node == null) 
                return; // don't record if null

            if (count == 0)
            {
                if (!node.Visited)
                    _handler.RecordAsSimilar(index, node);
            }
            else
                _handler.RecordAsDifferent(index, node, false);
            node.Visited = true;
        }

        /// <summary>
        /// Compares the fields inside the node at index 1 with the fields inside
        /// the nodes at index 2.
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <param name="nodes"></param>
        private void Compare(int index1, int index2, params IDataNode[] nodes)
        {
            foreach (IField field in nodes[index1].Fields)
            {
                IField other = _objectFinder.GetOther(field, nodes[index2]);
                if (other == null)
                {
                    _diffCount1++;
                    _handler.RecordAsDifferent(field, true);
                }
                else
                    Compare(field, other);
            }
        }

        /// <summary>
        /// Compares generic fields. If it detects that both fields are
        /// composite, it compares field1 with field2. Otherwise it just
        /// compares the values of the raw fields.
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        private void Compare(IField field1, IField field2)
        {
            bool isComposite1 = field1 is CompositeField;
            bool isComposite2 = field2 is CompositeField;
            if (isComposite1 && isComposite2)
            {
                Compare((CompositeField)field1, (CompositeField)field2);
                return; // comparing passed on to another method
            }
            else if (!isComposite2 && !isComposite2)
            {
                RawField rawField1 = (RawField)field1;
                RawField rawField2 = (RawField)field2;
                if (rawField1.Value.Equals(rawField2.Value))
                    return; // Both same, exit out
            }
            // Mark both its as different
            _diffCount1++;
            _diffCount2++;
            _handler.RecordAsDifferent(field1, true);
            _handler.RecordAsDifferent(field2, true);
        }

        /// <summary>
        /// Compares field1 against field2. It iterates through the fields
        /// of each composite field.
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        private void Compare(CompositeField field1, CompositeField field2)
        {
            foreach (IField field in field1.Fields)
            {
                IField other = _objectFinder.GetOther(field, field2);
                if (other == null)
                {
                    _diffCount1++;
                    _handler.RecordAsDifferent(field, true);
                }
                else
                    Compare(field, other);
            }
        }

        #endregion // Helper Methods
    }
}