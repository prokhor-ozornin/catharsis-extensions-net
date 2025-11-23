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
  /// <param name="random"></param>
  extension(Random random)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Sbyte(Random, IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public sbyte Sbyte(sbyte? from = null, sbyte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(sbyte.MinValue).MinMax(to.GetValueOrDefault(sbyte.MaxValue));
    
      return (sbyte) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    [CLSCompliant(false)]
    public sbyte Sbyte => random.Sbyte();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Sbyte(Random, sbyte?, sbyte?)"/>
    [CLSCompliant(false)]
    public sbyte Sbyte(IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Sbyte(Random, int, IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public IEnumerable<sbyte> Sbyte(int count, sbyte? from = null, sbyte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Sbyte(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Sbyte(Random, int, sbyte?, sbyte?)"/>
    [CLSCompliant(false)]
    public IEnumerable<sbyte> Sbyte(int count, IEnumerable<Range> ranges)
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
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Byte(Random, IEnumerable{System.Range})"/>
    public byte Byte(byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(byte.MinValue).MinMax(to.GetValueOrDefault(byte.MaxValue));

      return (byte) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public byte Byte => random.Byte();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Byte(Random, byte?, byte?)"/>
    public byte Byte(IEnumerable<Range> ranges)
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
    /// <param name="count">Number of bytes to generate.</param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>Array of randomly generated bytes. Length of array is equal to <paramref name="count"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Byte(Random, int, IEnumerable{System.Range})"/>
    public IEnumerable<byte> Byte(int count, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Byte(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Byte(Random, int, byte?, byte?)"/>
    public IEnumerable<byte> Byte(int count, IEnumerable<Range> ranges)
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
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Short(Random, IEnumerable{System.Range})"/>
    public short Short(short? from = null, short? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(short.MinValue).MinMax(to.GetValueOrDefault(short.MaxValue));

      return (short) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public short Short => random.Short();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Short(Random, short?, short?)"/>
    public short Short(IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Short(Random, int, IEnumerable{System.Range})"/>
    public IEnumerable<short> Short(int count, short? from = null, short? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Short(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Short(Random, int, short?, short?)"/>
    public IEnumerable<short> Short(int count, IEnumerable<Range> ranges)
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
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Ushort(Random, IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public ushort Ushort(ushort? from = null, ushort? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(ushort.MinValue).MinMax(to.GetValueOrDefault(ushort.MaxValue));

      return (ushort) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    [CLSCompliant(false)]
    public ushort Ushort => random.Ushort();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Ushort(Random, ushort?, ushort?)"/>
    [CLSCompliant(false)]
    public ushort Ushort(IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Ushort(Random, int, IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public IEnumerable<ushort> Ushort(int count, ushort? from = null, ushort? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Ushort(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Ushort(Random, int, ushort?, ushort?)"/>
    [CLSCompliant(false)]
    public IEnumerable<ushort> Ushort(int count, IEnumerable<Range> ranges)
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
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Int(Random, IEnumerable{System.Range})"/>
    public int Int(int? from = null, int? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(int.MinValue).MinMax(to.GetValueOrDefault(int.MaxValue));

      return random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public int Int => random.Int();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Int(Random, int?, int?)"/>
    public int Int(IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Int(Random, int, IEnumerable{System.Range})"/>
    public IEnumerable<int> Int(int count, int? from = null, int? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Int(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Int(Random, int, int?, int?)"/>
    public IEnumerable<int> Int(int count, IEnumerable<Range> ranges)
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Double(Random, int)"/>
    public double Double() => random?.NextDouble() ?? throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public double Double => random.Double();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Double(Random)"/>
    public IEnumerable<double> Double(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<double>(random.Double);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Char(Random, IEnumerable{System.Range})"/>
    public char Char(char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(char.MinValue).MinMax(to.GetValueOrDefault(char.MaxValue));

      return (char) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public char Char => random.Char();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Char(Random, char?, char?)"/>
    public char Char(IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Char(Random, int, IEnumerable{System.Range})"/>
    public IEnumerable<char> Char(int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Char(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Char(Random, int, char?, char?)"/>
    public IEnumerable<char> Char(int count, IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="String(Random, int, IEnumerable{System.Range})"/>
    public string String(int count, char? from = null, char? to = null)
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
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="String(Random, int, char?, char?)"/>
    public string String(int count, IEnumerable<Range> ranges)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="String(Random, int, int, IEnumerable{System.Range})"/>
    public IEnumerable<string> String(int size, int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.String(size, from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="String(Random, int, int, char?, char?)"/>
    public IEnumerable<string> String(int size, int count, IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Digits(Random, int, int)"/>
    public string Digits(int count) => String(random, count, ['0'..'9']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Digits(Random, int)"/>
    public IEnumerable<string> Digits(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Digits(size));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Letters(Random, int, int)"/>
    public string Letters(int count) => random.String(count, ['a'..'z', 'A'..'Z']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Letters(Random, int)"/>
    public IEnumerable<string> Letters(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Letters(size));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="AlphaDigits(Random, int, int)"/>
    public string AlphaDigits(int count) => random.String(count, ['a'..'z', 'A'..'Z', '0'..'9']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="AlphaDigits(Random, int)"/>
    public IEnumerable<string> AlphaDigits(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.AlphaDigits(size));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="SecureString(Random, int, IEnumerable{System.Range})"/>
    public SecureString SecureString(int count, char? from = null, char? to = null)
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
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="SecureString(Random, int, char?, char?)"/>
    public SecureString SecureString(int count, IEnumerable<Range> ranges)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="SecureString(Random, int, int, IEnumerable{System.Range})"/>
    public IEnumerable<SecureString> SecureString(int size, int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.SecureString(size, from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="SecureString(Random, int, int, char?, char?)"/>
    public IEnumerable<SecureString> SecureString(int size, int count, IEnumerable<Range> ranges)
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Range(Random, int, int?)"/>
    public Range Range(int? to = null) => random is not null ? System.Range.EndAt(Index.FromStart(Int(random, (int?) 0, to))) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public Range Range => random.Range();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Range(Random, int?)"/>
    public IEnumerable<Range> Range(int count, int? to = null) => random is not null ? count.Objects(() => random.Range(to)) : throw new ArgumentNullException(nameof(random));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Guid(Random, int)"/>
    public Guid Guid() => random is not null ? System.Guid.NewGuid() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public Guid Guid => random.Guid();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Guid(Random)"/>
    public IEnumerable<Guid> Guid(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<Guid>(random.Guid);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Object(Random, Type[])"/>
    public object Object(IEnumerable<Type> types)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (types is null) throw new ArgumentNullException(nameof(types));

      return types.IsEmpty() ? new object() : types.Random().Instance<object>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Object(Random, IEnumerable{Type})"/>
    public object Object(params Type[] types) => random.Object(types as IEnumerable<Type>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Object(Random, int, Type[])"/>
    public IEnumerable<object> Object(int count, IEnumerable<Type> types)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (types is null) throw new ArgumentNullException(nameof(types));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Object(types));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Object(Random, int, IEnumerable{Type})"/>
    public IEnumerable<object> Object(int count, params Type[] types) => random.Object(count, types as IEnumerable<Type>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="FileName(Random, int)"/>
    public string FileName() => random is not null ? Path.GetRandomFileName() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string FileName => random.FileName();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="FileName(Random)"/>
    public IEnumerable<string> FileName(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<string>(random.FileName);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DirectoryName(Random, int)"/>
    public string DirectoryName() => random is not null ? System.Guid.NewGuid().ToString("N") : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string DirectoryName => random.DirectoryName();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="DirectoryName(Random)"/>
    public IEnumerable<string> DirectoryName(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<string>(random.DirectoryName);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="FilePath(Random, int, DirectoryInfo)"/>
    public string FilePath(DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.FileName()) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string FilePath => random.FilePath();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="FilePath(Random, DirectoryInfo)"/>
    public IEnumerable<string> FilePath(int count, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.FilePath(directory));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="DirectoryPath(Random, int, DirectoryInfo)"/>
    public string DirectoryPath(DirectoryInfo parent = null) => random is not null ? Path.Combine(parent?.FullName ?? Path.GetTempPath(), random.DirectoryName()) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public string DirectoryPath => random.DirectoryPath();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="DirectoryPath(Random, DirectoryInfo)"/>
    public IEnumerable<string> DirectoryPath(int count, DirectoryInfo parent = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.DirectoryPath(parent));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Directory(Random, int, DirectoryInfo)"/>
    public DirectoryInfo Directory(DirectoryInfo parent = null) => random is not null ? System.IO.Directory.CreateDirectory(Path.Combine(parent?.FullName ?? Path.GetTempPath(), System.Guid.NewGuid().ToString("N"))) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public DirectoryInfo Directory => random.Directory();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Directory(Random, DirectoryInfo)"/>
    public IEnumerable<DirectoryInfo> Directory(int count, DirectoryInfo parent = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.Directory(parent));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="File(Random, int, DirectoryInfo)"/>
    public FileInfo File(DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.FileName()).ToFile().CreateWithPath() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public FileInfo File => random.File();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="File(Random, DirectoryInfo)"/>
    public IEnumerable<FileInfo> File(int count, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.File(directory));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/>
    public FileInfo BinaryFile(int size, byte? from = null, byte? to = null, DirectoryInfo directory = null)
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
    /// <param name="size"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="directory"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/>
    public async Task<FileInfo> BinaryFileAsync(int size, byte? from = null, byte? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="ranges"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFileAsync(Random, int,  IEnumerable{System.Range}, DirectoryInfo, CancellationToken)"/>
    public FileInfo BinaryFile(int size, IEnumerable<Range> ranges, DirectoryInfo directory = null)
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
    /// <param name="size"></param>
    /// <param name="ranges"></param>
    /// <param name="directory"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFile(Random, int, IEnumerable{System.Range}, DirectoryInfo)"/>
    public async Task<FileInfo> BinaryFileAsync(int size, IEnumerable<Range> ranges, DirectoryInfo directory = null, CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFileAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/>
    public IEnumerable<FileInfo> BinaryFile(int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null)
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
    public async IAsyncEnumerable<FileInfo> BinaryFileAsync(int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFileAsync(Random, int, int, IEnumerable{System.Range}, DirectoryInfo, CancellationToken)"/>
    public IEnumerable<FileInfo> BinaryFile(int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <param name="directory"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="BinaryFile(Random, int, int, IEnumerable{System.Range}, DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> BinaryFileAsync(int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="encoding"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="TextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/>
    public FileInfo TextFile(int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
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
    public async Task<FileInfo> TextFileAsync(int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="ranges"></param>
    /// <param name="encoding"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="TextFileAsync(Random, int, IEnumerable{System.Range}, Encoding, DirectoryInfo, CancellationToken)"/>
    public FileInfo TextFile(int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
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
    /// <param name="size"></param>
    /// <param name="ranges"></param>
    /// <param name="encoding"></param>
    /// <param name="directory"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="TextFile(Random, int, IEnumerable{System.Range}, Encoding, DirectoryInfo)"/>
    public async Task<FileInfo> TextFileAsync(int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
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
    public IEnumerable<FileInfo> TextFile(int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
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
    public async IAsyncEnumerable<FileInfo> TextFile(int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <param name="encoding"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="TextFileAsync(Random, int, int, IEnumerable{System.Range}, Encoding, DirectoryInfo, CancellationToken)"/>
    public IEnumerable<FileInfo> TextFile(int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <param name="encoding"></param>
    /// <param name="directory"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="TextFile(Random, int, int, IEnumerable{System.Range}, Encoding, DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> TextFileAsync(int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="PhysicalAddress(Random, int, IEnumerable{System.Range})"/>
    public PhysicalAddress PhysicalAddress(int size, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      return new PhysicalAddress(random.Byte(size, from, to).AsArray());
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="PhysicalAddress(Random, int, byte?, byte?)"/>
    public PhysicalAddress PhysicalAddress(int size, IEnumerable<Range> ranges)
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
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="PhysicalAddress(Random, int, int, IEnumerable{System.Range})"/>
    public IEnumerable<PhysicalAddress> PhysicalAddress(int size, int count, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.PhysicalAddress(size, from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="PhysicalAddress(Random, int, int, byte?, byte?)"/>
    public IEnumerable<PhysicalAddress> PhysicalAddress(int size, int count, IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="MemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/>
    public MemoryStream MemoryStream(int count, byte? from = null, byte? to = null)
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
    /// <param name="count"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="MemoryStream(Random, int, byte?, byte?)"/>
    public async Task<MemoryStream> MemoryStreamAsync(int count, byte? from = null, byte? to = null, CancellationToken cancellation = default)
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
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="MemoryStreamAsync(Random, int, IEnumerable{System.Range}, CancellationToken)"/>
    public MemoryStream MemoryStream(int count, IEnumerable<Range> ranges)
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
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="MemoryStream(Random, int, IEnumerable{System.Range})"/>
    public async Task<MemoryStream> MemoryStreamAsync(int count, IEnumerable<Range> ranges, CancellationToken cancellation = default)
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
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Stream(Random, IEnumerable{System.Range})"/>
    public Stream Stream(byte? from = null, byte? to = null) => random is not null ? new RandomStream(from, to) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para>[NEW]</para>
    /// </summary>
    public Stream Stream => random.Stream();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Stream(Random, byte?, byte?)"/>
    public Stream Stream(IEnumerable<Range> ranges) => random is not null ? new RandomRangeStream(ranges) : throw new ArgumentNullException(nameof(random));
  }

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