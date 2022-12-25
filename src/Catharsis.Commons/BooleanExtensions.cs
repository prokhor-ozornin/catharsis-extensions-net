namespace Catharsis.Commons;

/// <summary>
///   <para>Extension methods for boolean type.</para>
/// </summary>
/// <seealso cref="bool"/>
public static class BooleanExtensions
{
  /// <summary>
  ///   <para>Performs a logical AND operation between current <see cref="bool"/> value and another.</para>
  /// </summary>
  /// <param name="left">Current <see cref="bool"/> value ("left").</param>
  /// <param name="right">Second <see cref="bool"/> value ("right").</param>
  /// <returns>Result of logical operation.</returns>
  public static bool And(this bool left, bool right) => left && right;

  /// <summary>
  ///   <para>Performs a logical OR operation between current <see cref="bool"/> value and another.</para>
  /// </summary>
  /// <param name="left">Current <see cref="bool"/> value ("left").</param>
  /// <param name="right">Second <see cref="bool"/> value ("right").</param>
  /// <returns>Result of logical operation.</returns>
  public static bool Or(this bool left, bool right) => left || right;

  /// <summary>
  ///   <para>Performs a logical NOT operation with current <see cref="bool"/> value.</para>
  /// </summary>
  /// <param name="left">Current <see cref="bool"/> value.</param>
  /// <returns>Result of logical operation.</returns>
  public static bool Not(this bool left) => !left;

  /// <summary>
  ///   <para>Performs a logical XOR (Exclusive OR) operation between current <see cref="bool"/> value and another.</para>
  /// </summary>
  /// <param name="left">Current <see cref="bool"/> value ("left").</param>
  /// <param name="right">Second <see cref="bool"/> value ("right").</param>
  /// <returns>Result of logical operation.</returns>
  public static bool Xor(this bool left, bool right) => left ^ right;
}