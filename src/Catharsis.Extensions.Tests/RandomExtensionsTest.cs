using System.Security;
using System.Text;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RandomExtensions"/>.</para>
/// </summary>
public sealed class RandomExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Sbyte(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Sbyte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Sbyte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Validate(0, 0, 0, 0);
      Validate(sbyte.MinValue, sbyte.MinValue, sbyte.MinValue, sbyte.MinValue);
      Validate(sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue, sbyte.MaxValue);
      Validate(sbyte.MinValue, sbyte.MaxValue, sbyte.MinValue, sbyte.MaxValue);
      Validate(sbyte.MaxValue, sbyte.MinValue, sbyte.MinValue, sbyte.MaxValue);
      //Validate();

      //new Random().Sbyte(0, 0).Should().Be(0);
      //new Random().Sbyte(sbyte.MinValue, sbyte.MinValue).Should().Be(sbyte.MinValue);
      //new Random().Sbyte(sbyte.MaxValue, sbyte.MaxValue).Should().Be(sbyte.MaxValue);
      //new Random().Sbyte(sbyte.MinValue, sbyte.MaxValue).Should().Be(sbyte.MinValue, sbyte.MaxValue);
      //new Random().Sbyte(sbyte.MaxValue, sbyte.MinValue).Should().Be(sbyte.MinValue, sbyte.MaxValue);
      new Random().Sbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

      static void Validate(sbyte from, sbyte to, sbyte min, sbyte max) => new Random().Sbyte(min, max).Should().BeInRange(from, to);
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

      static void Validate(Range[] ranges, sbyte min, sbyte max) => new Random().Sbyte(ranges).Should().BeInRange(min, max);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.SbyteSequence(Random, int, sbyte?, sbyte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SbyteSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SbyteSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SbyteSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SbyteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().SbyteSequence(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      
      new Random().SbyteSequence(count, 0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().SbyteSequence(count, sbyte.MinValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MinValue);
      new Random().SbyteSequence(count, sbyte.MaxValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().SbyteSequence(count, sbyte.MinValue, sbyte.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().SbyteSequence(count, sbyte.MaxValue, sbyte.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
      new Random().SbyteSequence(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));

      static void Validate(int count, sbyte from, sbyte to)
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SbyteSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SbyteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().SbyteSequence(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().SbyteSequence(0));

      new Random().SbyteSequence(0, new[] { Range.All }).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().SbyteSequence(0, new [] {..int.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().SbyteSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().SbyteSequence(count, new [] {sbyte.MaxValue..sbyte.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
      new Random().SbyteSequence(count, new [] {Range.All}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().SbyteSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().SbyteSequence(count, new[] {..0, ..sbyte.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().SbyteSequence(count, new[] {..0, sbyte.MaxValue..0}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
      new Random().SbyteSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Byte(Random, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Byte(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Byte_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Byte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      
      new Random().Byte(0, 0).Should().Be(0);
      new Random().Byte(byte.MinValue, byte.MinValue).Should().Be(byte.MinValue);
      new Random().Byte(byte.MaxValue, byte.MaxValue).Should().Be(byte.MaxValue);
      new Random().Byte(byte.MinValue, byte.MaxValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().Byte(byte.MaxValue, byte.MinValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
      new Random().Byte().Should().BeInRange(byte.MinValue, byte.MaxValue);

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ByteSequence(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ByteSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ByteSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ByteSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ByteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ByteSequence(0).Should().BeOfType<IEnumerable<byte>>().And.NotBeNull().And.BeEmpty();

      new Random().ByteSequence(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ByteSequence(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ByteSequence(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ByteSequence(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ByteSequence(count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ByteSequence(null, 0, new[] { ..1 })).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ByteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ByteSequence(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeSameAs(new Random().ByteSequence(0));

      new Random().ByteSequence(0, new [] {Range.All}).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().ByteSequence(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().ByteSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ByteSequence(count, new[] {..byte.MinValue}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ByteSequence(count, new[] {byte.MaxValue..byte.MaxValue}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ByteSequence(count, new[] {Range.All}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ByteSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ByteSequence(count, new[] {..0, ..byte.MaxValue}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
      new Random().ByteSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Short(Random, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Short(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Short_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Short(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Short(0, 0).Should().Be(0);
      new Random().Short(short.MinValue, short.MinValue).Should().Be(short.MinValue);
      new Random().Short(short.MaxValue, short.MaxValue).Should().Be(short.MaxValue);
      new Random().Short(short.MinValue, short.MaxValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().Short(short.MaxValue, short.MinValue).Should().BeInRange(short.MinValue, short.MaxValue);
      new Random().Short().Should().BeInRange(short.MinValue, short.MaxValue);

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ShortSequence(Random, int, short?, short?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ShortSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ShortSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ShortSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ShortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ShortSequence(0).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().ShortSequence(count, 0, 0).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ShortSequence(count, short.MinValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MinValue);
      new Random().ShortSequence(count, short.MaxValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().ShortSequence(count, short.MinValue, short.MaxValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().ShortSequence(count, short.MaxValue, short.MinValue).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
      new Random().ShortSequence(count).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ShortSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ShortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().ShortSequence(0).Should().BeOfType<IEnumerable<short>>().And.BeSameAs(new Random().ShortSequence(0));

      new Random().ShortSequence(0, new[] {Range.All}).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();
      new Random().ShortSequence(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<short>>().And.BeEmpty();

      new Random().ShortSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ShortSequence(count, new[] {short.MaxValue..short.MaxValue}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
      new Random().ShortSequence(count, new [] {Range.All}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().ShortSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ShortSequence(count, new[] {..0, ..short.MaxValue}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().ShortSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<short>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Ushort(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Ushort_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Ushort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Ushort(0, 0).Should().Be(0);
      new Random().Ushort(ushort.MinValue, byte.MinValue).Should().Be(ushort.MinValue);
      new Random().Ushort(ushort.MaxValue, ushort.MaxValue).Should().Be(ushort.MaxValue);
      new Random().Ushort(ushort.MinValue, ushort.MaxValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().Ushort(ushort.MaxValue, ushort.MinValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
      new Random().Ushort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.UshortSequence(Random, int, ushort?, ushort?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.UshortSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void UshortSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.UshortSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().UshortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().UshortSequence(0).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().UshortSequence(count, 0, 0).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UshortSequence(count, ushort.MinValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().UshortSequence(count, ushort.MaxValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().UshortSequence(count, ushort.MinValue, ushort.MaxValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().UshortSequence(count, ushort.MaxValue, ushort.MinValue).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
      new Random().UshortSequence(count).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.UshortSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().UshortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().UshortSequence(0).Should().BeOfType<IEnumerable<ushort>>().And.BeSameAs(new Random().UshortSequence(0));

      new Random().UshortSequence(0, new[] {Range.All}).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();
      new Random().UshortSequence(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<ushort>>().And.BeEmpty();

      new Random().UshortSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UshortSequence(count, new[] {..ushort.MinValue}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
      new Random().UshortSequence(count, new[] {ushort.MaxValue..ushort.MaxValue}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
      new Random().UshortSequence(count, new[] {Range.All}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().UshortSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UshortSequence(count, new[] {..0, ..ushort.MaxValue}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
      new Random().UshortSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<ushort>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Int(Random, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Int(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Int_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Int(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Int(0, 0).Should().Be(0);
      new Random().Int(int.MinValue, int.MinValue).Should().Be(int.MinValue);
      new Random().Int(int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
      new Random().Int(int.MinValue, int.MaxValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().Int(int.MaxValue, int.MinValue).Should().BeInRange(int.MinValue, int.MaxValue);
      new Random().Int().Should().BeInRange(int.MinValue, int.MaxValue);

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.IntSequence(Random, int, int?, int?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IntSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IntSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IntSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IntSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().IntSequence(0).Should().BeOfType<IEnumerable<int>>().And.BeEmpty();

      new Random().IntSequence(count, 0, 0).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().IntSequence(count, int.MinValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MinValue);
      new Random().IntSequence(count, int.MaxValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().IntSequence(count, int.MinValue, int.MaxValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().IntSequence(count, int.MaxValue, int.MinValue).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
      new Random().IntSequence(count).Should().BeOfType<IEnumerable<int>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Uint(Random, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Uint(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Uint_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Uint(0, 0).Should().Be(0);
      new Random().Uint(uint.MinValue, uint.MinValue).Should().Be(uint.MinValue);
      new Random().Uint(uint.MaxValue, uint.MaxValue).Should().Be(uint.MaxValue);
      new Random().Uint(uint.MinValue, uint.MaxValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().Uint(uint.MaxValue, uint.MinValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
      new Random().Uint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Uint(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Uint(new[] {Range.All}).Should().Be(0);
      new Random().Uint(new[] {..0}).Should().Be(0);
      new Random().Uint().Should().BeInRange(uint.MinValue, uint.MaxValue);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.UintSequence(Random, int, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.UintSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void UintSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.UintSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().UintSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().UintSequence(0).Should().BeOfType<IEnumerable<uint>>().And.BeEmpty();

      new Random().UintSequence(count, 0, 0).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UintSequence(count, uint.MinValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MinValue);
      new Random().UintSequence(count, uint.MaxValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(uint.MaxValue);
      new Random().UintSequence(count, uint.MinValue, uint.MaxValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().UintSequence(count, uint.MaxValue, uint.MinValue).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
      new Random().UintSequence(count).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.UintSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().UintSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().UintSequence(0, new[] {Range.All}).Should().BeEmpty();

      new Random().UintSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UintSequence(count, new[] {Range.All}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().UintSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().UintSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<uint>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Long(Random, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Long(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Long_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Long(0, 0).Should().Be(0);
      new Random().Long(long.MinValue, long.MinValue).Should().Be(long.MinValue);
      new Random().Long(long.MaxValue, long.MaxValue).Should().Be(long.MaxValue);
      new Random().Long(long.MinValue, long.MaxValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().Long(long.MaxValue, long.MinValue).Should().BeInRange(long.MinValue, long.MaxValue);
      new Random().Long().Should().BeInRange(long.MinValue, long.MaxValue);

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Long(null, new[] {Range.All})).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Long(new [] {Range.All}).Should().Be(0);
      new Random().Long(new[] {..0}).Should().Be(0);
      new Random().Long().Should().BeInRange(long.MinValue, long.MaxValue);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.LongSequence(Random, int, long?, long?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.LongSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void LongSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.LongSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().LongSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().LongSequence(0).Should().BeOfType<IEnumerable<long>>().And.BeEmpty();

      new Random().LongSequence(count, 0, 0).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().LongSequence(count, long.MinValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MinValue);
      new Random().LongSequence(count, long.MaxValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(long.MaxValue);
      new Random().LongSequence(count, long.MinValue, long.MaxValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().LongSequence(count, long.MaxValue, long.MinValue).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
      new Random().LongSequence(count).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.LongSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().LongSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().LongSequence(0, new [] {Range.All}).Should().BeEmpty();
      new Random().LongSequence(0, new[] {..int.MaxValue}).Should().BeEmpty();

      new Random().LongSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().LongSequence(count, new[] {int.MaxValue..int.MaxValue}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
      new Random().LongSequence(count, new[] {Range.All}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);

      new Random().LongSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().LongSequence(count, new[] {..0, ..short.MaxValue}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
      new Random().LongSequence(count, new[] {..0, 1..2}).Should().BeOfType<IEnumerable<long>>().And.HaveCount(count).And.AllBeEquivalentTo(1);

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Float(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Float(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Float().Should().BeInRange(float.MinValue, float.MaxValue);
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FloatSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void FloatSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FloatSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().FloatSequence(0).Should().BeOfType<IEnumerable<float>>().And.BeEmpty();
      new Random().FloatSequence(count).Should().BeOfType<IEnumerable<float>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(float.MinValue, float.MaxValue));
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Double(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Double_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Double(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Double().Should().BeInRange(double.MinValue, double.MaxValue);
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DoubleSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void DoubleSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DoubleSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      const int count = 1000;

      new Random().DoubleSequence(0).Should().BeOfType<IEnumerable<double>>().And.BeEmpty();
      new Random().DoubleSequence(count).Should().BeOfType<IEnumerable<double>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(double.MinValue, double.MaxValue));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Char(Random, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Char(Random, IEnumerable{Range})"/></description></item>
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

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.CharSequence(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.CharSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void CharSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.CharSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().CharSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().CharSequence(0).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().CharSequence(count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().CharSequence(count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().CharSequence(count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().CharSequence(count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().CharSequence(count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
      new Random().CharSequence(count).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.CharSequence(null, 0, new[] {..1})).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().CharSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().CharSequence(0).Should().BeOfType<IEnumerable<char>>().And.BeSameAs(new Random().CharSequence(0));

      new Random().CharSequence(0, new[] {Range.All}).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();
      new Random().CharSequence(0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<char>>().And.BeEmpty();

      new Random().CharSequence(count, new[] {..0}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().CharSequence(count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
      new Random().CharSequence(count, new[] {char.MaxValue..char.MaxValue}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
      new Random().CharSequence(count, new[] {Range.All}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

      new Random().CharSequence(count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
      new Random().CharSequence(count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
      new Random().CharSequence(count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<char>>().And.HaveCount(count).And.AllBeEquivalentTo('a');

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.String(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.String(Random, int, IEnumerable{Range})"/></description></item>
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

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.StringSequence(Random, int, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.StringSequence(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void StringSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.StringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().StringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().StringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().SbyteSequence(0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      
      new Random().StringSequence(0, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();
      new Random().StringSequence(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllBeEquivalentTo(string.Empty);

      new Random().StringSequence(int.MaxValue, 0).Should().BeOfType<IEnumerable<sbyte>>().And.BeEmpty();

      new Random().StringSequence(0, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().StringSequence(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().StringSequence(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().StringSequence(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().StringSequence(size, count, char.MinValue, char.MaxValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().StringSequence(size, count, char.MaxValue, char.MinValue).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
      new Random().StringSequence(size, count).Should().BeOfType<IEnumerable<sbyte>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.StringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().StringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().StringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().StringSequence(0, 0).Should().BeOfType<IEnumerable<string>>().And.BeSameAs(new Random().StringSequence(0, 0));

      new Random().StringSequence(size, 0, new[] {Range.All}).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().StringSequence(size, 0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().StringSequence(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().StringSequence(size, count, new[] {..0}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().StringSequence(size, count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().StringSequence(size, count, new[] {char.MaxValue..char.MaxValue}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().StringSequence(size, count, new[] {Range.All}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().StringSequence(size, count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().StringSequence(size, count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().StringSequence(size, count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Digits(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Digits_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Digits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Digits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Digits(0).Should().BeOfType<string>().And.BeSameAs(new Random().Digits(0)).And.BeEmpty();
      new Random().Digits(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[0-9]+$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DigitsSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void DigitsSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DigitsSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().DigitsSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().DigitsSequence(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().DigitsSequence(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().DigitsSequence(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[0-9]+$"));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Letters(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Letters_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Letters(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Letters(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().Letters(0).Should().BeOfType<string>().And.BeSameAs(new Random().Letters(0)).And.BeEmpty();
      new Random().Letters(count).Should().BeOfType<string>().And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z]+$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LettersSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void LettersSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.LettersSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().LettersSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().LettersSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().LettersSequence(int.MaxValue, 0).Should().BeEmpty();

      new Random().LettersSequence(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().LettersSequence(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z]+$"));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.AlphaDigits(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void AlphaDigits_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.AlphaDigits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().AlphaDigits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().AlphaDigits(0).Should().BeOfType<string>().And.BeSameAs(new Random().AlphaDigits(0)).And.BeEmpty();
      new Random().AlphaDigits(count).Should().BeOfType<string>().And.NotBeSameAs(new Random().AlphaDigits(count)).And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z0-9]+$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.AlphaDigitsSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void AlphaDigitsSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.AlphaDigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().AlphaDigitsSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().AlphaDigitsSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().AlphaDigitsSequence(int.MaxValue, 0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().AlphaDigitsSequence(0, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

      new Random().AlphaDigitsSequence(size, count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z0-9]+$"));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.SecureString(Random, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SecureString(Random, int, IEnumerable{Range})"/></description></item>
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

      static void Validate()
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

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.SecureStringSequence(Random, int, int, char?, char?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.SecureStringSequence(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void SecureStringSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SecureStringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureStringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().SecureStringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().SecureStringSequence(int.MaxValue, 0).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();

      new Random().SecureStringSequence(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().SecureStringSequence(size, count, (char) 0, (char) 0).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureStringSequence(size, count, char.MinValue, char.MinValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().SecureStringSequence(size, count, char.MaxValue, char.MaxValue).Should().BeOfType<IEnumerable<SecureString>>().And.NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().SecureStringSequence(size, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.SecureStringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().SecureStringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().SecureStringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int size = 10;
      const int count = 1000;

      new Random().SecureStringSequence(size, 0, new[] {Range.All}).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();
      new Random().SecureStringSequence(size, 0, new[] {..int.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.BeEmpty();
      
      new Random().SecureStringSequence(0, count).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

      new Random().SecureStringSequence(size, count, new[] {..0}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureStringSequence(size, count, new[] {..char.MinValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
      new Random().SecureStringSequence(size, count, new[] {char.MaxValue..char.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
      new Random().SecureStringSequence(size, count, new[] {Range.All}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

      new Random().SecureStringSequence(size, count, new[] {..0, Range.All}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
      new Random().SecureStringSequence(size, count, new[] {..0, ..char.MaxValue}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
      new Random().SecureStringSequence(size, count, new[] {..0, 'a'..'b'}).Should().BeOfType<IEnumerable<SecureString>>().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeSequence(Random, int, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTimeSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateTimeSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateTimeSequence(0).Should().BeEmpty();
      new Random().DateTimeSequence(count, DateTime.MinValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MinValue);
      new Random().DateTimeSequence(count, DateTime.MaxValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MaxValue);
      new Random().DateTimeSequence(count, DateTime.MinValue, DateTime.MaxValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().DateTimeSequence(count, DateTime.MaxValue, DateTime.MinValue).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
      new Random().DateTimeSequence(count).Should().BeOfType<IEnumerable<DateTime>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeOffset(Random, DateTimeOffset?, DateTimeOffset?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffset_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTimeOffset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue));
      new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue));
      new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
      new Random().DateTimeOffset().Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeOffsetSequence(Random, int, DateTimeOffset?, DateTimeOffset?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeOffsetSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateTimeOffsetSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateTimeOffsetSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateTimeOffsetSequence(0).Should().BeEmpty();
      new Random().DateTimeOffsetSequence(count, DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MinValue);
      new Random().DateTimeOffsetSequence(count, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MaxValue);
      new Random().DateTimeOffsetSequence(count, DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().DateTimeOffsetSequence(count, DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
      new Random().DateTimeOffsetSequence(count).Should().BeOfType<IEnumerable<DateTimeOffset>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateOnly(Random, DateOnly?, DateOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DateOnly(DateOnly.MinValue, DateOnly.MinValue).Should().Be(DateOnly.MinValue);
      new Random().DateOnly(DateOnly.MaxValue, DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
      new Random().DateOnly(DateOnly.MinValue, DateOnly.MaxValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().DateOnly(DateOnly.MaxValue, DateOnly.MinValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
      new Random().DateOnly().Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateOnlySequence(Random, int, DateOnly?, DateOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateOnlySequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DateOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DateOnlySequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DateOnlySequence(0).Should().BeEmpty();

      new Random().DateOnlySequence(count, DateOnly.MinValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MinValue);
      new Random().DateOnlySequence(count, DateOnly.MaxValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MaxValue);
      new Random().DateOnlySequence(count, DateOnly.MinValue, DateOnly.MaxValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().DateOnlySequence(count, DateOnly.MaxValue, DateOnly.MinValue).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
      new Random().DateOnlySequence(count).Should().BeOfType<IEnumerable<DateOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TimeOnly(Random, TimeOnly?, TimeOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnly_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
      new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
      new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MinValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
      new Random().TimeOnly().Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TimeOnlySequence(Random, int, TimeOnly?, TimeOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void TimeOnlySequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TimeOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TimeOnlySequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().TimeOnlySequence(0).Should().BeEmpty();
      new Random().TimeOnlySequence(count, TimeOnly.MinValue, TimeOnly.MinValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MinValue);
      new Random().TimeOnlySequence(count, TimeOnly.MaxValue, TimeOnly.MaxValue).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MaxValue);
      new Random().TimeOnlySequence(count).Should().BeOfType<IEnumerable<TimeOnly>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue));
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Range(Random, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Range_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Range(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Range(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

      new int?[] { null, 0, int.MaxValue }.ForEach(max =>
      {
        var range = new Random().Range(max);
        range.Should().BeOfType<Range>();
        range.Start.IsFromEnd.Should().BeFalse();
        range.Start.Value.Should().Be(0);
        range.End.IsFromEnd.Should().BeFalse();
        range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
      });
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.RangeSequence(Random, int, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void RangeSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.RangeSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().RangeSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().RangeSequence(0).Should().BeOfType<IEnumerable<Range>>().And.BeEmpty();
      new Random().RangeSequence(count, 0).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Validate(element, 0));
      new Random().RangeSequence(count).Should().BeOfType<IEnumerable<Range>>().And.HaveCount(count).And.AllSatisfy(element => Validate(element, null));
    }

    return;

    static void Validate(Range range, int? max)
    {
      range.Start.IsFromEnd.Should().BeFalse();
      range.Start.Value.Should().Be(0);
      range.End.IsFromEnd.Should().BeFalse();
      range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Guid(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Guid_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Guid(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().Guid().Should().NotBe(Guid.Empty);
      new Random().Guid().Should().NotBe(Guid.NewGuid());
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.GuidSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void GuidSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.GuidSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().GuidSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().GuidSequence(0).Should().BeOfType<IEnumerable<Guid>>().And.BeEmpty();
      new Random().GuidSequence(count).Should().BeOfType<IEnumerable<Guid>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBe(Guid.Empty).And.NotBe(Guid.NewGuid()));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Object(Random, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Object(Random, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Object_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object(null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.ObjectSequence(Random, int, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.ObjectSequence(Random, int, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ObjectSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ObjectSequence(null, 0, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ObjectSequence(0, (IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ObjectSequence(-1, Enumerable.Empty<Type>())).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ObjectSequence(null, 0, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ObjectSequence(0, null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ObjectSequence(-1, [])).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileName_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().FileName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileNameSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileNameSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileNameSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().FileNameSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().FileNameSequence(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().FileNameSequence(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$"));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryName_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      new Random().DirectoryName().Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryNameSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryNameSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryNameSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DirectoryNameSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DirectoryNameSequence(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().DirectoryNameSequence(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$"));
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FilePath(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FilePath_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FilePath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().FilePath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().FilePath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FilePathSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FilePathSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FilePathSequence(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().FilePathSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().FilePathSequence(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();
      new Random().FilePathSequence(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Validate(path, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().FilePathSequence(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Validate(path, currentDirectory.FullName));
    }

    return;

    static void Validate(string path, string directory)
    {
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(directory, file));
      file.Should().BeOfType<string>().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryPath(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryPath_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      var path = new Random().DirectoryPath();
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(Path.GetTempPath(), file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      path = new Random().DirectoryPath(currentDirectory);
      file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
    }

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryPathSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryPathSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryPathSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DirectoryPathSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 1000;

      new Random().DirectoryPathSequence(0).Should().BeOfType<IEnumerable<string>>().And.BeEmpty();

      new Random().DirectoryPathSequence(count).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Validate(Path.GetTempPath(), path));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().DirectoryPathSequence(count, currentDirectory).Should().BeOfType<IEnumerable<string>>().And.HaveCount(count).And.AllSatisfy(path => Validate(currentDirectory.FullName, path));
    }

    return;

    static void Validate(string directory, string path)
    {
      var file = Path.GetFileName(path);
      path.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(directory, file));
      file.Should().BeOfType<string>().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Directory(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Directory_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Directory(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Validate(new Random().Directory(), Path.GetTempPath());

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Validate(currentDirectory, currentDirectory.FullName);
    }

    return;

    static void Validate(DirectoryInfo directory, string path)
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

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectorySequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectorySequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectorySequence(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().DirectorySequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().DirectorySequence(0).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.BeEmpty();

      new Random().DirectorySequence(count).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().DirectorySequence(count, currentDirectory).Should().BeOfType<IEnumerable<DirectoryInfo>>().And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, currentDirectory.FullName));
    }

    return;

    static void Validate(DirectoryInfo directory, string path)
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

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.File(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void File_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.File(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      Validate(new Random().File(), Path.GetTempPath());

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Validate(new Random().File(currentDirectory), currentDirectory.FullName);
    }

    return;

    static void Validate(FileInfo file, string path)
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

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileSequence(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().FileSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      const int count = 10;

      new Random().FileSequence(0).Should().BeOfType<IEnumerable<FileInfo>>().And.BeEmpty();

      new Random().FileSequence(count).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Validate(file, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().FileSequence(count, currentDirectory).Should().BeOfType<IEnumerable<FileInfo>>().And.HaveCount(count).And.AllSatisfy(file => Validate(file, currentDirectory.FullName));
    }

    return;

    static void Validate(FileInfo file, string path)
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

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFile(Random, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileAsync(Random, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
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

      Validate(new Random().BinaryFile(int.MinValue), tempPath

                                     //int.MinValue, null);


      Validate( int.MinValue);
      Validate(0);
      Validate(size);
      Validate(size, 0, 0);
      Validate(size, byte.MinValue, byte.MinValue);
      Validate(size, byte.MaxValue, byte.MaxValue);*/

      static void Validate(FileInfo file, string path, int size, byte? min, byte? max)
      {
        //AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, min, max, path.ToDirectory(), Attributes.CancellationToken())).ThrowExactlyAsync<TaskCanceledException>().Await();

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
      //AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      const int size = 4096;

      Validate(0);
      Validate(size);

      //Validate(size, Range.All);
      //Validate(size, ..int.MaxValue);
      Validate(size, ..0);
      Validate(size, ..byte.MinValue);
      Validate(size, byte.MaxValue..byte.MaxValue);

      //Validate(size, ..0, Range.All);
      //Validate(size, ..0, ..byte.MaxValue);
      Validate(size, ..0, 1..2);

      static void Validate(int size, params Range[] ranges)
      {
        var file = new Random().BinaryFileAsync(size, ranges, null, default).Await();

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
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.BinaryFileSequence(Random, int, int, byte?, byte?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileSequence(Random, int, int, IEnumerable{Range}, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFileSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().BinaryFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().BinaryFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.BinaryFileSequenceAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.BinaryFileSequenceAsync(Random, int, int, IEnumerable{Range}, DirectoryInfo, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void BinaryFileSequenceAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFile(Random, int, Encoding, DirectoryInfo, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFile_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFile(-1)).ThrowExactly<ArgumentNullException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      static void Validate()
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().TextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TextFileSequence(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFileSequence(Random, int, int, IEnumerable{Range}, Encoding, DirectoryInfo)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFileSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().TextFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().TextFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().TextFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.TextFileSequenceAsync(Random, int, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.TextFileSequenceAsync(Random, int, int, Encoding, DirectoryInfo, CancellationToken, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void TextFileSequenceAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6Address(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpV6Address_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpV6Address(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6AddressSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpV6AddressSequence_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpV6AddressSequence(null, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpV6AddressSequence(-1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.PhysicalAddress(Random, int, IEnumerable{Range})"/></description></item>
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

      new Random().ByteSequence(count, 0, 0).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(0);
      new Random().ByteSequence(count, byte.MinValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
      new Random().ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
      new Random().ByteSequence(count, byte.MinValue, byte.MaxValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ByteSequence(count, byte.MaxValue, byte.MinValue).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      new Random().ByteSequence(count).Should().BeOfType<IEnumerable<byte>>().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

      const int count = 1000;

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.PhysicalAddressSequence(Random, int, int, byte?, byte?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.PhysicalAddressSequence(Random, int, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void PhysicalAddressSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
      AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.MemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().MemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
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
      new Random().MemoryStreamAsync(0, null, null, Attributes.CancellationToken()).Await().Length.Should().Be(0);

      using (var stream = new Random().MemoryStreamAsync(count).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().MemoryStreamAsync(count, 0, 100).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().MemoryStreamAsync(count, 0, 0).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }

      static void Validate(MemoryStream stream, int count)
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
      new Random().MemoryStreamAsync(0, new[] {Range.All}, Attributes.CancellationToken()).Await().Length.Should().Be(0);

      using (var stream = new Random().MemoryStreamAsync(count, new [] {Range.All}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new [] {Range.All}, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().MemoryStreamAsync(count, new[] {..100}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new[] {Range.All}, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().MemoryStreamAsync(count, new[] {..0}).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamAsync(count, new[] {Range.All}, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }

      static void Validate(MemoryStream stream, int count)
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Stream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      static void Validate()
      {
      }
    }
  }


  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpAddress(Random, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IpAddress_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

      static void Validate()
      {
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.IpAddressSequence(Random, int, uint?, uint?)"/></description></item>
  ///     <item><description><see cref="RandomExtensions.IpAddressSequence(Random, int, IEnumerable{Range})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IpAddressSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpAddressSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().IpAddressSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

      static void Validate()
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

    return;

    static void Validate()
    {
    }
  }
}