namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for type <see cref="bool"/>.</para>
  /// </summary>
  /// <seealso cref="bool"/>
  public static class BooleanExtensions
  {
    /// <summary>
    ///   <para>Performs a logical AND operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool And(this bool self, bool other)
    {
      return self && other;
    }

    /// <summary>
    ///   <para>Performs a logical NOT operation with current <see cref="bool"/> value.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value.</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Not(this bool self)
    {
      return !self;
    }

    /// <summary>
    ///   <para>Performs a logical OR operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Or(this bool self, bool other)
    {
      return self || other;
    }

    /// <summary>
    ///   <para>Performs a logical XOR (Exclusive OR) operation between current <see cref="bool"/> value and another.</para>
    /// </summary>
    /// <param name="self">Current <see cref="bool"/> value ("left").</param>
    /// <param name="other">Second <see cref="bool"/> value ("right").</param>
    /// <returns>Result of logical operation.</returns>
    public static bool Xor(this bool self, bool other)
    {
      return self ^ other;
    }
  }
}