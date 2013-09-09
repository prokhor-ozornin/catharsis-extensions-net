namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Set of extension methods for <see cref="bool"/> primitive type.</para>
  ///   <seealso cref="bool"/>
  /// </summary>
  public static class BooleanExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool And(this bool left, bool right)
    {
      return left && right;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <returns></returns>
    public static bool Not(this bool left)
    {
      return !left;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool Or(this bool left, bool right)
    {
      return left || right;
    }
  }
}