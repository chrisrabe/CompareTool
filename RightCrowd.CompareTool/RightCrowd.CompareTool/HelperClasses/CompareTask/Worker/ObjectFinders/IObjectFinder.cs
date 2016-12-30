using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.ObjectFinders
{
    /// <summary>
    /// This class assists the task worker in the compare logic.
    /// 
    /// It is responsible for finding an object B with the
    /// same name property as object A from object C; such that
    /// there exists a collection of objects which are the same 
    /// type as object A inside object C.
    /// </summary>
    public interface IObjectFinder
    {
        /// <summary>
        /// Retrieves the collection of nodes from the given database. The
        /// returned nodes have the same name as the given node.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        ICollection<IDataNode> GetOther(IDataNode node, IDatabase database);

        /// <summary>
        /// Retrieves the collection of fields from the data node's fields.
        /// The returned fields have the same name as the given field.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        ICollection<IField> GetOther(IField field, IDataNode node);

        /// <summary>
        /// Retrieves the collection of fields from the composite field's children.
        /// The returned fields have the same name as the given field.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="composite"></param>
        /// <returns></returns>
        ICollection<IField> GetOther(IField field, CompositeField composite);
    }
}
