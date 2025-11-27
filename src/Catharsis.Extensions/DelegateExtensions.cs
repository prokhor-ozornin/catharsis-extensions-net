namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for reflection and meta-information related types.</para>
/// </summary>
/// <seealso cref="Delegate"/>
public static class DelegateExtensions
{
  /// <param name="delegate"></param>
  extension(Delegate @delegate)
  {
    /// <summary>
    ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
    /// </summary>
    /// <param name="other">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="delegate"/>.</param>
    /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="delegate"/> and removing the last occurrence of the invocation list of <paramref name="other"/>, if the invocation list of <paramref name="other"/> is found within the invocation list of <paramref name="delegate"/>. Returns <paramref name="delegate"/> if <paramref name="other"/> is <c>null</c> or if the invocation list of <paramref name="other"/> is not found within the invocation list of <paramref name="delegate"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="other"/> is equal to the invocation list of <paramref name="delegate"/> or if <paramref name="delegate"/> is a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="delegate"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    public Delegate Not(Delegate other)
    {
      if (@delegate is null) throw new ArgumentNullException(nameof(@delegate));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return Delegate.Remove(@delegate, other);
    }

    /// <summary>
    ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
    /// </summary>
    /// <param name="other">Second delegate to compare with the current.</param>
    /// <returns>New delegate which a combined invocation list from <paramref name="delegate"/> and <paramref name="other"/> delegates.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="delegate"/> or <paramref name="other"/> is <see langword="null"/>.</exception>
    public Delegate And(Delegate other)
    {
      if (@delegate is null) throw new ArgumentNullException(nameof(@delegate));
      if (other is null) throw new ArgumentNullException(nameof(other));

      return Delegate.Combine(@delegate, other);
    }
  }
}