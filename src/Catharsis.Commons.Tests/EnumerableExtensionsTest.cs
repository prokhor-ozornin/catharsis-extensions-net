using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="EnumerableExtensions"/>.</para>
/// </summary>
public sealed class EnumerableExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.AsArray{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_AsArray_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.AsArray<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().AsArray().Should().BeEmpty();
    Enumerable.Empty<object>().AsArray().Should().BeSameAs(Enumerable.Empty<object>().AsArray());

    var array = Array.Empty<object>();
    array.AsArray().Should().BeSameAs(array);

    var list = new List<object?> {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = list.AsArray();
    result.Should().NotBeSameAs(list);
    result.Should().Equal(list);
    list.AsArray().Should().NotBeSameAs(list.AsArray());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.IsEmpty{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.IsEmpty<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().IsEmpty().Should().BeTrue();
    Array.Empty<object>().IsEmpty().Should().BeTrue();

    new object?[] {null}.IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Indexed{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Indexed_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Indexed<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().Indexed().Should().BeEmpty();

    new object?[] {null}.Indexed().Should().ContainSingle(null, 0);

    var list = new List<object?> {null, 1, 55.5, string.Empty, Guid.Empty, null};
    list.Indexed().Should().Equal(list.Select((item, index) => (item, index)));
    list.Indexed().Should().NotBeSameAs(list.Indexed());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.NonNullable{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_NonNullable_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.NonNullable<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().NonNullable().Should().BeEmpty();
    Enumerable.Empty<object>().NonNullable().Should().NotBeSameAs(Enumerable.Empty<object>().NonNullable());

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    sequence.NonNullable().Should().Equal(sequence.Where(element => element != null));
    sequence.NonNullable().Should().NotBeSameAs(sequence.NonNullable());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Text{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Text_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Text<object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Min{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Min_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Min(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Min(null!)).ThrowExactly<ArgumentNullException>();

    IEnumerable<object?> first = Enumerable.Empty<object>();
    IEnumerable<object?> second = Enumerable.Empty<object>();
    first.Min(second).Should().BeSameAs(first);

    first = Enumerable.Empty<object>();
    second = new object?[] {null};
    first.Min(second).Should().BeSameAs(first);

    first = new object?[] {string.Empty};
    second = new object?[] {null};
    first.Min(second).Should().BeSameAs(first);

    first = new object?[] {string.Empty};
    second = new object?[] {null, string.Empty};
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Max{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Max_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Max(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Max(null!)).ThrowExactly<ArgumentNullException>();

    IEnumerable<object?> first = Enumerable.Empty<object>();
    IEnumerable<object?> second = Enumerable.Empty<object>();
    first.Max(second).Should().BeSameAs(first);

    first = Enumerable.Empty<object>();
    second = new object?[] {null};
    first.Max(second).Should().BeSameAs(second);

    first = new object?[] {string.Empty};
    second = new object?[] {null};
    first.Max(second).Should().BeSameAs(first);

    first = new object?[] {string.Empty};
    second = new object?[] {null, string.Empty};
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Join{T}(IEnumerable{T}, string?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Join_Method()
  {
    //AssertionExtensions.Should(() => EnumerableExtensions.Join<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().Join().Should().BeEmpty();
    Enumerable.Empty<object>().Join(",").Should().BeEmpty();

    new object?[] {null, string.Empty, "*", null}.Join().Should().Be("*");
    new object?[] {null, string.Empty, "*", null}.Join(",").Should().Be("*");
    new object?[] {null, string.Empty, "*", 100, null, "#"}.Join(",").Should().Be("*,100,#");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Repeat{T}(IEnumerable{T}, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Repeat_Method()
  {
    //AssertionExtensions.Should(() => EnumerableExtensions.Repeat<object>(null!, 1)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().Repeat(-1).Should().BeEmpty();
    Enumerable.Empty<object>().Repeat(0).Should().BeEmpty();
    Enumerable.Empty<object>().Repeat(1).Should().BeEmpty();

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    sequence.Repeat(-1).Should().BeEmpty();
    sequence.Repeat(0).Should().BeEmpty();

    var result = sequence.Repeat(1);
    result.Should().BeSameAs(sequence);
    result.Should().Equal(sequence);

    result = sequence.Repeat(2);
    result.Should().NotBeSameAs(sequence);
    result.Should().Equal(sequence.Concat(sequence));

    result = sequence.Repeat(3);
    result.Should().NotBeSameAs(sequence);
    result.Should().Equal(sequence.Concat(sequence).Concat(sequence));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ContainsAll{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ContainsAll_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ContainsAll(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ContainsAll(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().ContainsAll(Enumerable.Empty<object>()).Should().BeTrue();

    //Enumerable.Empty<object>().ContainsAll(new object?[] { null }).Should().BeFalse();
    new object?[] {null}.ContainsAll(Enumerable.Empty<object>()).Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.StartsWith{T}(IEnumerable{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.StartsWith{T}(IEnumerable{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_StartsWith_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.StartsWith(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().StartsWith(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => EnumerableExtensions.StartsWith<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.StartsWith(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().StartsWith(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => EnumerableExtensions.StartsWith<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.EndsWith{T}(IEnumerable{T}, IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.EndsWith{T}(IEnumerable{T}, T[])"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_EndsWith_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.EndsWith(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().EndsWith(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => EnumerableExtensions.EndsWith<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.EndsWith(null!, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().EndsWith(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => EnumerableExtensions.EndsWith<object>(null!)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.Base64(IEnumerable{char})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.Base64(IEnumerable{byte})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_Base64_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<char>) null!).Base64()).ThrowExactly<ArgumentNullException>();

      var bytes = RandomBytes;

      Enumerable.Empty<byte>().Base64().Should().BeEmpty();
      bytes.Base64().Should().Be(System.Convert.ToBase64String(bytes));
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).Base64()).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Hex(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Hex_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Hex(null!)).ThrowExactly<ArgumentNullException>();

    var bytes = RandomBytes;

    Enumerable.Empty<byte>().Hex().Should().BeEmpty();
    bytes.Hex().Should().HaveLength(bytes.Length * 2);
    bytes.Hex().IsMatch("[0-9A-Z]").Should().BeTrue();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Random{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Random_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Random<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().Random().Should().BeNull();

    var element = new object();
    new[] { element }.Random().Should().BeSameAs(element);

    var elements = new[] { "first", "second" };
    elements.Should().Contain(elements.Random());

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Randomize{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Randomize_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Randomize<object>(null!)).ThrowExactly<ArgumentNullException>();

    IEnumerable<object> collection = Array.Empty<object>();
    var result = collection.Randomize();
    result.Should().NotBeSameAs(collection);
    result.Should().BeEmpty();

    collection = new object[] {string.Empty};
    result = collection.Randomize();
    result.Should().NotBeSameAs(collection);
    result.Should().Equal(string.Empty);

    var sequence = new object[] {1, 2, 3, 4, 5};
    collection = new List<object>(sequence);
    result = collection.Randomize();
    result.Should().NotBeSameAs(collection);
    result.Should().Contain(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToAsyncEnumerable{T}(IEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToAsyncEnumerable_Method()
  {
    void Validate<T>(IEnumerable<T> sequence)
    {
      var result = sequence.ToAsyncEnumerable();
      result.Should().NotBeNull().And.NotBeSameAs(sequence.ToAsyncEnumerable());
      result.ToArray().Await().Should().Equal(sequence.ToArray());
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => EnumerableExtensions.ToAsyncEnumerable<object>(null!)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable()).ThrowExactly<OperationCanceledException>();

      Validate(Enumerable.Empty<object>());
      Validate(Array.Empty<object>());
      Validate(Randomizer.ObjectSequence(1000).ToArray());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToLinkedList{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToLinkedList_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToLinkedList<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().ToLinkedList().Should().BeEmpty();
    Enumerable.Empty<object>().ToLinkedList().Should().NotBeSameAs(Enumerable.Empty<object>().ToLinkedList());

    IEnumerable<int?> sequence = new int?[] {1, null, 2, null, 3};

    sequence.ToLinkedList().Should().Equal(sequence);
    sequence.ToLinkedList().Should().NotBeSameAs(sequence.ToLinkedList());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToSortedSet{T}(IEnumerable{T}, IComparer{T}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToSortedSet_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToSortedSet<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().ToSortedSet().Should().BeEmpty();
    Enumerable.Empty<object>().ToSortedSet().Should().NotBeSameAs(Enumerable.Empty<object>().ToSortedSet());

    IEnumerable<int?> sequence = new int?[] {1, null, 2, null, 3, null, 3, 2, 1};
    sequence.ToSortedSet().Should().Equal(null, 1, 2, 3);
    sequence.ToSortedSet(Comparer<int?>.Create((x, y) => x.GetValueOrDefault() < y.GetValueOrDefault() ? 1 : x.GetValueOrDefault() > y.GetValueOrDefault() ? -1 : 0)).Should().Equal(3, 2, 1, null);
    sequence.ToSortedSet().Should().NotBeSameAs(sequence.ToSortedSet());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToStack{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToStack_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToStack<object>(null!)).ThrowExactly<ArgumentNullException>();

    Enumerable.Empty<object>().ToStack().Should().BeEmpty();
    Enumerable.Empty<object>().ToStack().Should().NotBeSameAs(Enumerable.Empty<object>().ToStack());

    IEnumerable<int?> sequence = new int?[] {null, 1, null, 2, null, 3, null};
    sequence.ToStack().Should().Equal(sequence.Reverse());
    sequence.ToStack().Should().NotBeSameAs(sequence.ToStack());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToQueue<object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToPriorityQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToPriorityQueue<int, object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToImmutableQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToImmutableQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToImmutableQueue<object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStream(IEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStream(IEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).ToMemoryStream()).ThrowExactlyAsync<ArgumentNullException>().Await();
      
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null!).ToMemoryStream()).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToArraySegment{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToArraySegment_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToArraySegment<object>(null!)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToArraySegment(-1, 1)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToArraySegment(0, 1)).ThrowExactly<ArgumentException>();

    Enumerable.Empty<object>().ToArraySegment().Should().BeEmpty();
    Enumerable.Empty<object>().ToArraySegment(0, 0).Should().BeEmpty();
    Enumerable.Empty<object>().ToArraySegment().Should().NotBeSameAs(Enumerable.Empty<object>().ToArraySegment());

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    sequence.ToArraySegment().Should().NotBeSameAs(sequence.ToArraySegment());

    var result = sequence.ToArraySegment();
    result.Array.Should().BeSameAs(sequence);
    result.Offset.Should().Be(0);
    result.Should().Equal(sequence);

    result = sequence.ToArraySegment(1);
    result.Array.Should().BeSameAs(sequence);
    result.Offset.Should().Be(0);
    result.Should().Equal(sequence);

    result = sequence.ToArraySegment(null, 1);
    result.Array.Should().BeSameAs(sequence);
    result.Offset.Should().Be(0);
    result.Should().Equal(sequence);

    result = sequence.ToArraySegment(1, 2);
    result.Array.Should().BeSameAs(sequence);
    result.Offset.Should().Be(1);
    result.Should().Equal(sequence.Take(1..3));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToMemory{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToMemory_Method()
  {
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToMemory(-1, 0)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToMemory(0, 1)).ThrowExactly<ArgumentOutOfRangeException>();

    Enumerable.Empty<object>().ToMemory().IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToMemory(0, 0).IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToMemory().Should().NotBeSameAs(Enumerable.Empty<object>().ToMemory());

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    sequence.ToMemory().Should().NotBeSameAs(sequence.ToMemory());

    var result = sequence.ToMemory();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToMemory(1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToMemory(null, 1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToMemory(1, 2);
    result.Length.Should().Be(2);
    result.ToArray().Should().Equal(sequence.Take(1..3));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlyMemory{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlyMemory_Method()
  {
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToReadOnlyMemory(-1, 1)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToReadOnlyMemory(0, 1)).ThrowExactly<ArgumentOutOfRangeException>();

    Enumerable.Empty<object>().ToReadOnlyMemory().IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToReadOnlyMemory(0, 0).IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToReadOnlyMemory().Should().NotBeSameAs(Enumerable.Empty<object>().ToReadOnlyMemory());

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    sequence.ToReadOnlyMemory().Should().NotBeSameAs(sequence.ToReadOnlyMemory());

    var result = sequence.ToReadOnlyMemory();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlyMemory(1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlyMemory(null, 1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlyMemory(1, 2);
    result.Length.Should().Be(2);
    result.ToArray().Should().Equal(sequence.Take(1..3));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToSpan{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToSpan_Method()
  {
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToSpan(-1, 1)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToSpan(0, 1)).ThrowExactly<ArgumentOutOfRangeException>();

    Enumerable.Empty<object>().ToSpan().IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToSpan(0, 0).IsEmpty.Should().BeTrue();

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    var result = sequence.ToSpan();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToSpan(1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToSpan(null, 1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToSpan(1, 2);
    result.Length.Should().Be(2);
    result.ToArray().Should().Equal(sequence.Take(1..3));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlySpan{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlySpan_Method()
  {
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToReadOnlySpan(-1, 1)).ThrowExactly<ArgumentOutOfRangeException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToReadOnlySpan(0, 1)).ThrowExactly<ArgumentOutOfRangeException>();

    Enumerable.Empty<object>().ToReadOnlySpan().IsEmpty.Should().BeTrue();
    Enumerable.Empty<object>().ToReadOnlySpan(0, 0).IsEmpty.Should().BeTrue();

    var sequence = new object?[] {null, 1, 55.5, string.Empty, Guid.Empty, null};

    var result = sequence.ToReadOnlySpan();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlySpan(1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlySpan(null, 1);
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);

    result = sequence.ToReadOnlySpan(1, 2);
    result.Length.Should().Be(2);
    result.ToArray().Should().Equal(sequence.Take(1..3));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToRange(IEnumerable{Range})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToRange_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToRange(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToTuple{TKey, TValue}(IEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToTuple_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToTuple<object, object>(null!, element => element)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Enumerable.Empty<object>().ToTuple<object, object>(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToDictionary{TKey, TValue}(IEnumerable{(TKey Key, TValue Value)}, IEqualityComparer{TKey}?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToDictionary_Method()
  {
    //AssertionExtensions.Should(() => EnumerableExtensions.ToDictionary<int, object>())

    throw new NotImplementedException();
  }
}