namespace RightCrowd.CompareTool.HelperClasses.ValueConversion
{
    /// <summary>
    /// This class maps the value of a field to another value.
    /// </summary>
    public interface IConversionValue
    {
        /// <summary>
        /// Value of the field
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// The other value which it converts to
        /// </summary>
        string Conversion { get; set; }
    }
}
