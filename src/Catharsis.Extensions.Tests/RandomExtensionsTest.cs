using System.Security;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RandomExtensions"/>.</para>
/// </summary>
/// <seealso cref="RandomExtensions"/>
public sealed class RandomExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Sbyte(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Sbyte_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Byte(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Byte_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Short(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Short_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Ushort(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Ushort_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Int(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Int_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Uint(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uint_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Long(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Long_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Double(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Char(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Char_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Range(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Guid(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Guid_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_FileName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileName_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_DirectoryName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryName_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_FilePath(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void FilePath_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_DirectoryPath(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryPath_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_File(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void File_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.get_Stream(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Property()
  {
    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToSbyte(Random, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(Random, int, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToSbyte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSbyte()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Test(0, 0, 0, 0);
      Test(sbyte.MinValue, sbyte.MinValue, sbyte.MinValue, sbyte.MinValue);
      Test(sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue);
      Test(sbyte.MinValue, sbyte.MaxValue, sbyte.MinValue, sbyte.MaxValue);
      Test(sbyte.MaxValue, sbyte.MinValue, sbyte.MinValue, sbyte.MaxValue);
      //Test();

      //new Random().Sbyte(0, 0).Should().Be(0);
      //new Random().Sbyte(sbyte.MinValue, sbyte.MinValue).Should().Be(sbyte.MinValue);
      //new Random().Sbyte(sbyte.MaxValue, sbyte.MaxValue).Should().Be(sbyte.MaxValue);
      //new Random().Sbyte(sbyte.MinValue, sbyte.MaxValue).Should().Be(sbyte.MinValue, sbyte.MaxValue);
      //new Random().Sbyte(sbyte.MaxValue, sbyte.MinValue).Should().Be(sbyte.MinValue, sbyte.MaxValue);
      new Random().ToSbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Test(sbyte from, sbyte to, sbyte min, sbyte max) => new Random().ToSbyte(min, max).Should().BeInRange(from, to);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSbyte([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToSbyte([Range.All]).Should().Be(0);
      new Random().ToSbyte(new[] {..0}).Should().Be(0);
      new Random().ToSbyte([sbyte.MaxValue..sbyte.MaxValue]).Should().Be(sbyte.MaxValue);
      new Random().ToSbyte(new[] {..sbyte.MaxValue}).Should().BeInRange(0, sbyte.MaxValue);
      new Random().ToSbyte([sbyte.MaxValue..0]).Should().BeInRange(0, sbyte.MaxValue);
      new Random().ToSbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Test(Range[] ranges, sbyte min, sbyte max) => new Random().ToSbyte(ranges).Should().BeInRange(min, max);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSbyte(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSbyte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToSbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToSbyte(count, 0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToSbyte(count, sbyte.MinValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MinValue);
      new Random().ToSbyte(count, sbyte.MaxValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().ToSbyte(count, sbyte.MinValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().ToSbyte(count, sbyte.MaxValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().ToSbyte(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));

      throw new NotImplementedException();

      static void Test(int count, sbyte from, sbyte to)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSbyte(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSbyte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToSbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().ToSbyte(0));

      new Random().ToSbyte(0, [Range.All]).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ToSbyte(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToSbyte(count, new[] {..0}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToSbyte(count, [sbyte.MaxValue..sbyte.MaxValue]).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().ToSbyte(count, [Range.All]).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToSbyte(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToSbyte(count, new[] {..0, ..sbyte.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().ToSbyte(count, new[] {..0, sbyte.MaxValue..0}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().ToSbyte(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToByte(Random, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToByte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToByte()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      
      new Random().ToByte((byte?) 0, 0).Should().Be(0);
      //new Random().ToByte(byte.MinValue, byte.MinValue).Should().Be(byte.MinValue);
      //new Random().ToByte(byte.MaxValue, byte.MaxValue).Should().Be(byte.MaxValue);
      //new Random().ToByte(byte.MinValue, byte.MaxValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      //new Random().ToByte(byte.MaxValue, byte.MinValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().ToByte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToByte([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToByte([Range.All]).Should().Be(0);
      new Random().ToByte(new [] {..0}).Should().Be(0);
      new Random().ToByte(new[] {..byte.MinValue}).Should().Be(byte.MinValue);
      new Random().ToByte([byte.MaxValue..byte.MaxValue]).Should().Be(byte.MaxValue);
      new Random().ToByte(new[] {..byte.MaxValue}).Should().BeInRange(0, byte.MaxValue);
      new Random().ToByte([byte.MaxValue..0]).Should().BeInRange(0, byte.MaxValue);
      new Random().ToByte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToByte(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToByte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToByte(0).Should().BeOfType<IEnumerable<byte>>().And.NotBeNull().And.BeEmpty();

      new Random().ToByte(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ToByte(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ToByte(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ToByte(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ToByte(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToByte(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToByte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToByte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().ToByte(0));

      new Random().ToByte(0, [Range.All]).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ToByte(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToByte(count, new[] {..0}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, new[] {..byte.MinValue}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ToByte(count, [byte.MaxValue..byte.MaxValue]).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ToByte(count, [Range.All]).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToByte(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, new[] {..0, ..byte.MaxValue}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
      new Random().ToByte(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToShort(Random, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(Random, int, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToShort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToShort()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToShort((short?) 0, 0).Should().Be(0);
      new Random().ToShort(short.MinValue, short.MinValue).Should().Be(short.MinValue);
      new Random().ToShort(short.MaxValue, short.MaxValue).Should().Be(short.MaxValue);
      new Random().ToShort(short.MinValue, short.MaxValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().ToShort(short.MaxValue, short.MinValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().ToShort().Should().BeInRange(short.MinValue, short.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToShort([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToShort([Range.All]).Should().Be(0);
      new Random().ToShort(new[] {..0}).Should().Be(0);
      new Random().ToShort([short.MaxValue..short.MaxValue]).Should().Be(short.MaxValue);
      new Random().ToShort(new[] {..short.MaxValue}).Should().BeInRange(0, short.MaxValue);
      new Random().ToShort([short.MaxValue..0]).Should().BeInRange(0, short.MaxValue);
      new Random().ToShort().Should().BeInRange(short.MinValue, short.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToShort(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToShort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToShort(0).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().ToShort(count, 0, 0).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToShort(count, short.MinValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MinValue);
      new Random().ToShort(count, short.MaxValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().ToShort(count, short.MinValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().ToShort(count, short.MaxValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().ToShort(count).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToShort(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToShort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToShort(0).Should().BeOfType<IEnumerable<short>>().And.BeSameAs(new Random().ToShort(0));

      new Random().ToShort(0, [Range.All]).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();
      new Random().ToShort(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().ToShort(count, new[] {..0}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToShort(count, [short.MaxValue..short.MaxValue]).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().ToShort(count, [Range.All]).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToShort(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToShort(count, new[] {..0, ..short.MaxValue}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().ToShort(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToUshort(Random, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(Random, int, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUshort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUshort()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToUshort((ushort?) 0, 0).Should().Be(0);
      new Random().ToUshort(from: ushort.MinValue, to: byte.MinValue).Should().Be(ushort.MinValue);
      new Random().ToUshort(from: ushort.MaxValue, to: ushort.MaxValue).Should().Be(ushort.MaxValue);
      new Random().ToUshort(from: ushort.MinValue, to: ushort.MaxValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().ToUshort(from: ushort.MaxValue, to: ushort.MinValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().ToUshort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUshort([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToUshort([Range.All]).Should().Be(0);
      new Random().ToUshort(new[] {..0}).Should().Be(0);
      new Random().ToUshort(new[] {..ushort.MinValue}).Should().Be(ushort.MinValue);
      new Random().ToUshort([ushort.MaxValue..ushort.MaxValue]).Should().Be(ushort.MaxValue);
      new Random().ToUshort(new[] {..ushort.MaxValue}).Should().BeInRange(0, ushort.MaxValue);
      new Random().ToUshort([ushort.MaxValue..0]).Should().BeInRange(0, ushort.MaxValue);
      new Random().ToUshort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUshort(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToUshort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToUshort(0).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().ToUshort(count, 0, 0).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUshort(count, ushort.MinValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().ToUshort(count, ushort.MaxValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().ToUshort(count, ushort.MinValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().ToUshort(count, ushort.MaxValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().ToUshort(count).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUshort(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToUshort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToUshort(0).Should().BeOfType<IEnumerable<ushort>>().And.BeSameAs(new Random().ToUshort(0));

      new Random().ToUshort(0, [Range.All]).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();
      new Random().ToUshort(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().ToUshort(count, new[] {..0}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUshort(count, new[] {..ushort.MinValue}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().ToUshort(count, [ushort.MaxValue..ushort.MaxValue]).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().ToUshort(count, [Range.All]).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToUshort(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUshort(count, new[] {..0, ..ushort.MaxValue}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
      new Random().ToUshort(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToInt(Random, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(Random, int, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToInt_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToInt()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToInt((int?) 0, 0).Should().Be(0);
      new Random().ToInt((int?) int.MinValue, int.MinValue).Should().Be(int.MinValue);
      new Random().ToInt((int?) int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
      new Random().ToInt((int?) int.MinValue, int.MaxValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().ToInt((int?) int.MaxValue, int.MinValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().ToInt().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToInt([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToInt([Range.All]).Should().Be(0);
      new Random().ToInt(new[] {..0}).Should().Be(0);
      new Random().ToInt([int.MaxValue..int.MaxValue]).Should().Be(int.MaxValue);
      new Random().ToInt(new[] {..int.MaxValue}).Should().BeInRange(0, int.MaxValue);
      new Random().ToInt([int.MaxValue..0]).Should().BeInRange(0, int.MaxValue);
      new Random().ToInt().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToInt(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToInt(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToInt(0).Should().BeOfType<IEnumerable<int>>().And.BeEmpty();

      new Random().ToInt(count, 0, 0).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToInt(count, int.MinValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MinValue);
      new Random().ToInt(count, int.MaxValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().ToInt(count, int.MinValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().ToInt(count, int.MaxValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().ToInt(count).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToUint(Random, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUint(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUint(Random, int, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUint(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToUint_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUint()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToUint((uint?) 0, 0).Should().Be(0);
      new Random().ToUint(uint.MinValue, uint.MinValue).Should().Be(uint.MinValue);
      new Random().ToUint(uint.MaxValue, uint.MaxValue).Should().Be(uint.MaxValue);
      new Random().ToUint(uint.MinValue, uint.MaxValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().ToUint(uint.MaxValue, uint.MinValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().ToUint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUint([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToUint([Range.All]).Should().Be(0);
      new Random().ToUint(new[] {..0}).Should().Be(0);
      new Random().ToUint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUint(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToUint(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToUint(0).Should().BeOfType<IEnumerable<uint>>().And.BeEmpty();

      new Random().ToUint(count, 0, 0).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUint(count, uint.MinValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MinValue);
      new Random().ToUint(count, uint.MaxValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MaxValue);
      new Random().ToUint(count, uint.MinValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().ToUint(count, uint.MaxValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().ToUint(count).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToUint(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToUint(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToUint(0, [Range.All]).Should().BeEmpty();

      new Random().ToUint(count, new[] {..0}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUint(count, [Range.All]).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToUint(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUint(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToLong(Random, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToLong(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToLong(Random, int, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToLong(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToLong_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLong()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToLong((long?) 0, 0).Should().Be(0);
      new Random().ToLong(long.MinValue, long.MinValue).Should().Be(long.MinValue);
      new Random().ToLong(long.MaxValue, long.MaxValue).Should().Be(long.MaxValue);
      new Random().ToLong(long.MinValue, long.MaxValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().ToLong(long.MaxValue, long.MinValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().ToLong().Should().BeInRange(long.MinValue, long.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLong([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToLong([Range.All]).Should().Be(0);
      new Random().ToLong(new[] {..0}).Should().Be(0);
      new Random().ToLong().Should().BeInRange(long.MinValue, long.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLong(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToLong(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToLong(0).Should().BeOfType<IEnumerable<long>>().And.BeEmpty();

      new Random().ToLong(count, 0, 0).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToLong(count, long.MinValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MinValue);
      new Random().ToLong(count, long.MaxValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MaxValue);
      new Random().ToLong(count, long.MinValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().ToLong(count, long.MaxValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().ToLong(count).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLong(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToLong(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToLong(0, [Range.All]).Should().BeEmpty();
      new Random().ToLong(0, new[] {..int.MaxValue}).Should().BeEmpty();

      new Random().ToLong(count, new[] {..0}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToLong(count, [int.MaxValue..int.MaxValue]).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().ToLong(count, [Range.All]).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToLong(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToLong(count, new[] {..0, ..short.MaxValue}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().ToLong(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToFloat(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFloat(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFloat_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFloat()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToFloat().Should().BeInRange(float.MinValue, float.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFloat(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().ToFloat(0).Should().BeOfType<IEnumerable<float>>().And.BeEmpty();
      new Random().ToFloat(count).Should().BeOfType<IEnumerable<float>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(float.MinValue, float.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDouble(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDouble(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDouble_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDouble()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDouble().Should().BeInRange(double.MinValue, double.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDouble(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().ToDouble(0).Should().BeOfType<IEnumerable<double>>().And.BeEmpty();
      new Random().ToDouble(count).Should().BeOfType<IEnumerable<double>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(double.MinValue, double.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToChar(Random, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToChar_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToChar()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToChar((char) 0, (char) 0).Should().Be((char) 0);
      new Random().ToChar(char.MinValue, char.MinValue).Should().Be(char.MinValue);
      new Random().ToChar(char.MaxValue, char.MaxValue).Should().Be(char.MaxValue);
      new Random().ToChar(char.MinValue, char.MaxValue).Should().BeInRange(char.MinValue, char.MaxValue);
      new Random().ToChar(char.MaxValue, char.MinValue).Should().BeInRange(char.MinValue, char.MaxValue);
      new Random().ToChar().Should().BeInRange(char.MinValue, char.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToChar([Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToChar([Range.All]).Should().Be((char) 0);
      new Random().ToChar(new[] {..0}).Should().Be((char) 0);
      new Random().ToChar(new[] {..char.MinValue}).Should().Be(char.MinValue);
      new Random().ToChar([char.MaxValue..char.MaxValue]).Should().Be(char.MaxValue);
      new Random().ToChar(new[] {..char.MaxValue}).Should().BeInRange((char) 0, char.MaxValue);
      new Random().ToChar([char.MaxValue..0]).Should().BeInRange((char) 0, char.MaxValue);
      new Random().ToChar().Should().BeInRange(char.MinValue, char.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToChar(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToChar(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToChar(0).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().ToChar(count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToChar(count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToChar(count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToChar(count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToChar(count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToChar(count).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToChar(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToChar(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToChar(0).Should().BeOfType<IEnumerable<char>>().And.BeSameAs(new Random().ToChar(0));

      new Random().ToChar(0, [Range.All]).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();
      new Random().ToChar(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().ToChar(count, new[] {..0}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToChar(count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToChar(count, [char.MaxValue..char.MaxValue]).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToChar(count, [Range.All]).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().ToChar(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToChar(count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().ToChar(count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToText(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(Random, int, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToText_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToText(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToText(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToText(0)).And.BeEmpty();
      new Random().ToText(count, (char) 0, (char) 0).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToText(count, char.MinValue, char.MinValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToText(count, char.MaxValue, char.MaxValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToText(count, char.MinValue, char.MaxValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToText(count, char.MaxValue, char.MinValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToText(count).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToText(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToText(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToText(0));

      new Random().ToText(0, [Range.All]).Should().BeOfType<string>().And.BeEmpty();
      new Random().ToText(0, new[] {..int.MaxValue}).Should().BeOfType<string>().And.BeEmpty();

      new Random().ToText(count, new[] {..0}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToText(count, new[] {..char.MinValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToText(count, [char.MaxValue..char.MaxValue]).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToText(count, [Range.All]).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().ToText(count, new[] {..0, Range.All}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToText(count, new[] {..0, ..char.MaxValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().ToText(count, new[] {..0, 'a'..'b'}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToText(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToText(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToSbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToText(0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ToText(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(string.Empty);

      new Random().ToText(int.MaxValue, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToText(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToText(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToText(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToText(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToText(size, count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().ToText(size, count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().ToText(size, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToText(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToText(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToText(0, 0).Should().BeOfType<IEnumerable<string>>().And.BeSameAs(new Random().ToText(0, 0));

      new Random().ToText(size, 0, [Range.All]).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().ToText(size, 0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().ToText(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToText(size, count, new[] {..0}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToText(size, count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToText(size, count, [char.MaxValue..char.MaxValue]).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToText(size, count, [Range.All]).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().ToText(size, count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToText(size, count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().ToText(size, count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDigits(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDigits(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDigits_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDigits(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDigits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDigits(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToDigits(0)).And.BeEmpty();
      new Random().ToDigits(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[0-9]+$");

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDigits(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDigits(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToDigits(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToDigits(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().ToDigits(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToDigits(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[0-9]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToLetters(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToLetters(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToLetters_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLetters(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToLetters(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToLetters(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToLetters(0)).And.BeEmpty();
      new Random().ToLetters(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z]+$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToLetters(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToLetters(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToLetters(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToLetters(int.MaxValue, 0).Should().BeEmpty();

      new Random().ToLetters(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToLetters(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToAlphaDigits(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToAlphaDigits(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToAlphaDigits_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToAlphaDigits(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToAlphaDigits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToAlphaDigits(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToAlphaDigits(0)).And.BeEmpty();
      new Random().ToAlphaDigits(count).Should().BeOfType<string>().And.NotBeSameAs(new Random().ToAlphaDigits(count)).And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z0-9]+$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToAlphaDigits(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToAlphaDigits(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToAlphaDigits(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToAlphaDigits(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().ToAlphaDigits(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToAlphaDigits(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z0-9]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToSecureString(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(Random, int, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToSecureString_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSecureString(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToSecureString(0).Length.Should().Be(0);

      new Random().ToSecureString(count, (char) 0, (char) 0).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToSecureString(count, char.MinValue, char.MinValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToSecureString(count, char.MaxValue, char.MaxValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToSecureString(count, char.MinValue, char.MaxValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToSecureString(count, char.MaxValue, char.MinValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().ToSecureString(count).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      const int count = 1000;

      AssertionExtensions.Should(() => ((Random) null).ToSecureString(0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      new Random().ToSecureString(0, [Range.All]).Length.Should().Be(0);
      new Random().ToSecureString(0, new[] {..int.MaxValue}).Length.Should().Be(0);

      new Random().ToSecureString(count, new[] {..0}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToSecureString(count, new[] {..char.MinValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToSecureString(count, new[] {char.MaxValue..char.MaxValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToSecureString(count, [Range.All]).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().ToSecureString(count, new[] {..0, Range.All}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToSecureString(count, new[] {..0, ..char.MaxValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().ToSecureString(count, new[] {..0, 'a'..'b'}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSecureString(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToSecureString(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToSecureString(int.MaxValue, 0).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().ToSecureString(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().ToSecureString(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToSecureString(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToSecureString(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToSecureString(size, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToSecureString(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToSecureString(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToSecureString(size, 0, [Range.All]).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();
      new Random().ToSecureString(size, 0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().ToSecureString(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().ToSecureString(size, count, new[] {..0}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToSecureString(size, count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToSecureString(size, count, new[] {char.MaxValue..char.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToSecureString(size, count, [Range.All]).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().ToSecureString(size, count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToSecureString(size, count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().ToSecureString(size, count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDateTime(Random, DateTime?, DateTime?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDateTime(Random, int, DateTime?, DateTime?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateTime_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDateTime()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDateTime(DateTime.MinValue, DateTime.MinValue).Should().Be(DateTime.MinValue).And.BeSameDateAs(new Random().ToDateTime(DateTime.MinValue, DateTime.MinValue));
      new Random().ToDateTime(DateTime.MaxValue, DateTime.MaxValue).Should().Be(DateTime.MaxValue).And.BeSameDateAs(new Random().ToDateTime(DateTime.MaxValue, DateTime.MaxValue));
      new Random().ToDateTime(DateTime.MinValue, DateTime.MaxValue).Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().ToDateTime(DateTime.MaxValue, DateTime.MinValue).Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().ToDateTime().Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDateTime(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDateTime(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDateTime(0).Should().BeEmpty();
      new Random().ToDateTime(count, DateTime.MinValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MinValue);
      new Random().ToDateTime(count, DateTime.MaxValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MaxValue);
      new Random().ToDateTime(count, DateTime.MinValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().ToDateTime(count, DateTime.MaxValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().ToDateTime(count).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDateTimeOffset(Random, DateTimeOffset?, DateTimeOffset?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDateTimeOffset(Random, int, DateTimeOffset?, DateTimeOffset?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateTimeOffset_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDateTimeOffset()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue).And.BeSameDateAs(new Random().ToDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue));
      new Random().ToDateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue).And.BeSameDateAs(new Random().ToDateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue));
      new Random().ToDateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().ToDateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().ToDateTimeOffset().Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDateTimeOffset(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDateTimeOffset(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDateTimeOffset(0).Should().BeEmpty();
      new Random().ToDateTimeOffset(count, DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MinValue);
      new Random().ToDateTimeOffset(count, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MaxValue);
      new Random().ToDateTimeOffset(count, DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().ToDateTimeOffset(count, DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().ToDateTimeOffset(count).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDateOnly(Random, DateOnly?, DateOnly?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDateOnly(Random, int, DateOnly?, DateOnly?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDateOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDateOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDateOnly(DateOnly.MinValue, DateOnly.MinValue).Should().Be(DateOnly.MinValue);
      new Random().ToDateOnly(DateOnly.MaxValue, DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
      new Random().ToDateOnly(DateOnly.MinValue, DateOnly.MaxValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().ToDateOnly(DateOnly.MaxValue, DateOnly.MinValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().ToDateOnly().Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDateOnly(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDateOnly(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDateOnly(0).Should().BeEmpty();

      new Random().ToDateOnly(count, DateOnly.MinValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MinValue);
      new Random().ToDateOnly(count, DateOnly.MaxValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MaxValue);
      new Random().ToDateOnly(count, DateOnly.MinValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().ToDateOnly(count, DateOnly.MaxValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().ToDateOnly(count).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToTimeOnly(Random, TimeOnly?, TimeOnly?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTimeOnly(Random, int, TimeOnly?, TimeOnly?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTimeOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTimeOnly()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToTimeOnly(TimeOnly.MinValue, TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
      new Random().ToTimeOnly(TimeOnly.MaxValue, TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
      new Random().ToTimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().ToTimeOnly(TimeOnly.MaxValue, TimeOnly.MinValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().ToTimeOnly().Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTimeOnly(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTimeOnly(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToTimeOnly(0).Should().BeEmpty();
      new Random().ToTimeOnly(count, TimeOnly.MinValue, TimeOnly.MinValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MinValue);
      new Random().ToTimeOnly(count, TimeOnly.MaxValue, TimeOnly.MaxValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MaxValue);
      new Random().ToTimeOnly(count).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToTimeSpan(Random, TimeSpan?, TimeSpan?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTimeSpan(Random, int, TimeSpan?, TimeSpan?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTimeSpan_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTimeSpan()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTimeSpan(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTimeSpan(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToRange(Random, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToRange(Random, int, int?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToRange_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToRange()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToRange((int?) int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

      new int?[] { null, 0, int.MaxValue }.ForEach(max =>
      {
        var range = new Random().ToRange(max);
        range.Should().BeOfType<Range>();
        range.Start.IsFromEnd.Should().BeFalse();
        range.Start.Value.Should().Be(0);
        range.End.IsFromEnd.Should().BeFalse();
        range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
      });

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToRange(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToRange(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToRange(0).Should().BeOfType<IEnumerable<Range>>().And.BeEmpty();
      new Random().ToRange(count, 0).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Test(element, 0));
      new Random().ToRange(count).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Test(element, null));

      throw new NotImplementedException();

      static void Test(Range range, int? max)
      {
        range.Start.IsFromEnd.Should().BeFalse();
        range.Start.Value.Should().Be(0);
        range.End.IsFromEnd.Should().BeFalse();
        range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToGuid(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToGuid(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToGuid_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToGuid()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToGuid().Should().NotBe(Guid.Empty);
      new Random().ToGuid().Should().NotBe(Guid.NewGuid());

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToGuid(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToGuid(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToGuid(0).Should().BeOfType<IEnumerable<Guid>>().And.BeEmpty();
      new Random().ToGuid(count).Should().BeOfType<IEnumerable<Guid>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBe(Guid.Empty).And.NotBe(Guid.NewGuid()));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToObject(Random, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(Random, Type[])"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(Random, int, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(Random, int, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToObject_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToObject(Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToObject()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToObject(0, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(0, (IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ToObject(-1, Enumerable.Empty<Type>())).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToObject(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(0, null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ToObject(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToFileName(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFileName(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFileName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFileName()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToFileName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFileName(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToFileName(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToFileName(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().ToFileName(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDirectoryName(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDirectoryName(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDirectoryName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectoryName()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDirectoryName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectoryName(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDirectoryName(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDirectoryName(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().ToDirectoryName(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToFilePath(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFilePath(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFilePath_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFilePath()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().ToFilePath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToFilePath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().ToFilePath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToFilePath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFilePath(1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToFilePath(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToFilePath(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().ToFilePath(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(path, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().ToFilePath(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(path, currentDirectory.FullName));

      throw new NotImplementedException();

      static void Test(string path, string directory)
      {
        var file = Path.GetFileName(path);
        path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToFilePath()).And.Be(Path.Combine(directory, file));
        file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDirectoryPath(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDirectoryPath(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDirectoryPath_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectoryPath()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().ToDirectoryPath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToDirectoryPath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().ToDirectoryPath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToDirectoryPath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectoryPath(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDirectoryPath(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToDirectoryPath(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().ToDirectoryPath(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(Path.GetTempPath(), path));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().ToDirectoryPath(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(currentDirectory.FullName, path));

      throw new NotImplementedException();

      static void Test(string directory, string path)
      {
        var file = Path.GetFileName(path);
        path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().ToDirectoryPath()).And.Be(Path.Combine(directory, file));
        file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDirectory(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDirectory(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToDirectory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectory()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Test(new Random().ToDirectory(), Path.GetTempPath());

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      Test(currentDirectory, currentDirectory.FullName);

      static void Test(DirectoryInfo directory, string path)
      {
        directory.TryFinallyDelete(directory =>
        {
          directory.Should().BeOfType<DirectoryInfo>();
          directory.Exists.Should().BeTrue();
          directory.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          directory.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          directory.LinkTarget.Should().BeNull();
          directory.FullName.Should().Be(Path.Combine(path, directory.Name));
          directory.Name.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
          (directory.Attributes & FileAttributes.Directory).Should().Be(FileAttributes.Directory);
        });
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToDirectory(1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToDirectory(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().ToDirectory(0).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.BeEmpty();

      new Random().ToDirectory(count).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Test(directory, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().ToDirectory(count, currentDirectory).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Test(directory, currentDirectory.FullName));

      static void Test(DirectoryInfo directory, string path)
      {
        directory.TryFinallyDelete(directory =>
        {
          directory.Should().BeOfType<DirectoryInfo>();
          directory.Exists.Should().BeTrue();
          directory.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          directory.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          directory.LinkTarget.Should().BeNull();
          directory.FullName.Should().Be(Path.Combine(path, directory.Name));
          directory.Name.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
          (directory.Attributes & FileAttributes.Directory).Should().Be(FileAttributes.Directory);
        });
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToFile(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFile(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFile()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Test(new Random().ToFile(), Path.GetTempPath());

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      Test(new Random().ToFile(currentDirectory), currentDirectory.FullName);

      static void Test(FileInfo file, string path)
      {
        file.TryFinallyDelete(file =>
        {
          file.Should().BeOfType<FileInfo>();
          file.Exists.Should().BeTrue();
          file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          file.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          file.LinkTarget.Should().BeNull();
          file.FullName.Should().Be(Path.Combine(path, file.FullName));
          file.Name.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
          file.Length.Should().Be(0);
          file.IsReadOnly.Should().BeFalse();
        });
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToFile(1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().ToFile(0).Should().BeOfType<IEnumerable<FileInfo>>().And.BeEmpty();

      new Random().ToFile(count).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Test(file, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().ToFile(count, currentDirectory).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Test(file, currentDirectory.FullName));

      static void Test(FileInfo file, string path)
      {
        file.TryFinallyDelete(file =>
        {
          file.Should().BeOfType<FileInfo>();
          file.Exists.Should().BeTrue();
          file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          file.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          file.LinkTarget.Should().BeNull();
          file.FullName.Should().Be(Path.Combine(path, file.FullName));
          file.Name.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
          file.Length.Should().Be(0);
          file.IsReadOnly.Should().BeFalse();
        });
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(Random, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(Random, int, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(Random, int, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToBinaryFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFile(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFile(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFile(0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFile(0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(Random, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(Random, int, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToBinaryFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFileAsync(0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      /*const int size = 4096;

      var tempPath = Path.GetTempPath();

      Test(new Random().BinaryFile(int.MinValue), tempPath

                                     //int.MinValue, null);


      Test( int.MinValue);
      Test(0);
      Test(size);
      Test(size, 0, 0);
      Test(size, byte.MinValue, byte.MinValue);
      Test(size, byte.MaxValue, byte.MaxValue);*/

      static void Test(FileInfo file, string path, int size, byte? min, byte? max)
      {
        //AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, min, max, path.ToDirectory())).ThrowExactlyAsync<TaskCanceledException>().Await();

        size = Math.Max(0, size);

        file.TryFinallyDelete(info =>
        {
          info.Should().BeOfType<FileInfo>();
          info.Exists.Should().BeTrue();
          info.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          info.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          info.LinkTarget.Should().BeNull();
          info.FullName.Should().Be(Path.Combine(path, info.FullName));
          info.Name.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
          info.Length.Should().Be(0);
          info.IsReadOnly.Should().BeFalse();

          var bytes = info.ToBytesAsync().ToEnumerable();

          bytes.Should().HaveCount(size);

          if (min is not null)
          {
            bytes.Should().AllSatisfy(element => element.Should().BeGreaterThanOrEqualTo(min.Value));
          }

          if (max is not null)
          {
            bytes.Should().AllSatisfy(element => element.Should().BeLessThanOrEqualTo(max.Value));
          }
        });
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFileAsync(0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      //AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

      const int size = 4096;

      Test(0);
      Test(size);

      //Test(size, Range.All);
      //Test(size, ..int.MaxValue);
      Test(size, ..0);
      Test(size, ..byte.MinValue);
      Test(size, byte.MaxValue..byte.MaxValue);

      //Test(size, ..0, Range.All);
      //Test(size, ..0, ..byte.MaxValue);
      Test(size, ..0, 1..2);

      static void Test(int size, params Range[] ranges)
      {
        var file = new Random().ToBinaryFileAsync(size, ranges, null, CancellationToken.None).Await();

        size = Math.Max(0, size);

        file.TryFinallyDelete(file =>
        {
          file.Exists.Should().BeTrue();
          file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
          file.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
          file.Extension.Should().HaveLength(4);
          file.LinkTarget.Should().BeNull();
          file.FullName.Should().StartWith(Path.GetTempPath());
          file.Length.Should().Be(size);
          file.IsReadOnly.Should().BeFalse();

          var bytes = file.ToBytesAsync().ToArray();

          bytes.Should().HaveCount(size);

          var range = ranges.ToRange().Select(element => (byte) element).ToList();
          if (range.Any())
          {
            bytes.Should().BeSubsetOf(range);
          }
        });
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFileAsync(0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(-1, 0).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(0, -1).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToBinaryFileAsync(0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(-1, 0).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(0, -1).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToTextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(Random, int, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTextFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTextFile(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTextFile(-1)).ThrowExactly<ArgumentNullException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTextFile(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTextFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((Random) null).TextFile(0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      //AssertionExtensions.Should(() => new Random().TextFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      //AssertionExtensions.Should(() => new Random().TextFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((Random) null).TextFile(0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      //AssertionExtensions.Should(() => new Random().TextFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      //AssertionExtensions.Should(() => new Random().TextFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToTextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFileAsync(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTextFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTextFileAsync(0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToTextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToTextFileAsync(0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToTextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToIpV6Address(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToIpV6Address(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToIpV6Address_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpV6Address()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpV6Address(0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToIpV6Address(-1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(Random, int, int,byte?,byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToPhysicalAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToPhysicalAddress(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      //new Random().PhysicalAddress(int.MinValue).Should().NotBeNull().And.NotBeSameAs()
      //new Random().PhysicalAddress(0).Should().BeEmpty();

      new Random().ToByte(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ToByte(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ToByte(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ToByte(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ToByte(count).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToPhysicalAddress(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToPhysicalAddress(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToPhysicalAddress(0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToMemoryStream(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToMemoryStream(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToMemoryStream(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToMemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToMemoryStream(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToMemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToMemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToMemoryStreamAsync(Random, int, IEnumerable{Range}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToMemoryStreamAsync(0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      const int count = 1000;

      new Random().ToMemoryStreamAsync(0).Await().Length.Should().Be(0);
      new Random().ToMemoryStreamAsync(0).Await().Length.Should().Be(0);

      using (var stream = new Random().ToMemoryStreamAsync(count).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, 0, 100).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, 0, 0).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }

      static void Test(MemoryStream stream, int count)
      {
        stream.Length.Should().Be(count);
        stream.Position.Should().Be(0);
        stream.CanRead.Should().BeTrue();
        stream.CanWrite.Should().BeTrue();
        stream.CanSeek.Should().BeTrue();
        stream.CanTimeout.Should().BeFalse();
        stream.Capacity.Should().Be(count);
      }
    }

    using (new AssertionScope())
    {
      const int count = 1000;

      AssertionExtensions.Should(() => ((Random) null).ToMemoryStreamAsync(0, [1..2])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();

      new Random().ToMemoryStreamAsync(0, [Range.All]).Await().Length.Should().Be(0);

      using (var stream = new Random().ToMemoryStreamAsync(count, [Range.All]).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, [Range.All])).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, new[] {..100}).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, [Range.All])).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, new[] {..0}).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, [Range.All])).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }

      static void Test(MemoryStream stream, int count)
      {
        stream.Length.Should().Be(count);
        stream.Position.Should().Be(0);
        stream.CanRead.Should().BeTrue();
        stream.CanWrite.Should().BeTrue();
        stream.CanSeek.Should().BeTrue();
        stream.CanTimeout.Should().BeFalse();
        stream.Capacity.Should().Be(count);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToStream(Random, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToStream(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToStream()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToStream()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToIpAddress(Random, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToIpAddress(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToIpAddress(Random, int, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToIpAddress(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToIpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpAddress()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpAddress()).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpAddress(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToIpAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Random) null).ToIpAddress(0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToIpAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }
}