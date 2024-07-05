namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="Delegate"/>
public static class DelegateExtensions
{
  /// <summary>
  ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
  /// </summary>
  /// <param name="left">Current delegate to combine with the second.</param>
  /// <param name="right">Second delegate to compare with the current.</param>
  /// <returns>New delegate which a combined invocation list from <paramref name="left"/> and <paramref name="right"/> delegates.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  public static Delegate And(this Delegate left, Delegate right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return Delegate.Combine(left, right);
  }

  /// <summary>
  ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
  /// </summary>
  /// <param name="left">The delegate from which to remove the invocation list of <paramref name="right"/>.</param>
  /// <param name="right">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="left"/>.</param>
  /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="left"/> and removing the last occurrence of the invocation list of <paramref name="right"/>, if the invocation list of <paramref name="right"/> is found within the invocation list of <paramref name="left"/>. Returns <paramref name="left"/> if <paramref name="right"/> is <c>null</c> or if the invocation list of <paramref name="right"/> is not found within the invocation list of <paramref name="left"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="right"/> is equal to the invocation list of <paramref name="left"/> or if <paramref name="left"/> is a <c>null</c> reference.</returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="left"/> or <paramref name="right"/> is <see langword="null"/>.</exception>
  public static Delegate Not(this Delegate left, Delegate right)
  {
    if (left is null) throw new ArgumentNullException(nameof(left));
    if (right is null) throw new ArgumentNullException(nameof(right));

    return Delegate.Remove(left, right);
  }
}