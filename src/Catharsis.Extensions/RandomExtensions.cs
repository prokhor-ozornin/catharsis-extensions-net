using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for random numbers generators.</para>
/// </summary>
/// <seealso cref="Random"/>
public static class RandomExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="SbyteInRange(Random, Range[])"/>
  public static sbyte Sbyte(this Random random, sbyte? from = null, sbyte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(sbyte.MinValue).MinMax(to.GetValueOrDefault(sbyte.MaxValue));
    
    return (sbyte) random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Sbyte(Random, sbyte?, sbyte?)"/>
  public static sbyte SbyteInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Sbyte();

      case 1:
        var range = ranges.First();
        return random.Sbyte((sbyte?) range.Start.Value, (sbyte?) range.End.Value);

      default:
        return (sbyte) ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="SbyteSequenceInRange(Random, int, Range[])"/>
  public static IEnumerable<sbyte> SbyteSequence(this Random random, int count, sbyte? from = null, sbyte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Sbyte(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="SbyteSequence(Random, int, sbyte?, sbyte?)"/>
  public static IEnumerable<sbyte> SbyteSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return Enumerable.Empty<sbyte>();
    }

    switch (ranges.Length)
    {
      case 0:
        return random.SbyteSequence(count);

      case 1:
        var range = ranges.First();
        return random.SbyteSequence(count, (sbyte?) range.Start.Value, (sbyte?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (sbyte) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static byte Byte(this Random random, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(byte.MinValue).MinMax(to.GetValueOrDefault(byte.MaxValue));

    return (byte) random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static byte ByteInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Byte();

      case 1:
        var range = ranges.First();
        return random.Byte((byte?) range.Start.Value, (byte?) range.End.Value);

      default:
        return (byte) ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para>Generates specified number of random bytes.</para>
  /// </summary>
  /// <param name="random">Randomization object that is being extended.</param>
  /// <param name="count">Number of bytes to generate.</param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns>Array of randomly generated bytes. Length of array is equal to <paramref name="count"/>.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<byte> ByteSequence(this Random random, int count, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Byte(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<byte> ByteSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.ByteSequence(count);

      case 1:
        var range = ranges.First();
        return random.ByteSequence(count, (byte?) range.Start.Value, (byte?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (byte) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static short Short(this Random random, short? from = null, short? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(short.MinValue).MinMax(to.GetValueOrDefault(short.MaxValue));

    return (short) random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static short ShortInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Short();

      case 1:
        var range = ranges.First();
        return random.Short((short?) range.Start.Value, (short?) range.End.Value);

      default:
        return (short) ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<short> ShortSequence(this Random random, int count, short? from = null, short? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Short(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<short> ShortSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.ShortSequence(count);

      case 1:
        var range = ranges.First();
        return random.ShortSequence(count, (short?) range.Start.Value, (short?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (short) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static ushort Ushort(this Random random, ushort? from = null, ushort? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(ushort.MinValue).MinMax(to.GetValueOrDefault(ushort.MaxValue));

    return (ushort) random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static ushort UshortInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Ushort();

      case 1:
        var range = ranges.First();
        return random.Ushort((ushort?) range.Start.Value, (ushort?) range.End.Value);

      default:
        return (ushort) ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<ushort> UshortSequence(this Random random, int count, ushort? from = null, ushort? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Ushort(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<ushort> UshortSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.UshortSequence(count);

      case 1:
        var range = ranges.First();
        return random.UshortSequence(count, (ushort?) range.Start.Value, (ushort?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (ushort) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static int Int(this Random random, int? from = null, int? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(int.MinValue).MinMax(to.GetValueOrDefault(int.MaxValue));

    return random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static int IntInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Int();

      case 1:
        var range = ranges.First();
        return random.Int(range.Start.Value, range.End.Value);

      default:
        return ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<int> IntSequence(this Random random, int count, int? from = null, int? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Int(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<int> IntSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.IntSequence(count);

      case 1:
        var range = ranges.First();
        return random.IntSequence(count, range.Start.Value, range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static double Double(this Random random) => random?.NextDouble() ?? throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<double> DoubleSequence(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(random.Double);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static char Char(this Random random, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(char.MinValue).MinMax(to.GetValueOrDefault(char.MaxValue));

    return (char) random.Next(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static char CharInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Char();

      case 1:
        var range = ranges.First();
        return random.Char((char?) range.Start.Value, (char?) range.End.Value);

      default:
        return (char) ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<char> CharSequence(this Random random, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Char(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<char> CharSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.CharSequence(count);

      case 1:
        var range = ranges.First();
        return random.CharSequence(count, (char?) range.Start.Value, (char?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (char) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string String(this Random random, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    
    if (count == 0)
    {
      return string.Empty;
    }

    return random.CharSequence(count, from, to).AsArray().ToText();
  }


  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string StringInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return string.Empty;
    }
    
    switch (ranges.Length)
    {
      case 0:
        return random.String(count);

      case 1:
        var range = ranges.First();
        return random.String(count, (char?) range.Start.Value, (char?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        var chars = count.Objects(() => (char) totalRange.Random());
        return chars.AsArray().ToText();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> StringSequence(this Random random, int size, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.String(size, from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> StringSequenceInRange(this Random random, int size, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.StringSequence(size, count);

      case 1:
        var range = ranges.First();
        return random.StringSequence(size, count, (char?) range.Start.Value, (char?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => new char[size].Fill(() => (char) totalRange.Random()).AsArray().ToText());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static string Digits(this Random random, int count) => random.StringInRange(count, '0'..'9');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> DigitsSequence(this Random random, int size, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Digits(size));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string Letters(this Random random, int count) => random.StringInRange(count, 'a'..'z', 'A'..'Z');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> LettersSequence(this Random random, int size, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Letters(size));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static string AlphaDigits(this Random random, int count) => random.StringInRange(count, 'a'..'z', 'A'..'Z', '0'..'9');

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> AlphaDigitsSequence(this Random random, int size, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.AlphaDigits(size));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static SecureString SecureString(this Random random, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return new SecureString();
    }

    var result = new SecureString();

    count.Times(() => result.AppendChar(random.Char(from, to)));

    return result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static SecureString SecureStringInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return new SecureString();
    }

    switch (ranges.Length)
    {
      case 0:
        return random.SecureString(count);

      case 1:
        var range = ranges.First();
        return random.SecureString(count, (char?) range.Start.Value, (char?) range.End.Value);

      default:
        var result = new SecureString();
        var totalRange = ranges.ToRange();
       
        count.Times(() => result.AppendChar((char) totalRange.Random()));
        
        return result;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<SecureString> SecureStringSequence(this Random random, int size, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.SecureString(size, from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<SecureString> SecureStringSequenceInRange(this Random random, int size, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.SecureStringSequence(size, count);

      case 1:
        var range = ranges.First();
        return random.SecureStringSequence(size, count, (char?) range.Start.Value, (char?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();

        return count.Objects(() =>
        {
          var secure = new SecureString();
          size.Times(() => secure.AppendChar((char) totalRange.Random()));
          return secure;
        });
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static Range Range(this Random random, int? to = null) => random is not null ? System.Range.EndAt(Index.FromStart(random.Int(0, to))) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static IEnumerable<Range> RangeSequence(this Random random, int count, int? to = null) => random is not null ? count.Objects(() => random.Range(to)) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static Guid Guid(this Random random) => random is not null ? System.Guid.NewGuid() : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<Guid> GuidSequence(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(random.Guid);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static object Object(this Random random, IEnumerable<Type> types)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (types is null) throw new ArgumentNullException(nameof(types));

    return types.IsEmpty() ? new object() : types.Random().Instance<object>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static object Object(this Random random, params Type[] types) => random.Object(types as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<object> ObjectSequence(this Random random, int count, IEnumerable<Type> types)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (types is null) throw new ArgumentNullException(nameof(types));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Object(types));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<object> ObjectSequence(this Random random, int count, params Type[] types) => random.ObjectSequence(count, types as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static string FileName(this Random random) => random is not null ? Path.GetRandomFileName() : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> FileNameSequence(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(random.FileName);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static string DirectoryName(this Random random) => random is not null ? System.Guid.NewGuid().ToString("N") : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> DirectoryNameSequence(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(random.DirectoryName);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static string FilePath(this Random random, DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.FileName()) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> FilePathSequence(this Random random, int count, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.FilePath(directory));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static string DirectoryPath(this Random random, DirectoryInfo parent = null) => random is not null ? Path.Combine(parent?.FullName ?? Path.GetTempPath(), random.DirectoryName()) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<string> DirectoryPathSequence(this Random random, int count, DirectoryInfo parent = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.DirectoryPath(parent));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static DirectoryInfo Directory(this Random random, DirectoryInfo parent = null) => random is not null ? System.IO.Directory.CreateDirectory(Path.Combine(parent?.FullName ?? Path.GetTempPath(), System.Guid.NewGuid().ToString("N"))) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="parent"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<DirectoryInfo> DirectorySequence(this Random random, int count, DirectoryInfo parent = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Directory(parent));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static FileInfo File(this Random random, DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.FileName()).ToFile().CreateWithPath() : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<FileInfo> FileSequence(this Random random, int count, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.File(directory));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static FileInfo BinaryFile(this Random random, int size, byte? from = null, byte? to = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.File(directory);
    }

    var bytes = random.ByteSequence(size, from, to);

    var file = random.File(directory);

    try
    {
      bytes.WriteTo(file);
    }
    catch
    {
      file.Delete();
    }

    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<FileInfo> BinaryFileAsync(this Random random, int size, byte? from = null, byte? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return random.File(directory);
    }

    var bytes = random.ByteSequence(size, from, to);
    
    var file = random.File(directory);

    try
    {
      await bytes.WriteToAsync(file, cancellation).ConfigureAwait(false);
    }
    catch
    {
      file.Delete();
    }

    return file;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="directory"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static FileInfo BinaryFileInRange(this Random random, int size, DirectoryInfo directory = null, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.BinaryFile(size, null, null, directory);
    }

    switch (ranges.Length)
    {
      case 0:
        return random.BinaryFile(size, null, null, directory);

      case 1:
        var range = ranges.First();
        return random.BinaryFile(size, (byte?) range.Start.Value, (byte?) range.End.Value, directory);

      default:
        var totalRange = ranges.ToRange();
        var bytes = size.Objects(() => (byte) totalRange.Random());

        var file = random.File(directory);

        try
        {
          bytes.WriteTo(file);
        }
        catch
        {
          file.Delete();
        }

        return file;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<FileInfo> BinaryFileInRangeAsync(this Random random, int size, DirectoryInfo directory = null, CancellationToken cancellation = default, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return await random.BinaryFileAsync(size, null, null, directory, cancellation).ConfigureAwait(false);
    }

    switch (ranges.Length)
    {
      case 0:
        return await random.BinaryFileAsync(size, null, null, directory, cancellation).ConfigureAwait(false);

      case 1:
        var range = ranges.First();
        return await random.BinaryFileAsync(size, (byte?) range.Start.Value, (byte?) range.End.Value, directory, cancellation).ConfigureAwait(false);

      default:
        var totalRange = ranges.ToRange();
        var bytes = size.Objects(() => (byte) totalRange.Random());

        var file = random.File(directory);

        try
        {
          await bytes.WriteToAsync(file, cancellation).ConfigureAwait(false);
        }
        catch
        {
          file.Delete();
        }

        return file;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<FileInfo> BinaryFileSequence(this Random random, int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var i = 1; i <= count; i++)
    {
      yield return random.BinaryFile(size, from, to, directory);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async IAsyncEnumerable<FileInfo> BinaryFileSequenceAsync(this Random random, int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    for (var i = 1; i <= count; i++)
    {
      yield return await random.BinaryFileAsync(size, from, to, directory, cancellation).ConfigureAwait(false);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="directory"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<FileInfo> BinaryFileSequenceInRange(this Random random, int size, int count, DirectoryInfo directory = null, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Length)
    {
      case 0:
        foreach (var file in random.BinaryFileSequence(size, count, null, null, directory))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        foreach (var file in random.BinaryFileSequence(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory))
        {
          yield return file;
        }

        break;

      default:
        var totalRange = ranges.ToRange();

        for (var i = 1; i <= count; i++)
        {
          var bytes = size.Objects(() => (byte) totalRange.Random());

          var file = random.File(directory);

          try
          {
            bytes.WriteTo(file);
          }
          catch
          {
            file.Delete();
          }

          yield return file;
        }

        break;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async IAsyncEnumerable<FileInfo> BinaryFileSequenceInRangeAsync(this Random random, int size, int count, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Length)
    {
      case 0:
        await foreach (var file in random.BinaryFileSequenceAsync(size, count, null, null, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        await foreach (var file in random.BinaryFileSequenceAsync(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      default:
        var totalRange = ranges.ToRange();

        for (var i = 1; i <= count; i++)
        {
          var bytes = size.Objects(() => (byte) totalRange.Random());

          var file = random.File(directory);

          try
          {
            await bytes.WriteToAsync(file, cancellation).ConfigureAwait(false);
          }
          catch
          {
            file.Delete();
          }

          yield return file;
        }

        break;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="encoding"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static FileInfo TextFile(this Random random, int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.File(directory);
    }

    var text = random.String(size, from, to);

    return random.File(directory).WriteText(text, encoding);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="encoding"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<FileInfo> TextFileAsync(this Random random, int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return random.File(directory);
    }

    var text = random.String(size, from, to);

    return await random.File(directory).WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static FileInfo TextFileInRange(this Random random, int size, Encoding encoding = null, DirectoryInfo directory = null, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.TextFile(size, encoding, null, null, directory);
    }

    switch (ranges.Length)
    {
      case 0:
        return random.TextFile(size, encoding, null, null, directory);

      case 1:
        var range = ranges.First();
        return random.TextFile(size, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory);

      default:
        var totalRange = ranges.ToRange();
        var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
        return random.File(directory).WriteText(chars.ToText(), encoding);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<FileInfo> TextFileInRangeAsync(this Random random, int size, Encoding encoding = null, DirectoryInfo directory = null, CancellationToken cancellation = default, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return await random.TextFileAsync(size, encoding, null, null, directory, cancellation).ConfigureAwait(false);
    }

    switch (ranges.Length)
    {
      case 0:
        return await random.TextFileAsync(size, encoding, null, null, directory, cancellation).ConfigureAwait(false);

      case 1:
        var range = ranges.First();
        return await random.TextFileAsync(size, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory, cancellation).ConfigureAwait(false);

      default:
        var totalRange = ranges.ToRange();
        var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
        return await random.File(directory).WriteTextAsync(chars.ToText(), encoding, cancellation).ConfigureAwait(false);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="encoding"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<FileInfo> TextFileSequence(this Random random, int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    for (var i = 1; i <= count; i++)
    {
      yield return random.TextFile(size, encoding, from, to, directory);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="encoding"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async IAsyncEnumerable<FileInfo> TextFileSequenceAsync(this Random random, int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    for (var i = 1; i <= count; i++)
    {
      yield return await random.TextFileAsync(size, encoding, from, to, directory, cancellation).ConfigureAwait(false);
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<FileInfo> TextFileSequenceInRange(this Random random, int size, int count, Encoding encoding = null, DirectoryInfo directory = null, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Length)
    {
      case 0:
        foreach (var file in random.TextFileSequence(size, count, encoding, null, null, directory))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        foreach (var file in random.TextFileSequence(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory))
        {
          yield return file;
        }

        break;

      default:
        var totalRange = ranges.ToRange();

        for (var i = 1; i <= count; i++)
        {
          var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
          yield return random.File(directory).WriteText(chars.ToText(), encoding);
        }

        break;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async IAsyncEnumerable<FileInfo> TextFileSequenceInRangeAsync(this Random random, int size, int count, Encoding encoding = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Length)
    {
      case 0:
        await foreach (var file in random.TextFileSequenceAsync(size, count, encoding, null, null, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        await foreach (var file in random.TextFileSequenceAsync(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      default:
        var totalRange = ranges.ToRange();

        for (var i = 1; i <= count; i++)
        {
          var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
          yield return await random.File(directory).WriteTextAsync(chars.ToText(), encoding, cancellation).ConfigureAwait(false);
        }

        break;
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static IPAddress IpV6Address(this Random random) => random is not null ? new IPAddress(random.Guid().ToByteArray()) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<IPAddress> IpV6AddressSequence(this Random random, int count) => random is not null ? count.Objects(random.IpV6Address) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static PhysicalAddress PhysicalAddress(this Random random, int size, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    return new PhysicalAddress(random.ByteSequence(size, from, to).AsArray());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static PhysicalAddress PhysicalAddressInRange(this Random random, int size, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    switch (ranges.Length)
    {
      case 0:
        return random.PhysicalAddress(size);

      case 1:
        var range = ranges.First();
        return random.PhysicalAddress(size, (byte?) range.Start.Value, (byte?) range.End.Value);

      default:
        return new PhysicalAddress(random.ByteSequenceInRange(size, ranges).AsArray());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<PhysicalAddress> PhysicalAddressSequence(this Random random, int size, int count, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.PhysicalAddress(size, from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<PhysicalAddress> PhysicalAddressSequenceInRange(this Random random, int size, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.PhysicalAddressSequence(size, count);

      case 1:
        var range = ranges.First();
        return random.PhysicalAddressSequence(size, count, (byte?) range.Start.Value, (byte?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => new PhysicalAddress(new byte[size].Fill(() => (byte) totalRange.Random()).AsArray()));
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static MemoryStream MemoryStream(this Random random, int count, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    var stream = new MemoryStream();

    if (count > 0)
    {
      stream.WriteBytes(random.ByteSequence(count, from, to)).MoveToStart();
    }

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<MemoryStream> MemoryStreamAsync(this Random random, int count, byte? from = null, byte? to = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    if (count > 0)
    {
      (await stream.WriteBytesAsync(random.ByteSequence(count, from, to), cancellation).ConfigureAwait(false)).MoveToStart();
    }

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static MemoryStream MemoryStreamInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    var stream = new MemoryStream();

    if (count > 0)
    {
      var range = ranges.ToRange();
      var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.ByteSequence(count)).AsArray();

      stream.WriteBytes(bytes).MoveToStart();
    }

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="cancellation"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static async Task<MemoryStream> MemoryStreamInRangeAsync(this Random random, int count, CancellationToken cancellation = default, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    if (count > 0)
    {
      var range = ranges.ToRange();
      var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.ByteSequence(count)).AsArray();

      (await stream.WriteBytesAsync(bytes, cancellation).ConfigureAwait(false)).MoveToStart();
    }

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static Stream Stream(this Random random, byte? from = null, byte? to = null) => random is not null ? new RandomStream(from, to) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static Stream StreamInRange(this Random random, params Range[] ranges) => random is not null ? new RandomRangeStream(ranges) : throw new ArgumentNullException(nameof(random));

#if NET8_0
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static uint Uint(this Random random, uint? from = null, uint? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(uint.MinValue).MinMax(to.GetValueOrDefault(uint.MaxValue));

    return (uint) random.Long(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static uint UintInRange(this Random random, params Range[] ranges) => (uint?) random?.LongInRange(ranges) ?? throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<uint> UintSequence(this Random random, int count, uint? from = null, uint? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Uint(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<uint> UintSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.UintSequence(count);

      case 1:
        var range = ranges.First();
        return random.UintSequence(count, (uint?) range.Start.Value, (uint?) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (uint) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static long Long(this Random random, long? from = null, long? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    var range = from.GetValueOrDefault(long.MinValue).MinMax(to.GetValueOrDefault(long.MaxValue));

    return random.NextInt64(range.Min, range.Max);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static long LongInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.Long();

      case 1:
        var range = ranges.First();
        return random.Long(range.Start.Value, range.End.Value);

      default:
        return ranges.ToRange().Random();
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<long> LongSequence(this Random random, int count, long? from = null, long? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.Long(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<long> LongSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.LongSequence(count);

      case 1:
        var range = ranges.First();
        return random.LongSequence(count, range.Start.Value, range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => (long) totalRange.Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static float Float(this Random random) => random?.NextSingle() ?? throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<float> FloatSequence(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(random.Float);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static IPAddress IpAddress(this Random random, uint? from = null, uint? to = null) => random is not null ? new IPAddress(random.Uint(from, to)) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static IPAddress IpAddressInRange(this Random random, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Length)
    {
      case 0:
        return random.IpAddress();

      case 1:
        var range = ranges.First();
        return random.IpAddress((uint?) range.Start.Value, (uint?) range.End.Value);

      default:
        return new IPAddress((uint) ranges.ToRange().Random());
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<IPAddress> IpAddressSequence(this Random random, int count, uint? from = null, uint? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.IpAddress(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<IPAddress> IpAddressSequenceInRange(this Random random, int count, params Range[] ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Length)
    {
      case 0:
        return random.IpAddressSequence(count);

      case 1:
        var range = ranges.First();
        return random.IpAddressSequence(count, (uint) range.Start.Value, (uint) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => new IPAddress((uint) totalRange.Random()));
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static DateTime DateTime(this Random random, DateTime? from = null, DateTime? to = null) => random is not null ? new DateTime(random.Long((from ?? System.DateTime.MinValue).Ticks, (to ?? System.DateTime.MaxValue).Ticks), DateTimeKind.Utc) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<DateTime> DateTimeSequence(this Random random, int count, DateTime? from = null, DateTime? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.DateTime(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static DateTimeOffset DateTimeOffset(this Random random, DateTimeOffset? from = null, DateTimeOffset? to = null) => random is not null ? new DateTimeOffset(random.Long((from ?? System.DateTimeOffset.MinValue).Ticks, (to ?? System.DateTimeOffset.MaxValue).Ticks), TimeSpan.Zero) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<DateTimeOffset> DateTimeOffsetSequence(this Random random, int count, DateTimeOffset? from = null, DateTimeOffset? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.DateTimeOffset(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static DateOnly DateOnly(this Random random, DateOnly? from = null, DateOnly? to = null) => random.DateTime(from?.ToDateTime(System.TimeOnly.MinValue), to?.ToDateTime(System.TimeOnly.MaxValue)).ToDateOnly();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<DateOnly> DateOnlySequence(this Random random, int count, DateOnly? from = null, DateOnly? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.DateOnly(from, to));
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  public static TimeOnly TimeOnly(this Random random, TimeOnly? from = null, TimeOnly? to = null) => random is not null ? System.TimeOnly.FromTimeSpan(TimeSpan.FromTicks(random.Long((from ?? System.TimeOnly.MinValue).Ticks, (to ?? System.TimeOnly.MaxValue).Ticks))) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static IEnumerable<TimeOnly> TimeOnlySequence(this Random random, int count, TimeOnly? from = null, TimeOnly? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.TimeOnly(from, to));
  }
#endif

  private sealed class RandomStream : Stream
  {
    private readonly byte? min;
    private readonly byte? max;

    private Random Randomizer { get; } = new();

    public RandomStream(byte? min = null, byte? max = null)
    {
      this.min = min;
      this.max = max;
    }

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => 0;

    public override long Position
    {
      get => 0;
      set
      {
      }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      buffer.Fill(() => Randomizer.Byte(min, max), offset, count);
      return count;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Flush()
    {
    }
  }

  private sealed class RandomRangeStream : Stream
  {
    private readonly IEnumerable<int> range;

    private Random Randomizer { get; } = new();

    public RandomRangeStream(params Range[] ranges) => range = ranges.ToRange();

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => 0;

    public override long Position
    {
      get => 0;
      set
      {
      }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      buffer.Fill(() => range.Any() ? (byte) range.Random() : Randomizer.Byte(), offset, count);
      return count;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Flush()
    {
    }
  }
}