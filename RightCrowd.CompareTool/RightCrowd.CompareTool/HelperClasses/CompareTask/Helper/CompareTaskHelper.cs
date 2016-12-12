using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Helper
{
    /// <summary>
    /// This class assists the task worker in the compare logic.
    /// 
    /// It is responsible for finding an object B with the
    /// same name property as object A from object C; such that
    /// there exists a collection of objects which are the same 
    /// type as object A inside object C.
    /// </summary>
    public class CompareTaskHelper : ICompareTaskHelper
    {
        /// <summary>
        /// Gets the other field with the same name as the field parameter
        /// from the composite field. Returns null if no fields have the 
        /// same name.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="composite"></param>
        /// <returns></returns>
        public IField GetOther(IField field, CompositeField composite)
        {
            foreach(IField other in composite.Fields)
            {
                if (field.Name.Equals(other.Name))
                    return other;
            }
            return null;
        }

        /// <summary>
        /// Gets the other field with the same name as the field parameter
        /// from the data node. Returns null if no fields have the same
        /// name.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public IField GetOther(IField field, IDataNode node)
        {
            foreach(IField other in node.Fields)
            {
                if (field.Name.Equals(other.Name))
                    return other;
            }
            return null;
        }

        /// <summary>
        /// Gets the other node with the same name as the node parameter
        /// from the database. Returns null if no nodes have the same name.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public IDataNode GetOther(IDataNode node, IDatabase database)
        {
            foreach(IDataNode other in database.Data)
            {
                if (node.FileName.Equals(other.FileName))
                {
                    return other;
                }
            }
            return null;
        }
    }
}
