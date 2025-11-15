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
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, int, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Sbyte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Sbyte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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
      new Random().Sbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Test(sbyte from, sbyte to, sbyte min, sbyte max) => new Random().Sbyte(min, max).Should().BeInRange(from, to);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Sbyte(null, [Range.All])).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Sbyte(new[] { Range.All }).Should().Be(0);
      new Random().Sbyte(new[] { ..0 }).Should().Be(0);
      new Random().Sbyte(new[] { sbyte.MaxValue..sbyte.MaxValue }).Should().Be(sbyte.MaxValue);
      new Random().Sbyte(new[] { ..sbyte.MaxValue }).Should().BeInRange(0, sbyte.MaxValue);
      new Random().Sbyte(new[] { sbyte.MaxValue..0 }).Should().BeInRange(0, sbyte.MaxValue);
      new Random().Sbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Test(Range[] ranges, sbyte min, sbyte max) => new Random().Sbyte(ranges).Should().BeInRange(min, max);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Sbyte(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Sbyte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Sbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().Sbyte(count, 0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Sbyte(count, sbyte.MinValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MinValue);
      new Random().Sbyte(count, sbyte.MaxValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().Sbyte(count, sbyte.MinValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().Sbyte(count, sbyte.MaxValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().Sbyte(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));

      throw new NotImplementedException();

      static void Test(int count, sbyte from, sbyte to)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Sbyte(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Sbyte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Sbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().Sbyte(0));

      new Random().Sbyte(0, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().Sbyte(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().Sbyte(count, new[] { ..0 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Sbyte(count, new[] { sbyte.MaxValue..sbyte.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().Sbyte(count, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Sbyte(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Sbyte(count, new[] { ..0, ..sbyte.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().Sbyte(count, new[] { ..0, sbyte.MaxValue..0 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().Sbyte(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Byte(Random, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Byte(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Byte(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Byte(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Byte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Byte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      
      RandomExtensions.Byte(new Random(), (byte?) 0, 0).Should().Be(0);
      new Random().Byte(from: byte.MinValue, to: byte.MinValue).Should().Be(byte.MinValue);
      new Random().Byte(from: byte.MaxValue, to: byte.MaxValue).Should().Be(byte.MaxValue);
      new Random().Byte(from: byte.MinValue, to: byte.MaxValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().Byte(from: byte.MaxValue, to: byte.MinValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().Byte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Byte(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Byte(new[] {Range.All}).Should().Be(0);
      new Random().Byte(new[] {..0}).Should().Be(0);
      new Random().Byte(new[] {..byte.MinValue}).Should().Be(byte.MinValue);
      new Random().Byte(new[] {byte.MaxValue..byte.MaxValue}).Should().Be(byte.MaxValue);
      new Random().Byte(new[] {..byte.MaxValue}).Should().BeInRange(0, byte.MaxValue);
      new Random().Byte(new[] {byte.MaxValue..0}).Should().BeInRange(0, byte.MaxValue);
      new Random().Byte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Byte(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Byte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Byte(0).Should().BeOfType<IEnumerable<byte>>().And.NotBeNull().And.BeEmpty();

      new Random().Byte(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Byte(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().Byte(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().Byte(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().Byte(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().Byte(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Byte(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Byte(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Byte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().Byte(0));

      new Random().Byte(0, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().Byte(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().Byte(count, new[] { ..0 }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Byte(count, new[] { ..byte.MinValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().Byte(count, new[] { byte.MaxValue..byte.MaxValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().Byte(count, new[] { Range.All }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Byte(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Byte(count, new[] { ..0, ..byte.MaxValue }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
      new Random().Byte(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Short(Random, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Short(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Short(Random, int, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Short(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Short_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Short(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.Short(new Random(), (short?) 0, 0).Should().Be(0);
      new Random().Short(short.MinValue, short.MinValue).Should().Be(short.MinValue);
      new Random().Short(short.MaxValue, short.MaxValue).Should().Be(short.MaxValue);
      new Random().Short(short.MinValue, short.MaxValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().Short(short.MaxValue, short.MinValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().Short().Should().BeInRange(short.MinValue, short.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Short(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Short(new [] {Range.All}).Should().Be(0);
      new Random().Short(new[] {..0}).Should().Be(0);
      new Random().Short(new[] {short.MaxValue..short.MaxValue}).Should().Be(short.MaxValue);
      new Random().Short(new[] {..short.MaxValue}).Should().BeInRange(0, short.MaxValue);
      new Random().Short(new[] {short.MaxValue..0}).Should().BeInRange(0, short.MaxValue);
      new Random().Short().Should().BeInRange(short.MinValue, short.MaxValue);

      throw new NotImplementedException();
      
      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Short(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Short(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Short(0).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().Short(count, 0, 0).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Short(count, short.MinValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MinValue);
      new Random().Short(count, short.MaxValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().Short(count, short.MinValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().Short(count, short.MaxValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().Short(count).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Short(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Short(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Short(0).Should().BeOfType<IEnumerable<short>>().And.BeSameAs(new Random().Short(0));

      new Random().Short(0, new[] { Range.All }).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();
      new Random().Short(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().Short(count, new[] { ..0 }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Short(count, new[] { short.MaxValue..short.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().Short(count, new[] { Range.All }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Short(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Short(count, new[] { ..0, ..short.MaxValue }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().Short(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, int, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Ushort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Ushort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.Ushort(new Random(), (ushort?) 0, 0).Should().Be(0);
      new Random().Ushort(from: ushort.MinValue, to: byte.MinValue).Should().Be(ushort.MinValue);
      new Random().Ushort(from: ushort.MaxValue, to: ushort.MaxValue).Should().Be(ushort.MaxValue);
      new Random().Ushort(from: ushort.MinValue, to: ushort.MaxValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().Ushort(from: ushort.MaxValue, to: ushort.MinValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().Ushort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Ushort(null, new [] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Ushort(new[] {Range.All}).Should().Be(0);
      new Random().Ushort(new[] {..0}).Should().Be(0);
      new Random().Ushort(new[] {..ushort.MinValue}).Should().Be(ushort.MinValue);
      new Random().Ushort(new[] {ushort.MaxValue..ushort.MaxValue}).Should().Be(ushort.MaxValue);
      new Random().Ushort(new[] {..ushort.MaxValue}).Should().BeInRange(0, ushort.MaxValue);
      new Random().Ushort(new[] {ushort.MaxValue..0}).Should().BeInRange(0, ushort.MaxValue);
      new Random().Ushort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Ushort(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Ushort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Ushort(0).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().Ushort(count, 0, 0).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Ushort(count, ushort.MinValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().Ushort(count, ushort.MaxValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().Ushort(count, ushort.MinValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().Ushort(count, ushort.MaxValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().Ushort(count).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Ushort(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Ushort(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Ushort(0).Should().BeOfType<IEnumerable<ushort>>().And.BeSameAs(new Random().Ushort(0));

      new Random().Ushort(0, new[] { Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();
      new Random().Ushort(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().Ushort(count, new[] { ..0 }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Ushort(count, new[] { ..ushort.MinValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().Ushort(count, new[] { ushort.MaxValue..ushort.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().Ushort(count, new[] { Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().Ushort(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Ushort(count, new[] { ..0, ..ushort.MaxValue }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
      new Random().Ushort(count, new[] { ..0, 1..2 }).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Int(Random, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Int(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Int(Random, int, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Int(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Int_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Int(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      RandomExtensions.Int(new Random(), (int?) 0, 0).Should().Be(0);
      RandomExtensions.Int(new Random(), (int?) int.MinValue, int.MinValue).Should().Be(int.MinValue);
      RandomExtensions.Int(new Random(), (int?) int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
      RandomExtensions.Int(new Random(), (int?) int.MinValue, int.MaxValue).Should().BeInRange(int.MinValue, int.MaxValue);
      RandomExtensions.Int(new Random(), (int?) int.MaxValue, int.MinValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().Int().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Int(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Int(new[] {Range.All}).Should().Be(0);
      new Random().Int(new[] {..0}).Should().Be(0);
      new Random().Int(new[] {int.MaxValue..int.MaxValue}).Should().Be(int.MaxValue);
      new Random().Int(new[] {..int.MaxValue}).Should().BeInRange(0, int.MaxValue);
      new Random().Int(new[] {int.MaxValue..0}).Should().BeInRange(0, int.MaxValue);
      new Random().Int().Should().BeInRange(int.MinValue, int.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Int(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Int(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Int(0).Should().BeOfType<IEnumerable<int>>().And.BeEmpty();

      new Random().Int(count, 0, 0).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Int(count, int.MinValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MinValue);
      new Random().Int(count, int.MaxValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().Int(count, int.MinValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().Int(count, int.MaxValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().Int(count).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));

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
  ///     <item><description><see cref="RandomExtensions.Double(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Double(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Double_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Double(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Double().Should().BeInRange(double.MinValue, double.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Double(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().Double(0).Should().BeOfType<IEnumerable<double>>().And.BeEmpty();
      new Random().Double(count).Should().BeOfType<IEnumerable<double>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(double.MinValue, double.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Char(Random, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Char(Random, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Char(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Char(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Char_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Char(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Char((char) 0, (char) 0).Should().Be((char) 0);
      new Random().Char(char.MinValue, char.MinValue).Should().Be(char.MinValue);
      new Random().Char(char.MaxValue, char.MaxValue).Should().Be(char.MaxValue);
      new Random().Char(char.MinValue, char.MaxValue).Should().BeInRange(char.MinValue, char.MaxValue);
      new Random().Char(char.MaxValue, char.MinValue).Should().BeInRange(char.MinValue, char.MaxValue);
      new Random().Char().Should().BeInRange(char.MinValue, char.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Char(null, new [] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Char(new[] {Range.All}).Should().Be((char) 0);
      new Random().Char(new[] {..0}).Should().Be((char) 0);
      new Random().Char(new[] {..char.MinValue}).Should().Be(char.MinValue);
      new Random().Char(new[] {char.MaxValue..char.MaxValue}).Should().Be(char.MaxValue);
      new Random().Char(new[] {..char.MaxValue}).Should().BeInRange((char) 0, char.MaxValue);
      new Random().Char(new[] {char.MaxValue..0}).Should().BeInRange((char) 0, char.MaxValue);
      new Random().Char().Should().BeInRange(char.MinValue, char.MaxValue);

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Char(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Char(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Char(0).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().Char(count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().Char(count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().Char(count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().Char(count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().Char(count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().Char(count).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Char(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Char(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Char(0).Should().BeOfType<IEnumerable<char>>().And.BeSameAs(new Random().Char(0));

      new Random().Char(0, new[] { Range.All }).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();
      new Random().Char(0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().Char(count, new[] { ..0 }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().Char(count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().Char(count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().Char(count, new[] { Range.All }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().Char(count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().Char(count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().Char(count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.String(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.String(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.String(Random, int, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.String(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void String_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.String(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().String(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().String(0).Should().BeOfType<string>().And.BeSameAs(new Random().String(0)).And.BeEmpty();
      new Random().String(count, (char) 0, (char) 0).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().String(count, char.MinValue, char.MinValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().String(count, char.MaxValue, char.MaxValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().String(count, char.MinValue, char.MaxValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().String(count, char.MaxValue, char.MinValue).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().String(count).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.String(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().String(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().String(0).Should().BeOfType<string>().And.BeSameAs(new Random().String(0));

      new Random().String(0, new[] {Range.All}).Should().BeOfType<string>().And.BeEmpty();
      new Random().String(0, new[] {..int.MaxValue}).Should().BeOfType<string>().And.BeEmpty();

      new Random().String(count, new[] {..0}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().String(count, new[] {..char.MinValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().String(count, new[] {char.MaxValue..char.MaxValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().String(count, new[] {Range.All}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().String(count, new[] {..0, Range.All}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().String(count, new[] {..0, ..char.MaxValue}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().String(count, new[] {..0, 'a'..'b'}).ToCharArray().Should().BeOfType<string>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.String(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().String(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().String(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().Sbyte(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().String(0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().String(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(string.Empty);

      new Random().String(int.MaxValue, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().String(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().String(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().String(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().String(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().String(size, count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().String(size, count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().String(size, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.String(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().String(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().String(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().String(0, 0).Should().BeOfType<IEnumerable<string>>().And.BeSameAs(new Random().String(0, 0));

      new Random().String(size, 0, new[] { Range.All }).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().String(size, 0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().String(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().String(size, count, new[] { ..0 }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().String(size, count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().String(size, count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().String(size, count, new[] { Range.All }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().String(size, count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().String(size, count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().String(size, count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

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
  ///     <item><description><see cref="RandomExtensions.Letters(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Letters(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Letters_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Letters(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Letters(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Letters(0).Should().BeOfType<string>().And.BeSameAs(new Random().Letters(0)).And.BeEmpty();
      new Random().Letters(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z]+$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Letters(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Letters(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().Letters(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().Letters(int.MaxValue, 0).Should().BeEmpty();

      new Random().Letters(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().Letters(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.AlphaDigits(Random, int)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.AlphaDigits(Random, int, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void AlphaDigits_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.AlphaDigits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().AlphaDigits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().AlphaDigits(0).Should().BeOfType<string>().And.BeSameAs(new Random().AlphaDigits(0)).And.BeEmpty();
      new Random().AlphaDigits(count).Should().BeOfType<string>().And.NotBeSameAs(new Random().AlphaDigits(count)).And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z0-9]+$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.AlphaDigits(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().AlphaDigits(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().AlphaDigits(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().AlphaDigits(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().AlphaDigits(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().AlphaDigits(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z0-9]+$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.SecureString(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SecureString(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SecureString(Random,int,int,char?,char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SecureString(Random,int,int,IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SecureString_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().SecureString(0).Length.Should().Be(0);

      new Random().SecureString(count, (char) 0, (char) 0).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().SecureString(count, char.MinValue, char.MinValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().SecureString(count, char.MaxValue, char.MaxValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().SecureString(count, char.MinValue, char.MaxValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().SecureString(count, char.MaxValue, char.MinValue).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().SecureString(count).ToText().ToCharArray().Should().BeOfType<SecureString>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      const int count = 1000;

      AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      new Random().SecureString(0, new[] {Range.All}).Length.Should().Be(0);
      new Random().SecureString(0, new[] {..int.MaxValue}).Length.Should().Be(0);

      new Random().SecureString(count, new[] {..0}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().SecureString(count, new[] {..char.MinValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().SecureString(count, new[] {char.MaxValue..char.MaxValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().SecureString(count, new[] {Range.All}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().SecureString(count, new[] {..0, Range.All}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().SecureString(count, new[] {..0, ..char.MaxValue}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().SecureString(count, new[] {..0, 'a'..'b'}).ToText().ToCharArray().Should().BeOfType<char[]>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureString(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().SecureString(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().SecureString(int.MaxValue, 0).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().SecureString(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().SecureString(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureString(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().SecureString(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().SecureString(size, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureString(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().SecureString(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().SecureString(size, 0, new[] { Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();
      new Random().SecureString(size, 0, new[] { ..int.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().SecureString(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().SecureString(size, count, new[] { ..0 }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureString(size, count, new[] { ..char.MinValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().SecureString(size, count, new[] { char.MaxValue..char.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().SecureString(size, count, new[] { Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().SecureString(size, count, new[] { ..0, Range.All }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureString(size, count, new[] { ..0, ..char.MaxValue }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().SecureString(size, count, new[] { ..0, 'a'..'b' }).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

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
  ///     <item><description><see cref="RandomExtensions.Range(Random, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Range(Random, int, int?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Range_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Range(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => RandomExtensions.Range(new Random(), (int?) int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

      new int?[] { null, 0, int.MaxValue }.ForEach(max =>
      {
        var range = new Random().Range(max);
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
      AssertionExtensions.Should(() => RandomExtensions.Range(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Range(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Range(0).Should().BeOfType<IEnumerable<Range>>().And.BeEmpty();
      new Random().Range(count, 0).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Test(element, 0));
      new Random().Range(count).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Test(element, null));

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
  ///     <item><description><see cref="RandomExtensions.Guid(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Guid(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Guid_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Guid(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Guid().Should().NotBe(Guid.Empty);
      new Random().Guid().Should().NotBe(Guid.NewGuid());

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Guid(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Guid(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Guid(0).Should().BeOfType<IEnumerable<Guid>>().And.BeEmpty();
      new Random().Guid(count).Should().BeOfType<IEnumerable<Guid>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBe(Guid.Empty).And.NotBe(Guid.NewGuid()));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Object(Random, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Object(Random, Type[])"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Object(Random, int, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Object(Random, int, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object(null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, 0, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object(0, (IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().Object(-1, Enumerable.Empty<Type>())).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, 0, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object(0, null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().Object(-1, [])).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.FileName(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.FileName(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FileName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().FileName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileName(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().FileName(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().FileName(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().FileName(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.DirectoryName(Random)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.DirectoryName(Random, int)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void DirectoryName_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DirectoryName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryName(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DirectoryName(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DirectoryName(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().DirectoryName(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$"));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.FilePath(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.FilePath(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void FilePath_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FilePath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().FilePath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().FilePath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FilePath(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().FilePath(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().FilePath(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().FilePath(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(path, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().FilePath(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Test(path, currentDirectory.FullName));

      throw new NotImplementedException();

      static void Test(string path, string directory)
      {
        var file = Path.GetFileName(path);
        path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(directory, file));
        file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.FilePath(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.FilePath(Random, int, DirectoryInfo)"/></description></item>
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
  ///     <item><description><see cref="RandomExtensions.Directory(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Directory(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Directory_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Directory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Test(new Random().Directory(), Path.GetTempPath());

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
      AssertionExtensions.Should(() => RandomExtensions.Directory(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Directory(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().Directory(0).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.BeEmpty();

      new Random().Directory(count).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Test(directory, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().Directory(count, currentDirectory).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Test(directory, currentDirectory.FullName));

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
  ///     <item><description><see cref="RandomExtensions.File(Random, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.File(Random, int, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void File_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.File(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Test(new Random().File(), Path.GetTempPath());

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      Test(new Random().File(currentDirectory), currentDirectory.FullName);

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
      AssertionExtensions.Should(() => RandomExtensions.File(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().File(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().File(0).Should().BeOfType<IEnumerable<FileInfo>>().And.BeEmpty();

      new Random().File(count).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Test(file, Path.GetTempPath()));

      var currentDirectory = System.IO.Directory.GetCurrentDirectory().ToDirectory();
      new Random().File(count, currentDirectory).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Test(file, currentDirectory.FullName));

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
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFile(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

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
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
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
        var file = new Random().BinaryFileAsync(size, ranges, null, CancellationToken.None).Await();

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
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileAsync(null, 0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(-1, 0).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, -1).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileAsync(null, 0, 0).ToArrayAsync()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(-1, 0).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, -1).ToArrayAsync()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFile(-1)).ThrowExactly<ArgumentNullException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

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
  ///     <item><description><see cref="RandomExtensions.TextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFileAsync(Random, int, IEnumerable{Range}, Encoding, DirectoryInfo, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFileAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().TextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().TextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

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
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, IEnumerable{Range})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void PhysicalAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      //new Random().PhysicalAddress(int.MinValue).Should().NotBeNull().And.NotBeSameAs()
      //new Random().PhysicalAddress(0).Should().BeEmpty();

      new Random().Byte(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().Byte(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().Byte(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().Byte(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().Byte(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().Byte(count).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.MemoryStream(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.MemoryStream(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.MemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().MemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.MemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().MemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.MemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.MemoryStreamAsync(Random, int, IEnumerable{Range}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void MemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.MemoryStreamAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().MemoryStreamAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      const int count = 1000;

      new Random().MemoryStreamAsync(0).Await().Length.Should().Be(0);
      new Random().MemoryStreamAsync(0, null, null).Await().Length.Should().Be(0);

      using (var stream = new Random().MemoryStreamAsync(count).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().MemoryStreamAsync(count, 0, 100).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().MemoryStreamAsync(count, 0, 0).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null)).ThrowExactlyAsync<OperationCanceledException>().Await();

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

      AssertionExtensions.Should(() => RandomExtensions.MemoryStreamAsync(null, 0, new[] {1..2})).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();

      new Random().MemoryStreamAsync(0, new[] {Range.All}).Await().Length.Should().Be(0);

      using (var stream = new Random().MemoryStreamAsync(count, new [] {Range.All}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new [] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().MemoryStreamAsync(count, new[] {..100}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new[] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

        Test(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().MemoryStreamAsync(count, new[] {..0}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new[] {Range.All})).ThrowExactlyAsync<OperationCanceledException>().Await();

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
  ///     <item><description><see cref="RandomExtensions.Stream(Random, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Stream(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Stream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Stream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      throw new NotImplementedException();

      static void Test()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Stream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

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