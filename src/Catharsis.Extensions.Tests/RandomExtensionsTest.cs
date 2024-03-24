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
  ///   <para>Performs testing of <see cref="RandomExtensions.Sbyte(Random, sbyte?, sbyte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Sbyte_Method()
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
    //new Random().Sbyte(sbyte.MinValue, sbyte.MaxValue).Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);
    //new Random().Sbyte(sbyte.MaxValue, sbyte.MinValue).Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);
    new Random().Sbyte().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

    return;

    static void Validate(sbyte from, sbyte to, sbyte min, sbyte max) => new Random().Sbyte(min, max).Should().BeInRange(from, to);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SbyteInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().SbyteInRange(Range.All).Should().Be(0);
    new Random().SbyteInRange(..0).Should().Be(0);
    new Random().SbyteInRange(sbyte.MaxValue..sbyte.MaxValue).Should().Be(sbyte.MaxValue);
    new Random().SbyteInRange(..sbyte.MaxValue).Should().BeInRange(0, sbyte.MaxValue);
    new Random().SbyteInRange(sbyte.MaxValue..0).Should().BeInRange(0, sbyte.MaxValue);
    new Random().SbyteInRange().Should().BeInRange(sbyte.MinValue, sbyte.MaxValue);

    return;

    static void Validate(Range[] ranges, sbyte min, sbyte max) => new Random().SbyteInRange(ranges).Should().BeInRange(min, max);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteSequence(Random, int, sbyte?, sbyte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SbyteSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SbyteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().SbyteSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().SbyteSequence(0)).And.BeEmpty();
    
    new Random().SbyteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().SbyteSequence(count, sbyte.MinValue, sbyte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MinValue);
    new Random().SbyteSequence(count, sbyte.MaxValue, sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
    new Random().SbyteSequence(count, sbyte.MinValue, sbyte.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().SbyteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
    new Random().SbyteSequence(count, sbyte.MaxValue, sbyte.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().SbyteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));
    new Random().SbyteSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().SbyteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(sbyte.MinValue, sbyte.MaxValue));

    return;

    static void Validate(int count, sbyte from, sbyte to)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SbyteSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SbyteSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SbyteSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SbyteSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().SbyteSequenceInRange(0).Should().BeSameAs(new Random().SbyteSequenceInRange(0));
    new Random().SbyteSequenceInRange(count).Should().NotBeSameAs(new Random().SbyteSequenceInRange(count));

    new Random().SbyteSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().SbyteSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();
    
    new Random().SbyteSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().SbyteSequenceInRange(count, sbyte.MaxValue..sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(sbyte.MaxValue);
    new Random().SbyteSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().SbyteSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().SbyteSequenceInRange(count, ..0, ..sbyte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
    new Random().SbyteSequenceInRange(count, ..0, sbyte.MaxValue..0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, sbyte.MaxValue));
    new Random().SbyteSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Byte(Random, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Byte_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Byte(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    
    new Random().Byte(0, 0).Should().Be(0);
    new Random().Byte(byte.MinValue, byte.MinValue).Should().Be(byte.MinValue);
    new Random().Byte(byte.MaxValue, byte.MaxValue).Should().Be(byte.MaxValue);
    new Random().Byte(byte.MinValue, byte.MaxValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
    new Random().Byte(byte.MaxValue, byte.MinValue).Should().BeInRange(byte.MinValue, byte.MaxValue);
    new Random().Byte().Should().BeInRange(byte.MinValue, byte.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().ByteInRange(Range.All).Should().Be(0);
    new Random().ByteInRange(..0).Should().Be(0);
    new Random().ByteInRange(..byte.MinValue).Should().Be(byte.MinValue);
    new Random().ByteInRange(byte.MaxValue..byte.MaxValue).Should().Be(byte.MaxValue);
    new Random().ByteInRange(..byte.MaxValue).Should().BeInRange(0, byte.MaxValue);
    new Random().ByteInRange(byte.MaxValue..0).Should().BeInRange(0, byte.MaxValue);
    new Random().ByteInRange().Should().BeInRange(byte.MinValue, byte.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteSequence(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().ByteSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().ByteSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().ByteSequence(0)).And.BeEmpty();

    new Random().ByteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ByteSequence(count, byte.MinValue, byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    new Random().ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    new Random().ByteSequence(count, byte.MinValue, byte.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().ByteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
    new Random().ByteSequence(count, byte.MaxValue, byte.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().ByteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
    new Random().ByteSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().ByteSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ByteSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ByteSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ByteSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().ByteSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().ByteSequenceInRange(0).Should().BeSameAs(new Random().ByteSequenceInRange(0));
    new Random().ByteSequenceInRange(count).Should().NotBeSameAs(new Random().ByteSequenceInRange(count));

    new Random().ByteSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().ByteSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().ByteSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ByteSequenceInRange(count, ..byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    new Random().ByteSequenceInRange(count, byte.MaxValue..byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    new Random().ByteSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().ByteSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ByteSequenceInRange(count, ..0, ..byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, byte.MaxValue));
    new Random().ByteSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Short(Random, short?, short?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Short_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Short(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Short(0, 0).Should().Be(0);
    new Random().Short(short.MinValue, short.MinValue).Should().Be(short.MinValue);
    new Random().Short(short.MaxValue, short.MaxValue).Should().Be(short.MaxValue);
    new Random().Short(short.MinValue, short.MaxValue).Should().BeInRange(short.MinValue, short.MaxValue);
    new Random().Short(short.MaxValue, short.MinValue).Should().BeInRange(short.MinValue, short.MaxValue);
    new Random().Short().Should().BeInRange(short.MinValue, short.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ShortInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().ShortInRange(Range.All).Should().Be(0);
    new Random().ShortInRange(..0).Should().Be(0);
    new Random().ShortInRange(short.MaxValue..short.MaxValue).Should().Be(short.MaxValue);
    new Random().ShortInRange(..short.MaxValue).Should().BeInRange(0, short.MaxValue);
    new Random().ShortInRange(short.MaxValue..0).Should().BeInRange(0, short.MaxValue);
    new Random().ShortInRange().Should().BeInRange(short.MinValue, short.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortSequence(Random, int, short?, short?)"/> method.</para>
  /// </summary>
  [Fact]
  public void ShortSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().ShortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().ShortSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().ShortSequence(0)).And.BeEmpty();

    new Random().ShortSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ShortSequence(count, short.MinValue, short.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MinValue);
    new Random().ShortSequence(count, short.MaxValue, short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
    new Random().ShortSequence(count, short.MinValue, short.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().ShortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
    new Random().ShortSequence(count, short.MaxValue, short.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().ShortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));
    new Random().ShortSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().ShortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(short.MinValue, short.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.ShortSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void ShortSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.ShortSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().ShortSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().ShortSequenceInRange(0).Should().BeSameAs(new Random().ShortSequenceInRange(0));
    new Random().ShortSequenceInRange(count).Should().NotBeSameAs(new Random().ShortSequenceInRange(count));

    new Random().ShortSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().ShortSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().ShortSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ShortSequenceInRange(count, short.MaxValue..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(short.MaxValue);
    new Random().ShortSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().ShortSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ShortSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    new Random().ShortSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Ushort(Random, ushort?, ushort?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Ushort_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Ushort(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Ushort(0, 0).Should().Be(0);
    new Random().Ushort(ushort.MinValue, byte.MinValue).Should().Be(ushort.MinValue);
    new Random().Ushort(ushort.MaxValue, ushort.MaxValue).Should().Be(ushort.MaxValue);
    new Random().Ushort(ushort.MinValue, ushort.MaxValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
    new Random().Ushort(ushort.MaxValue, ushort.MinValue).Should().BeInRange(ushort.MinValue, ushort.MaxValue);
    new Random().Ushort().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void UshortInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().UshortInRange(Range.All).Should().Be(0);
    new Random().UshortInRange(..0).Should().Be(0);
    new Random().UshortInRange(..ushort.MinValue).Should().Be(ushort.MinValue);
    new Random().UshortInRange(ushort.MaxValue..ushort.MaxValue).Should().Be(ushort.MaxValue);
    new Random().UshortInRange(..ushort.MaxValue).Should().BeInRange(0, ushort.MaxValue);
    new Random().UshortInRange(ushort.MaxValue..0).Should().BeInRange(0, ushort.MaxValue);
    new Random().UshortInRange().Should().BeInRange(ushort.MinValue, ushort.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortSequence(Random, int, ushort?, ushort?)"/> method.</para>
  /// </summary>
  [Fact]
  public void UshortSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().UshortSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().UshortSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().UshortSequence(0)).And.BeEmpty();

    new Random().UshortSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UshortSequence(count, ushort.MinValue, ushort.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
    new Random().UshortSequence(count, ushort.MaxValue, ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
    new Random().UshortSequence(count, ushort.MinValue, ushort.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().UshortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
    new Random().UshortSequence(count, ushort.MaxValue, ushort.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().UshortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));
    new Random().UshortSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().UshortSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(ushort.MinValue, ushort.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UshortSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void UshortSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UshortSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().UshortSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().UshortSequenceInRange(0).Should().BeSameAs(new Random().UshortSequenceInRange(0));
    new Random().UshortSequenceInRange(count).Should().NotBeSameAs(new Random().UshortSequenceInRange(count));

    new Random().UshortSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().UshortSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().UshortSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UshortSequenceInRange(count, ..ushort.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MinValue);
    new Random().UshortSequenceInRange(count, ushort.MaxValue..ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(ushort.MaxValue);
    new Random().UshortSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().UshortSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UshortSequenceInRange(count, ..0, ..ushort.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, ushort.MaxValue));
    new Random().UshortSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Int(Random, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Int_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Int(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Int(0, 0).Should().Be(0);
    new Random().Int(int.MinValue, int.MinValue).Should().Be(int.MinValue);
    new Random().Int(int.MaxValue, int.MaxValue).Should().Be(int.MaxValue);
    new Random().Int(int.MinValue, int.MaxValue).Should().BeInRange(int.MinValue, int.MaxValue);
    new Random().Int(int.MaxValue, int.MinValue).Should().BeInRange(int.MinValue, int.MaxValue);
    new Random().Int().Should().BeInRange(int.MinValue, int.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IntInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().IntInRange(Range.All).Should().Be(0);
    new Random().IntInRange(..0).Should().Be(0);
    new Random().IntInRange(int.MaxValue..int.MaxValue).Should().Be(int.MaxValue);
    new Random().IntInRange(..int.MaxValue).Should().BeInRange(0, int.MaxValue);
    new Random().IntInRange(int.MaxValue..0).Should().BeInRange(0, int.MaxValue);
    new Random().IntInRange().Should().BeInRange(int.MinValue, int.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntSequence(Random, int, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IntSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().IntSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().IntSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().IntSequence(0)).And.BeEmpty();

    new Random().IntSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().IntSequence(count, int.MinValue, int.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MinValue);
    new Random().IntSequence(count, int.MaxValue, int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    new Random().IntSequence(count, int.MinValue, int.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().IntSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
    new Random().IntSequence(count, int.MaxValue, int.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().IntSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));
    new Random().IntSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().IntSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(int.MinValue, int.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IntSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IntSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IntSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().IntSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().IntSequenceInRange(0).Should().BeSameAs(new Random().IntSequenceInRange(0));
    new Random().IntSequenceInRange(count).Should().NotBeSameAs(new Random().IntSequenceInRange(count));

    new Random().IntSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().IntSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().IntSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().IntSequenceInRange(count, int.MaxValue..int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    new Random().IntSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().IntSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().IntSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    new Random().IntSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

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
    AssertionExtensions.Should(() => RandomExtensions.Double(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Double().Should().BeInRange(double.MinValue, double.MaxValue);

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
    AssertionExtensions.Should(() => RandomExtensions.DoubleSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    const int count = 1000;

    new Random().DoubleSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().DoubleSequence(0)).And.BeEmpty();
    new Random().DoubleSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().DoubleSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(double.MinValue, double.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Char(Random, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Char_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Char(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Char((char) 0, (char) 0).Should().Be((char) 0);
    new Random().Char(char.MinValue, char.MinValue).Should().Be(char.MinValue);
    new Random().Char(char.MaxValue, char.MaxValue).Should().Be(char.MaxValue);
    new Random().Char(char.MinValue, char.MaxValue).Should().BeInRange(char.MinValue, char.MaxValue);
    new Random().Char(char.MaxValue, char.MinValue).Should().BeInRange(char.MinValue, char.MaxValue);
    new Random().Char().Should().BeInRange(char.MinValue, char.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().CharInRange(Range.All).Should().Be((char) 0);
    new Random().CharInRange(..0).Should().Be((char) 0);
    new Random().CharInRange(..char.MinValue).Should().Be(char.MinValue);
    new Random().CharInRange(char.MaxValue..char.MaxValue).Should().Be(char.MaxValue);
    new Random().CharInRange(..char.MaxValue).Should().BeInRange((char) 0, char.MaxValue);
    new Random().CharInRange(char.MaxValue..0).Should().BeInRange((char) 0, char.MaxValue);
    new Random().CharInRange().Should().BeInRange(char.MinValue, char.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharSequence(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void CharSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().CharSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().CharSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().CharSequence(0)).And.BeEmpty();

    new Random().CharSequence(count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().CharSequence(count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().CharSequence(count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().CharSequence(count, char.MinValue, char.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().CharSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().CharSequence(count, char.MaxValue, char.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().CharSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().CharSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().CharSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.CharSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void CharSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.CharSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().CharSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().CharSequenceInRange(0).Should().BeSameAs(new Random().CharSequenceInRange(0));
    new Random().CharSequenceInRange(count).Should().NotBeSameAs(new Random().CharSequenceInRange(count));

    new Random().CharSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().CharSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().CharSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().CharSequenceInRange(count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().CharSequenceInRange(count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().CharSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);

    new Random().CharSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().CharSequenceInRange(count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    new Random().CharSequenceInRange(count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo('a');

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.String(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.String(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().String(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().String(0).Should().BeSameAs(new Random().String(0)).And.BeEmpty();
    new Random().String(count, (char) 0, (char) 0).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().String(count, char.MinValue, char.MinValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().String(count, char.MaxValue, char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().String(count, char.MinValue, char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().String(count, char.MaxValue, char.MinValue).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().String(count).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void StringInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().StringInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().StringInRange(0).Should().BeSameAs(new Random().StringInRange(0));
    new Random().StringInRange(count).Should().NotBeSameAs(new Random().StringInRange(count));

    new Random().StringInRange(0, Range.All).Should().BeEmpty();
    new Random().StringInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().StringInRange(count, ..0).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().StringInRange(count, ..char.MinValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().StringInRange(count, char.MaxValue..char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().StringInRange(count, Range.All).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);

    new Random().StringInRange(count, ..0, Range.All).ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().StringInRange(count, ..0, ..char.MaxValue).ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    new Random().StringInRange(count, ..0, 'a'..'b').ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo('a');

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringSequence(Random, int, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void StringSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().StringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().StringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().SbyteSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().SbyteSequence(0)).And.BeEmpty();
    
    new Random().StringSequence(0, 0).Should().NotBeNull().And.NotBeSameAs(new Random().StringSequence(0, 0)).And.BeEmpty();
    new Random().StringSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(string.Empty);

    new Random().StringSequence(int.MaxValue, 0).Should().NotBeNull().And.NotBeSameAs(new Random().StringSequence(int.MaxValue, 0)).And.BeEmpty();

    new Random().StringSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    new Random().StringSequence(size, count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().StringSequence(size, count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    new Random().StringSequence(size, count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    new Random().StringSequence(size, count, char.MinValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
    new Random().StringSequence(size, count, char.MaxValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));
    new Random().StringSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StringSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void StringSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StringSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().StringSequenceInRange(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().StringSequenceInRange(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().StringSequenceInRange(0, 0).Should().BeSameAs(new Random().StringSequenceInRange(0, 0));
    new Random().StringSequenceInRange(0, count).Should().NotBeSameAs(new Random().StringSequenceInRange(0, count));

    new Random().StringSequenceInRange(size, 0, Range.All).Should().BeEmpty();
    new Random().StringSequenceInRange(size, 0, ..int.MaxValue).Should().BeEmpty();

    new Random().StringSequenceInRange(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    new Random().StringSequenceInRange(size, count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().StringSequenceInRange(size, count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    new Random().StringSequenceInRange(size, count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    new Random().StringSequenceInRange(size, count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

    new Random().StringSequenceInRange(size, count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().StringSequenceInRange(size, count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
    new Random().StringSequenceInRange(size, count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Digits(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void Digits_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Digits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().Digits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().Digits(0).Should().BeSameAs(new Random().Digits(0)).And.BeEmpty();
    new Random().Digits(count).Should().NotBeSameAs(new Random().Digits(count)).And.HaveLength(count).And.MatchRegex(@"^[0-9]+$");

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
    AssertionExtensions.Should(() => RandomExtensions.DigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().DigitsSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().DigitsSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().DigitsSequence(0, 0).Should().NotBeSameAs(new Random().DigitsSequence(0, 0));
    new Random().DigitsSequence(0, count).Should().NotBeSameAs(new Random().DigitsSequence(0, count));

    new Random().DigitsSequence(int.MaxValue, 0).Should().BeEmpty();

    new Random().DigitsSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    new Random().DigitsSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[0-9]+$"));

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
    AssertionExtensions.Should(() => RandomExtensions.Letters(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().Letters(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().Letters(0).Should().BeSameAs(new Random().Letters(0)).And.BeEmpty();
    new Random().Letters(count).Should().NotBeSameAs(new Random().Letters(count)).And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z]+$");

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
    AssertionExtensions.Should(() => RandomExtensions.LettersSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().LettersSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().LettersSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().LettersSequence(0, 0).Should().NotBeSameAs(new Random().LettersSequence(0, 0));
    new Random().LettersSequence(0, count).Should().NotBeSameAs(new Random().LettersSequence(0, count));

    new Random().LettersSequence(int.MaxValue, 0).Should().BeEmpty();

    new Random().LettersSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    new Random().LettersSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z]+$"));

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
    AssertionExtensions.Should(() => RandomExtensions.AlphaDigits(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().AlphaDigits(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().AlphaDigits(0).Should().BeSameAs(new Random().AlphaDigits(0)).And.BeEmpty();
    new Random().AlphaDigits(count).Should().NotBeSameAs(new Random().AlphaDigits(count)).And.HaveLength(count).And.MatchRegex(@"^[a-zA-Z0-9]+$");

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
    AssertionExtensions.Should(() => RandomExtensions.AlphaDigitsSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().AlphaDigitsSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().AlphaDigitsSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().AlphaDigitsSequence(0, 0).Should().NotBeSameAs(new Random().AlphaDigitsSequence(0, 0));
    new Random().AlphaDigitsSequence(0, count).Should().NotBeSameAs(new Random().AlphaDigitsSequence(0, count));

    new Random().AlphaDigitsSequence(int.MaxValue, 0).Should().BeEmpty();

    new Random().AlphaDigitsSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeEmpty());

    new Random().AlphaDigitsSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().MatchRegex(@"^[a-zA-Z0-9]+$"));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureString(Random, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureString_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureString(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SecureString(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().SecureString(0).Should().NotBeSameAs(new Random().String(0));
    new Random().SecureString(count).Should().NotBeSameAs(new Random().SecureString(count));

    new Random().SecureString(0).Length.Should().Be(0);

    new Random().SecureString(count, (char) 0, (char) 0).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().SecureString(count, char.MinValue, char.MinValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().SecureString(count, char.MaxValue, char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().SecureString(count, char.MinValue, char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().SecureString(count, char.MaxValue, char.MinValue).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));
    new Random().SecureString(count).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(char.MinValue, char.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureStringInRange_Method()
  {
    const int count = 1000;

    AssertionExtensions.Should(() => RandomExtensions.SecureStringInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SecureStringInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    new Random().SecureStringInRange(0).Should().NotBeSameAs(new Random().SecureStringInRange(0));
    new Random().SecureStringInRange(count).Should().NotBeSameAs(new Random().SecureStringInRange(count));

    new Random().SecureStringInRange(0, Range.All).Length.Should().Be(0);
    new Random().SecureStringInRange(0, ..int.MaxValue).Length.Should().Be(0);

    new Random().SecureStringInRange(count, ..0).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().SecureStringInRange(count, ..char.MinValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MinValue);
    new Random().SecureStringInRange(count, char.MaxValue..char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo(char.MaxValue);
    new Random().SecureStringInRange(count, Range.All).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);

    new Random().SecureStringInRange(count, ..0, Range.All).ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo((char) 0);
    new Random().SecureStringInRange(count, ..0, ..char.MaxValue).ToText().ToCharArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange((char) 0, char.MaxValue));
    new Random().SecureStringInRange(count, ..0, 'a'..'b').ToText().ToCharArray().Should().HaveCount(count).And.AllBeEquivalentTo('a');

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringSequence(Random, int, int, char?, char?)"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureStringSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureStringSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SecureStringSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().SecureStringSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().SecureStringSequence(0, 0).Should().NotBeSameAs(new Random().SecureStringSequence(0, 0));
    new Random().SecureStringSequence(0, count).Should().NotBeSameAs(new Random().SecureStringSequence(0, count));

    new Random().SecureStringSequence(int.MaxValue, 0).Should().BeEmpty();

    new Random().SecureStringSequence(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

    new Random().SecureStringSequence(size, count, (char) 0, (char) 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().SecureStringSequence(size, count, char.MinValue, char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    new Random().SecureStringSequence(size, count, char.MaxValue, char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    new Random().SecureStringSequence(size, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange(char.MinValue, char.MaxValue)));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.SecureStringSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void SecureStringSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.SecureStringSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().SecureStringSequenceInRange(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().SecureStringSequenceInRange(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int size = 10;
    const int count = 1000;

    new Random().SecureStringSequenceInRange(0, 0).Should().BeSameAs(new Random().SecureStringSequenceInRange(0, 0));
    new Random().SecureStringSequenceInRange(0, count).Should().NotBeSameAs(new Random().SecureStringSequenceInRange(0, count));

    new Random().SecureStringSequenceInRange(size, 0, Range.All).Should().BeEmpty();
    new Random().SecureStringSequenceInRange(size, 0, ..int.MaxValue).Should().BeEmpty();

    new Random().SecureStringSequenceInRange(0, count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Length.Should().Be(0));

    new Random().SecureStringSequenceInRange(size, count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().SecureStringSequenceInRange(size, count, ..char.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MinValue));
    new Random().SecureStringSequenceInRange(size, count, char.MaxValue..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo(char.MaxValue));
    new Random().SecureStringSequenceInRange(size, count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));

    new Random().SecureStringSequenceInRange(size, count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo((char) 0));
    new Random().SecureStringSequenceInRange(size, count, ..0, ..char.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllSatisfy(x => x.Should().BeInRange((char) 0, char.MaxValue)));
    new Random().SecureStringSequenceInRange(size, count, ..0, 'a'..'b').Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.ToText().ToCharArray().Should().HaveCount(size).And.AllBeEquivalentTo('a'));

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
    AssertionExtensions.Should(() => RandomExtensions.Range(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().Range(int.MinValue)).ThrowExactly<ArgumentOutOfRangeException>();

    foreach (var max in new int?[] {null, 0, int.MaxValue})
    {
      var range = new Random().Range(max);
      range.Should().NotBeNull().And.NotBeSameAs(new Random().Range());
      range.Start.IsFromEnd.Should().BeFalse();
      range.Start.Value.Should().Be(0);
      range.End.IsFromEnd.Should().BeFalse();
      range.End.Value.Should().BeInRange(0, max ?? int.MaxValue);
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

      new Random().RangeSequence(0).Should().NotBeSameAs(new Random().RangeSequence(0)).And.BeEmpty();
      new Random().RangeSequence(count, 0).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => Validate(element, 0));
      new Random().RangeSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().RangeSequence(count)).And.HaveCount(count).And.AllSatisfy(element => Validate(element, null));
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
    AssertionExtensions.Should(() => RandomExtensions.Guid(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Guid().Should().NotBe(Guid.Empty);
    new Random().Guid().Should().NotBe(Guid.NewGuid());

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
    AssertionExtensions.Should(() => RandomExtensions.GuidSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().GuidSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().GuidSequence(0).Should().NotBeSameAs(new Random().GuidSequence(0)).And.BeEmpty();
    new Random().GuidSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().GuidSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBe(Guid.Empty).And.NotBe(Guid.NewGuid()));

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
      
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.Object(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().Object(null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");

    }

    return;

    static void Validate()
    {
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


    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.ObjectSequence(null, 0, [])).ThrowExactly<ArgumentNullException>().WithParameterName("random");
      AssertionExtensions.Should(() => new Random().ObjectSequence(0, null)).ThrowExactly<ArgumentNullException>().WithParameterName("types");
      AssertionExtensions.Should(() => new Random().ObjectSequence(-1, [])).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    }

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.FileName(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void FileName_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.FileName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().FileName().Should().NotBeNullOrWhiteSpace().And.NotBeSameAs(new Random().FileName()).And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

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
    AssertionExtensions.Should(() => RandomExtensions.FileNameSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().FileNameSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().FileNameSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().FileNameSequence(0)).And.BeEmpty();
    new Random().FileNameSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().FileNameSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$"));

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
    AssertionExtensions.Should(() => RandomExtensions.DirectoryName(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().DirectoryName().Should().NotBeNullOrWhiteSpace().And.NotBeSameAs(new Random().DirectoryName()).And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

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
    AssertionExtensions.Should(() => RandomExtensions.DirectoryNameSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().DirectoryNameSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().DirectoryNameSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().DirectoryNameSequence(0)).And.BeEmpty();
    new Random().DirectoryNameSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().DirectoryNameSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().NotBeNullOrWhiteSpace().And.HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$"));

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
    AssertionExtensions.Should(() => RandomExtensions.FilePath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    var path = new Random().FilePath();
    var file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(Path.GetTempPath(), file));
    file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

    var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
    path = new Random().FilePath(currentDirectory);
    file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
    file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");

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

      new Random().FilePathSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().FilePathSequence(0)).And.BeEmpty();
      new Random().FilePathSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(path, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().FilePathSequence(count, currentDirectory).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(path, currentDirectory.FullName));
    }

    return;

    static void Validate(string path, string directory)
    {
      var file = Path.GetFileName(path);
      path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().FilePath()).And.Be(Path.Combine(directory, file));
      file.Should().MatchRegex("^[a-zA-Z0-9]*\\.[a-zA-Z0-9]{3}$");
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DirectoryPath(Random, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void DirectoryPath_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DirectoryPath(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    var path = new Random().DirectoryPath();
    var file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(Path.GetTempPath(), file));
    file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

    var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
    path = new Random().DirectoryPath(currentDirectory);
    file = Path.GetFileName(path);
    path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath(currentDirectory)).And.Be(Path.Combine(currentDirectory.FullName, file));
    file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");

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

      new Random().DirectoryPathSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().DirectoryPathSequence(0)).And.BeEmpty();

      new Random().DirectoryPathSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(Path.GetTempPath(), path));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().DirectoryPathSequence(count, currentDirectory).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(path => Validate(currentDirectory.FullName, path));
    }

    return;

    static void Validate(string directory, string path)
    {
      var file = Path.GetFileName(path);
      path.Should().NotBeNullOrWhiteSpace().And.NotBe(new Random().DirectoryPath()).And.Be(Path.Combine(directory, file));
      file.Should().HaveLength(32).And.MatchRegex("^[a-zA-Z0-9]*$");
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

      new Random().DirectorySequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().DirectorySequence(0)).And.BeEmpty();

      new Random().DirectorySequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().DirectorySequence(count)).And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().DirectorySequence(count, currentDirectory).Should().NotBeNull().And.NotBeSameAs(new Random().DirectorySequence(count, currentDirectory)).And.HaveCount(count).And.AllSatisfy(directory => Validate(directory, currentDirectory.FullName));
    }

    return;

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

      new Random().FileSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().FileSequence(0)).And.BeEmpty();

      new Random().FileSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().FileSequence(count)).And.HaveCount(count).And.AllSatisfy(file => Validate(file, Path.GetTempPath()));

      var currentDirectory = Directory.GetCurrentDirectory().ToDirectory();
      new Random().FileSequence(count, currentDirectory).Should().NotBeNull().And.NotBeSameAs(new Random().FileSequence(count, currentDirectory)).And.HaveCount(count).And.AllSatisfy(file => Validate(file, currentDirectory.FullName));
    }

    return;

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
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFile(Random, int, byte?, byte?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFile_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().BinaryFile(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileAsync(Random, int, byte?, byte?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileAsync_Method()
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
    }

    return;

    static void Validate(FileInfo file, string path, int size, byte? min, byte? max)
    {
      //AssertionExtensions.Should(() => new Random().BinaryFileAsync(0, min, max, path.ToDirectory(), Attributes.CancellationToken())).ThrowExactlyAsync<TaskCanceledException>().Await();

      size = Math.Max(0, size);

      file.TryFinallyDelete(info =>
      {
        info.Should().NotBeNull();
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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileInRange(Random, int, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileInRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().BinaryFileInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileInRangeAsync(Random, int, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileInRangeAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => RandomExtensions.BinaryFileInRangeAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileInRangeAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
      AssertionExtensions.Should(() => new Random().BinaryFileInRangeAsync(0, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

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
    }

    return;

    static void Validate(int size, params Range[] ranges)
    {
      var file = new Random().BinaryFileInRangeAsync(size, null, default, ranges).Await();

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

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequence(Random, int, int, byte?, byte?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().BinaryFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().BinaryFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceAsync(Random, int, int, byte?, byte?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileSequenceAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceInRange(Random, int, int, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceInRange(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceInRange(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceInRange(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.BinaryFileSequenceInRangeAsync(Random, int, int, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void BinaryFileSequenceInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.BinaryFileSequenceInRangeAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceInRangeAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
    AssertionExtensions.Should(() => new Random().BinaryFileSequenceInRangeAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFile(Random, int, Encoding, char?, char?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFile_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFile(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().TextFile(-1)).ThrowExactly<ArgumentNullException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileAsync(Random, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().TextFileAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileInRange(Random, int, Encoding, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileInRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().TextFileInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileInRangeAsync(Random, int, Encoding, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileInRangeAsync(null, 0)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().TextFileInRangeAsync(-1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequence(Random, int, int, Encoding, char?, char?, DirectoryInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequence(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().TextFileSequence(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().TextFileSequence(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceAsync(Random, int, int, Encoding, char?, char?, DirectoryInfo, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileSequenceAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
    AssertionExtensions.Should(() => new Random().TextFileSequenceAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceInRange(Random, int, int, Encoding, DirectoryInfo, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceInRange(null, 0, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().TextFileSequenceInRange(-1, 0).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().TextFileSequenceInRange(0, -1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.TextFileSequenceInRangeAsync(Random, int, int, Encoding, DirectoryInfo, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void TextFileSequenceInRangeAsync_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.TextFileSequenceInRangeAsync(null, 0, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();
    AssertionExtensions.Should(() => new Random().TextFileSequenceInRangeAsync(-1, 0).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("size").Await();
    AssertionExtensions.Should(() => new Random().TextFileSequenceInRangeAsync(0, -1).ToArrayAsync()).ThrowExactlyAsync<ArgumentOutOfRangeException>().WithParameterName("count").Await();

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6Address(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpV6Address_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpV6Address(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpV6AddressSequence(Random, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpV6AddressSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpV6AddressSequence(null, 0).ToArray()).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().IpV6AddressSequence(-1).ToArray()).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddress(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddress_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddress(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().PhysicalAddress(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

    const int count = 1000;

    new Random().PhysicalAddress(0).Should().NotBeSameAs(new Random().PhysicalAddress(0));
    new Random().PhysicalAddress(count).Should().NotBeSameAs(new Random().PhysicalAddress(count));

    //new Random().PhysicalAddress(int.MinValue).Should().NotBeNull().And.NotBeSameAs()
    //new Random().PhysicalAddress(0).Should().BeEmpty();

    new Random().ByteSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().ByteSequence(count, byte.MinValue, byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MinValue);
    new Random().ByteSequence(count, byte.MaxValue, byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(byte.MaxValue);
    new Random().ByteSequence(count, byte.MinValue, byte.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
    new Random().ByteSequence(count, byte.MaxValue, byte.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
    new Random().ByteSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddressInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressInRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().PhysicalAddressInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");

    const int count = 1000;

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressSequence(Random, int, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddressSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressSequence(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().PhysicalAddressSequence(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.PhysicalAddressSequenceInRange(Random, int, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void PhysicalAddressSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.PhysicalAddressSequenceInRange(null, 0, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().PhysicalAddressSequenceInRange(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("size");
    AssertionExtensions.Should(() => new Random().PhysicalAddressSequenceInRange(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStream(Random, int, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemoryStream_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.MemoryStream(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().MemoryStream(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamAsync(Random, int, byte?, byte?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void MemoryStreamAsync_Method()
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
    }

    return;

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

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void MemoryStreamInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.MemoryStreamInRange(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().MemoryStreamInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.MemoryStreamInRangeAsync(Random, int, CancellationToken, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void MemoryStreamInRangeAsync_Method()
  {
    using (new AssertionScope())
    {
      const int count = 1000;

      AssertionExtensions.Should(() => RandomExtensions.MemoryStreamInRangeAsync(null, 0, default, 1..2)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("random").Await();

      new Random().MemoryStreamInRangeAsync(0, default, Range.All).Await().Length.Should().Be(0);
      new Random().MemoryStreamInRangeAsync(0, Attributes.CancellationToken(), Range.All).Await().Length.Should().Be(0);

      using (var stream = new Random().MemoryStreamInRangeAsync(count, default, Range.All).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamInRangeAsync(count, Attributes.CancellationToken(), Range.All)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(byte.MinValue, byte.MaxValue));
      }

      using (var stream = new Random().MemoryStreamInRangeAsync(count, default, ..100).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamInRangeAsync(count, Attributes.CancellationToken(), Range.All)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, 100));
      }

      using (var stream = new Random().MemoryStreamInRangeAsync(count, default, ..0).Await())
      {
        AssertionExtensions.Should(() => new Random().MemoryStreamInRangeAsync(count, Attributes.CancellationToken(), Range.All)).ThrowExactlyAsync<OperationCanceledException>().Await();

        Validate(stream, count);

        stream.ToArray().Should().HaveCount(count).And.AllBeEquivalentTo(0);
      }
    }

    return;

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

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Stream(Random, byte?, byte?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Stream_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Stream(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.StreamInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void StreamInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.StreamInRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Uint(Random, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Uint_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Uint(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Uint(0, 0).Should().Be(0);
    new Random().Uint(uint.MinValue, uint.MinValue).Should().Be(uint.MinValue);
    new Random().Uint(uint.MaxValue, uint.MaxValue).Should().Be(uint.MaxValue);
    new Random().Uint(uint.MinValue, uint.MaxValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
    new Random().Uint(uint.MaxValue, uint.MinValue).Should().BeInRange(uint.MinValue, uint.MaxValue);
    new Random().Uint().Should().BeInRange(uint.MinValue, uint.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UintInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void UintInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UintInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().UintInRange(Range.All).Should().Be(0);
    new Random().UintInRange(..0).Should().Be(0);
    new Random().UintInRange().Should().BeInRange(uint.MinValue, uint.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UintSequence(Random, int, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void UintSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UintSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().UintSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().UintSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().UintSequence(0)).And.BeEmpty();

    new Random().UintSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UintSequence(count, uint.MinValue, uint.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(uint.MinValue);
    new Random().UintSequence(count, uint.MaxValue, uint.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(uint.MaxValue);
    new Random().UintSequence(count, uint.MinValue, uint.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().UintSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
    new Random().UintSequence(count, uint.MaxValue, uint.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().UintSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));
    new Random().UintSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().UintSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(uint.MinValue, uint.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.UintSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void UintSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.UintSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().UintSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().UintSequenceInRange(0).Should().BeSameAs(new Random().UintSequenceInRange(0));
    new Random().UintSequenceInRange(count).Should().NotBeSameAs(new Random().UintSequenceInRange(count));

    new Random().UintSequenceInRange(0, Range.All).Should().BeEmpty();

    new Random().UintSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UintSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().UintSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().UintSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Long(Random, long?, long?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Long_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Long(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Long(0, 0).Should().Be(0);
    new Random().Long(long.MinValue, long.MinValue).Should().Be(long.MinValue);
    new Random().Long(long.MaxValue, long.MaxValue).Should().Be(long.MaxValue);
    new Random().Long(long.MinValue, long.MaxValue).Should().BeInRange(long.MinValue, long.MaxValue);
    new Random().Long(long.MaxValue, long.MinValue).Should().BeInRange(long.MinValue, long.MaxValue);
    new Random().Long().Should().BeInRange(long.MinValue, long.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void LongInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongInRange(null, Range.All)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().LongInRange(Range.All).Should().Be(0);
    new Random().LongInRange(..0).Should().Be(0);
    new Random().LongInRange().Should().BeInRange(long.MinValue, long.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongSequence(Random, int, long?, long?)"/> method.</para>
  /// </summary>
  [Fact]
  public void LongSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().LongSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().LongSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().LongSequence(0)).And.BeEmpty();

    new Random().LongSequence(count, 0, 0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().LongSequence(count, long.MinValue, long.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(long.MinValue);
    new Random().LongSequence(count, long.MaxValue, long.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(long.MaxValue);
    new Random().LongSequence(count, long.MinValue, long.MaxValue).Should().NotBeNull().And.NotBeSameAs(new Random().LongSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
    new Random().LongSequence(count, long.MaxValue, long.MinValue).Should().NotBeNull().And.NotBeSameAs(new Random().LongSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));
    new Random().LongSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().LongSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(long.MinValue, long.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.LongSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void LongSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.LongSequenceInRange(null, 0, ..1)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().LongSequenceInRange(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().LongSequenceInRange(0).Should().BeSameAs(new Random().LongSequenceInRange(0));
    new Random().LongSequenceInRange(count).Should().NotBeSameAs(new Random().LongSequenceInRange(count));

    new Random().LongSequenceInRange(0, Range.All).Should().BeEmpty();
    new Random().LongSequenceInRange(0, ..int.MaxValue).Should().BeEmpty();

    new Random().LongSequenceInRange(count, ..0).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().LongSequenceInRange(count, int.MaxValue..int.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);
    new Random().LongSequenceInRange(count, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);

    new Random().LongSequenceInRange(count, ..0, Range.All).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(0);
    new Random().LongSequenceInRange(count, ..0, ..short.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(0, short.MaxValue));
    new Random().LongSequenceInRange(count, ..0, 1..2).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(1);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.Float(Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void Float_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.Float(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().Float().Should().BeInRange(float.MinValue, float.MaxValue);

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
    AssertionExtensions.Should(() => RandomExtensions.FloatSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    const int count = 1000;

    new Random().FloatSequence(0).Should().NotBeNull().And.NotBeSameAs(new Random().FloatSequence(0)).And.BeEmpty();
    new Random().FloatSequence(count).Should().NotBeNull().And.NotBeSameAs(new Random().FloatSequence(count)).And.HaveCount(count).And.AllSatisfy(element => element.Should().BeInRange(float.MinValue, float.MaxValue));

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddress(Random, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpAddress_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddress(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressInRange(Random, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IpAddressInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressInRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressSequence(Random, int, uint?, uint?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IpAddressSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().IpAddressSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.IpAddressSequenceInRange(Random, int, Range[])"/> method.</para>
  /// </summary>
  [Fact]
  public void IpAddressSequenceInRange_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.IpAddressSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().IpAddressSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    return;

    static void Validate()
    {
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTime(Random, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTime_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTime(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().DateTime(DateTime.MinValue, DateTime.MinValue).Should().Be(DateTime.MinValue).And.BeSameDateAs(new Random().DateTime(DateTime.MinValue, DateTime.MinValue));
    new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue).Should().Be(DateTime.MaxValue).And.BeSameDateAs(new Random().DateTime(DateTime.MaxValue, DateTime.MaxValue));
    new Random().DateTime(DateTime.MinValue, DateTime.MaxValue).Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
    new Random().DateTime(DateTime.MaxValue, DateTime.MinValue).Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);
    new Random().DateTime().Should().BeIn(DateTimeKind.Utc).And.BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue);

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="RandomExtensions.DateTimeSequence(Random, int, DateTime?, DateTime?)"/> method.</para>
  /// </summary>
  [Fact]
  public void DateTimeSequence_Method()
  {
    AssertionExtensions.Should(() => RandomExtensions.DateTimeSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().DateTimeSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().DateTimeSequence(0).Should().NotBeSameAs(new Random().DateTimeSequence(0)).And.BeEmpty();
    new Random().DateTimeSequence(count).Should().NotBeSameAs(new Random().DateTimeSequence(count));
    new Random().DateTimeSequence(count, DateTime.MinValue, DateTime.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MinValue);
    new Random().DateTimeSequence(count, DateTime.MaxValue, DateTime.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTime.MaxValue);
    new Random().DateTimeSequence(count, DateTime.MinValue, DateTime.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
    new Random().DateTimeSequence(count, DateTime.MaxValue, DateTime.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));
    new Random().DateTimeSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateTime.MinValue).And.BeOnOrBefore(DateTime.MaxValue));

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
    AssertionExtensions.Should(() => RandomExtensions.DateTimeOffset(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().Be(DateTimeOffset.MinValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MinValue));
    new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().Be(DateTimeOffset.MaxValue).And.BeSameDateAs(new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MaxValue));
    new Random().DateTimeOffset(DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
    new Random().DateTimeOffset(DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);
    new Random().DateTimeOffset().Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue);

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
    AssertionExtensions.Should(() => RandomExtensions.DateTimeOffsetSequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().DateTimeOffsetSequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().DateTimeOffsetSequence(0).Should().NotBeSameAs(new Random().DateTimeOffsetSequence(0)).And.BeEmpty();
    new Random().DateTimeOffsetSequence(count).Should().NotBeSameAs(new Random().DateTimeOffsetSequence(count));
    new Random().DateTimeOffsetSequence(count, DateTimeOffset.MinValue, DateTimeOffset.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MinValue);
    new Random().DateTimeOffsetSequence(count, DateTimeOffset.MaxValue, DateTimeOffset.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateTimeOffset.MaxValue);
    new Random().DateTimeOffsetSequence(count, DateTimeOffset.MinValue, DateTimeOffset.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
    new Random().DateTimeOffsetSequence(count, DateTimeOffset.MaxValue, DateTimeOffset.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));
    new Random().DateTimeOffsetSequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().HaveOffset(TimeSpan.Zero).And.BeOnOrAfter(DateTimeOffset.MinValue).And.BeOnOrBefore(DateTimeOffset.MaxValue));

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
    AssertionExtensions.Should(() => RandomExtensions.DateOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().DateOnly(DateOnly.MinValue, DateOnly.MinValue).Should().Be(DateOnly.MinValue);
    new Random().DateOnly(DateOnly.MaxValue, DateOnly.MaxValue).Should().Be(DateOnly.MaxValue);
    new Random().DateOnly(DateOnly.MinValue, DateOnly.MaxValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
    new Random().DateOnly(DateOnly.MaxValue, DateOnly.MinValue).Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);
    new Random().DateOnly().Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue);

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
    AssertionExtensions.Should(() => RandomExtensions.DateOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().DateOnlySequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().DateOnlySequence(0).Should().NotBeSameAs(new Random().DateOnlySequence(0)).And.BeEmpty();
    new Random().DateOnlySequence(count).Should().NotBeSameAs(new Random().DateOnlySequence(count));

    new Random().DateOnlySequence(count, DateOnly.MinValue, DateOnly.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MinValue);
    new Random().DateOnlySequence(count, DateOnly.MaxValue, DateOnly.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(DateOnly.MaxValue);
    new Random().DateOnlySequence(count, DateOnly.MinValue, DateOnly.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
    new Random().DateOnlySequence(count, DateOnly.MaxValue, DateOnly.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));
    new Random().DateOnlySequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(DateOnly.MinValue).And.BeOnOrBefore(DateOnly.MaxValue));

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
    AssertionExtensions.Should(() => RandomExtensions.TimeOnly(null)).ThrowExactly<ArgumentNullException>().WithParameterName("random");

    new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MinValue).Should().Be(TimeOnly.MinValue);
    new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MaxValue).Should().Be(TimeOnly.MaxValue);
    new Random().TimeOnly(TimeOnly.MinValue, TimeOnly.MaxValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
    new Random().TimeOnly(TimeOnly.MaxValue, TimeOnly.MinValue).Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);
    new Random().TimeOnly().Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue);

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
    AssertionExtensions.Should(() => RandomExtensions.TimeOnlySequence(null, 0)).ThrowExactly<ArgumentNullException>().WithParameterName("random");
    AssertionExtensions.Should(() => new Random().TimeOnlySequence(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    const int count = 1000;

    new Random().TimeOnlySequence(0).Should().NotBeSameAs(new Random().TimeOnlySequence(0)).And.BeEmpty();
    new Random().TimeOnlySequence(count).Should().NotBeSameAs(new Random().TimeOnlySequence(count));
    new Random().TimeOnlySequence(count, TimeOnly.MinValue, TimeOnly.MinValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MinValue);
    new Random().TimeOnlySequence(count, TimeOnly.MaxValue, TimeOnly.MaxValue).Should().NotBeNull().And.HaveCount(count).And.AllBeEquivalentTo(TimeOnly.MaxValue);
    new Random().TimeOnlySequence(count).Should().NotBeNull().And.HaveCount(count).And.AllSatisfy(element => element.Should().BeOnOrAfter(TimeOnly.MinValue).And.BeOnOrBefore(TimeOnly.MaxValue));

    return;

    static void Validate()
    {
    }
  }
}