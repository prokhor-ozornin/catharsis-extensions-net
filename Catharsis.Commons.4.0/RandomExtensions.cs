using System;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Random"/>.</para>
  ///   <seealso cref="Random"/>
  /// </summary>
  public static class RandomExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="random"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException"></exception>
    public static byte[] Bytes(this Random random, int count)
    {
      Assertion.NotNull(random);
      Assertion.True(count > 0);

      var numbers = new byte[count];
      random.NextBytes(numbers);
      return numbers;
    }
  }
}