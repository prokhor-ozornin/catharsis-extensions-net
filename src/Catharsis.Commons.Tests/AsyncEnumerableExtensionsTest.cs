﻿using System.Collections.Immutable;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="AsyncEnumerableExtensions"/>.</para>
/// </summary>
public sealed class AsyncEnumerableExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.IsEmpty{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_IsEmpty_Methods()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, bool empty)
    {
      sequence.IsEmpty().Await().Should().Be(empty);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null!).IsEmpty()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().IsEmpty(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().IsEmpty(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().IsEmpty(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Array.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Randomizer.GuidSequence(1).ToAsyncEnumerable(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{int, T}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{T}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ForEach_Methods()
  {
    using (new AssertionScope())
    {
      void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        using var task = sequence.ForEach((index, value) => collection.Insert(index, value));

        task.Await().Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      //AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null!).ForEach((_, _) => {})).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ForEach((Action<int, object>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ForEach((_, _) => { }, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ForEach((_, _) => {}, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }

    using (new AssertionScope())
    {
      void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        using var task = sequence.ForEach(value => collection.Add(value));

        task.Await().Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      //AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null!).ForEach(_ => { })).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ForEach((Action<object>) null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ForEach(_ => { }, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ForEach(_ => { }, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToArray{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToArray();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToArray<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToArray(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToArray(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToArray(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToList{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToList();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToList<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToList(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToList(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToList(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToLinkedList{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToLinkedList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToLinkedList();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToLinkedList<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToLinkedList(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToLinkedList(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToLinkedList(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements, IEqualityComparer<T>? comparer = null)
    {
      using var task = sequence.ToHashSet(comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements);
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToHashSet<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToHashSet(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToHashSet(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToHashSet(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToSortedSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IComparer<T>? comparer = null)
    {
      using var task = sequence.ToSortedSet();
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToSortedSet(comparer));
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToSortedSet<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToSortedSet(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToSortedSet(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToSortedSet(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey>? comparer = null) where TKey : notnull
    {
      using var task = sequence.ToDictionary(key, comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key, comparer));
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToDictionary<object, object>(null!, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionary<object, byte>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToDictionary<object, object>(value => value, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToDictionary<object, object>(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToDictionary<object, byte>(value => value, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToStack{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToStack_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
    {
      using var task = sequence.ToStack();
      task.Await().Should().NotBeNull().And.Equal(elements.Reverse());
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToStack<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToStack(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToStack(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToStack(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToQueue{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToQueue();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToQueue<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToQueue(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToQueue(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToQueue(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToPriorityQueue_Method()
  {
    void Validate<TElement, TPriority>(IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IEnumerable<(TElement, TPriority)> elements, IComparer<TPriority>? comparer = null)
    {
      using var task = sequence.ToPriorityQueue(comparer);
      var result = task.Await();
      result.Should().NotBeNull();
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<TPriority>.Default);
      result.UnorderedItems.Should().BeEquivalentTo(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToPriorityQueue<object, object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<(object, object)>().ToAsyncEnumerable().ToQueue(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToQueue(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<(object, object)>().ToAsyncEnumerable(), Array.Empty<(object, object)>());

      var objects = Randomizer.GuidSequence(100).ToArray();
      var elements = objects.Select((index, value) => (value, index));
      Validate(elements.ToAsyncEnumerable(), elements);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        using var task = sequence.ToMemoryStream();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      //AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).ToMemoryStream()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToAsyncEnumerable().ToMemoryStream(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomBytes.ToAsyncEnumerable().ToMemoryStream(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToMemoryStream(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.ToAsyncEnumerable(), bytes);
    }

    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        using var task = sequence.ToMemoryStream();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      //AssertionExtensions.Should(() => ((IEnumerable<byte[]>) null!).ToMemoryStream()).ThrowExactlyAsync<ArgumentNullException>().Await();

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToArraySegment{T}(IAsyncEnumerable{T}, int?, int?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToArraySegment_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements, int? offset = null, int? count = null)
    {
      using var task = sequence.ToArraySegment(offset, count);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToArraySegment(offset, count));
      result.Array.Should().Equal(elements);
      result.Offset.Should().Be(offset != null && count != null ? offset : 0);
      result.Count.Should().Be(offset != null &&  count != null ? count : elements.Length);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToArraySegment<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToArraySegment(-1, 0)).ThrowExactlyAsync<ArgumentOutOfRangeException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToArraySegment(0, 1)).ThrowExactlyAsync<ArgumentException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToArraySegment(null, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToArraySegment(null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToArraySegment(null, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
      Validate(objects.ToAsyncEnumerable(), objects, 1);
      Validate(objects.ToAsyncEnumerable(), objects, null, 1);
      Validate(objects.ToAsyncEnumerable(), objects, 0, objects.Length);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToMemory{T}(IAsyncEnumerable{T}, int?, int?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToMemory_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, int? offset = null, int? count = null)
    {
      using var task = sequence.ToMemory(offset, count);
      var result = task.Await();
      result.Should().NotBeNull();
      result.ToArray().Should().Equal(elements.ToArraySegment(offset, count));
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToMemory<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToMemory(-1, 0)).ThrowExactlyAsync<ArgumentOutOfRangeException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToMemory(0, 1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToMemory(null, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToMemory(null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToMemory(null, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
      Validate(objects.ToAsyncEnumerable(), objects, 1);
      Validate(objects.ToAsyncEnumerable(), objects, null, 1);
      Validate(objects.ToAsyncEnumerable(), objects, 0, objects.Length);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlyMemory{T}(IAsyncEnumerable{T}, int?, int?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlyMemory_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, int? offset = null, int? count = null)
    {
      using var task = sequence.ToReadOnlyMemory(offset, count);
      var result = task.Await();
      result.Should().NotBeNull();
      result.ToArray().Should().Equal(elements.ToArraySegment(offset, count));
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlyMemory<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToReadOnlyMemory(-1, 0)).ThrowExactlyAsync<ArgumentOutOfRangeException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToReadOnlyMemory(0, 1)).ThrowExactlyAsync<ArgumentOutOfRangeException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToReadOnlyMemory(null, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToReadOnlyMemory(null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToReadOnlyMemory(null, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
      Validate(objects.ToAsyncEnumerable(), objects, 1);
      Validate(objects.ToAsyncEnumerable(), objects, null, 1);
      Validate(objects.ToAsyncEnumerable(), objects, 0, objects.Length);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableArray{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToImmutableArray();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableArray<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableArray(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableArray(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableArray(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableList{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToImmutableList();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableList<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableList(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableList(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableList(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IEqualityComparer<T>? comparer = null)
    {
      using var task = sequence.ToImmutableHashSet(comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToImmutableHashSet(comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableHashSet<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableHashSet(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableHashSet(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableHashSet(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey>? comparer = null) where TKey : notnull
    {
      using var task = sequence.ToImmutableDictionary(key, comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToImmutableDictionary(key, comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableDictionary<object, object>(null!, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionary<object, byte>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableDictionary<object, object>(value => value, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableDictionary<object, object>(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableDictionary<object, byte>(value => value, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}?, IEqualityComparer{TValue}?, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableSortedDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null) where TKey : notnull
    {
      using var task = sequence.ToImmutableSortedDictionary(key, keyComparer, valueComparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key).ToImmutableSortedDictionary(keyComparer, valueComparer));
      result.KeyComparer.Should().BeSameAs(keyComparer ?? Comparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableSortedDictionary<object, object>(null!, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      //AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionary<object, byte>(null!)).ThrowExactyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableSortedDictionary<object, object>(value => value, null, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableSortedDictionary<object, object>(value => value, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableSortedDictionary<object, byte>(value => value, null, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableQueue{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      using var task = sequence.ToImmutableQueue();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableQueue<object>(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<object>().ToAsyncEnumerable().ToImmutableQueue(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableQueue(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.Bytes().ToImmutableQueue(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<object>().ToAsyncEnumerable(), Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }
}