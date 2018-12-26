namespace Catharsis.Commons
{
  using System;

  /// <summary>
  ///   <para>Set of math-related extensions methods.</para>
  /// </summary>
  public static class MathExtensions
  {
    /// <summary>
    ///   <para>Returns the absolute value of 16-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static short Abs(this short self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of 32-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static int Abs(this int self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of 64-bit signed integer.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static long Abs(this long self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of single-precision floating-point number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static float Abs(this float self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of double-precision floating-point number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static double Abs(this double self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the absolute value of a <see cref="decimal"/> number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Absolute value of <paramref name="self"/>.</returns>
    public static decimal Abs(this decimal self)
    {
      return Math.Abs(self);
    }

    /// <summary>
    ///   <para>Returns the smallest integer greater than or equal to the specified number.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number.</param>
    /// <returns>The smallest integral value that is greater than or equal to <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Ceiling(double)"/>
    public static double Ceil(this double self)
    {
      return Math.Ceiling(self);
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this byte self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this short self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this int self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Determines whether specified numeric value is an even number.</para>
    /// </summary>
    /// <param name="self">Numeric value.</param>
    /// <returns><c>true</c> if <paramref name="self"/> is even number, <c>false</c> if not.</returns>
    public static bool Even(this long self)
    {
      return self % 2 == 0;
    }

    /// <summary>
    ///   <para>Returns the largest integer less than or equal to the specified number.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number.</param>
    /// <returns>The largest integer less than or equal to <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Floor(double)"/>
    public static double Floor(this double self)
    {
      return Math.Floor(self);
    }

    /// <summary>
    ///   <para>Returns a specified number raised to the specified power.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number to be raised to a power.</param>
    /// <param name="power">A double-precision floating-point number that specifies a power.</param>
    /// <returns>The number <paramref name="self"/> raised to the power <paramref name="power"/>.</returns>
    /// <seealso cref="Math.Pow(double, double)"/>
    public static double Power(this double self, double power)
    {
      return Math.Pow(self, power);
    }

    /// <summary>
    ///   <para>Generates specified number of random bytes.</para>
    /// </summary>
    /// <param name="self">Randomization object that is being extended.</param>
    /// <param name="count">Number of bytes to generate.</param>
    /// <returns>Array of randomly generated bytes. Length of array is equal to <paramref name="count"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="self"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException"></exception>
    /// <seealso cref="Random.NextBytes(byte[])"/>
    public static byte[] Bytes(this Random self, int count)
    {
      Assertion.NotNull(self);
      Assertion.True(count > 0);

      var numbers = new byte[count];
      self.NextBytes(numbers);
      return numbers;
    }
    
    /// <summary>
    ///   <para>Rounds a double-precision floating-point value to the nearest integral value.</para>
    /// </summary>
    /// <param name="self">A double-precision floating-point number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Round(double)"/>
    public static double Round(this double self)
    {
      return Math.Round(self);
    }

    /// <summary>
    ///   <para>Rounds a decimal value to the nearest integral value.</para>
    /// </summary>
    /// <param name="self">A decimal number to be rounded.</param>
    /// <returns>The integer nearest <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Round(decimal)"/>
    public static decimal Round(this decimal self)
    {
      return Math.Round(self);
    }

    /// <summary>
    ///   <para>Returns the square root of a specified number.</para>
    /// </summary>
    /// <param name="self">Source number.</param>
    /// <returns>Square root of <paramref name="self"/>.</returns>
    /// <seealso cref="Math.Sqrt(double)"/>
    public static double Sqrt(this double self)
    {
      return Math.Sqrt(self);
    }
  }
}