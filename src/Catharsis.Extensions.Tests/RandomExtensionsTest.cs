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
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToSbyte"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(System.Random,int,sbyte?,sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSbyte(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Sbyte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToSbyte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      AssertionExtensions.Should(() => RandomExtensions.ToSbyte(null, [Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToSbyte(new[] { Range.All }).Should().Be(0);
      new Random().ToSbyte(new[] { ..0 }).Should().Be(0);
      new Random().ToSbyte(new[] { sbyte.MaxValue..sbyte.MaxValue }).Should().Be(sbyte.MaxValue);
      new Random().ToSbyte(new[] { ..sbyte.MaxValue }).Should().BeInRange(0, sbyte.MaxValue);
      new Random().ToSbyte(new[] { sbyte.MaxValue..0 }).Should().BeInRange(0, sbyte.MaxValue);
      new Random().ToSbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Test(Range[] ranges, sbyte min, sbyte max) => new Random().ToSbyte(ranges).Should().BeInRange(min, max);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToSbyte(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToSbyte(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSbyte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToSbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().ToSbyte(0));

      new Random().ToSbyte(0, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ToSbyte(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToSbyte(count, new[] { ..0 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToSbyte(count, new[] { sbyte.MaxValue..sbyte.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().ToSbyte(count, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToSbyte(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToSbyte(count, new[] { ..0, ..sbyte.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().ToSbyte(count, new[] { ..0, sbyte.MaxValue..0 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().ToSbyte(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToByte"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(System.Random,int,byte?,byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToByte(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Byte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToByte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      
      RandomExtensions.ToByte(new Random(), (byte?) 0, 0).Should().Be(0);
      new Random().ToByte(from: byte.MinValue, to: byte.MinValue).Should().Be(byte.MinValue);
      new Random().ToByte(from: byte.MaxValue, to: byte.MaxValue).Should().Be(byte.MaxValue);
      new Random().ToByte(from: byte.MinValue, to: byte.MaxValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().ToByte(from: byte.MaxValue, to: byte.MinValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().ToByte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToByte(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToByte(new[] {Range.All}).Should().Be(0);
      new Random().ToByte(new[] {..0}).Should().Be(0);
      new Random().ToByte(new[] {..byte.MinValue}).Should().Be(byte.MinValue);
      new Random().ToByte(new[] {byte.MaxValue..byte.MaxValue}).Should().Be(byte.MaxValue);
      new Random().ToByte(new[] {..byte.MaxValue}).Should().BeInRange(0, byte.MaxValue);
      new Random().ToByte(new[] {byte.MaxValue..0}).Should().BeInRange(0, byte.MaxValue);
      new Random().ToByte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToByte(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToByte(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToByte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToByte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().ToByte(0));

      new Random().ToByte(0, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ToByte(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ToByte(count, new[] { ..0 }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, new[] { ..byte.MinValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ToByte(count, new[] { byte.MaxValue..byte.MaxValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ToByte(count, new[] { Range.All }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToByte(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToByte(count, new[] { ..0, ..byte.MaxValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
      new Random().ToByte(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToShort"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(System.Random,int,short?,short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToShort(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Short_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToShort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.ToShort(new Random(), (short?) 0, 0).Should().Be(0);
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
      AssertionExtensions.Should(() => RandomExtensions.ToShort(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToShort(new [] {Range.All}).Should().Be(0);
      new Random().ToShort(new[] {..0}).Should().Be(0);
      new Random().ToShort(new[] {short.MaxValue..short.MaxValue}).Should().Be(short.MaxValue);
      new Random().ToShort(new[] {..short.MaxValue}).Should().BeInRange(0, short.MaxValue);
      new Random().ToShort(new[] {short.MaxValue..0}).Should().BeInRange(0, short.MaxValue);
      new Random().ToShort().Should().BeInRange(short.MinValue, short.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToShort(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToShort(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToShort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToShort(0).Should().BeOfType<IEnumerable<short>>().And.BeSameAs(new Random().ToShort(0));

      new Random().ToShort(0, new[] { Range.All }).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();
      new Random().ToShort(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().ToShort(count, new[] { ..0 }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToShort(count, new[] { short.MaxValue..short.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().ToShort(count, new[] { Range.All }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToShort(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToShort(count, new[] { ..0, ..short.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().ToShort(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToUshort"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(System.Random,int,ushort?,ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToUshort(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Ushort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToUshort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.ToUshort(new Random(), (ushort?) 0, 0).Should().Be(0);
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
      AssertionExtensions.Should(() => RandomExtensions.ToUshort(null, new [] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToUshort(new[] {Range.All}).Should().Be(0);
      new Random().ToUshort(new[] {..0}).Should().Be(0);
      new Random().ToUshort(new[] {..ushort.MinValue}).Should().Be(ushort.MinValue);
      new Random().ToUshort(new[] {ushort.MaxValue..ushort.MaxValue}).Should().Be(ushort.MaxValue);
      new Random().ToUshort(new[] {..ushort.MaxValue}).Should().BeInRange(0, ushort.MaxValue);
      new Random().ToUshort(new[] {ushort.MaxValue..0}).Should().BeInRange(0, ushort.MaxValue);
      new Random().ToUshort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToUshort(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToUshort(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToUshort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToUshort(0).Should().BeOfType<IEnumerable<ushort>>().And.BeSameAs(new Random().ToUshort(0));

      new Random().ToUshort(0, new[] { Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();
      new Random().ToUshort(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().ToUshort(count, new[] { ..0 }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUshort(count, new[] { ..ushort.MinValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().ToUshort(count, new[] { ushort.MaxValue..ushort.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().ToUshort(count, new[] { Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ToUshort(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ToUshort(count, new[] { ..0, ..ushort.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
      new Random().ToUshort(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToInt"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(System.Random,int,int?,int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToInt(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Int_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToInt(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.ToInt(new Random(), (int?) 0, 0).Should().Be(0);
      RandomExtensions.ToInt(new Random(), (int?) int.MinValue, int.MinValue).Should().Be(int.MinValue);
      RandomExtensions.ToInt(new Random(), (int?) int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
      RandomExtensions.ToInt(new Random(), (int?) int.MinValue, int.MaxValue).Should().BeInRange(int.MinValue, int.MaxValue);
      RandomExtensions.ToInt(new Random(), (int?) int.MaxValue, int.MinValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().ToInt().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToInt(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToInt(new[] {Range.All}).Should().Be(0);
      new Random().ToInt(new[] {..0}).Should().Be(0);
      new Random().ToInt(new[] {int.MaxValue..int.MaxValue}).Should().Be(int.MaxValue);
      new Random().ToInt(new[] {..int.MaxValue}).Should().BeInRange(0, int.MaxValue);
      new Random().ToInt(new[] {int.MaxValue..0}).Should().BeInRange(0, int.MaxValue);
      new Random().ToInt().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToInt(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.Uint(Random, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Uint(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Uint(Random, int, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Uint(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Uint_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.Uint(new Random(), (uint?) 0, 0).Should().Be(0);
      new Random().Uint(uint.MinValue, uint.MinValue).Should().Be(uint.MinValue);
      new Random().Uint(uint.MaxValue, uint.MaxValue).Should().Be(uint.MaxValue);
      new Random().Uint(uint.MinValue, uint.MaxValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().Uint(uint.MaxValue, uint.MinValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().Uint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Uint(new[] {Range.All}).Should().Be(0);
      new Random().Uint(new[] {..0}).Should().Be(0);
      new Random().Uint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Uint(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Uint(0).Should().BeOfType<IEnumerable<uint>>().And.BeEmpty();

      new Random().Uint(count, 0, 0).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Uint(count, uint.MinValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MinValue);
      new Random().Uint(count, uint.MaxValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MaxValue);
      new Random().Uint(count, uint.MinValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().Uint(count, uint.MaxValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().Uint(count).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Uint(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Uint(0, new[] { Range.All }).Should().BeEmpty();

      new Random().Uint(count, new[] { ..0 }).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Uint(count, new[] { Range.All }).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Uint(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Uint(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Long(Random, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Long(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Long(Random, int, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Long(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Long_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.Long(new Random(), (long?) 0, 0).Should().Be(0);
      new Random().Long(long.MinValue, long.MinValue).Should().Be(long.MinValue);
      new Random().Long(long.MaxValue, long.MaxValue).Should().Be(long.MaxValue);
      new Random().Long(long.MinValue, long.MaxValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().Long(long.MaxValue, long.MinValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().Long().Should().BeInRange(long.MinValue, long.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Long(new [] {Range.All}).Should().Be(0);
      new Random().Long(new[] {..0}).Should().Be(0);
      new Random().Long().Should().BeInRange(long.MinValue, long.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Long(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Long(0).Should().BeOfType<IEnumerable<long>>().And.BeEmpty();

      new Random().Long(count, 0, 0).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Long(count, long.MinValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MinValue);
      new Random().Long(count, long.MaxValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MaxValue);
      new Random().Long(count, long.MinValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().Long(count, long.MaxValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().Long(count).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Long(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Long(0, new[] { Range.All }).Should().BeEmpty();
      new Random().Long(0, new[] { ..int.MaxValue }).Should().BeEmpty();

      new Random().Long(count, new[] { ..0 }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Long(count, new[] { int.MaxValue..int.MaxValue }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().Long(count, new[] { Range.All }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Long(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Long(count, new[] { ..0, ..short.MaxValue }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().Long(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Float(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Float(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Float_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Float(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Float().Should().BeInRange(float.MinValue, float.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Float(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().Float(0).Should().BeOfType<IEnumerable<float>>().And.BeEmpty();
      new Random().Float(count).Should().BeOfType<IEnumerable<float>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(float.MinValue, float.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDouble"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDouble(System.Random,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Double_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDouble(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDouble().Should().BeInRange(double.MinValue, double.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDouble(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
  ///     <item><description><see cref="RandomExtensions.ToChar"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(System.Random,int,char?,char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToChar(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Char_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToChar(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      AssertionExtensions.Should(() => RandomExtensions.ToChar(null, new [] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToChar(new[] {Range.All}).Should().Be((char) 0);
      new Random().ToChar(new[] {..0}).Should().Be((char) 0);
      new Random().ToChar(new[] {..char.MinValue}).Should().Be(char.MinValue);
      new Random().ToChar(new[] {char.MaxValue..char.MaxValue}).Should().Be(char.MaxValue);
      new Random().ToChar(new[] {..char.MaxValue}).Should().BeInRange((char) 0, char.MaxValue);
      new Random().ToChar(new[] {char.MaxValue..0}).Should().BeInRange((char) 0, char.MaxValue);
      new Random().ToChar().Should().BeInRange(char.MinValue, char.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToChar(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToChar(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToChar(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToChar(0).Should().BeOfType<IEnumerable<char>>().And.BeSameAs(new Random().ToChar(0));

      new Random().ToChar(0, new[] { Range.All }).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();
      new Random().ToChar(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().ToChar(count, new[] { ..0 }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToChar(count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToChar(count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToChar(count, new[] { Range.All }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().ToChar(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToChar(count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().ToChar(count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToText"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(System.Random,int,int,char?,char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToText(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToText(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToText(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ToText(0).Should().BeOfType<string>().And.BeSameAs(new Random().ToText(0));

      new Random().ToText(0, new[] {Range.All}).Should().BeOfType<string>().And.BeEmpty();
      new Random().ToText(0, new[] {..int.MaxValue}).Should().BeOfType<string>().And.BeEmpty();

      new Random().ToText(count, new[] {..0}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToText(count, new[] {..char.MinValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToText(count, new[] {char.MaxValue..char.MaxValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToText(count, new[] {Range.All}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

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
      AssertionExtensions.Should(() => RandomExtensions.ToText(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToText(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToText(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToText(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToText(0, 0).Should().BeOfType<IEnumerable<string>>().And.BeSameAs(new Random().ToText(0, 0));

      new Random().ToText(size, 0, new[] { Range.All }).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().ToText(size, 0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().ToText(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().ToText(size, count, new[] { ..0 }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToText(size, count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToText(size, count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToText(size, count, new[] { Range.All }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().ToText(size, count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToText(size, count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().ToText(size, count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Digits(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Digits(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Digits_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Digits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Digits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Digits(0).Should().BeOfType<string>().And.BeSameAs(new Random().Digits(0)).And.BeEmpty();
      new Random().Digits(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[0-9]+$");

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Digits(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Digits(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().Digits(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().Digits(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().Digits(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().Digits(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[0-9]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToLetters"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToLetters(System.Random,int,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Letters_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToLetters(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToLetters(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToAlphaDigits"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToAlphaDigits(System.Random,int,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AlphaDigits_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToAlphaDigits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToAlphaDigits(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToSecureString"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(System.Random,int,int,char?,char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToSecureString(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SecureString_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToSecureString(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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

      AssertionExtensions.Should(() => RandomExtensions.ToSecureString(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      new Random().ToSecureString(0, new[] {Range.All}).Length.Should().Be(0);
      new Random().ToSecureString(0, new[] {..int.MaxValue}).Length.Should().Be(0);

      new Random().ToSecureString(count, new[] {..0}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().ToSecureString(count, new[] {..char.MinValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().ToSecureString(count, new[] {char.MaxValue..char.MaxValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().ToSecureString(count, new[] {Range.All}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

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
      AssertionExtensions.Should(() => RandomExtensions.ToSecureString(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToSecureString(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToSecureString(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToSecureString(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().ToSecureString(size, 0, new[] { Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();
      new Random().ToSecureString(size, 0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().ToSecureString(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().ToSecureString(size, count, new[] { ..0 }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToSecureString(size, count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().ToSecureString(size, count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().ToSecureString(size, count, new[] { Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().ToSecureString(size, count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().ToSecureString(size, count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().ToSecureString(size, count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.DateTime(Random, DateTime?, DateTime?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.DateTime(Random, int, DateTime?, DateTime?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DateTime_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTime(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateTime(DateTime.MinValue, DateTime.MinValue).Should().Be(DateTime.MinValue).And.BeSameDateAs(new Random().DateTime(DateTime.MinValue, DateTime.MinValue));
      new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue).Should().Be(DateTime.MaxValue).And.BeSameDateAs(new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue));
      new Random().DateTime(DateTime.MinValue, DateTime.MaxValue).Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().DateTime(DateTime.MaxValue, DateTime.MinValue).Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().DateTime().Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTime(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateTime(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateTime(0).Should().BeEmpty();
      new Random().DateTime(count, DateTime.MinValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MinValue);
      new Random().DateTime(count, DateTime.MaxValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MaxValue);
      new Random().DateTime(count, DateTime.MinValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().DateTime(count, DateTime.MaxValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().DateTime(count).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.DateTimeOffset(Random, DateTimeOffset?, DateTimeOffset?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.DateTimeOffset(Random, int, DateTimeOffset?, DateTimeOffset?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DateTimeOffset_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTimeOffset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue));
      new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue));
      new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().DateTimeOffset().Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTimeOffset(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateTimeOffset(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateTimeOffset(0).Should().BeEmpty();
      new Random().DateTimeOffset(count, DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MinValue);
      new Random().DateTimeOffset(count, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MaxValue);
      new Random().DateTimeOffset(count, DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().DateTimeOffset(count, DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().DateTimeOffset(count).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.DateOnly(Random, DateOnly?, DateOnly?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.DateOnly(Random, int, DateOnly?, DateOnly?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DateOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateOnly(DateOnly.MinValue, DateOnly.MinValue).Should().Be(DateOnly.MinValue);
      new Random().DateOnly(DateOnly.MaxValue, DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
      new Random().DateOnly(DateOnly.MinValue, DateOnly.MaxValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().DateOnly(DateOnly.MaxValue, DateOnly.MinValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().DateOnly().Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateOnly(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateOnly(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateOnly(0).Should().BeEmpty();

      new Random().DateOnly(count, DateOnly.MinValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MinValue);
      new Random().DateOnly(count, DateOnly.MaxValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MaxValue);
      new Random().DateOnly(count, DateOnly.MinValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().DateOnly(count, DateOnly.MaxValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().DateOnly(count).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TimeOnly(Random, TimeOnly?, TimeOnly?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TimeOnly(Random, int, TimeOnly?, TimeOnly?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TimeOnly_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
      new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
      new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MinValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().TimeOnly().Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeOnly(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TimeOnly(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().TimeOnly(0).Should().BeEmpty();
      new Random().TimeOnly(count, TimeOnly.MinValue, TimeOnly.MinValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MinValue);
      new Random().TimeOnly(count, TimeOnly.MaxValue, TimeOnly.MaxValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MaxValue);
      new Random().TimeOnly(count).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TimeSpan(Random, TimeSpan?, TimeSpan?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TimeSpan(Random, int, TimeSpan?, TimeSpan?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TimeSpan_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeSpan(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeSpan(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TimeSpan(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

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
  ///     <item><description><see cref="RandomExtensions.ToRange"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToRange(System.Random,int,int?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => RandomExtensions.ToRange(new Random(), (int?) int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

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
      AssertionExtensions.Should(() => RandomExtensions.ToRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToGuid"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToGuid(System.Random,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Guid_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToGuid(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToGuid().Should().NotBe(Guid.Empty);
      new Random().ToGuid().Should().NotBe(Guid.NewGuid());

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToGuid(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToObject"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(System.Random,System.Type[])"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(System.Random,int,System.Collections.Generic.IEnumerable{System.Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToObject(System.Random,int,System.Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToObject(null, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToObject(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToObject(null, 0, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(0, (IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ToObject(-1, Enumerable.Empty<Type>())).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToObject(null, 0, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToObject(0, null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ToObject(-1, [])).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToFileName"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFileName(System.Random,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FileName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToFileName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToFileName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToFileName(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToDirectoryName"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDirectoryName(System.Random,int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DirectoryName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDirectoryName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().ToDirectoryName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDirectoryName(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToFilePath"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFilePath(System.Random,int,System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FilePath_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToFilePath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      AssertionExtensions.Should(() => RandomExtensions.ToFilePath(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToFilePath"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFilePath(System.Random,int,System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DirectoryPath_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().DirectoryPath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().DirectoryPath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryPath(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DirectoryPath(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DirectoryPath(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().DirectoryPath(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(Path.GetTempPath(), path));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().DirectoryPath(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(currentDirectory.FullName, path));

      throw new NotImplementedException();

      static void Test(string directory, string path)
      {
        var file = Path.GetFileName(path);
        path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(directory, file));
        file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToDirectory"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToDirectory(System.Random,int,System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Directory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToDirectory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      AssertionExtensions.Should(() => RandomExtensions.ToDirectory(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToFile"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToFile(System.Random,int,System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void File_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToFile(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      AssertionExtensions.Should(() => RandomExtensions.ToFile(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(System.Random,int,int,byte?,byte?,System.IO.DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFile(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,byte?,byte?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.IO.DirectoryInfo,System.Threading.CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync(System.Random,int,int,byte?,byte?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToBinaryFileAsync"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
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
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
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
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFileAsync(null, 0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(-1, 0).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToBinaryFileAsync(0, -1).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToBinaryFileAsync(null, 0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToTextFile"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(System.Random,int,System.Collections.Generic.IEnumerable{System.Range},System.Text.Encoding,System.IO.DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(System.Random,int,int,System.Text.Encoding,char?,char?,System.IO.DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFile(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range},System.Text.Encoding,System.IO.DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToTextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTextFile(-1)).ThrowExactly<ArgumentNullException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToTextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToTextFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      //AssertionExtensions.Should(() => new Random().TextFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      //AssertionExtensions.Should(() => new Random().TextFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToTextFileAsync(System.Random,int,System.Text.Encoding,char?,char?,System.IO.DirectoryInfo,System.Threading.CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToTextFileAsync"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToTextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToTextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToTextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
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
  ///     <item><description><see cref="RandomExtensions.IpV6Address(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpV6Address(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IpV6Address_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpV6Address(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpV6Address(null, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpV6Address(-1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(System.Random,int,int,byte?,byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToPhysicalAddress(System.Random,int,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void PhysicalAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToPhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
      AssertionExtensions.Should(() => RandomExtensions.ToPhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToPhysicalAddress(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().ToPhysicalAddress(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToPhysicalAddress(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToMemoryStream"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToMemoryStream(System.Random,int,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToMemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ToMemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToMemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
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
  ///     <item><description><see cref="RandomExtensions.ToMemoryStreamAsync(System.Random,int,byte?,byte?,System.Threading.CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToMemoryStreamAsync"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToMemoryStreamAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      const int count = 1000;

      new Random().ToMemoryStreamAsync(0).Await().Length.Should().Be(0);
      new Random().ToMemoryStreamAsync(0, null, null).Await().Length.Should().Be(0);

      using (var stream = new Random().ToMemoryStreamAsync(count).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, 0, 100).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, 0, 0).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

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

      AssertionExtensions.Should(() => RandomExtensions.ToMemoryStreamAsync(null, 0, new[] {1..2})).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();

      new Random().ToMemoryStreamAsync(0, new[] {Range.All}).Await().Length.Should().Be(0);

      using (var stream = new Random().ToMemoryStreamAsync(count, new [] {Range.All}).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, new [] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, new[] {..100}).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, new[] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().ToMemoryStreamAsync(count, new[] {..0}).Await())
      {
        AssertionExtensions.Should(() => new Random().ToMemoryStreamAsync(count, new[] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

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
  ///     <item><description><see cref="RandomExtensions.ToStream"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ToStream(System.Random,System.Collections.Generic.IEnumerable{System.Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ToStream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, int, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTime(Random, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTime(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateTime(DateTime.MinValue, DateTime.MinValue).Should().Be(DateTime.MinValue).And.BeSameDateAs(new Random().DateTime(DateTime.MinValue, DateTime.MinValue));
      new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue).Should().Be(DateTime.MaxValue).And.BeSameDateAs(new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue));
      new Random().DateTime(DateTime.MinValue, DateTime.MaxValue).Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().DateTime(DateTime.MaxValue, DateTime.MinValue).Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
      new Random().DateTime().Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
    }

    throw new NotImplementedException();

    static void Test()
    {
    }
  }
}