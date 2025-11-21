using System.Collections.ObjectModel;
using static System.Math;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for collections of various types.</para>
/// </summary>
/// <seealso cref="IList{T}"/>
public static class IListExtensions
{
  /// <param name="list"></param>
  /// <typeparam name="T"></typeparam>
  extension<T>(IList<T> list)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="random"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    public IList<T> Randomize(Random random = null)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));

      if (list.Count == 0)
      {
        return list;
      }

      var randomizer = random ?? new Random();

      var n = list.Count;

      while (n > 1)
      {
        n--;
        var k = randomizer.Next(n + 1);
        (list[k], list[n]) = (list[n], list[k]);
      }

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="firstIndex"></param>
    /// <param name="secondIndex"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public IList<T> Swap(int firstIndex, int secondIndex)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (firstIndex < 0 || firstIndex > list.Count) throw new ArgumentOutOfRangeException(nameof(firstIndex));
      if (secondIndex < 0 || secondIndex > list.Count) throw new ArgumentOutOfRangeException(nameof(secondIndex));

      (list[firstIndex], list[secondIndex]) = (list[secondIndex], list[firstIndex]);

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="filler"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="list"/> or <paramref name="filler"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Fill{T}(IList{T}, Func{int, T}, int?, int?)"/>
    public IList<T> Fill(Func<T> filler, int? offset = null, int? count = null) => list?.Fill(_ => filler(), offset, count) ?? throw new ArgumentNullException(nameof(filler));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="filler"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="list"/> or <paramref name="filler"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Fill{T}(IList{T}, Func{T}, int?, int?)"/>
    public IList<T> Fill(Func<int, T> filler, int? offset = null, int? count = null)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (filler is null) throw new ArgumentNullException(nameof(filler));
      if (offset is < 0 || offset > list.Count) throw new ArgumentOutOfRangeException(nameof(offset));
      if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));
    
      var fromIndex = offset ?? 0;
      var toIndex = Min(list.Count, count is not null ? fromIndex + count.Value : list.Count - fromIndex);

      for (var index = fromIndex; index < toIndex; index++)
      {
        list[index] = filler(index);
      }

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    public IReadOnlyList<T> AsReadOnly() => list is not null ? new ReadOnlyCollection<T>(list) : throw new ArgumentNullException(nameof(list));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="position"></param>
    /// <param name="element"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    /// <seealso cref="With{T}(IList{T}, int, IEnumerable{T})"/>
    /// <seealso cref="With{T}(IList{T}, int, T[])"/>
    public IList<T> With(int position, T element)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (position < 0) throw new ArgumentOutOfRangeException(nameof(position));

      list[position] = element;

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="list"/> or <paramref name="elements"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="With{T}(IList{T}, int, T)"/>
    /// <seealso cref="With{T}(IList{T}, int, T[])"/>
    public IList<T> With(int offset, IEnumerable<T> elements)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (elements is null) throw new ArgumentNullException(nameof(elements));
      if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));

      elements.ForEach((index, element) => list.Insert(offset + index, element));

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="elements"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="With{T}(IList{T}, int, T)"/>
    /// <seealso cref="With{T}(IList{T}, int, IEnumerable{T})"/>
    public IList<T> With(int offset, params T[] elements) => list.With(offset, elements as IEnumerable<T>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="list"/> or <paramref name="positions"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{T}(IList{T}, int[])"/>
    /// <seealso cref="Without{T}(IList{T}, int, int?, Predicate{T})"/>
    public IList<T> Without(IEnumerable<int> positions)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (positions is null) throw new ArgumentNullException(nameof(positions));

      foreach (var position in positions)
      {
        list.RemoveAt(position);
      }

      return list;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="positions"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    /// <seealso cref="Without{T}(IList{T}, IEnumerable{int})"/>
    /// <seealso cref="Without{T}(IList{T}, int, int?, Predicate{T})"/>
    public IList<T> Without(params int[] positions) => list.Without(positions as IEnumerable<int>);

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <param name="condition"></param>
    /// <returns>Back self-reference to the given <paramref name="list"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="list"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <seealso cref="Without{T}(IList{T}, IEnumerable{int})"/>
    /// <seealso cref="Without{T}(IList{T}, int[])"/>
    public IList<T> Without(int offset, int? count = null, Predicate<T> condition = null)
    {
      if (list is null) throw new ArgumentNullException(nameof(list));
      if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
      if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));

      for (var i = offset; i < offset + (count ?? list.Count - offset); i++)
      {
        if (condition is null || condition(list[i]))
        {
          list.RemoveAt(i);
        }
      }

      return list;
    }
  }
}