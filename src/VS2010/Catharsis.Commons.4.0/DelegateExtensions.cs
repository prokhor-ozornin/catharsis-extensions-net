using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Delegate"/>.</para>
  /// </summary>
  /// <seealso cref="Delegate"/>
  public static class DelegateExtensions
  {
    /// <summary>
    ///   <para>Concatenates the invocation list of a current delegate and a second one.</para>
    /// </summary>
    /// <param name="self">Current delegate to combine with the second.</param>
    /// <param name="other">Second delegate to compare with the current.</param>
    /// <returns>New delegate which a combined invocation list from <paramref name="self"/> and <paramref name="other"/> delegates.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="self"/> or <paramref name="other"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Combine(Delegate, Delegate)"/>
    public static Delegate And(this Delegate self, Delegate other)
    {
      Assertion.NotNull(self);
      Assertion.NotNull(other);

      return Delegate.Combine(self, other);
    }

    /// <summary>
    ///   <para>Removes the last occurrence of the invocation list of a delegate from the invocation list of another delegate.</para>
    /// </summary>
    /// <param name="self">The delegate from which to remove the invocation list of <paramref name="other"/>.</param>
    /// <param name="other">The delegate that supplies the invocation list to remove from the invocation list of <paramref name="self"/>.</param>
    /// <returns>A new delegate with an invocation list formed by taking the invocation list of <paramref name="self"/> and removing the last occurrence of the invocation list of <paramref name="other"/>, if the invocation list of <paramref name="other"/> is found within the invocation list of <paramref name="self"/>. Returns <paramref name="self"/> if <paramref name="other"/> is <c>null</c> or if the invocation list of <paramref name="other"/> is not found within the invocation list of <paramref name="self"/>. Returns a <c>null</c> reference if the invocation list of <paramref name="other"/> is equal to the invocation list of <paramref name="self"/> or if <paramref name="self"/> is a <c>null</c> reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="Delegate.Remove(Delegate, Delegate)"/>
    public static Delegate Not(this Delegate self, Delegate other)
    {
      Assertion.NotNull(self);

      return Delegate.Remove(self, other);
    }
  }
}