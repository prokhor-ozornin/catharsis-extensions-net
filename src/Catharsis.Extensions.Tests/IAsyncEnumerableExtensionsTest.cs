using System.Collections.Immutable;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="IAsyncEnumerableExtensions"/>.</para>
/// </summary>
public sealed class IAsyncEnumerableExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.IsEmpty{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmpty_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, bool empty)
    {
      sequence.IsEmpty().Should().Be(empty);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, true);
      Validate(Array.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Randomizer.GuidSequence(1).ToAsyncEnumerable(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.IsEmptyAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmptyAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, bool empty) { sequence.IsEmptyAsync().Await().Should().Be(empty); }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmptyAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.IsEmptyAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, true);
      Validate(Array.Empty<object>().ToAsyncEnumerable(), true);
      Validate(Randomizer.GuidSequence(1).ToAsyncEnumerable(), false);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{T})"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ForEach{T}(IAsyncEnumerable{T}, Action{int, T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ForEach_Methods()
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

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEach((Action<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

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

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach((_, _) => { })).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEach((Action<int, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ForEachAsync{T}(IAsyncEnumerable{T}, Action{T}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ForEachAsync{T}(IAsyncEnumerable{T}, Action{int, T}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ForEachAsync_Methods()
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

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync(_ => { })).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((Action<object>) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
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

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync((_, _) => {})).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((Action<int, object>) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ForEachAsync((_, _) => {}, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();
      
      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.WithEnforcedCancellation{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithEnforcedCancellation_Method()
  {
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).WithEnforcedCancellation(default)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => EmptyAsyncEnumerable.WithEnforcedCancellation(Cancellation)).ThrowExactly<OperationCanceledException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToEnumerable{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements) { sequence.ToEnumerable().Should().NotBeNull().And.NotBeSameAs(sequence.ToEnumerable()).And.Equal(elements); }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToEnumerable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToArray().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToArrayAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToArrayAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToArrayAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToLinkedList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinkedList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToLinkedList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToLinkedList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToLinkedListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinkedListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToLinkedListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToLinkedListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToLinkedListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyList_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyListAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements, IEqualityComparer<T> comparer = null)
    {
      var result = sequence.ToHashSet(comparer);
      result.Should().NotBeNull().And.Equal(elements);
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToHashSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToHashSetAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToHashSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IComparer<T> comparer = null)
    {
      var result = sequence.ToSortedSet();
      result.Should().NotBeNull().And.Equal(elements.ToSortedSet(comparer));
      result.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToSortedSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedSetAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToSortedSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionary_Method()
  {
    void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var result = sequence.ToDictionary(key, comparer);
      result.Should().NotBeNull().And.Equal(elements.ToDictionary(key, comparer));
      result.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionaryAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToDictionaryAsync(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyDictionary_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyDictionary<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToReadOnlyDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyDictionaryAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToReadOnlyDictionaryAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToValueTuple{T}(IAsyncEnumerable{T})"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToValueTuple{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToValueTuple_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTuple<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTuple<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToValueTuple<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToValueTupleAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToValueTupleAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToValueTupleAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTupleAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTupleAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToValueTupleAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToStack{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStack_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
    {
      sequence.ToStack().Should().NotBeNull().And.Equal(elements.Reverse());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToStack<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToStackAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStackAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
    {
      var task = sequence.ToStackAsync();
      task.Await().Should().NotBeNull().And.Equal(elements.Reverse());
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToStackAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToStackAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToQueue().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToQueueAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToQueueAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte})"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToMemoryStream(IAsyncEnumerable{byte[]})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStream_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.ToAsyncEnumerable(), bytes);
    }

    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToMemoryStreamAsync(IAsyncEnumerable{byte}, CancellationToken)"/></description></item>
  ///     <item><description><see cref="IAsyncEnumerableExtensions.ToMemoryStreamAsync(IAsyncEnumerable{byte[]}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToMemoryStreamAsync_Methods()
  {
    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.ToAsyncEnumerable(), bytes);
    }

    using (new AssertionScope())
    {
      void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte[]>().ToAsyncEnumerable().ToMemoryStreamAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), Array.Empty<byte>());

      var bytes = RandomBytes;
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlySet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlySet_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlySet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlySetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlySetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlySetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPriorityQueue_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToPriorityQueue<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(Enumerable.Empty<(object, object)>().ToAsyncEnumerable(), Array.Empty<(object, object)>());

      var objects = Randomizer.GuidSequence(100).ToArray();
      var elements = objects.Select((index, value) => (value, index));
      Validate(elements.ToAsyncEnumerable(), elements);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToPriorityQueueAsync{TElement, TPriority}(IAsyncEnumerable{(TElement Element, TPriority Priority)}, IComparer{TPriority}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPriorityQueueAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToPriorityQueueAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<(object, object)>().ToAsyncEnumerable().ToQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<(object, object)>().ToAsyncEnumerable(), Array.Empty<(object, object)>());

      var objects = Randomizer.GuidSequence(100).ToArray();
      var elements = objects.Select((index, value) => (value, index));
      Validate(elements.ToAsyncEnumerable(), elements);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableArray_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableArray().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableArrayAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableArrayAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableArrayAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableList_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableList().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableListAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableListAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableListAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableHashSet_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements, IEqualityComparer<T> comparer = null)
    {
      var result = sequence.ToImmutableHashSet(comparer);
      result.Should().NotBeNull().And.Equal(elements.ToImmutableHashSet(comparer));
      result.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableHashSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableHashSetAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableHashSetAsync(null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedSet_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedSetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableDictionary_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableDictionaryAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableDictionaryAsync(value => value, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedDictionary_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }
  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedDictionaryAsync_Method()
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
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableSortedDictionaryAsync(value => value, null, null, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>(), value => value);

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects, value => value);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableQueue_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      sequence.ToImmutableQueue().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableQueueAsync_Method()
  {
    void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
    {
      var task = sequence.ToImmutableQueueAsync();
      task.Await().Should().NotBeNull().And.Equal(elements);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => EmptyAsyncEnumerable.ToImmutableQueueAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(EmptyAsyncEnumerable, Array.Empty<object>());

      var objects = Randomizer.GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);
    }
  }
}