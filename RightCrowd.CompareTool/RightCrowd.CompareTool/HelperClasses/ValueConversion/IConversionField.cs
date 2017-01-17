using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.ValueConversion
{
    /// <summary>
    /// This interface maps the field to a collection of conversion values.
    /// </summary>
    public interface IConversionField
    {
        string Field { get; set; }

        ICollection<IConversionValue> Values { get; set; }
    }
}
