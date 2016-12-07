namespace RightCrowd.CompareTool.Models.DataModels.Field
{
    /// <summary>
    /// This represents a field inside a data node.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the field.
        /// </summary>
        string Value { get; set; }
    }
}
