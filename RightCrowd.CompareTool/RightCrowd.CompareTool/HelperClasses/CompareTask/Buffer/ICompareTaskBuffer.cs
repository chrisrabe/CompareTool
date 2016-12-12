namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer
{
    /// <summary>
    /// Contains a collection of tasks which could be retrieved
    /// by the manager. It is important to note that the 'Next'
    /// property has a synchronous 'get' accessor. This is because
    /// multiple compare task worker threads will be using this
    /// method and making this accessor synchronous avoids
    /// unintended errors or unexpected outcomes.
    /// </summary>
    public interface ICompareTaskBuffer
    {
    }
}
