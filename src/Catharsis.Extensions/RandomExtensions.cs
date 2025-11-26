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
    [CLSCompliant(false)]
    public sbyte Sbyte => random.ToSbyte();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public byte Byte => random.ToByte();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public short Short => random.ToShort();

    /// <summary>
    ///   <para></para>
    /// </summary>
    [CLSCompliant(false)]
    public ushort Ushort => random.ToUshort();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public int Int => random.ToInt();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public double Double => random.ToDouble();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public char Char => random.ToChar();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Range Range => random.ToRange();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Guid Guid => random.ToGuid();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string FileName => random.ToFileName();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string DirectoryName => random.ToDirectoryName();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string FilePath => random.ToFilePath();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public string DirectoryPath => random.ToDirectoryPath();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public FileInfo File => random.ToFile();

    /// <summary>
    ///   <para></para>
    /// </summary>
    public Stream Stream => random.ToStream();

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToSbyte(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public sbyte ToSbyte(sbyte? from = null, sbyte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(sbyte.MinValue).MinMax(to.GetValueOrDefault(sbyte.MaxValue));
    
      return (sbyte) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="ToSbyte(Random, sbyte?, sbyte?)"/>
    [CLSCompliant(false)]
    public sbyte ToSbyte(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToSbyte();

        case 1:
          var range = ranges.First();
          return random.ToSbyte((sbyte?) range.Start.Value, (sbyte?) range.End.Value);

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
    /// <seealso cref="ToSbyte(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public IEnumerable<sbyte> ToSbyte(int count, sbyte? from = null, sbyte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToSbyte(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToSbyte(System.Random,int,sbyte?,sbyte?)"/>
    [CLSCompliant(false)]
    public IEnumerable<sbyte> ToSbyte(int count, IEnumerable<Range> ranges)
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
          return random.ToSbyte(count);

        case 1:
          var range = ranges.First();
          return random.ToSbyte(count, (sbyte?) range.Start.Value, (sbyte?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToByte(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    public byte ToByte(byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(byte.MinValue).MinMax(to.GetValueOrDefault(byte.MaxValue));

      return (byte) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Byte(Random, byte?, byte?)"/>
    public byte ToByte(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToByte();

        case 1:
          var range = ranges.First();
          return random.ToByte((byte?) range.Start.Value, (byte?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToByte(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<byte> ToByte(int count, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToByte(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToByte(System.Random,int,byte?,byte?)"/>
    public IEnumerable<byte> ToByte(int count, IEnumerable<Range> ranges)
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
          return random.ToByte(count);

        case 1:
          var range = ranges.First();
          return random.ToByte(count, (byte?) range.Start.Value, (byte?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToShort(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    public short ToShort(short? from = null, short? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(short.MinValue).MinMax(to.GetValueOrDefault(short.MaxValue));

      return (short) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Short(Random, short?, short?)"/>
    public short ToShort(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToShort();

        case 1:
          var range = ranges.First();
          return random.ToShort((short?) range.Start.Value, (short?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToShort(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<short> ToShort(int count, short? from = null, short? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToShort(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToShort(System.Random,int,short?,short?)"/>
    public IEnumerable<short> ToShort(int count, IEnumerable<Range> ranges)
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
          return random.ToShort(count);

        case 1:
          var range = ranges.First();
          return random.ToShort(count, (short?) range.Start.Value, (short?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToUshort(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public ushort ToUshort(ushort? from = null, ushort? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(ushort.MinValue).MinMax(to.GetValueOrDefault(ushort.MaxValue));

      return (ushort) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Ushort(Random, ushort?, ushort?)"/>
    [CLSCompliant(false)]
    public ushort ToUshort(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToUshort();

        case 1:
          var range = ranges.First();
          return random.ToUshort((ushort?) range.Start.Value, (ushort?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToUshort(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    [CLSCompliant(false)]
    public IEnumerable<ushort> ToUshort(int count, ushort? from = null, ushort? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToUshort(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToUshort(System.Random,int,ushort?,ushort?)"/>
    [CLSCompliant(false)]
    public IEnumerable<ushort> ToUshort(int count, IEnumerable<Range> ranges)
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
          return random.ToUshort(count);

        case 1:
          var range = ranges.First();
          return random.ToUshort(count, (ushort?) range.Start.Value, (ushort?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToInt(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    public int ToInt(int? from = null, int? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(int.MinValue).MinMax(to.GetValueOrDefault(int.MaxValue));

      return random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Int(Random, int?, int?)"/>
    public int ToInt(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToInt();

        case 1:
          var range = ranges.First();
          return ToInt(random, (int?) range.Start.Value, range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToInt(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<int> ToInt(int count, int? from = null, int? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToInt(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToInt(System.Random,int,int?,int?)"/>
    public IEnumerable<int> ToInt(int count, IEnumerable<Range> ranges)
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
          return random.ToInt(count);

        case 1:
          var range = ranges.First();
          return random.ToInt(count, range.Start.Value, range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToDouble(System.Random,int)"/>
    public double ToDouble() => random?.NextDouble() ?? throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Double(Random)"/>
    public IEnumerable<double> ToDouble(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<double>(random.ToDouble);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToChar(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    public char ToChar(char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      var range = from.GetValueOrDefault(char.MinValue).MinMax(to.GetValueOrDefault(char.MaxValue));

      return (char) random.Next(range.Min, range.Max);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Char(Random, char?, char?)"/>
    public char ToChar(IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));

      switch (ranges.Count())
      {
        case 0:
          return random.ToChar();

        case 1:
          var range = ranges.First();
          return random.ToChar((char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToChar(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<char> ToChar(int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToChar(from, to));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToChar(System.Random,int,char?,char?)"/>
    public IEnumerable<char> ToChar(int count, IEnumerable<Range> ranges)
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
          return random.ToChar(count);

        case 1:
          var range = ranges.First();
          return random.ToChar(count, (char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToText(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public string ToText(int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
    
      if (count == 0)
      {
        return string.Empty;
      }

      return random.ToChar(count, from, to).AsArray().ToText();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToString(System.Random,int,char?,char?)"/>
    public string ToText(int count, IEnumerable<Range> ranges)
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
          return random.ToText(count);

        case 1:
          var range = ranges.First();
          return random.ToText(count, (char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToText(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<string> ToText(int size, int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToText(size, from, to));
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
    /// <seealso cref="RandomExtensions.ToText(System.Random,int,int,char?,char?)"/>
    public IEnumerable<string> ToText(int size, int count, IEnumerable<Range> ranges)
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
          return random.ToText(size, count);

        case 1:
          var range = ranges.First();
          return random.ToText(size, count, (char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToDigits(System.Random,int,int)"/>
    public string ToDigits(int count) => random.ToText(count, ['0'..'9']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Digits(Random, int)"/>
    public IEnumerable<string> ToDigits(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToDigits(size));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToLetters(System.Random,int,int)"/>
    public string ToLetters(int count) => random.ToText(count, ['a'..'z', 'A'..'Z']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Letters(Random, int)"/>
    public IEnumerable<string> ToLetters(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToLetters(size));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToAlphaDigits(System.Random,int,int)"/>
    public string ToAlphaDigits(int count) => random.ToText(count, ['a'..'z', 'A'..'Z', '0'..'9']);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="AlphaDigits(Random, int)"/>
    public IEnumerable<string> ToAlphaDigits(int size, int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToAlphaDigits(size));
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
    /// <seealso cref="RandomExtensions.ToSecureString(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public SecureString ToSecureString(int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      if (count == 0)
      {
        return new SecureString();
      }

      var result = new SecureString();

      count.Times(() => result.AppendChar(random.ToChar(from, to)));

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
    public SecureString ToSecureString(int count, IEnumerable<Range> ranges)
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
          return random.ToSecureString(count);

        case 1:
          var range = ranges.First();
          return random.ToSecureString(count, (char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToSecureString(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<SecureString> ToSecureString(int size, int count, char? from = null, char? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToSecureString(size, from, to));
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
    /// <seealso cref="RandomExtensions.ToSecureString(System.Random,int,int,char?,char?)"/>
    public IEnumerable<SecureString> ToSecureString(int size, int count, IEnumerable<Range> ranges)
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
          return random.ToSecureString(size, count);

        case 1:
          var range = ranges.First();
          return random.ToSecureString(size, count, (char?) range.Start.Value, (char?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToRange(System.Random,int,int?)"/>
    public Range ToRange(int? to = null) => random is not null ? System.Range.EndAt(Index.FromStart(ToInt(random, (int?) 0, to))) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Range(Random, int?)"/>
    public IEnumerable<Range> ToRange(int count, int? to = null) => random is not null ? count.Objects(() => random.ToRange(to)) : throw new ArgumentNullException(nameof(random));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToGuid(System.Random,int)"/>
    public Guid ToGuid() => random is not null ? System.Guid.NewGuid() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Guid(Random)"/>
    public IEnumerable<Guid> ToGuid(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<Guid>(random.ToGuid);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToObject(System.Random,System.Type[])"/>
    public object ToObject(IEnumerable<Type> types)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (types is null) throw new ArgumentNullException(nameof(types));

      return types.IsEmpty ? new object() : types.Random().Instance<object>();
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Object(Random, IEnumerable{Type})"/>
    public object ToObject(params Type[] types) => random.ToObject(types as IEnumerable<Type>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToObject(System.Random,int,System.Type[])"/>
    public IEnumerable<object> ToObject(int count, IEnumerable<Type> types)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (types is null) throw new ArgumentNullException(nameof(types));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToObject(types));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="types"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="RandomExtensions.ToObject(System.Random,int,System.Collections.Generic.IEnumerable{System.Type})"/>
    public IEnumerable<object> ToObject(int count, params Type[] types) => random.ToObject(count, types as IEnumerable<Type>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToFileName(System.Random,int)"/>
    public string ToFileName() => random is not null ? Path.GetRandomFileName() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="FileName(Random)"/>
    public IEnumerable<string> ToFileName(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<string>(random.ToFileName);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToDirectoryName(System.Random,int)"/>
    public string ToDirectoryName() => random is not null ? System.Guid.NewGuid().ToString("N") : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="DirectoryName(Random)"/>
    public IEnumerable<string> ToDirectoryName(int count)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects<string>(random.ToDirectoryName);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToFilePath(System.Random,int,System.IO.DirectoryInfo)"/>
    public string ToFilePath(DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.ToFileName()) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="FilePath(Random, DirectoryInfo)"/>
    public IEnumerable<string> ToFilePath(int count, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToFilePath(directory));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToDirectoryPath(System.Random,int,System.IO.DirectoryInfo)"/>
    public string ToDirectoryPath(DirectoryInfo parent = null) => random is not null ? Path.Combine(parent?.FullName ?? Path.GetTempPath(), random.ToDirectoryName()) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="DirectoryPath(Random, DirectoryInfo)"/>
    public IEnumerable<string> ToDirectoryPath(int count, DirectoryInfo parent = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToDirectoryPath(parent));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToDirectory(System.Random,int,System.IO.DirectoryInfo)"/>
    public DirectoryInfo ToDirectory(DirectoryInfo parent = null) => random is not null ? System.IO.Directory.CreateDirectory(Path.Combine(parent?.FullName ?? Path.GetTempPath(), System.Guid.NewGuid().ToString("N"))) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Directory(Random, DirectoryInfo)"/>
    public IEnumerable<DirectoryInfo> ToDirectory(int count, DirectoryInfo parent = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToDirectory(parent));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <seealso cref="RandomExtensions.ToFile(System.Random,int,System.IO.DirectoryInfo)"/>
    public FileInfo ToFile(DirectoryInfo directory = null) => random is not null ? Path.Combine(directory?.FullName ?? Path.GetTempPath(), random.ToFileName()).ToFile().CreateWithPath() : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="count"></param>
    /// <param name="directory"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="File(Random, DirectoryInfo)"/>
    public IEnumerable<FileInfo> ToFile(int count, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToFile(directory));
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
    /// <seealso cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,byte?,byte?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public FileInfo ToBinaryFile(int size, byte? from = null, byte? to = null, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      if (size == 0)
      {
        return random.ToFile(directory);
      }

      var bytes = random.ToByte(size, from, to);

      var file = random.ToFile(directory);

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
    public async Task<FileInfo> ToBinaryFileAsync(int size, byte? from = null, byte? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      cancellation.ThrowIfCancellationRequested();

      if (size == 0)
      {
        return random.ToFile(directory);
      }

      var bytes = random.ToByte(size, from, to);
    
      var file = random.ToFile(directory);

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
    /// <seealso cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public FileInfo ToBinaryFile(int size, IEnumerable<Range> ranges, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      if (size == 0)
      {
        return random.ToBinaryFile(size, null, null, directory);
      }

      switch (ranges.Count())
      {
        case 0:
          return random.ToBinaryFile(size, null, null, directory);

        case 1:
          var range = ranges.First();
          return random.ToBinaryFile(size, (byte?) range.Start.Value, (byte?) range.End.Value, directory);

        default:
          var totalRange = ranges.ToRange();
          var bytes = size.Objects(() => (byte) totalRange.Random());

          var file = random.ToFile(directory);

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
    /// <seealso cref="RandomExtensions.ToBinaryFile(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo)"/>
    public async Task<FileInfo> ToBinaryFileAsync(int size, IEnumerable<Range> ranges, DirectoryInfo directory = null, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      cancellation.ThrowIfCancellationRequested();

      if (size == 0)
      {
        return await random.ToBinaryFileAsync(size, null, null, directory, cancellation).ConfigureAwait(false);
      }

      switch (ranges.Count())
      {
        case 0:
          return await random.ToBinaryFileAsync(size, null, null, directory, cancellation).ConfigureAwait(false);

        case 1:
          var range = ranges.First();
          return await random.ToBinaryFileAsync(size, (byte?) range.Start.Value, (byte?) range.End.Value, directory, cancellation).ConfigureAwait(false);

        default:
          var totalRange = ranges.ToRange();
          var bytes = size.Objects(() => (byte) totalRange.Random());

          var file = random.ToFile(directory);

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
    /// <seealso cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,int,byte?,byte?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public IEnumerable<FileInfo> ToBinaryFile(int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      for (var i = 1; i <= count; i++)
      {
        yield return random.ToBinaryFile(size, from, to, directory);
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
    /// <seealso cref="RandomExtensions.ToBinaryFile(System.Random,int,int,byte?,byte?,System.IO.DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> ToBinaryFileAsync(int size, int count, byte? from = null, byte? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      cancellation.ThrowIfCancellationRequested();

      for (var i = 1; i <= count; i++)
      {
        yield return await random.ToBinaryFileAsync(size, from, to, directory, cancellation).ConfigureAwait(false);
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
    /// <seealso cref="RandomExtensions.ToBinaryFileAsync"/>
    public IEnumerable<FileInfo> ToBinaryFile(int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null)
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
          foreach (var file in random.ToBinaryFile(size, count, null, null, directory))
          {
            yield return file;
          }

          break;

        case 1:
          var range = ranges.First();

          foreach (var file in random.ToBinaryFile(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory))
          {
            yield return file;
          }

          break;

        default:
          var totalRange = ranges.ToRange();

          for (var i = 1; i <= count; i++)
          {
            var bytes = size.Objects(() => (byte) totalRange.Random());

            var file = random.ToFile(directory);

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
    /// <seealso cref="RandomExtensions.ToBinaryFile(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> ToBinaryFileAsync(int size, int count, IEnumerable<Range> ranges, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
          await foreach (var file in random.ToBinaryFileAsync(size, count, null, null, directory, cancellation).ConfigureAwait(false))
          {
            yield return file;
          }

          break;

        case 1:
          var range = ranges.First();

          await foreach (var file in random.ToBinaryFileAsync(size, count, (byte?) range.Start.Value, (byte?) range.End.Value, directory, cancellation).ConfigureAwait(false))
          {
            yield return file;
          }

          break;

        default:
          var totalRange = ranges.ToRange();

          for (var i = 1; i <= count; i++)
          {
            var bytes = size.Objects(() => (byte) totalRange.Random());

            var file = random.ToFile(directory);

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
    /// <seealso cref="RandomExtensions.ToTextFileAsync(System.Random,int,System.Text.Encoding,char?,char?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public FileInfo ToTextFile(int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      if (size == 0)
      {
        return random.ToFile(directory);
      }

      var text = random.ToText(size, from, to);

      return random.ToFile(directory).WriteText(text, encoding);
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
    public async Task<FileInfo> ToTextFileAsync(int size, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      cancellation.ThrowIfCancellationRequested();

      if (size == 0)
      {
        return random.ToFile(directory);
      }

      var text = random.ToText(size, from, to);

      return await random.ToFile(directory).WriteTextAsync(text, encoding, cancellation).ConfigureAwait(false);
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
    /// <seealso cref="RandomExtensions.ToTextFileAsync"/>
    public FileInfo ToTextFile(int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      if (size == 0)
      {
        return random.ToTextFile(size, encoding, null, null, directory);
      }

      switch (ranges.Count())
      {
        case 0:
          return random.ToTextFile(size, encoding, null, null, directory);

        case 1:
          var range = ranges.First();
          return random.ToTextFile(size, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory);

        default:
          var totalRange = ranges.ToRange();
          var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
          return random.ToFile(directory).WriteText(chars.ToText(), encoding);
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
    /// <seealso cref="RandomExtensions.ToTextFile(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.Text.Encoding,System.IO.DirectoryInfo)"/>
    public async Task<FileInfo> ToTextFileAsync(int size, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      cancellation.ThrowIfCancellationRequested();

      if (size == 0)
      {
        return await random.ToTextFileAsync(size, encoding, null, null, directory, cancellation).ConfigureAwait(false);
      }

      switch (ranges.Count())
      {
        case 0:
          return await random.ToTextFileAsync(size, encoding, null, null, directory, cancellation).ConfigureAwait(false);

        case 1:
          var range = ranges.First();
          return await random.ToTextFileAsync(size, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory, cancellation).ConfigureAwait(false);

        default:
          var totalRange = ranges.ToRange();
          var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
          return await random.ToFile(directory).WriteTextAsync(chars.ToText(), encoding, cancellation).ConfigureAwait(false);
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
    /// <seealso cref="RandomExtensions.ToTextFile(System.Random,int,int,System.Text.Encoding,char?,char?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public IEnumerable<FileInfo> ToTextFile(int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      for (var i = 1; i <= count; i++)
      {
        yield return random.ToTextFile(size, encoding, from, to, directory);
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
    /// <seealso cref="RandomExtensions.ToTextFile(System.Random,int,int,System.Text.Encoding,char?,char?,System.IO.DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> ToTextFile(int size, int count, Encoding encoding = null, char? from = null, char? to = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      cancellation.ThrowIfCancellationRequested();

      for (var i = 1; i <= count; i++)
      {
        yield return await random.ToTextFileAsync(size, encoding, from, to, directory, cancellation).ConfigureAwait(false);
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
    /// <seealso cref="RandomExtensions.ToTextFileAsync(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range},System.Text.Encoding,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/>
    public IEnumerable<FileInfo> ToTextFile(int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null)
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
          foreach (var file in random.ToTextFile(size, count, encoding, null, null, directory))
          {
            yield return file;
          }

          break;

        case 1:
          var range = ranges.First();

          foreach (var file in random.ToTextFile(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory))
          {
            yield return file;
          }

          break;

        default:
          var totalRange = ranges.ToRange();

          for (var i = 1; i <= count; i++)
          {
            var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
            yield return random.ToFile(directory).WriteText(chars.ToText(), encoding);
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
    /// <seealso cref="RandomExtensions.ToTextFile(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range},System.Text.Encoding,System.IO.DirectoryInfo)"/>
    public async IAsyncEnumerable<FileInfo> ToTextFileAsync(int size, int count, IEnumerable<Range> ranges, Encoding encoding = null, DirectoryInfo directory = null, [EnumeratorCancellation] CancellationToken cancellation = default)
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
          await foreach (var file in random.ToTextFile(size, count, encoding, null, null, directory, cancellation).ConfigureAwait(false))
          {
            yield return file;
          }

          break;

        case 1:
          var range = ranges.First();

          await foreach (var file in random.ToTextFile(size, count, encoding, (char?) range.Start.Value, (char?) range.End.Value, directory, cancellation).ConfigureAwait(false))
          {
            yield return file;
          }

          break;

        default:
          var totalRange = ranges.ToRange();

          for (var i = 1; i <= count; i++)
          {
            var chars = size.Objects(() => (char) totalRange.Random()).AsArray();
            yield return await random.ToFile(directory).WriteTextAsync(chars.ToText(), encoding, cancellation).ConfigureAwait(false);
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
    /// <seealso cref="RandomExtensions.ToPhysicalAddress(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public PhysicalAddress ToPhysicalAddress(int size, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      return new PhysicalAddress(random.ToByte(size, from, to).AsArray());
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
    public PhysicalAddress ToPhysicalAddress(int size, IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

      switch (ranges.Count())
      {
        case 0:
          return random.ToPhysicalAddress(size);

        case 1:
          var range = ranges.First();
          return random.ToPhysicalAddress(size, (byte?) range.Start.Value, (byte?) range.End.Value);

        default:
          return new PhysicalAddress(random.ToByte(size, ranges).AsArray());
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
    /// <seealso cref="RandomExtensions.ToPhysicalAddress(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public IEnumerable<PhysicalAddress> ToPhysicalAddress(int size, int count, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      return count.Objects(() => random.ToPhysicalAddress(size, from, to));
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
    /// <seealso cref="RandomExtensions.ToPhysicalAddress(System.Random,int,int,byte?,byte?)"/>
    public IEnumerable<PhysicalAddress> ToPhysicalAddress(int size, int count, IEnumerable<Range> ranges)
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
          return random.ToPhysicalAddress(size, count);

        case 1:
          var range = ranges.First();
          return random.ToPhysicalAddress(size, count, (byte?) range.Start.Value, (byte?) range.End.Value);

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
    /// <seealso cref="RandomExtensions.ToMemoryStreamAsync(System.Random,int,byte?,byte?,System.Threading.CancellationToken)"/>
    public MemoryStream ToMemoryStream(int count, byte? from = null, byte? to = null)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      var stream = new MemoryStream();

      if (count > 0)
      {
        stream.WriteBytes(random.ToByte(count, from, to)).MoveToStart();
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
    public async Task<MemoryStream> ToMemoryStreamAsync(int count, byte? from = null, byte? to = null, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      cancellation.ThrowIfCancellationRequested();

      var stream = new MemoryStream();

      if (count > 0)
      {
        (await stream.WriteBytesAsync(random.ToByte(count, from, to), cancellation).ConfigureAwait(false)).MoveToStart();
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
    /// <seealso cref="RandomExtensions.ToMemoryStreamAsync"/>
    public MemoryStream ToMemoryStream(int count, IEnumerable<Range> ranges)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      var stream = new MemoryStream();

      if (count > 0)
      {
        var range = ranges.ToRange();
        var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.ToByte(count)).AsArray();

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
    /// <seealso cref="RandomExtensions.ToMemoryStream(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/>
    public async Task<MemoryStream> ToMemoryStreamAsync(int count, IEnumerable<Range> ranges, CancellationToken cancellation = default)
    {
      if (random is null) throw new ArgumentNullException(nameof(random));
      if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

      cancellation.ThrowIfCancellationRequested();

      var stream = new MemoryStream();

      if (count > 0)
      {
        var range = ranges.ToRange();
        var bytes = (range.Any() ? count.Objects(() => (byte) range.Random()) : random.ToByte(count)).AsArray();

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
    /// <seealso cref="RandomExtensions.ToStream(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/>
    public Stream ToStream(byte? from = null, byte? to = null) => random is not null ? new RandomStream(from, to) : throw new ArgumentNullException(nameof(random));
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="ranges"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="random"/> is <see langword="null"/>.</exception>
    public Stream ToStream(IEnumerable<Range> ranges) => random is not null ? new RandomRangeStream(ranges) : throw new ArgumentNullException(nameof(random));
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
      buffer.Fill(() => Randomizer.ToByte(Min, Max), offset, count);
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
      buffer.Fill(() => Range.Any() ? (byte) Range.Random() : Randomizer.ToByte(), offset, count);
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