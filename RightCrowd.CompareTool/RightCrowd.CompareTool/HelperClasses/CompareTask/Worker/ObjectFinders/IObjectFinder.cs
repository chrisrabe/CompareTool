using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;

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
        /// Gets the other node with the same name property as
        /// the node passed from the given database.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        IDataNode GetOther(IDataNode node, IDatabase database);

        /// <summary>
        /// Gets the other field with the same name from the
        /// data node passed.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        IField GetOther(IField field, IDataNode node);

        /// <summary>
        /// Gets the other field with the same name property as
        /// the field passed from the composite field.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="composite"></param>
        /// <returns></returns>
        IField GetOther(IField field, CompositeField composite);
    }
}
