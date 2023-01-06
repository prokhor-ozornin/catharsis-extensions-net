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
  ///   <para>Performs testing of <see cref="EnumerableExtensions.IsEmpty{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_IsEmpty_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.IsEmpty<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().IsEmpty().Should().BeTrue();
    Array.Empty<object>().IsEmpty().Should().BeTrue();

    new object[] { null }.IsEmpty().Should().BeFalse();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.ForEach{T}(IEnumerable{T}, Action{int, T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_ForEach_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.ForEach<object>(null, _ => {})).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ForEach((Action<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.ForEach<object>(null, (_, _) => { })).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ForEach((Action<int, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Min{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Min_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Min(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Min(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    var first = Enumerable.Empty<object>();
    var second = Enumerable.Empty<object>();
    first.Min(second).Should().BeSameAs(first);

    first = Enumerable.Empty<object>();
    second = new object[] { null };
    first.Min(second).Should().BeSameAs(first);

    first = new object[] { string.Empty };
    second = new object[] { null };
    first.Min(second).Should().BeSameAs(first);

    first = new object[] { string.Empty };
    second = new object[] { null, string.Empty };
    first.Min(second).Should().BeSameAs(first);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Max{T}(IEnumerable{T}, IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Max_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Max(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("left");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Max(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");

    var first = Enumerable.Empty<object>();
    var second = Enumerable.Empty<object>();
    first.Max(second).Should().BeSameAs(first);

    first = Enumerable.Empty<object>();
    second = new object[] { null };
    first.Max(second).Should().BeSameAs(second);

    first = new object[] { string.Empty };
    second = new object[] { null };
    first.Max(second).Should().BeSameAs(first);

    first = new object[] { string.Empty };
    second = new object[] { null, string.Empty };
    first.Max(second).Should().BeSameAs(second);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Contains{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Contains_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Contains(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Contains(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

    Enumerable.Empty<object>().Contains(Enumerable.Empty<object>()).Should().BeTrue();

    Enumerable.Empty<object>().Contains(new object[] { null }).Should().BeFalse();
    new object[] { null }.Contains(Enumerable.Empty<object>()).Should().BeTrue();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Range{T}(IEnumerable{T}, int?, int?)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Range_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Range<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Range(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("offset");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Range(0, -1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.StartsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_StartsWith_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.StartsWith(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().StartsWith(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.EndsWith{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_EndsWith_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.EndsWith(null, Enumerable.Empty<object>())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().EndsWith(null)).ThrowExactly<ArgumentNullException>().WithParameterName("other");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Join{T}(IEnumerable{T}, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Join_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Join<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().Join().Should().BeEmpty();
    Enumerable.Empty<object>().Join(",").Should().BeEmpty();

    new object[] { null, string.Empty, "*", null }.Join().Should().Be("*");
    new object[] { null, string.Empty, "*", null }.Join(",").Should().Be("*");
    new object[] { null, string.Empty, "*", 100, null, "#" }.Join(",").Should().Be("*,100,#");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Repeat{T}(IEnumerable{T}, int)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Repeat_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Repeat<object>(null, 1)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Enumerable.Empty<object>().Repeat(-1)).ThrowExactly<ArgumentOutOfRangeException>().WithParameterName("count");

    Enumerable.Empty<object>().Repeat(0).Should().BeEmpty();
    Enumerable.Empty<object>().Repeat(1).Should().BeEmpty();

    var sequence = new object[] { null, 1, 55.5, string.Empty, Guid.Empty, null };

    sequence.Repeat(0).Should().BeEmpty();
    sequence.Repeat(1).Should().BeSameAs(sequence).And.Equal(sequence);
    sequence.Repeat(2).Should().NotBeSameAs(sequence).And.Equal(sequence.Concat(sequence));
    sequence.Repeat(3).Should().NotBeSameAs(sequence).And.Equal(sequence.Concat(sequence).Concat(sequence));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Random{T}(IEnumerable{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Random_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Random<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().Random().Should().BeNull();

    var element = new object();
    new[] { element }.Random().Should().BeSameAs(element);

    var elements = new[] { "first", "second" };
    elements.Should().Contain(elements.Random());

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.Randomize{T}(IEnumerable{T}, Random)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_Randomize_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.Randomize<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    IEnumerable<object> collection = Array.Empty<object>();
    collection.Randomize().Should().NotBeSameAs(collection).And.BeEmpty();

    collection = new object[] { string.Empty };
    collection.Randomize().Should().NotBeSameAs(collection).And.Equal(string.Empty);

    var sequence = new object[] { 1, 2, 3, 4, 5 };
    collection = new List<object>(sequence);
    collection.Randomize().Should().NotBeSameAs(collection).And.Contain(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.WithCancellation{T}(IEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WithCancellation_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.WithCancellation<object>(null, default)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => EnumerableExtensions.WithCancellation<object>(null, Cancellation)).ThrowExactly<OperationCanceledException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.AsArray{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_AsArray_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.AsArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().AsArray().Should().BeEmpty().And.BeSameAs(Enumerable.Empty<object>().AsArray());

    var array = Array.Empty<object>();
    array.AsArray().Should().BeSameAs(array);

    var list = new List<object> {null, 1, 55.5, string.Empty, Guid.Empty, null};
    list.AsArray().Should().NotBeSameAs(list.AsArray()).And.NotBeSameAs(list).And.Equal(list);
  }


  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.AsNotNullable{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_AsNotNullable_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.AsNotNullable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().AsNotNullable().Should().BeEmpty().And.NotBeSameAs(Enumerable.Empty<object>().AsNotNullable());

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    sequence.AsNotNullable().Should().NotBeSameAs(sequence.AsNotNullable()).And.Equal(sequence.Where(element => element != null));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToBase64(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToBase64_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToBase64()).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToHex(IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToHex_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToHex(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    var bytes = RandomBytes;

    Enumerable.Empty<byte>().ToHex().Should().BeEmpty();
    bytes.ToHex().Should().HaveLength(bytes.Length * 2);
    bytes.ToHex().IsMatch("[0-9A-Z]").Should().BeTrue();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToAsyncEnumerable{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToAsyncEnumerable_Method()
  {
    void Validate<T>(IEnumerable<T> sequence)
    {
      var result = sequence.ToAsyncEnumerable();
      result.Should().NotBeNull().And.NotBeSameAs(sequence.ToAsyncEnumerable());
      result.ToArray().Should().Equal(sequence.ToArray());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.ToAsyncEnumerable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

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
    AssertionExtensions.Should(() => EnumerableExtensions.ToLinkedList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToLinkedList().Should().NotBeSameAs(Enumerable.Empty<object>().ToLinkedList()).And.BeEmpty();

    IEnumerable<int?> sequence = new int?[] {1, null, 2, null, 3};
    sequence.ToLinkedList().Should().NotBeSameAs(sequence.ToLinkedList()).And.Equal(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlyList{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlyList_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToReadOnlyList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToSortedSet{T}(IEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToSortedSet_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToSortedSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToSortedSet().Should().NotBeSameAs(Enumerable.Empty<object>().ToSortedSet()).And.BeEmpty();

    IEnumerable<int?> sequence = new int?[] {1, null, 2, null, 3, null, 3, 2, 1};
    sequence.ToSortedSet().Should().NotBeSameAs(sequence.ToSortedSet()).And.Equal(null, 1, 2, 3);
    sequence.ToSortedSet(Comparer<int?>.Create((x, y) => x.GetValueOrDefault() < y.GetValueOrDefault() ? 1 : x.GetValueOrDefault() > y.GetValueOrDefault() ? -1 : 0)).Should().Equal(3, 2, 1, null);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlySet{T}(IEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlySet_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToReadOnlySet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToStack{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToStack_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToStack<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToStack().Should().NotBeSameAs(Enumerable.Empty<object>().ToStack()).And.BeEmpty();

    IEnumerable<int?> sequence = new int?[] {null, 1, null, 2, null, 3, null};
    sequence.ToStack().Should().NotBeSameAs(sequence.ToStack()).And.Equal(sequence.Reverse());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToPriorityQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToPriorityQueue<int, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToImmutableQueue{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToImmutableQueue_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToImmutableQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToArraySegment{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToArraySegment_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToArraySegment<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToArraySegment().Should().NotBeNull().And.NotBeSameAs(Enumerable.Empty<object>().ToArraySegment()).And.BeEmpty();

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = sequence.ToArraySegment();
    result.Should().NotBeNull().And.NotBeSameAs(sequence.ToArraySegment()).And.Equal(sequence);
    result.Array.Should().BeSameAs(sequence);
    result.Offset.Should().Be(0);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToMemory{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToMemory_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToMemory<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    var memory = Enumerable.Empty<object>().ToMemory();
    memory.Should().NotBeNull().And.NotBeSameAs(Enumerable.Empty<object>().ToMemory());
    memory.IsEmpty.Should().BeTrue();

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = sequence.ToMemory();
    result.Should().NotBeNull().And.NotBeSameAs(sequence.ToMemory());
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlyMemory{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlyMemory_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToReadOnlyMemory<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    var memory = Enumerable.Empty<object>().ToReadOnlyMemory();
    memory.IsEmpty.Should().BeTrue();
    memory.Should().NotBeNull().And.NotBeSameAs(Enumerable.Empty<object>().ToReadOnlyMemory());

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = sequence.ToReadOnlyMemory();
    result.Should().NotBeNull().And.NotBeSameAs(sequence.ToReadOnlyMemory());
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToSpan{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToSpan_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToSpan<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToSpan().IsEmpty.Should().BeTrue();

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = sequence.ToSpan();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlySpan{T}(IEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlySpan_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToReadOnlySpan<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Enumerable.Empty<object>().ToReadOnlySpan().IsEmpty.Should().BeTrue();

    var sequence = new object[] {null, 1, 55.5, string.Empty, Guid.Empty, null};
    var result = sequence.ToReadOnlySpan();
    result.Length.Should().Be(sequence.Length);
    result.ToArray().Should().Equal(sequence);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToRange(IEnumerable{Range})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToRange_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToRange(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.ToValueTuple{T}(IEnumerable{T})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.ToValueTuple{TKey, TValue}(IEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_ToValueTuple_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.ToValueTuple<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => EnumerableExtensions.ToValueTuple<object, object>(null, element => element)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToValueTuple<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToDictionary{TKey, TValue}(IEnumerable{(TKey Key, TValue Value)}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToDictionary_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="EnumerableExtensions.ToReadOnlyDictionary{TKey, TValue}(IEnumerable{(TKey Key, TValue Value)}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_ToReadOnlyDictionary_Method()
  {
    AssertionExtensions.Should(() => EnumerableExtensions.ToReadOnlyDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStream(IEnumerable{byte})"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStream(IEnumerable{byte[]})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStreamAsync(IEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="EnumerableExtensions.ToMemoryStreamAsync(IEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IEnumerable_ToMemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();
  }
}