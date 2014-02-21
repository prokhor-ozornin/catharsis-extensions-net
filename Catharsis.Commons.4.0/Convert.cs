namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public sealed class Convert
  {
    private static readonly Convert converter = new Convert();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public static Convert To
    {
      get { return converter; }
    }
  }
}