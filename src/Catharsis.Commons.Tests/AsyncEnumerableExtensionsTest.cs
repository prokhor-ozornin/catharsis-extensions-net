using System.Collections.Immutable;
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
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.IsEmpty{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_IsEmpty_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, bool empty)
    {
      sequence.IsEmpty().Should().Be(empty);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmpty()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, true);
      Validate(Array.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Randomizer.GuidSequence(1).ToAsyncEnumerable(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.IsEmptyAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_IsEmptyAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, bool empty) { sequence.IsEmptyAsync().Await().Should().Be(empty); }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmptyAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.IsEmptyAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().IsEmptyAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().IsEmptyAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, true);
      Validate(Array.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Randomizer.GuidSequence(1).ToAsyncEnumerable(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{int, T})"/></description></item>
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

        var result = sequence.ForEach(value => collection.Add(value));

        result.Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach(_ => { })).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }

    using (new AssertionScope())
    {
      void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        var result = sequence.ForEach((index, value) => collection.Insert(index, value));

        result.Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach((_, _) => { })).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEachAsync{T}(IAsyncEnumerable{T}, Action{T}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ForEachAsync{T}(IAsyncEnumerable{T}, Action{int, T}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ForEachAsync_Methods()
  {
    using (new AssertionScope())
    {
      void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        var task = sequence.ForEachAsync(value => collection.Add(value));

        task.Await().Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync(_ => { })).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((Action<object>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync(_ => { }, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }

    using (new AssertionScope())
    {
      void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        var task = sequence.ForEachAsync((index, value) => collection.Insert(index, value));

        task.Await().Should().NotBeNull().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync((_, _) => {})).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((Action<int, object>) null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((_, _) => {}, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      
      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToEnumerable{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToEnumerable_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements) { sequence.ToEnumerable().Should().NotBeNull().And.NotBeSameAs(sequence.ToEnumerable()).And.Equal(elements); }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToEnumerable<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.WithEnforcedCancellation{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_WithEnforcedCancellation_Method()
  {
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).WithEnforcedCancellation(default)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).WithEnforcedCancellation(Cancellation)).ThrowExactly<OperationCanceledException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToArray().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToArray<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToArrayAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToArrayAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToArrayAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToArrayAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToArrayAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToList<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }
  
  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToListAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToListAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToLinkedList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToLinkedList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToLinkedList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToLinkedList<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToLinkedListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToLinkedListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToLinkedListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToLinkedListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToLinkedListAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToLinkedListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToLinkedListAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlyList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlyList_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlyList<object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlyListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlyListAsync_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlyListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements, IEqualityComparer<T> comparer = null)
    {
      var result = sequence.ToHashSet(comparer);
      result.Should().NotBeNull().And.Equal(elements);
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToHashSet<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToHashSetAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements, IEqualityComparer<T> comparer = null)
    {
      var task = sequence.ToHashSetAsync(comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements);
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToHashSetAsync(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToHashSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToHashSetAsync(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToSortedSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IComparer<T> comparer = null)
    {
      var result = sequence.ToSortedSet();
      result.Should().NotBeNull().And.Equal(elements.ToSortedSet(comparer));
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToSortedSet<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToSortedSetAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IComparer<T> comparer = null)
    {
      var task = sequence.ToSortedSetAsync();
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToSortedSet(comparer));
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToSortedSetAsync(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToSortedSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToSortedSetAsync(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlySet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlySet_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlySet<object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlySetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlySetAsync_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlySetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var result = sequence.ToDictionary(key, comparer);
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key, comparer));
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToDictionaryAsync_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var task = sequence.ToDictionaryAsync(key, comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key, comparer));
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToDictionaryAsync(value => value, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToDictionaryAsync(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToDictionaryAsync<object, byte>(value => value, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlyDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlyDictionary_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlyDictionary<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToReadOnlyDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToReadOnlyDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToReadOnlyDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToReadOnlyDictionaryAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToReadOnlyDictionaryAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToValueTuple{T}(IAsyncEnumerable{T})"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToValueTuple{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToValueTuple_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToValueTuple<object>(null)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToValueTuple<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToValueTuple<object, object>(null)).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToValueTupleAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToValueTupleAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToValueTupleAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToValueTupleAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToValueTupleAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToValueTupleAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToStack{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToStack_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
    {
      sequence.ToStack().Should().NotBeNull().And.Equal(elements.Reverse());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToStack<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToStackAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToStackAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
    {
      var task = sequence.ToStackAsync();
      task.Await().Should().NotBeNull().And.Equal(elements.Reverse());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToStackAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToStackAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToStackAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToStackAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToQueue().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToQueue<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToQueueAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToQueueAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToQueueAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToQueueAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToPriorityQueue_Method()
  {
    void Validate<TElement, TPriority>(IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IEnumerable<(TElement, TPriority)> elements, IComparer<TPriority> comparer = null)
    {
      var result = sequence.ToPriorityQueue(comparer);
      result.Should().NotBeNull();
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<TPriority>.Default);
      result.UnorderedItems.Should().BeEquivalentTo(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToPriorityQueue<object, object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(Enumerable.Empty<(object, object)>().ToAsyncEnumerable(), Array.Empty<(object, object)>());

      var objects = Randomizer.GuidSequence(100).ToArray();
      var elements = objects.Select((index, value) => (value, index));
      Validate(elements.ToAsyncEnumerable(), elements);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToPriorityQueueAsync{TElement, TPriority}(IAsyncEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToPriorityQueueAsync_Method()
  {
    void Validate<TElement, TPriority>(IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IEnumerable<(TElement, TPriority)> elements, IComparer<TPriority> comparer = null)
    {
      var task = sequence.ToPriorityQueueAsync(comparer);
      var result = task.Await();
      result.Should().NotBeNull();
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<TPriority>.Default);
      result.UnorderedItems.Should().BeEquivalentTo(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToPriorityQueueAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<(object, object)>().ToAsyncEnumerable().ToQueueAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<(object, object)>().ToAsyncEnumerable(), Array.Empty<(object, object)>());

      var objects = Randomizer.GuidSequence(100).ToArray();
      var elements = objects.Select((index, value) => (value, index));
      Validate(elements.ToAsyncEnumerable(), elements);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte})"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte[]})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      static void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>();

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.ToAsyncEnumerable(), bytes);
    }

    using (new AssertionScope())
    {
      static void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>();

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStreamAsync(IAsyncEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="AsyncEnumerableExtensions.ToMemoryStreamAsync(IAsyncEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToMemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      static void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomBytes.ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<TaskCanceledException>().Await();

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.ToAsyncEnumerable(), bytes);
    }

    using (new AssertionScope())
    {
      static void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte[]>().ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomBytes.Chunk(4096).ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableArray().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableArrayAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableArrayAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableArrayAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableArrayAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableArrayAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableArrayAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableArrayAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableArrayAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableList<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableListAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableListAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IEqualityComparer<T> comparer = null)
    {
      var result = sequence.ToImmutableHashSet(comparer);
      result.Should().NotBeNull().And.Equal(elements.ToImmutableHashSet(comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableHashSet<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableHashSetAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IEqualityComparer<T> comparer = null)
    {
      var task = sequence.ToImmutableHashSetAsync(comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToImmutableHashSet(comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableHashSetAsync(null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableHashSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableHashSetAsync(null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableSortedSet_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableSortedSet<object>(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableSortedSetAsync_Method()
  {
    AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var result = sequence.ToImmutableDictionary(key, comparer);
      result.Should().NotBeNull().And.Equal(elements.ToImmutableDictionary(key, comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableDictionaryAsync_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var task = sequence.ToImmutableDictionaryAsync(key, comparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToImmutableDictionary(key, comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableDictionaryAsync(value => value, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableDictionaryAsync(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableDictionaryAsync<object, byte>(value => value, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableSortedDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
    {
      var result = sequence.ToImmutableSortedDictionary(key, keyComparer, valueComparer);
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key).ToImmutableSortedDictionary(keyComparer, valueComparer));
      result.KeyComparer.Should().BeSameAs(keyComparer ?? Comparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableSortedDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }
  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableSortedDictionaryAsync_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
    {
      var task = sequence.ToImmutableSortedDictionaryAsync(key, keyComparer, valueComparer);
      var result = task.Await();
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key).ToImmutableSortedDictionary(keyComparer, valueComparer));
      result.KeyComparer.Should().BeSameAs(keyComparer ?? Comparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableSortedDictionaryAsync(value => value, null, null, Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableSortedDictionaryAsync(value => value, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableSortedDictionaryAsync<object, byte>(value => value, null, null, Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableQueue().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableQueue<object>(null)).ThrowExactly<ArgumentNullException>();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="AsyncEnumerableExtensions.ToImmutableQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IAsyncEnumerable_ToImmutableQueueAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableQueueAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => AsyncEnumerableExtensions.ToImmutableQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableQueueAsync(Cancellation)).NotThrowAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => RandomObjects.ToAsyncEnumerable().ToImmutableQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      AssertionExtensions.Should(() => Stream.Null.ToBytesAsync().ToImmutableQueueAsync(Cancellation)).ThrowAsync<TaskCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }
}