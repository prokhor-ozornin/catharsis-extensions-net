using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="RandomExtensions"/>.</para>
/// </summary>
public sealed class RandomExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Sbyte(Random, sbyte?, sbyte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Sbyte_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Sbyte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    Randomizer.Sbyte(0, 0).Should().Be(0);
    Randomizer.Sbyte(sbyte.MinValue, sbyte.MinValue).Should().Be(sbyte.MinValue);
    Randomizer.Sbyte(sbyte.MaxValue, sbyte.MaxValue).Should().Be(sbyte.MaxValue);
    Randomizer.Sbyte(sbyte.MinValue, sbyte.MaxValue).Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);
    Randomizer.Sbyte(sbyte.MaxValue, sbyte.MinValue).Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SbyteInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => Randomizer.SbyteInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("minValue");

    Randomizer.SbyteInRange(..0).Should().Be(0);
    Randomizer.SbyteInRange(Range.All).Should().Be(0);
    Randomizer.SbyteInRange(sbyte.MaxValue..sbyte.MaxValue).Should().Be(sbyte.MaxValue);
    Randomizer.SbyteInRange(..sbyte.MaxValue).Should().BeInRange(0, sbyte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteSequence(Random, int, sbyte?, sbyte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SbyteSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SbyteSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.SbyteSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.SbyteSequence(int.MinValue)).And.BeEmpty();
    Randomizer.SbyteSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.SbyteSequence(0)).And.BeEmpty();
    
    Randomizer.SbyteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.SbyteSequence(count, sbyte.MinValue, sbyte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MinValue);
    Randomizer.SbyteSequence(count, sbyte.MaxValue, sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
    Randomizer.SbyteSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.SbyteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SbyteSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SbyteSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.SbyteSequenceInRange(0).Should().BeSameAs(Randomizer.SbyteSequenceInRange(0));
    Randomizer.SbyteSequenceInRange(count).Should().NotBeSameAs(Randomizer.SbyteSequenceInRange(count));

    Randomizer.SbyteSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.SbyteSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.SbyteSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();
    
    Randomizer.SbyteSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.SbyteSequenceInRange(count, sbyte.MaxValue..sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
    Randomizer.SbyteSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.SbyteSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.SbyteSequenceInRange(count, ..0, ..sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
    Randomizer.SbyteSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Byte(Random, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Byte_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Byte(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Byte(1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Byte(0, 0).Should().Be(0);
    Randomizer.Byte(byte.MinValue, byte.MinValue).Should().Be(byte.MinValue);
    Randomizer.Byte(byte.MaxValue, byte.MaxValue).Should().Be(byte.MaxValue);
    Randomizer.Byte().Should().BeInRange(byte.MinValue, byte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ByteInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ByteInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.ByteInRange(..0).Should().Be(0);
    Randomizer.ByteInRange(Range.All).Should().Be(0);
    Randomizer.ByteInRange(..byte.MinValue).Should().Be(byte.MinValue);
    Randomizer.ByteInRange(byte.MaxValue..byte.MaxValue).Should().Be(byte.MaxValue);
    Randomizer.ByteInRange(..byte.MaxValue).Should().BeInRange(0, byte.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteSequence(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ByteSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ByteSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.ByteSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.ByteSequence(int.MinValue)).And.BeEmpty();
    Randomizer.ByteSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.ByteSequence(0)).And.BeEmpty();

    Randomizer.ByteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ByteSequence(count, byte.MinValue, byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    Randomizer.ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    Randomizer.ByteSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.ByteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ByteSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ByteSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.ByteSequenceInRange(0).Should().BeSameAs(Randomizer.ByteSequenceInRange(0));
    Randomizer.ByteSequenceInRange(count).Should().NotBeSameAs(Randomizer.ByteSequenceInRange(count));

    Randomizer.ByteSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.ByteSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.ByteSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.ByteSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ByteSequenceInRange(count, ..byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    Randomizer.ByteSequenceInRange(count, byte.MaxValue..byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    Randomizer.ByteSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.ByteSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ByteSequenceInRange(count, ..0, ..byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
    Randomizer.ByteSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Short(Random, short?, short?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Short_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Short(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Short(1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Short(0, 0).Should().Be(0);
    Randomizer.Short(short.MinValue, short.MinValue).Should().Be(short.MinValue);
    Randomizer.Short(short.MaxValue, short.MaxValue).Should().Be(short.MaxValue);
    Randomizer.Short().Should().BeInRange(short.MinValue, short.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ShortInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ShortInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.ShortInRange(..0).Should().Be(0);
    Randomizer.ShortInRange(Range.All).Should().Be(0);
    Randomizer.ShortInRange(short.MaxValue..short.MaxValue).Should().Be(short.MaxValue);
    Randomizer.ShortInRange(..short.MaxValue).Should().BeInRange(0, short.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortSequence(Random, int, short?, short?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ShortSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ShortSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.ShortSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.ShortSequence(int.MinValue)).And.BeEmpty();
    Randomizer.ShortSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.ShortSequence(0)).And.BeEmpty();

    Randomizer.ShortSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ShortSequence(count, short.MinValue, short.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MinValue);
    Randomizer.ShortSequence(count, short.MaxValue, short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
    Randomizer.ShortSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.ShortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_ShortSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.ShortSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.ShortSequenceInRange(0).Should().BeSameAs(Randomizer.ShortSequenceInRange(0));
    Randomizer.ShortSequenceInRange(count).Should().NotBeSameAs(Randomizer.ShortSequenceInRange(count));

    Randomizer.ShortSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.ShortSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.ShortSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.ShortSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ShortSequenceInRange(count, short.MaxValue..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
    Randomizer.ShortSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.ShortSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ShortSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    Randomizer.ShortSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Ushort(Random, ushort?, ushort?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Ushort_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Ushort(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Ushort(1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Ushort(0, 0).Should().Be(0);
    Randomizer.Ushort(ushort.MinValue, ushort.MinValue).Should().Be(ushort.MinValue);
    Randomizer.Ushort(ushort.MaxValue, ushort.MaxValue).Should().Be(ushort.MaxValue);
    Randomizer.Ushort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_UshortInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.UshortInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.UshortInRange(..0).Should().Be(0);
    Randomizer.UshortInRange(Range.All).Should().Be(0);
    Randomizer.UshortInRange(..ushort.MinValue).Should().Be(ushort.MinValue);
    Randomizer.UshortInRange(ushort.MaxValue..ushort.MaxValue).Should().Be(ushort.MaxValue);
    Randomizer.UshortInRange(..ushort.MaxValue).Should().BeInRange(0, ushort.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortSequence(Random, int, ushort?, ushort?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_UshortSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.UshortSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.UshortSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.UshortSequence(int.MinValue)).And.BeEmpty();
    Randomizer.UshortSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.UshortSequence(0)).And.BeEmpty();

    Randomizer.UshortSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.UshortSequence(count, ushort.MinValue, ushort.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
    Randomizer.UshortSequence(count, ushort.MaxValue, ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
    Randomizer.UshortSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.UshortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_UshortSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.UshortSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.UshortSequenceInRange(0).Should().BeSameAs(Randomizer.UshortSequenceInRange(0));
    Randomizer.UshortSequenceInRange(count).Should().NotBeSameAs(Randomizer.UshortSequenceInRange(count));

    Randomizer.UshortSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.UshortSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.UshortSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.UshortSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.UshortSequenceInRange(count, ..ushort.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
    Randomizer.UshortSequenceInRange(count, ushort.MaxValue..ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
    Randomizer.UshortSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.UshortSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.UshortSequenceInRange(count, ..0, ..ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
    Randomizer.UshortSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Int(Random, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Int_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Int(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Int(1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Int(0, 0).Should().Be(0);
    Randomizer.Int(int.MinValue, int.MinValue).Should().Be(int.MinValue);
    Randomizer.Int(int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
    Randomizer.Int().Should().BeInRange(int.MinValue, int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IntInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.IntInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.IntInRange(..0).Should().Be(0);
    Randomizer.IntInRange(Range.All).Should().Be(0);
    Randomizer.IntInRange(int.MaxValue..int.MaxValue).Should().Be(int.MaxValue);
    Randomizer.IntInRange(..int.MaxValue).Should().BeInRange(0, int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntSequence(Random, int, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IntSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.IntSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.IntSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.IntSequence(int.MinValue)).And.BeEmpty();
    Randomizer.IntSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.IntSequence(0)).And.BeEmpty();

    Randomizer.IntSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.IntSequence(count, int.MinValue, int.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MinValue);
    Randomizer.IntSequence(count, int.MaxValue, int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    Randomizer.IntSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.IntSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IntSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.IntSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.IntSequenceInRange(0).Should().BeSameAs(Randomizer.IntSequenceInRange(0));
    Randomizer.IntSequenceInRange(count).Should().NotBeSameAs(Randomizer.IntSequenceInRange(count));

    Randomizer.IntSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.IntSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.IntSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.IntSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.IntSequenceInRange(count, int.MaxValue..int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    Randomizer.IntSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.IntSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.IntSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    Randomizer.IntSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Long(Random, long?, long?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Long_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Long(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Long(1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Long(0, 0).Should().Be(0);
    Randomizer.Long(long.MinValue, long.MinValue).Should().Be(long.MinValue);
    Randomizer.Long(long.MaxValue, long.MaxValue).Should().Be(long.MaxValue);
    Randomizer.Long().Should().BeInRange(long.MinValue, long.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_LongInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.LongInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.LongInRange(..0).Should().Be(0);
    Randomizer.LongInRange(Range.All).Should().Be(0);
    Randomizer.LongInRange(int.MaxValue..int.MaxValue).Should().Be(int.MaxValue);
    Randomizer.LongInRange(..int.MaxValue).Should().BeInRange(0, int.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongSequence(Random, int, long?, long?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_LongSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.LongSequence(1, 1, 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.LongSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.LongSequence(int.MinValue)).And.BeEmpty();
    Randomizer.LongSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.LongSequence(0)).And.BeEmpty();

    Randomizer.LongSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.LongSequence(count, long.MinValue, long.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(long.MinValue);
    Randomizer.LongSequence(count, long.MaxValue, long.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(long.MaxValue);
    Randomizer.LongSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.LongSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_LongSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.LongSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.LongSequenceInRange(0).Should().BeSameAs(Randomizer.LongSequenceInRange(0));
    Randomizer.LongSequenceInRange(count).Should().NotBeSameAs(Randomizer.LongSequenceInRange(count));

    Randomizer.LongSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.LongSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.LongSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.LongSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.LongSequenceInRange(count, int.MaxValue..int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    Randomizer.LongSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    Randomizer.LongSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.LongSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    Randomizer.LongSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Float(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Float_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Float(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.Float().Should().BeInRange(float.MinValue, float.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FloatSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FloatSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.FloatSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.FloatSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.FloatSequence(int.MinValue)).And.BeEmpty();
    Randomizer.FloatSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.FloatSequence(0)).And.BeEmpty();

    Randomizer.FloatSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.FloatSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(float.MinValue, float.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Double(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Double_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Double(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.Double().Should().BeInRange(double.MinValue, double.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DoubleSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DoubleSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DoubleSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.DoubleSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.DoubleSequence(int.MinValue)).And.BeEmpty();
    Randomizer.DoubleSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.DoubleSequence(0)).And.BeEmpty();

    Randomizer.DoubleSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.DoubleSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(double.MinValue, double.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Char(Random, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Char_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Char(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Char((char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.Char((char) 0, (char) 0).Should().Be((char) 0);
    Randomizer.Char(char.MinValue, char.MinValue).Should().Be(char.MinValue);
    Randomizer.Char(char.MaxValue, char.MaxValue).Should().Be(char.MaxValue);
    Randomizer.Char().Should().BeInRange(char.MinValue, char.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_CharInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharInRange(null, Range.All)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.CharInRange(1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.CharInRange(..0).Should().Be((char) 0);
    Randomizer.CharInRange(Range.All).Should().Be((char) 0);
    Randomizer.CharInRange(..char.MinValue).Should().Be(char.MinValue);
    Randomizer.CharInRange(char.MaxValue..char.MaxValue).Should().Be(char.MaxValue);
    Randomizer.CharInRange(..char.MaxValue).Should().BeInRange((char) 0, char.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharSequence(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_CharSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharSequence(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.CharSequence(1, (char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.CharSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.CharSequence(int.MinValue)).And.BeEmpty();
    Randomizer.CharSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.CharSequence(0)).And.BeEmpty();

    Randomizer.CharSequence(count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.CharSequence(count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.CharSequence(count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.CharSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.CharSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_CharSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.CharSequenceInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.CharSequenceInRange(0).Should().BeSameAs(Randomizer.CharSequenceInRange(0));
    Randomizer.CharSequenceInRange(count).Should().NotBeSameAs(Randomizer.CharSequenceInRange(count));

    Randomizer.CharSequenceInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.CharSequenceInRange(0, Range.All).Should().BeEmpty();
    Randomizer.CharSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.CharSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.CharSequenceInRange(count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.CharSequenceInRange(count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.CharSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

    Randomizer.CharSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.CharSequenceInRange(count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    Randomizer.CharSequenceInRange(count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo('a');
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.String(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_String_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.String(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.String(1, (char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.String(0).Should().BeSameAs(Randomizer.String(0));
    Randomizer.String(count).Should().NotBeSameAs(Randomizer.String(count));

    Randomizer.String(int.MinValue).Should().BeEmpty();
    Randomizer.String(0).Should().BeEmpty();

    Randomizer.String(count, (char) 0, (char) 0).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.String(count, char.MinValue, char.MinValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.String(count, char.MaxValue, char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.String(count).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_StringInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.StringInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.StringInRange(0).Should().BeSameAs(Randomizer.StringInRange(0));
    Randomizer.StringInRange(count).Should().NotBeSameAs(Randomizer.StringInRange(count));

    Randomizer.StringInRange(int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.StringInRange(0, Range.All).Should().BeEmpty();
    Randomizer.StringInRange(0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.StringInRange(count, ..0).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.StringInRange(count, ..char.MinValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.StringInRange(count, char.MaxValue..char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.StringInRange(count, Range.All).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);

    Randomizer.StringInRange(count, ..0, Range.All).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.StringInRange(count, ..0, ..char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    Randomizer.StringInRange(count, ..0, 'a'..'b').ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo('a');
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringSequence(Random, int, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_StringSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.StringSequence(0, 0, (char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int size = 10;
    const int count = 1000;


    Randomizer.SbyteSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.SbyteSequence(int.MinValue)).And.BeEmpty();
    Randomizer.SbyteSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.SbyteSequence(0)).And.BeEmpty();


    Randomizer.StringSequence(0, 0).Should().NotBeNull().And.NotBeSameAs(Randomizer.StringSequence(0, 0)).And.BeEmpty();
    Randomizer.StringSequence(0, count).Should().NotBeNull().And.NotBeSameAs(Randomizer.StringSequence(0, count)).And.BeEmpty();

    Randomizer.StringSequence(int.MaxValue, int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.StringSequence(int.MaxValue, int.MinValue)).And.BeEmpty();
    Randomizer.StringSequence(int.MaxValue, 0).Should().NotBeNull().And.NotBeSameAs(Randomizer.StringSequence(int.MaxValue, 0)).And.BeEmpty();

    Randomizer.StringSequence(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());
    Randomizer.StringSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    Randomizer.StringSequence(size, count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.StringSequence(size, count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    Randomizer.StringSequence(size, count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    Randomizer.StringSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue)));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_StringSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.StringSequenceInRange(0, 0, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.StringSequenceInRange(0, 0).Should().BeSameAs(Randomizer.StringSequenceInRange(0, 0));
    Randomizer.StringSequenceInRange(0, count).Should().NotBeSameAs(Randomizer.StringSequenceInRange(0, count));

    Randomizer.StringSequenceInRange(size, int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.StringSequenceInRange(size, 0, Range.All).Should().BeEmpty();
    Randomizer.StringSequenceInRange(size, 0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.StringSequenceInRange(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());
    Randomizer.StringSequenceInRange(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    Randomizer.StringSequenceInRange(size, count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.StringSequenceInRange(size, count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    Randomizer.StringSequenceInRange(size, count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    Randomizer.StringSequenceInRange(size, count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

    Randomizer.StringSequenceInRange(size, count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.StringSequenceInRange(size, count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue)));
    Randomizer.StringSequenceInRange(size, count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Digits(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Digits_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Digits(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.Digits(0).Should().BeSameAs(Randomizer.Digits(0));
    Randomizer.Digits(count).Should().NotBeSameAs(Randomizer.Digits(count));

    Randomizer.Digits(int.MinValue).Should().BeEmpty();
    Randomizer.Digits(0).Should().BeEmpty();

    Randomizer.Digits(count).Should().HaveLength(count).And.MatchRegex(@"^[0-9]+$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DigitsSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DigitsSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.DigitsSequence(0, 0).Should().NotBeSameAs(Randomizer.DigitsSequence(0, 0));
    Randomizer.DigitsSequence(0, count).Should().NotBeSameAs(Randomizer.DigitsSequence(0, count));

    Randomizer.DigitsSequence(int.MaxValue, int.MinValue).Should().BeEmpty();
    Randomizer.DigitsSequence(int.MaxValue, 0).Should().BeEmpty();

    Randomizer.DigitsSequence(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());
    Randomizer.DigitsSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    Randomizer.DigitsSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[0-9]+$"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Letters(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Letters_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Letters(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.Letters(0).Should().BeSameAs(Randomizer.Letters(0));
    Randomizer.Letters(count).Should().NotBeSameAs(Randomizer.Letters(count));

    Randomizer.Letters(int.MinValue).Should().BeEmpty();
    Randomizer.Letters(0).Should().BeEmpty();

    Randomizer.Letters(count).Should().HaveLength(count).And.MatchRegex(@"^[a-zA-Z]+$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LettersSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_LettersSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LettersSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.LettersSequence(0, 0).Should().NotBeSameAs(Randomizer.LettersSequence(0, 0));
    Randomizer.LettersSequence(0, count).Should().NotBeSameAs(Randomizer.LettersSequence(0, count));

    Randomizer.LettersSequence(int.MaxValue, int.MinValue).Should().BeEmpty();
    Randomizer.LettersSequence(int.MaxValue, 0).Should().BeEmpty();

    Randomizer.LettersSequence(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());
    Randomizer.LettersSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    Randomizer.LettersSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z]+$"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.AlphaDigits(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_AlphaDigits_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.AlphaDigits(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.AlphaDigits(0).Should().BeSameAs(Randomizer.AlphaDigits(0));
    Randomizer.AlphaDigits(count).Should().NotBeSameAs(Randomizer.AlphaDigits(count));

    Randomizer.AlphaDigits(int.MinValue).Should().BeEmpty();
    Randomizer.AlphaDigits(0).Should().BeEmpty();

    Randomizer.AlphaDigits(count).Should().HaveLength(count).And.MatchRegex(@"^[a-zA-Z0-9]+$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.AlphaDigitsSequence(Random, int, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_AlphaDigitsSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.AlphaDigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.AlphaDigitsSequence(0, 0).Should().NotBeSameAs(Randomizer.AlphaDigitsSequence(0, 0));
    Randomizer.AlphaDigitsSequence(0, count).Should().NotBeSameAs(Randomizer.AlphaDigitsSequence(0, count));

    Randomizer.AlphaDigitsSequence(int.MaxValue, int.MinValue).Should().BeEmpty();
    Randomizer.AlphaDigitsSequence(int.MaxValue, 0).Should().BeEmpty();

    Randomizer.AlphaDigitsSequence(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());
    Randomizer.AlphaDigitsSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    Randomizer.AlphaDigitsSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z0-9]+$"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureString(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SecureString_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SecureString(1, (char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int count = 1000;

    Randomizer.SecureString(0).Should().NotBeSameAs(Randomizer.String(0));
    Randomizer.SecureString(count).Should().NotBeSameAs(Randomizer.SecureString(count));

    Randomizer.SecureString(int.MinValue).Length.Should().Be(0);
    Randomizer.SecureString(0).Length.Should().Be(0);

    Randomizer.SecureString(count, (char) 0, (char) 0).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.SecureString(count, char.MinValue, char.MinValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.SecureString(count, char.MaxValue, char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.SecureString(count).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SecureStringInRange_Method()
  {
    const int count = 1000;

    AssertionExtensions.Should(() => RandomExtensions.SecureStringInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SecureStringInRange(1, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    Randomizer.SecureStringInRange(0).Should().NotBeSameAs(Randomizer.SecureStringInRange(0));
    Randomizer.SecureStringInRange(count).Should().NotBeSameAs(Randomizer.SecureStringInRange(count));

    Randomizer.SecureStringInRange(int.MinValue, Range.All).Length.Should().Be(0);
    Randomizer.SecureStringInRange(0, Range.All).Length.Should().Be(0);
    Randomizer.SecureStringInRange(0, ..int.MaxValue).Length.Should().Be(0);

    Randomizer.SecureStringInRange(count, ..0).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.SecureStringInRange(count, ..char.MinValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    Randomizer.SecureStringInRange(count, char.MaxValue..char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    Randomizer.SecureStringInRange(count, Range.All).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);

    Randomizer.SecureStringInRange(count, ..0, Range.All).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    Randomizer.SecureStringInRange(count, ..0, ..char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    Randomizer.SecureStringInRange(count, ..0, 'a'..'b').ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo('a');
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringSequence(Random, int, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SecureStringSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureStringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SecureStringSequence(0, 0, (char) 1, (char) 0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.SecureStringSequence(0, 0).Should().NotBeSameAs(Randomizer.SecureStringSequence(0, 0));
    Randomizer.SecureStringSequence(0, count).Should().NotBeSameAs(Randomizer.SecureStringSequence(0, count));

    Randomizer.SecureStringSequence(int.MaxValue, int.MinValue).Should().BeEmpty();
    Randomizer.SecureStringSequence(int.MaxValue, 0).Should().BeEmpty();

    Randomizer.SecureStringSequence(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));
    Randomizer.SecureStringSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

    Randomizer.SecureStringSequence(size, count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.SecureStringSequence(size, count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    Randomizer.SecureStringSequence(size, count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    Randomizer.SecureStringSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue)));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_SecureStringSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureStringSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.SecureStringSequenceInRange(0, 0, 1..0)).ThrowExactly<ArgumentOutOfRangeException>();

    const int size = 10;
    const int count = 1000;

    Randomizer.SecureStringSequenceInRange(0, 0).Should().BeSameAs(Randomizer.SecureStringSequenceInRange(0, 0));
    Randomizer.SecureStringSequenceInRange(0, count).Should().NotBeSameAs(Randomizer.SecureStringSequenceInRange(0, count));

    Randomizer.SecureStringSequenceInRange(size, int.MinValue, Range.All).Should().BeEmpty();
    Randomizer.SecureStringSequenceInRange(size, 0, Range.All).Should().BeEmpty();
    Randomizer.SecureStringSequenceInRange(size, 0, ..int.MaxValue).Should().BeEmpty();

    Randomizer.SecureStringSequenceInRange(int.MinValue, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));
    Randomizer.SecureStringSequenceInRange(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

    Randomizer.SecureStringSequenceInRange(size, count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.SecureStringSequenceInRange(size, count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    Randomizer.SecureStringSequenceInRange(size, count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    Randomizer.SecureStringSequenceInRange(size, count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

    Randomizer.SecureStringSequenceInRange(size, count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    Randomizer.SecureStringSequenceInRange(size, count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue)));
    Randomizer.SecureStringSequenceInRange(size, count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTime(Random, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateTime_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTime(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.DateTime(DateTime.MinValue, DateTime.MinValue).Should().Be(DateTime.MinValue).And.BeSameDateAs(Randomizer.DateTime(DateTime.MinValue, DateTime.MinValue));
    Randomizer.DateTime(DateTime.MaxValue, DateTime.MaxValue).Should().Be(DateTime.MaxValue).And.BeSameDateAs(Randomizer.DateTime(DateTime.MaxValue, DateTime.MaxValue));

    Randomizer.DateTime().Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeSequence(Random, int, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateTimeSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTimeSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.DateTimeSequence(0).Should().NotBeSameAs(Randomizer.DateTimeSequence(0));
    Randomizer.DateTimeSequence(count).Should().NotBeSameAs(Randomizer.DateTimeSequence(count));

    Randomizer.DateTimeSequence(int.MinValue).Should().BeEmpty();
    Randomizer.DateTimeSequence(0).Should().BeEmpty();

    Randomizer.DateTimeSequence(count, DateTime.MinValue, DateTime.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MinValue);
    Randomizer.DateTimeSequence(count, DateTime.MaxValue, DateTime.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MaxValue);
    Randomizer.DateTimeSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeOffset(Random, DateTimeOffset?, DateTimeOffset?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateTimeOffset_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTimeOffset(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue).And.BeSameDateAs(Randomizer.DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue));
    Randomizer.DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue).And.BeSameDateAs(Randomizer.DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue));

    Randomizer.DateTimeOffset().Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeOffsetSequence(Random, int, DateTimeOffset?, DateTimeOffset?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateTimeOffsetSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTimeOffsetSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.DateTimeOffsetSequence(0).Should().NotBeSameAs(Randomizer.DateTimeOffsetSequence(0));
    Randomizer.DateTimeOffsetSequence(count).Should().NotBeSameAs(Randomizer.DateTimeOffsetSequence(count));

    Randomizer.DateTimeOffsetSequence(int.MinValue).Should().BeEmpty();
    Randomizer.DateTimeOffsetSequence(0).Should().BeEmpty();

    Randomizer.DateTimeOffsetSequence(count, DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MinValue);
    Randomizer.DateTimeOffsetSequence(count, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MaxValue);
    Randomizer.DateTimeOffsetSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateOnly(Random, DateOnly?, DateOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateOnly_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateOnly(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.DateOnly(DateOnly.MinValue, DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    Randomizer.DateOnly(DateOnly.MaxValue, DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);

    Randomizer.DateOnly().Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateOnlySequence(Random, int, DateOnly?, DateOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DateOnlySequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.DateOnlySequence(0).Should().NotBeSameAs(Randomizer.DateOnlySequence(0));
    Randomizer.DateOnlySequence(count).Should().NotBeSameAs(Randomizer.DateOnlySequence(count));

    Randomizer.DateOnlySequence(int.MinValue).Should().BeEmpty();
    Randomizer.DateOnlySequence(0).Should().BeEmpty();

    Randomizer.DateOnlySequence(count, DateOnly.MinValue, DateOnly.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MinValue);
    Randomizer.DateOnlySequence(count, DateOnly.MaxValue, DateOnly.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MaxValue);
    Randomizer.DateOnlySequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TimeOnly(Random, TimeOnly?, TimeOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TimeOnly_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TimeOnly(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.TimeOnly(TimeOnly.MinValue, TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    Randomizer.TimeOnly(TimeOnly.MaxValue, TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);

    Randomizer.TimeOnly().Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TimeOnlySequence(Random, int, TimeOnly?, TimeOnly?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TimeOnlySequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TimeOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.TimeOnlySequence(0).Should().NotBeSameAs(Randomizer.TimeOnlySequence(0));
    Randomizer.TimeOnlySequence(count).Should().NotBeSameAs(Randomizer.TimeOnlySequence(count));

    Randomizer.TimeOnlySequence(int.MinValue).Should().BeEmpty();
    Randomizer.TimeOnlySequence(0).Should().BeEmpty();

    Randomizer.TimeOnlySequence(count, TimeOnly.MinValue, TimeOnly.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MinValue);
    Randomizer.TimeOnlySequence(count, TimeOnly.MaxValue, TimeOnly.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MaxValue);
    Randomizer.TimeOnlySequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Range(Random, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Range_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Range(null)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Randomizer.Range(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

    foreach (var max in new int?[] {null, 0, int.MaxValue})
    {
      var range = Randomizer.Range(max);
      range.Should().NotBeNull().And.NotBeSameAs(Randomizer.Range());
      range.Start.IsFromEnd.Should().BeFalse();
      range.Start.Value.Should().Be(0);
      range.End.IsFromEnd.Should().BeFalse();
      range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.RangeSequence(Random, int, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_RangeSequence_Method()
  {
    static void Validate(Range range, int? max)
    {
      range.Start.IsFromEnd.Should().BeFalse();
      range.Start.Value.Should().Be(0);
      range.End.IsFromEnd.Should().BeFalse();
      range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.RangeSequence(null, 0)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Randomizer.RangeSequence(1, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>();

      const int count = 1000;

      Randomizer.RangeSequence(0).Should().NotBeSameAs(Randomizer.RangeSequence(0));
      Randomizer.RangeSequence(count).Should().NotBeSameAs(Randomizer.RangeSequence(count));

      Randomizer.RangeSequence(int.MinValue).Should().BeEmpty();
      Randomizer.RangeSequence(0).Should().BeEmpty();

      Randomizer.RangeSequence(count, 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => Validate(element, 0));
      Randomizer.RangeSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => Validate(element, null));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Guid(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Guid_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Guid(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.Guid().Should().NotBe(Guid.Empty);
    Randomizer.Guid().Should().NotBe(Guid.NewGuid());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.GuidSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_GuidSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.GuidSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.GuidSequence(0).Should().NotBeSameAs(Randomizer.GuidSequence(0));
    Randomizer.GuidSequence(count).Should().NotBeSameAs(Randomizer.GuidSequence(count));

    Randomizer.GuidSequence(int.MinValue).Should().BeEmpty();
    Randomizer.GuidSequence(0).Should().BeEmpty();

    Randomizer.GuidSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBe(Guid.Empty).And.NotBe(Guid.NewGuid()));
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="RandomExtensions.Object(Random, IEnumerable{Type})"/></description></item>
  ///     <item><description><see cref="RandomExtensions.Object(Random, Type[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Random_Object_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new Random().Object((IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>();


    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, Array.Empty<Type>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new Random().Object(null)).ThrowExactly<ArgumentNullException>();

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
  public void Random_ObjectSequence_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ObjectSequence(null, 0, Enumerable.Empty<Type>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new Random().ObjectSequence(0, (IEnumerable<Type>) null)).ThrowExactly<ArgumentNullException>();


    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ObjectSequence(null, 0, Array.Empty<Type>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => new Random().ObjectSequence(0, null)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FileName_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.FileName(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.FileName().Should().NotBeNullOrWhiteSpace().And.NotBeSameAs(Randomizer.FileName()).And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileNameSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FileNameSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.FileNameSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.FileNameSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileNameSequence(int.MinValue)).And.BeEmpty();
    Randomizer.FileNameSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileNameSequence(0)).And.BeEmpty();

    Randomizer.FileNameSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileNameSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DirectoryName_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DirectoryName(null)).ThrowExactly<ArgumentNullException>();

    Randomizer.DirectoryName().Should().NotBeNullOrWhiteSpace().And.NotBeSameAs(Randomizer.DirectoryName()).And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryNameSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DirectoryNameSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DirectoryNameSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.DirectoryNameSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectoryNameSequence(int.MinValue)).And.BeEmpty();
    Randomizer.DirectoryNameSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectoryNameSequence(0)).And.BeEmpty();

    Randomizer.DirectoryNameSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectoryNameSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]$"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FilePath(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FilePath_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.FilePath(null)).ThrowExactly<ArgumentNullException>();

    var path = Randomizer.FilePath();
    var file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.FilePath()).And.Be(Path.Combine(Path.GetTempPath(), file));
    file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

    var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
    path = Randomizer.FilePath(currentDirectory);
    file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.FilePath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
    file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FilePathSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FilePathSequence_Method()
  {
    static void Validate(string path, string directory)
    {
      var file = Path.GetFileName(path);
      path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.FilePath()).And.Be(Path.Combine(directory, file));
      file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FilePathSequence(null, 1)).ThrowExactly<ArgumentNullException>();

      const int count = 1000;

      Randomizer.FilePathSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.FilePathSequence(int.MinValue)).And.BeEmpty();
      Randomizer.FilePathSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.FilePathSequence(0)).And.BeEmpty();

      Randomizer.FilePathSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(path, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Randomizer.FilePathSequence(count, currentDirectory).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(path, currentDirectory.FullName));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryPath(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DirectoryPath_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DirectoryPath(null)).ThrowExactly<ArgumentNullException>();

    var path = Randomizer.DirectoryPath();
    var file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.DirectoryPath()).And.Be(Path.Combine(Path.GetTempPath(), file));
    file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

    var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
    path = Randomizer.DirectoryPath(currentDirectory);
    file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.DirectoryPath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
    file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryPathSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DirectoryPathSequence_Method()
  {
    static void Validate(string directory, string path)
    {
      var file = Path.GetFileName(path);
      path.Should().NotBeNullOrWhiteSpace().And.NotBe(Randomizer.DirectoryPath()).And.Be(Path.Combine(directory, file));
      file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectoryPathSequence(null, 0)).ThrowExactly<ArgumentNullException>();

      const int count = 1000;

      Randomizer.DirectoryPathSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectoryPathSequence(int.MinValue)).And.BeEmpty();
      Randomizer.DirectoryPathSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectoryPathSequence(0)).And.BeEmpty();

      Randomizer.DirectoryPathSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(Path.GetTempPath(), path));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Randomizer.DirectoryPathSequence(count, currentDirectory).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(currentDirectory.FullName, path));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Directory(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Directory_Method()
  {
    static void Validate(DirectoryInfo directory, string path)
    {
      directory.TryFinallyDelete(directory =>
      {
        directory.Should().NotBeNull();
        directory.Exists.Should().BeTrue();
        directory.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
        directory.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
        directory.LinkTarget.Should().BeNull();
        directory.FullName.Should().Be(Path.Combine(path, directory.Name));
        directory.Name.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
        (directory.Attributes & FileAttributes.Directory).Should().Be(FileAttributes.Directory);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Directory(null)).ThrowExactly<ArgumentNullException>();

      Validate(Randomizer.Directory(), Path.GetTempPath());

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Validate(currentDirectory, currentDirectory.FullName);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectorySequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_DirectorySequence_Method()
  {
    static void Validate(DirectoryInfo directory, string path)
    {
      directory.TryFinallyDelete(directory =>
      {
        directory.Should().NotBeNull();
        directory.Exists.Should().BeTrue();
        directory.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
        directory.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
        directory.LinkTarget.Should().BeNull();
        directory.FullName.Should().Be(Path.Combine(path, directory.Name));
        directory.Name.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
        (directory.Attributes & FileAttributes.Directory).Should().Be(FileAttributes.Directory);
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.DirectorySequence(null, 1)).ThrowExactly<ArgumentNullException>();

      const int count = 10;

      Randomizer.DirectorySequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectorySequence(int.MinValue)).And.BeEmpty();
      Randomizer.DirectorySequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectorySequence(0)).And.BeEmpty();

      Randomizer.DirectorySequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectorySequence(count)).And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Randomizer.DirectorySequence(count, currentDirectory).Should().NotBeNull().And.NotBeSameAs(Randomizer.DirectorySequence(count, currentDirectory)).And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, currentDirectory.FullName));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.File(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_File_Method()
  {
    static void Validate(FileInfo file, string path)
    {
      file.TryFinallyDelete(file =>
      {
        file.Should().NotBeNull();
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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.File(null)).ThrowExactly<ArgumentNullException>();

      Validate(Randomizer.File(), Path.GetTempPath());

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Validate(Randomizer.File(currentDirectory), currentDirectory.FullName);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileSequence(Random, int, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_FileSequence_Method()
  {
    static void Validate(FileInfo file, string path)
    {
      file.TryFinallyDelete(file =>
      {
        file.Should().NotBeNull();
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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.FileSequence(null, 1)).ThrowExactly<ArgumentNullException>();

      const int count = 10;

      Randomizer.FileSequence(int.MinValue).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileSequence(int.MinValue)).And.BeEmpty();
      Randomizer.FileSequence(0).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileSequence(0)).And.BeEmpty();

      Randomizer.FileSequence(count).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileSequence(count)).And.HaveCount(count).And.AllSatisfy(file => Validate(file, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      Randomizer.FileSequence(count, currentDirectory).Should().NotBeNull().And.NotBeSameAs(Randomizer.FileSequence(count, currentDirectory)).And.HaveCount(count).And.AllSatisfy(file => Validate(file, currentDirectory.FullName));
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFile_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileInRange(Random, int, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileInRange(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequence(Random, int, int, byte?, byte?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceInRange(Random, int, int, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFile_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileInRange(Random, int, Encoding, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileInRange(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequence(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceInRange(Random, int, int, Encoding, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileAsync_Method()
  {
    static void Validate(FileInfo file, string path, int size, byte? min, byte? max)
    {
      AssertionExtensions.Should(() => Randomizer.BinaryFileAsync(0, min, max, path.ToDirectory(), Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      size = Math.Max(0, size);

      file.TryFinallyDelete(file =>
      {
        file.Should().NotBeNull();
        file.Exists.Should().BeTrue();
        file.CreationTimeUtc.Should().BeOnOrBefore(DateTime.UtcNow);
        file.LastAccessTime.Should().BeOnOrAfter(DateTime.UtcNow);
        file.LinkTarget.Should().BeNull();
        file.FullName.Should().Be(Path.Combine(path, file.FullName));
        file.Name.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
        file.Length.Should().Be(0);
        file.IsReadOnly.Should().BeFalse();

        var bytes = file.ToBytesAsync().ToEnumerable();

        bytes.Should().HaveCount(size);

        if (min != null)
        {
          bytes.Should().AllSatisfy(element => element.Should().BeGreaterThanOrEqualTo(min.Value));
        }

        if (max != null)
        {
          bytes.Should().AllSatisfy(element => element.Should().BeLessThanOrEqualTo(max.Value));
        }
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().Await();

      /*const int size = 4096;

      var tempPath = Path.GetTempPath();

      Validate(Randomizer.BinaryFile(int.MinValue), tempPath

                                     //int.MinValue, null);


      Validate( int.MinValue);
      Validate(0);
      Validate(size);
      Validate(size, 0, 0);
      Validate(size, byte.MinValue, byte.MinValue);
      Validate(size, byte.MaxValue, byte.MaxValue);*/
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileInRangeAsync(Random, int, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileInRangeAsync_Method()
  {
    static void Validate(int size, params Range[] ranges)
    {
      AssertionExtensions.Should(() => Randomizer.BinaryFileInRangeAsync(0, null, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await(); // TODO TaskCancelledException

      var file = Randomizer.BinaryFileInRangeAsync(size, null, default, ranges).Await();

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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileInRangeAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().Await();

      const int size = 4096;

      Validate(int.MinValue);
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
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileSequenceAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceAsync(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceInRangeAsync(Random, int, int, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_BinaryFileSequenceInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceInRangeAsync(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileInRangeAsync(Random, int, Encoding, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileInRangeAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceAsync(Random, int, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileSequenceAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceAsync(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceInRangeAsync(Random, int, int, Encoding, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_TextFileSequenceInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceInRangeAsync(null, 0, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddress(Random, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpAddress_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpAddressInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressInRange(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressSequence(Random, int, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpAddressSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpAddressSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6Address(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpV6Address_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpV6Address(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6AddressSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_IpV6AddressSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpV6AddressSequence(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddress(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_PhysicalAddress_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 1)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    Randomizer.PhysicalAddress(0).Should().NotBeSameAs(Randomizer.PhysicalAddress(0));
    Randomizer.PhysicalAddress(count).Should().NotBeSameAs(Randomizer.PhysicalAddress(count));

    //Randomizer.PhysicalAddress(int.MinValue).Should().NotBeNull().And.NotBeSameAs()
    //Randomizer.PhysicalAddress(0).Should().BeEmpty();

    Randomizer.ByteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    Randomizer.ByteSequence(count, byte.MinValue, byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    Randomizer.ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    Randomizer.ByteSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_PhysicalAddressInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressInRange(null, 1)).ThrowExactly<ArgumentNullException>();

    const int count = 1000;

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressSequence(Random, int, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_PhysicalAddressSequence_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_PhysicalAddressSequenceInRange_Method()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStream(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_MemoryStream_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.MemoryStream(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_MemoryStreamInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.MemoryStreamInRange(null, 0)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_MemoryStreamAsync_Method()
  {
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

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.MemoryStreamAsync(null, 0, 1, 2)).ThrowExactlyAsync<ArgumentNullException>().Await();

      const int count = 1000;

      Randomizer.MemoryStreamAsync(int.MinValue).Await().Length.Should().Be(0);
      Randomizer.MemoryStreamAsync(-1, null, null, Cancellation).Await().Length.Should().Be(0);

      Randomizer.MemoryStreamAsync(0).Await().Length.Should().Be(0);
      Randomizer.MemoryStreamAsync(0, null, null, Cancellation).Await().Length.Should().Be(0);

      using (var stream = Randomizer.MemoryStreamAsync(count).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamAsync(count, null, null, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = Randomizer.MemoryStreamAsync(count, 0, 100).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamAsync(count, null, null, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = Randomizer.MemoryStreamAsync(count, 0, 0).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamAsync(count, null, null, Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamInRangeAsync(Random, int, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_MemoryStreamInRangeAsync_Method()
  {
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

    using (new AssertionScope())
    {
      const int count = 1000;

      AssertionExtensions.Should(() => RandomExtensions.MemoryStreamInRangeAsync(null, 0, default, 1..2)).ThrowExactlyAsync<ArgumentNullException>().Await();

      Randomizer.MemoryStreamInRangeAsync(-1, default, Range.All).Await().Length.Should().Be(0);
      Randomizer.MemoryStreamInRangeAsync(-1, Cancellation, Range.All).Await().Length.Should().Be(0);

      Randomizer.MemoryStreamInRangeAsync(0, default, Range.All).Await().Length.Should().Be(0);
      Randomizer.MemoryStreamInRangeAsync(0, Cancellation, Range.All).Await().Length.Should().Be(0);

      using (var stream = Randomizer.MemoryStreamInRangeAsync(count, default, Range.All).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamInRangeAsync(count, Cancellation, Range.All)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = Randomizer.MemoryStreamInRangeAsync(count, default, ..100).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamInRangeAsync(count, Cancellation, Range.All)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = Randomizer.MemoryStreamInRangeAsync(count, default, ..0).Await())
      {
        AssertionExtensions.Should(() => Randomizer.MemoryStreamInRangeAsync(count, Cancellation, Range.All)).ThrowExactlyAsync<TaskCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Stream(Random, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_Stream_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Stream(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StreamInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void Random_StreamInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StreamInRange(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }
}