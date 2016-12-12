namespace RightCrowd.CompareTool.Models.DataModels.Fields
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
        /// Gets or sets the flag which indicates
        /// whether this field is stored inside the
        /// other database(s).
        /// </summary>
        bool Different { get; set; }
    }
}
