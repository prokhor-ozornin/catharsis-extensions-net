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
  /// <seealso cref="Sbyte(Random, IEnumerable{Range})"/>
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
  public static sbyte Sbyte(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="Sbyte(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<sbyte> Sbyte(this Random random, int count, sbyte? from = null, sbyte? to = null)
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
  /// <seealso cref="Sbyte(Random, int, sbyte?, sbyte?)"/>
  public static IEnumerable<sbyte> Sbyte(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Sbyte(count);

      case 1:
        var range = ranges.First();
        return random.Sbyte(count, (sbyte?) range.Start.Value, (sbyte?) range.End.Value);

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
  /// <seealso cref="Byte(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Byte(Random, byte?, byte?)"/>
  public static byte Byte(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="Byte(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<byte> Byte(this Random random, int count, byte? from = null, byte? to = null)
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
  /// <seealso cref="Byte(Random, int, byte?, byte?)"/>
  public static IEnumerable<byte> Byte(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Byte(count);

      case 1:
        var range = ranges.First();
        return random.Byte(count, (byte?) range.Start.Value, (byte?) range.End.Value);

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
  /// <seealso cref="Short(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Short(Random, short?, short?)"/>
  public static short Short(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="Short(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<short> Short(this Random random, int count, short? from = null, short? to = null)
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
  /// <seealso cref="Short(Random, int, short?, short?)"/>
  public static IEnumerable<short> Short(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Short(count);

      case 1:
        var range = ranges.First();
        return random.Short(count, (short?) range.Start.Value, (short?) range.End.Value);

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
  /// <seealso cref="Ushort(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Ushort(Random, ushort?, ushort?)"/>
  public static ushort Ushort(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="Ushort(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<ushort> Ushort(this Random random, int count, ushort? from = null, ushort? to = null)
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
  /// <seealso cref="Ushort(Random, int, ushort?, ushort?)"/>
  public static IEnumerable<ushort> Ushort(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Ushort(count);

      case 1:
        var range = ranges.First();
        return random.Ushort(count, (ushort?) range.Start.Value, (ushort?) range.End.Value);

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
  /// <seealso cref="Int(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Int(Random, int?, int?)"/>
  public static int Int(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
    {
      case 0:
        return random.Int();

      case 1:
        var range = ranges.First();
        return Int(random, (int?) range.Start.Value, range.End.Value);

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
  /// <seealso cref="Int(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<int> Int(this Random random, int count, int? from = null, int? to = null)
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
  /// <seealso cref="Int(Random, int, int?, int?)"/>
  public static IEnumerable<int> Int(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Int(count);

      case 1:
        var range = ranges.First();
        return random.Int(count, range.Start.Value, range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => totalRange.Random());
    }
  }

#if NET8_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Uint(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Uint(Random, uint?, uint?)"/>
  public static uint Uint(this Random random, IEnumerable<Range> ranges) => (uint?) random?.Long(ranges) ?? throw new ArgumentNullException(nameof(random));

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
  /// <seealso cref="Uint(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<uint> Uint(this Random random, int count, uint? from = null, uint? to = null)
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
  /// <seealso cref="Uint(Random, int, uint?, uint?)"/>
  public static IEnumerable<uint> Uint(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Uint(count);

      case 1:
        var range = ranges.First();
        return random.Uint(count, (uint?) range.Start.Value, (uint?) range.End.Value);

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
  /// <seealso cref="Long(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Long(Random, long?, long?)"/>
  public static long Long(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
    {
      case 0:
        return random.Long();

      case 1:
        var range = ranges.First();
        return Long(random, (long?) range.Start.Value, range.End.Value);

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
  /// <seealso cref="Long(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<long> Long(this Random random, int count, long? from = null, long? to = null)
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
  /// <seealso cref="Long(Random, int, long?, long?)"/>
  public static IEnumerable<long> Long(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Long(count);

      case 1:
        var range = ranges.First();
        return random.Long(count, range.Start.Value, range.End.Value);

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
  /// <seealso cref="Float(Random, int)"/>
  public static float Float(this Random random) => random?.NextSingle() ?? throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Float(Random)"/>
  public static IEnumerable<float> Float(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<float>(random.Float);
  }
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Double(Random, int)"/>
  public static double Double(this Random random) => random?.NextDouble() ?? throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Double(Random)"/>
  public static IEnumerable<double> Double(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<double>(random.Double);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Char(Random, IEnumerable{Range})"/>
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
  /// <seealso cref="Char(Random, char?, char?)"/>
  public static char Char(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="Char(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<char> Char(this Random random, int count, char? from = null, char? to = null)
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
  /// <seealso cref="Char(Random, int, char?, char?)"/>
  public static IEnumerable<char> Char(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.Char(count);

      case 1:
        var range = ranges.First();
        return random.Char(count, (char?) range.Start.Value, (char?) range.End.Value);

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
  /// <seealso cref="String(Random, int, IEnumerable{Range})"/>
  public static string String(this Random random, int count, char? from = null, char? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    
    if (count == 0)
    {
      return string.Empty;
    }

    return random.Char(count, from, to).AsArray().ToText();
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
  /// <seealso cref="String(Random, int, char?, char?)"/>
  public static string String(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return string.Empty;
    }
    
    switch (ranges.Count())
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
  /// <seealso cref="String(Random, int, int, IEnumerable{Range})"/>
  public static IEnumerable<string> String(this Random random, int size, int count, char? from = null, char? to = null)
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
  /// <seealso cref="String(Random, int, int, char?, char?)"/>
  public static IEnumerable<string> String(this Random random, int size, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.String(size, count);

      case 1:
        var range = ranges.First();
        return random.String(size, count, (char?) range.Start.Value, (char?) range.End.Value);

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
  /// <seealso cref="Digits(Random, int, int)"/>
  public static string Digits(this Random random, int count) => String(random, count, ['0'..'9']);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Digits(Random, int)"/>
  public static IEnumerable<string> Digits(this Random random, int size, int count)
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
  /// <seealso cref="Letters(Random, int, int)"/>
  public static string Letters(this Random random, int count) => random.String(count, ['a'..'z', 'A'..'Z']);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Letters(Random, int)"/>
  public static IEnumerable<string> Letters(this Random random, int size, int count)
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
  /// <seealso cref="AlphaDigits(Random, int, int)"/>
  public static string AlphaDigits(this Random random, int count) => random.String(count, ['a'..'z', 'A'..'Z', '0'..'9']);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="size"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="AlphaDigits(Random, int)"/>
  public static IEnumerable<string> AlphaDigits(this Random random, int size, int count)
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
  /// <seealso cref="SecureString(Random, int, IEnumerable{Range})"/>
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
  /// <seealso cref="SecureString(Random, int, char?, char?)"/>
  public static SecureString SecureString(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return new SecureString();
    }

    switch (ranges.Count())
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
  /// <seealso cref="SecureString(Random, int, int, IEnumerable{Range})"/>
  public static IEnumerable<SecureString> SecureString(this Random random, int size, int count, char? from = null, char? to = null)
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
  /// <seealso cref="SecureString(Random, int, int, char?, char?)"/>
  public static IEnumerable<SecureString> SecureString(this Random random, int size, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.SecureString(size, count);

      case 1:
        var range = ranges.First();
        return random.SecureString(size, count, (char?) range.Start.Value, (char?) range.End.Value);

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

#if NET8_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="DateTime(Random, int, DateTime?, DateTime?)"/>
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
  /// <seealso cref="DateTime(Random, Nullable{DateTime}, Nullable{DateTime})"/>
  public static IEnumerable<DateTime> DateTime(this Random random, int count, DateTime? from = null, DateTime? to = null)
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
  /// <seealso cref="DateTimeOffset(Random, int, DateTimeOffset?, DateTimeOffset?)"/>
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
  /// <seealso cref="DateTimeOffset(Random, Nullable{DateTimeOffset}, Nullable{DateTimeOffset})"/>
  public static IEnumerable<DateTimeOffset> DateTimeOffset(this Random random, int count, DateTimeOffset? from = null, DateTimeOffset? to = null)
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
  /// <seealso cref="DateOnly(Random, int, DateOnly?, DateOnly?)"/>
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
  /// <seealso cref="DateOnly(Random, Nullable{DateOnly}, Nullable{DateOnly})"/>
  public static IEnumerable<DateOnly> DateOnly(this Random random, int count, DateOnly? from = null, DateOnly? to = null)
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
  /// <seealso cref="TimeOnly(Random, int, TimeOnly?, TimeOnly?)"/>
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
  /// <seealso cref="TimeOnly(Random, Nullable{TimeOnly}, Nullable{TimeOnly})"/>
  public static IEnumerable<TimeOnly> TimeOnly(this Random random, int count, TimeOnly? from = null, TimeOnly? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects(() => random.TimeOnly(from, to));
  }
#endif

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Range(Random, int, int?)"/>
  public static Range Range(this Random random, int? to = null) => random is not null ? System.Range.EndAt(Index.FromStart(Int(random, (int?) 0, to))) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Range(Random, int?)"/>
  public static IEnumerable<Range> Range(this Random random, int count, int? to = null) => random is not null ? count.Objects(() => random.Range(to)) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Guid(Random, int)"/>
  public static Guid Guid(this Random random) => random is not null ? System.Guid.NewGuid() : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="Guid(Random)"/>
  public static IEnumerable<Guid> Guid(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<Guid>(random.Guid);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="types"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Object(Random, Type[])"/>
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
  /// <seealso cref="Object(Random, IEnumerable{Type})"/>
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
  /// <seealso cref="Object(Random, int, Type[])"/>
  public static IEnumerable<object> Object(this Random random, int count, IEnumerable<Type> types)
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
  /// <seealso cref="Object(Random, int, IEnumerable{Type})"/>
  public static IEnumerable<object> Object(this Random random, int count, params Type[] types) => random.Object(count, types as IEnumerable<Type>);

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="FileName(Random, int)"/>
  public static string FileName(this Random random) => random is not null ? Path.GetRandomFileName() : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="FileName(Random)"/>
  public static IEnumerable<string> FileName(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<string>(random.FileName);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="DirectoryName(Random, int)"/>
  public static string DirectoryName(this Random random) => random is not null ? System.Guid.NewGuid().ToString("N") : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="DirectoryName(Random)"/>
  public static IEnumerable<string> DirectoryName(this Random random, int count)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    return count.Objects<string>(random.DirectoryName);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="FilePath(Random, int, DirectoryInfo)"/>
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
  /// <seealso cref="FilePath(Random, DirectoryInfo)"/>
  public static IEnumerable<string> FilePath(this Random random, int count, DirectoryInfo directory = null)
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
  /// <seealso cref="DirectoryPath(Random, int, DirectoryInfo)"/>
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
  /// <seealso cref="DirectoryPath(Random, DirectoryInfo)"/>
  public static IEnumerable<string> DirectoryPath(this Random random, int count, DirectoryInfo parent = null)
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
  /// <seealso cref="Directory(Random, int, DirectoryInfo)"/>
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
  /// <seealso cref="Directory(Random, DirectoryInfo)"/>
  public static IEnumerable<DirectoryInfo> Directory(this Random random, int count, DirectoryInfo parent = null)
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
  /// <seealso cref="File(Random, int, DirectoryInfo)"/>
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
  /// <seealso cref="File(Random, DirectoryInfo)"/>
  public static IEnumerable<FileInfo> File(this Random random, int count, DirectoryInfo directory = null)
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
  /// <seealso cref="BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/>
  public static FileInfo BinaryFile(this Random random, int size, byte? from = null, byte? to = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.File(directory);
    }

    var bytes = random.Byte(size, from, to);

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
  /// <seealso cref="BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/>
  public static async Task<FileInfo> BinaryFileAsync(this Random random, int size, byte? from = null, byte? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return random.File(directory);
    }

    var bytes = random.Byte(size, from, to);
    
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
  /// <param name="ranges"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="BinaryFileAsync(Random, int,  IEnumerable{Range}, DirectoryInfo, CancellationToken)"/>
  public static FileInfo BinaryFile(this Random random, int size, IEnumerable<Range> ranges, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.BinaryFile(size, null, null, directory);
    }

    switch (ranges.Count())
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
  /// <param name="ranges"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="BinaryFile(Random, int, IEnumerable{Range}, DirectoryInfo)"/>
  public static async Task<FileInfo> BinaryFileAsync(this Random random, int size, IEnumerable<Range> ranges, DirectoryInfo directory = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return await random.BinaryFileAsync(size, null, null, directory, cancellation).ConfigureAwait(false);
    }

    switch (ranges.Count())
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
  /// <seealso cref="BinaryFileAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/>
  public static IEnumerable<FileInfo> BinaryFile(this Random random, int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null)
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
  /// <seealso cref="BinaryFile(Random, int, int, byte?, byte?, DirectoryInfo)"/>
  public static async IAsyncEnumerable<FileInfo> BinaryFileAsync(this Random random, int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
  /// <param name="ranges"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="BinaryFileAsync(Random, int, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/>
  public static IEnumerable<FileInfo> BinaryFile(this Random random, int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Count())
    {
      case 0:
        foreach (var file in random.BinaryFile(size, count, null, null, directory))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        foreach (var file in random.BinaryFile(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory))
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
  /// <param name="ranges"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="BinaryFile(Random, int, int, IEnumerable{Range}, DirectoryInfo)"/>
  public static async IAsyncEnumerable<FileInfo> BinaryFileAsync(this Random random, int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Count())
    {
      case 0:
        await foreach (var file in random.BinaryFileAsync(size, count, null, null, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        await foreach (var file in random.BinaryFileAsync(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory, cancellation).ConfigureAwait(false))
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
  /// <seealso cref="TextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/>
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
  /// <seealso cref="TextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/>
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
  /// <param name="ranges"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="TextFileAsync(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo, CancellationToken)"/>
  public static FileInfo TextFile(this Random random, int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    if (size == 0)
    {
      return random.TextFile(size, encoding, null, null, directory);
    }

    switch (ranges.Count())
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
  /// <param name="ranges"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="TextFile(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/>
  public static async Task<FileInfo> TextFileAsync(this Random random, int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    cancellation.ThrowIfCancellationRequested();

    if (size == 0)
    {
      return await random.TextFileAsync(size, encoding, null, null, directory, cancellation).ConfigureAwait(false);
    }

    switch (ranges.Count())
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
  /// <seealso cref="TextFile(Random, int, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/>
  public static IEnumerable<FileInfo> TextFile(this Random random, int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
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
  /// <seealso cref="TextFile(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/>
  public static async IAsyncEnumerable<FileInfo> TextFile(this Random random, int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
  /// <param name="ranges"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="TextFileAsync(Random, int, int, IEnumerable{Range}, Encoding, DirectoryInfo, CancellationToken)"/>
  public static IEnumerable<FileInfo> TextFile(this Random random, int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Count())
    {
      case 0:
        foreach (var file in random.TextFile(size, count, encoding, null, null, directory))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        foreach (var file in random.TextFile(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory))
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
  /// <param name="ranges"></param>
  /// <param name="encoding"></param>
  /// <param name="directory"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="TextFile(Random, int, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/>
  public static async IAsyncEnumerable<FileInfo> TextFileAsync(this Random random, int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    if (count == 0)
    {
      yield break;
    }

    switch (ranges.Count())
    {
      case 0:
        await foreach (var file in random.TextFile(size, count, encoding, null, null, directory, cancellation).ConfigureAwait(false))
        {
          yield return file;
        }

        break;

      case 1:
        var range = ranges.First();

        await foreach (var file in random.TextFile(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory, cancellation).ConfigureAwait(false))
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

#if NET8_0_OR_GREATER
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IpAddress(Random, IEnumerable{Range})"/>
  public static IPAddress IpAddress(this Random random, uint? from = null, uint? to = null) => random is not null ? new IPAddress(random.Uint(from, to)) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IpAddress(Random, uint?, uint?)"/>
  public static IPAddress IpAddress(this Random random, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));

    switch (ranges.Count())
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
  /// <seealso cref="IpAddress(Random, int, IEnumerable{Range})"/>
  public static IEnumerable<IPAddress> IpAddress(this Random random, int count, uint? from = null, uint? to = null)
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
  /// <seealso cref="IpAddress(Random, int, uint?, uint?)"/>
  public static IEnumerable<IPAddress> IpAddress(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.IpAddress(count);

      case 1:
        var range = ranges.First();
        return random.IpAddress(count, (uint) range.Start.Value, (uint) range.End.Value);

      default:
        var totalRange = ranges.ToRange();
        return count.Objects(() => new IPAddress((uint) totalRange.Random()));
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="IpV6Address(Random, int)"/>
  public static IPAddress IpV6Address(this Random random) => random is not null ? new IPAddress(random.Guid().ToByteArray()) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="IpV6Address(Random)"/>
  public static IEnumerable<IPAddress> IpV6Address(this Random random, int count) => random is not null ? count.Objects<IPAddress>(random.IpV6Address) : throw new ArgumentNullException(nameof(random));
#endif

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
  /// <seealso cref="PhysicalAddress(Random, int, IEnumerable{Range})"/>
  public static PhysicalAddress PhysicalAddress(this Random random, int size, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    return new PhysicalAddress(random.Byte(size, from, to).AsArray());
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
  /// <seealso cref="PhysicalAddress(Random, int, byte?, byte?)"/>
  public static PhysicalAddress PhysicalAddress(this Random random, int size, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

    switch (ranges.Count())
    {
      case 0:
        return random.PhysicalAddress(size);

      case 1:
        var range = ranges.First();
        return random.PhysicalAddress(size, (byte?) range.Start.Value, (byte?) range.End.Value);

      default:
        return new PhysicalAddress(random.Byte(size, ranges).AsArray());
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
  /// <seealso cref="PhysicalAddress(Random, int, int, IEnumerable{Range})"/>
  public static IEnumerable<PhysicalAddress> PhysicalAddress(this Random random, int size, int count, byte? from = null, byte? to = null)
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
  /// <seealso cref="PhysicalAddress(Random, int, int, byte?, byte?)"/>
  public static IEnumerable<PhysicalAddress> PhysicalAddress(this Random random, int size, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    if (count == 0)
    {
      return [];
    }

    switch (ranges.Count())
    {
      case 0:
        return random.PhysicalAddress(size, count);

      case 1:
        var range = ranges.First();
        return random.PhysicalAddress(size, count, (byte?) range.Start.Value, (byte?) range.End.Value);

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
  /// <seealso cref="MemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/>
  public static MemoryStream MemoryStream(this Random random, int count, byte? from = null, byte? to = null)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    var stream = new MemoryStream();

    if (count > 0)
    {
      stream.WriteBytes(random.Byte(count, from, to)).MoveToStart();
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
  /// <seealso cref="MemoryStream(Random, int, byte?, byte?)"/>
  public static async Task<MemoryStream> MemoryStreamAsync(this Random random, int count, byte? from = null, byte? to = null, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    if (count > 0)
    {
      (await stream.WriteBytesAsync(random.Byte(count, from, to), cancellation).ConfigureAwait(false)).MoveToStart();
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
  /// <seealso cref="MemoryStreamAsync(Random, int, IEnumerable{Range}, CancellationToken)"/>
  public static MemoryStream MemoryStream(this Random random, int count, IEnumerable<Range> ranges)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    var stream = new MemoryStream();

    if (count > 0)
    {
      var range = ranges.ToRange();
      var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.Byte(count)).AsArray();

      stream.WriteBytes(bytes).MoveToStart();
    }

    return stream;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="count"></param>
  /// <param name="ranges"></param>
  /// <param name="cancellation"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  /// <seealso cref="MemoryStream(Random, int, IEnumerable{Range})"/>
  public static async Task<MemoryStream> MemoryStreamAsync(this Random random, int count, IEnumerable<Range> ranges, CancellationToken cancellation = default)
  {
    if (random is null) throw new ArgumentNullException(nameof(random));
    if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

    cancellation.ThrowIfCancellationRequested();

    var stream = new MemoryStream();

    if (count > 0)
    {
      var range = ranges.ToRange();
      var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.Byte(count)).AsArray();

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
  /// <seealso cref="Stream(Random, IEnumerable{Range})"/>
  public static Stream Stream(this Random random, byte? from = null, byte? to = null) => random is not null ? new RandomStream(from, to) : throw new ArgumentNullException(nameof(random));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="random"></param>
  /// <param name="ranges"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
  /// <seealso cref="Stream(Random, byte?, byte?)"/>
  public static Stream Stream(this Random random, IEnumerable<Range> ranges) => random is not null ? new RandomRangeStream(ranges) : throw new ArgumentNullException(nameof(random));

  private sealed class RandomStream : Stream
  {
    private byte? Min { get; }
    private byte? Max { get; }
    private Random Randomizer { get; } = new();

    public RandomStream(byte? min = null, byte? max = null)
    {
      Min = min;
      Max = max;
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
      buffer.Fill(() => Randomizer.Byte(Min, Max), offset, count);
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
    private IEnumerable<int> Range { get; }
    private Random Randomizer { get; } = new();

    public RandomRangeStream(IEnumerable<Range> ranges) => Range = ranges.ToRange();

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
      buffer.Fill(() => Range.Any() ? (byte) Range.Random() : Randomizer.Byte(), offset, count);
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