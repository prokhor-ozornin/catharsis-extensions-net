using System.Collections.Immutable;
using Catharsis.Commons;
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
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmpty()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate(true, Attributes.EmptyAsyncEnumerable());
    Validate(true, Array.Empty<object>().ToAsyncEnumerable());
    Validate(false, new Random().GuidSequence(1).ToAsyncEnumerable());

    return;

    static void Validate<T>(bool result, IAsyncEnumerable<T> sequence) => sequence.IsEmpty().Should().Be(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.IsEmptyAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IsEmptyAsync_Method()
  {
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).IsEmptyAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().IsEmptyAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate(true, Attributes.EmptyAsyncEnumerable());
    Validate(true, Array.Empty<object>().ToAsyncEnumerable());
    Validate(false, new Random().GuidSequence(1).ToAsyncEnumerable());

    return;

    static void Validate<T>(bool result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.IsEmptyAsync();
      task.Should().BeOfType<Task<bool>>();
      task.Await().Should().Be(result);
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
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach(_ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEach((Action<object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(Attributes.EmptyAsyncEnumerable(), []);

      var objects = new Random().GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);

      static void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
      {
        var collection = new List<T>();

        var result = sequence.ForEach(value => collection.Add(value));

        result.Should().BeOfType<IAsyncEnumerable<T>>().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }
    }

    using (new AssertionScope())
    {
      static void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        var result = sequence.ForEach((index, value) => collection.Insert(index, value));

        result.Should().BeOfType<IAsyncEnumerable<T>>().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }

      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEach((_, _) => { })).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEach((Action<int, object>) null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");

      Validate(Attributes.EmptyAsyncEnumerable(), []);

      var objects = new Random().GuidSequence(1000).ToArray();
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
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync(_ => { })).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEachAsync((Action<object>) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEachAsync(_ => { }, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Attributes.EmptyAsyncEnumerable(), []);

      var objects = new Random().GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);

      static void Validate<T>(IAsyncEnumerable<T> sequence, IEnumerable<T> elements)
      {
        var collection = new List<T>();

        var task = sequence.ForEachAsync(value => collection.Add(value));
        task.Should().BeOfType<Task<IAsyncEnumerable<T>>>();
        task.Await().Should().BeOfType<IAsyncEnumerable<T>>().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).ForEachAsync((_, _) => {})).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEachAsync((Action<int, object>) null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ForEachAsync((_, _) => {}, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
      
      Validate(Attributes.EmptyAsyncEnumerable(), []);

      var objects = new Random().GuidSequence(1000).ToArray();
      Validate(objects.ToAsyncEnumerable(), objects);

      static void Validate<T>(IAsyncEnumerable<T> sequence, T[] elements)
      {
        var collection = new List<T>();

        var task = sequence.ForEachAsync((index, value) => collection.Insert(index, value));
        task.Should().BeOfType<Task<IAsyncEnumerable<T>>>();
        task.Await().Should().BeOfType<IAsyncEnumerable<T>>().And.BeSameAs(sequence);
        collection.Should().Equal(elements);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.WithEnforcedCancellation{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WithEnforcedCancellation_Method()
  {
    AssertionExtensions.Should(() => ((IAsyncEnumerable<object>) null).WithEnforcedCancellation(default)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().WithEnforcedCancellation(Attributes.CancellationToken())).ThrowExactly<OperationCanceledException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToEnumerable{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToEnumerable_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToEnumerable<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToEnumerable().Should().BeOfType<IAsyncEnumerable<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToArray_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(T[] result, IAsyncEnumerable<T> sequence) => sequence.ToArray().Should().BeOfType<T[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToArrayAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToArrayAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToArrayAsync();
      task.Should().BeOfType<Task<T[]>>();
      task.Await().Should().BeOfType<T[]>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToList_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToList().Should().BeOfType<IEnumerable<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToListAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToListAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToListAsync();
      task.Should().BeOfType<Task<List<T>>>();
      task.Await().Should().BeOfType<List<T>>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToLinkedList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinkedList_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToLinkedList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(T[] result, IAsyncEnumerable<T> sequence) => sequence.ToLinkedList().Should().BeOfType<T[]>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToLinkedListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToLinkedListAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToLinkedListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToLinkedListAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToLinkedListAsync();
      task.Should().BeOfType<Task<LinkedList<T>>>();
      task.Await().Should().BeOfType<LinkedList<T>>().And.Equal(result);
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

    return;

    static void Validate<T>(IAsyncEnumerable<T> sequence)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyListAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();

    return;

    static void Validate<T>(IAsyncEnumerable<T> sequence)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToHashSet_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToHashSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
      var set = sequence.ToHashSet(comparer);
      set.Should().BeOfType<T[]>().And.Equal(result);
      set.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToHashSetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToHashSetAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
      var task = sequence.ToHashSetAsync(comparer);
      var set = task.Await();
      set.Should().BeOfType<IEnumerable<T>>().And.Equal(result);
      set.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToSortedSet{T}(IAsyncEnumerable{T}, IComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedSet_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToSortedSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IComparer<T> comparer = null)
    {
      var set = sequence.ToSortedSet();
      set.Should().BeOfType<SortedSet<T>>().And.Equal(result.ToSortedSet(comparer));
      set.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToSortedSetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToSortedSetAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IComparer<T> comparer = null)
    {
      var task = sequence.ToSortedSetAsync();
      var set = task.Await();
      set.Should().BeOfType<SortedSet<T>>().And.Equal(result.ToSortedSet(comparer));
      set.Comparer.Should().BeSameAs(comparer ?? Comparer<T>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionary_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    Validate([], Attributes.EmptyAsyncEnumerable(), value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable(), value => value);

    return;

    static void Validate<TKey, TValue>(IEnumerable<TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var dictionary = sequence.ToDictionary(key, comparer);
      dictionary.Should().BeOfType<Dictionary<TKey, TValue>>().And.Equal(result.ToDictionary(key, comparer));
      dictionary.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToDictionaryAsync(value => value, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable(), value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable(), value => value);

    return;

    static void Validate<TKey, TValue>(IEnumerable<TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var task = sequence.ToDictionaryAsync(key, comparer);
      var dictionary = task.Await();
      dictionary.Should().BeOfType<Dictionary<TKey, TValue>>().And.Equal(result.ToDictionary(key, comparer));
      dictionary.Comparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyDictionary_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyDictionary<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToReadOnlyDictionary<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlyDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlyDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlyDictionaryAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToReadOnlyDictionaryAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();

    throw new NotImplementedException();

    return;

    static void Validate<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null)
    {
    }
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTuple<object, object>(null, value => value)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToValueTuple<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

      static void Validate()
      {
      }
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

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToValueTupleAsync<object, object>(null, value => value)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToValueTupleAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToStack{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStack_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToStack<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray().Reverse();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToStack().Should().BeOfType<Stack<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToStackAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToStackAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToStackAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToStackAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray().Reverse();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToStackAsync();
      task.Should().BeOfType<Task<Stack<T>>>();
      task.Await().Should().BeOfType<Stack<T>>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToQueue_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate([], Attributes.EmptyAsyncEnumerable());

      var objects = new Random().GuidSequence(1000).ToArray();
      Validate(objects, objects.ToAsyncEnumerable());
    }

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToQueue().Should().BeOfType<Queue<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToQueueAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToQueueAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToQueueAsync();
      task.Should().BeOfType<Task<Queue<T>>>();
      task.Await().Should().BeOfType<Queue<T>>().And.Equal(result);
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
      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), []);

      var bytes = Attributes.RandomBytes();
      Validate(bytes.ToAsyncEnumerable(), bytes);

      static void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStream()).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), []);

      var bytes = Attributes.RandomBytes();
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);

      static void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        using var stream = sequence.ToMemoryStream();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }
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
      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte>().ToAsyncEnumerable().ToMemoryStreamAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<byte>().ToAsyncEnumerable(), []);

      var bytes = Attributes.RandomBytes();
      Validate(bytes.ToAsyncEnumerable(), bytes);

      static void Validate(IAsyncEnumerable<byte> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((IAsyncEnumerable<byte[]>) null).ToMemoryStreamAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
      AssertionExtensions.Should(() => Enumerable.Empty<byte[]>().ToAsyncEnumerable().ToMemoryStreamAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

      Validate(Enumerable.Empty<byte[]>().ToAsyncEnumerable(), []);

      var bytes = Attributes.RandomBytes();
      Validate(bytes.Chunk(byte.MaxValue).ToAsyncEnumerable(), bytes);

      static void Validate(IAsyncEnumerable<byte[]> sequence, byte[] bytes)
      {
        var task = sequence.ToMemoryStreamAsync();
        using var stream = task.Await();

        stream.Should().HavePosition(0).And.HaveLength(bytes.Length);
        stream.ToArray().Should().Equal(bytes);
      }
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

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToReadOnlySetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToReadOnlySetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToReadOnlySetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToPriorityQueue{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPriorityQueue_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToPriorityQueue<object, object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Enumerable.Empty<(object, object)>().ToAsyncEnumerable());

    var objects = new Random().GuidSequence(100).ToArray();
    var elements = objects.Select((index, value) => (value, index));
    Validate(elements, elements.ToAsyncEnumerable());

    return;

    static void Validate<TElement, TPriority>(IEnumerable<(TElement, TPriority)> result, IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null)
    {
      var queue = sequence.ToPriorityQueue(comparer);
      queue.Should().BeOfType<PriorityQueue<TElement, TPriority>>();
      queue.Comparer.Should().BeSameAs(comparer ?? Comparer<TPriority>.Default);
      queue.UnorderedItems.Should().BeEquivalentTo(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToPriorityQueueAsync{TElement, TPriority}(IAsyncEnumerable{ValueTuple{TElement, TPriority}}, IComparer{TPriority}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToPriorityQueueAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToPriorityQueueAsync<object, object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<(object, object)>().ToAsyncEnumerable().ToQueueAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Enumerable.Empty<(object, object)>().ToAsyncEnumerable());

    var objects = new Random().GuidSequence(100).ToArray();
    var elements = objects.Select((index, value) => (value, index));
    Validate(elements, elements.ToAsyncEnumerable());

    return;

    static void Validate<TElement, TPriority>(IEnumerable<(TElement, TPriority)> result, IAsyncEnumerable<(TElement Element, TPriority Priority)> sequence, IComparer<TPriority> comparer = null)
    {
      var task = sequence.ToPriorityQueueAsync(comparer);
      var queue = task.Await();
      queue.Should().BeOfType<PriorityQueue<TElement, TPriority>>();
      queue.Comparer.Should().BeSameAs(comparer ?? Comparer<TPriority>.Default);
      queue.UnorderedItems.Should().BeEquivalentTo(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableArray{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableArray_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableArray<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToImmutableArray().Should().BeOfType<ImmutableArray<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableArrayAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableArrayAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableArrayAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableArrayAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToImmutableArrayAsync();
      task.Should().BeOfType<Task<ImmutableArray<T>>>();
      task.Await().Should().BeOfType<ImmutableArray<T>>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableList{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableList_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableList<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToImmutableList().Should().BeOfType<ImmutableList<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableListAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableListAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableListAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableListAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToImmutableListAsync();
      task.Should().BeOfType<Task<ImmutableList<T>>>();
      task.Await().Should().BeOfType<ImmutableList<T>>().And.Equal(result);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableHashSet{T}(IAsyncEnumerable{T}, IEqualityComparer{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableHashSet_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableHashSet<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
      var set = sequence.ToImmutableHashSet(comparer);
      set.Should().BeOfType<ImmutableHashSet<T>>().And.Equal(result.ToImmutableHashSet(comparer));
      set.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableHashSetAsync{T}(IAsyncEnumerable{T}, IEqualityComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableHashSetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableHashSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableHashSetAsync(null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IEqualityComparer<T> comparer = null)
    {
      var task = sequence.ToImmutableHashSetAsync(comparer);
      var set = task.Await();
      set.Should().BeOfType<ImmutableHashSet<T>>().And.Equal(result.ToImmutableHashSet(comparer));
      set.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<T>.Default);
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

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IComparer<T> comparer = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedSetAsync{T}(IAsyncEnumerable{T}, IComparer{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedSetAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedSetAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();

    throw new NotImplementedException();

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence, IComparer<T> comparer = null)
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableDictionary_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    Validate([], Attributes.EmptyAsyncEnumerable(), value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable(), value => value);

    return;

    static void Validate<TKey, TValue>(IEnumerable<TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var dictionary = sequence.ToImmutableDictionary(key, comparer);
      dictionary.Should().BeOfType<ImmutableDictionary<TKey, TValue>>().And.Equal(result.ToImmutableDictionary(key, comparer));
      dictionary.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
      dictionary.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IEqualityComparer{TKey}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableDictionaryAsync(value => value, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable(), value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable(), value => value);

    return;

    static void Validate<TKey, TValue>(IEnumerable<TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IEqualityComparer<TKey> comparer = null) where TKey : notnull
    {
      var task = sequence.ToImmutableDictionaryAsync(key, comparer);
      var dictionary = task.Await();
      dictionary.Should().BeOfType<ImmutableDictionary<TKey, TValue>>().And.Equal(result.ToImmutableDictionary(key, comparer));
      dictionary.KeyComparer.Should().BeSameAs(comparer ?? EqualityComparer<TKey>.Default);
      dictionary.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedDictionary{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedDictionary_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedDictionary<object, object>(null, _ => new object())).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionary<object, byte>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("key");

    Validate([], Attributes.EmptyAsyncEnumerable(), value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable(), value => value);

    return;

    static void Validate<TKey, TValue>(IEnumerable<TValue> result, IAsyncEnumerable<TValue> sequence, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
    {
      var dictionary = sequence.ToImmutableSortedDictionary(key, keyComparer, valueComparer);
      dictionary.Should().BeOfType<ImmutableDictionary<TKey, TValue>>().And.Equal(result.ToDictionary(key).ToImmutableSortedDictionary(keyComparer, valueComparer));
      dictionary.KeyComparer.Should().BeSameAs(keyComparer ?? Comparer<TKey>.Default);
      dictionary.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync{TKey, TValue}(IAsyncEnumerable{TValue}, Func{TValue, TKey}, IComparer{TKey}, IEqualityComparer{TValue}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableSortedDictionaryAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableSortedDictionaryAsync<object, object>(null, _ => new object())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Stream.Null.ToAsyncEnumerable().ToImmutableSortedDictionaryAsync<object, byte>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("key").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableSortedDictionaryAsync(value => value, null, null, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate(Attributes.EmptyAsyncEnumerable(), [], value => value);

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects.ToAsyncEnumerable(), objects, value => value);

    return;

    static void Validate<TKey, TValue>(IAsyncEnumerable<TValue> sequence, IEnumerable<TValue> elements, Func<TValue, TKey> key, IComparer<TKey> keyComparer = null, IEqualityComparer<TValue> valueComparer = null) where TKey : notnull
    {
      var task = sequence.ToImmutableSortedDictionaryAsync(key, keyComparer, valueComparer);
      var result = task.Await();
      result.Should().BeOfType<ImmutableSortedDictionary<TKey, TValue>>().And.Equal(elements.ToDictionary(key).ToImmutableSortedDictionary(keyComparer, valueComparer));
      result.KeyComparer.Should().BeSameAs(keyComparer ?? Comparer<TKey>.Default);
      result.ValueComparer.Should().BeSameAs(EqualityComparer<TKey>.Default);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableQueue{T}(IAsyncEnumerable{T})"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableQueue_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableQueue<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("sequence");

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence) => sequence.ToImmutableQueue().Should().BeOfType<ImmutableQueue<T>>().And.Equal(result);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="IAsyncEnumerableExtensions.ToImmutableQueueAsync{T}(IAsyncEnumerable{T}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToImmutableQueueAsync_Method()
  {
    AssertionExtensions.Should(() => IAsyncEnumerableExtensions.ToImmutableQueueAsync<object>(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("sequence").Await();
    AssertionExtensions.Should(() => Attributes.EmptyAsyncEnumerable().ToImmutableQueueAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();

    Validate([], Attributes.EmptyAsyncEnumerable());

    var objects = new Random().GuidSequence(1000).ToArray();
    Validate(objects, objects.ToAsyncEnumerable());

    return;

    static void Validate<T>(IEnumerable<T> result, IAsyncEnumerable<T> sequence)
    {
      var task = sequence.ToImmutableQueueAsync();
      task.Should().BeOfType<Task<ImmutableQueue<T>>>();
      task.Await().Should().BeOfType<ImmutableQueue<T>>().And.Equal(result);
    }
  }
}