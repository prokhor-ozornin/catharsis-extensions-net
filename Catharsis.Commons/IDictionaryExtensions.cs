using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="IDictionary{TKey,TValue}"/>.</para>
  ///   <seealso cref="IDictionary{TKey,TValue}"/>
  /// </summary>
  public static class IDictionaryExtensions
  {
    /// <summary>
    ///   <para>Adds specified element with key to the dictionary and returns dictionary reference back to perform further operations.</para>
    ///   <seealso cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Dictionary to add element to.</param>
    /// <param name="key">Key, under which the element will be stored in the dictionary.</param>
    /// <param name="value">Value to store in the dictionary.</param>
    /// <returns>Reference to the supplied dictionary <paramref name="dictionary"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="key"/> is a <c>null</c> reference.</exception>
    public static IDictionary<KEY, VALUE> AddNext<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, KEY key, VALUE value)
    {
      Assertion.NotNull(dictionary);
      Assertion.NotNull(key);

      dictionary.Add(key, value);
      return dictionary;
    }

    /// <summary>
    ///   <para>Wraps specified dictionary, restricting the type of key/value pairs it may hold. Only the pairs that match the specified condition may be added to it.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <param name="predicate">Predicate that specifies the condition for key/value pairs to match.</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="dictionary"/> or <paramref name="predicate"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped dictionary contains any key/value pairs that do not match specified condition predicate, they are removed on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="ArgumentException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.this[TKey]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsConstrained<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, Predicate<KeyValuePair<KEY, VALUE>> predicate)
    {
      Assertion.NotNull(dictionary);
      Assertion.NotNull(predicate);

      return new ConstrainedDictionary<KEY, VALUE>(dictionary, predicate);
    }

    /// <summary>
    ///   <para>Wraps specified dictionary, making it "read-only", so that all methods that modify its state will throw a <see cref="NotSupportedException"/>.</para>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>The list of methods that throw <see cref="NotSupportedException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Remove(TKey)"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsImmutable<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary)
    {
      Assertion.NotNull(dictionary);

      return new ImmutableDictionary<KEY, VALUE>(dictionary);
    }

    /// <summary>
    ///   <para>Wraps specified dictionary, restricting its maximum size, so that it does not contain more than specified maximum number of key/value pairs.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <param name="size">Dictionary maximum size (number of key/value pairs allowed).</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If key/values pairs count is the wrapped dictionary is greater than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.this[TKey]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsMaxSized<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, int size)
    {
      Assertion.NotNull(dictionary);

      return new SizedDictionary<KEY, VALUE>(dictionary, size);
    }


    /// <summary>
    ///   <para>Wraps specified dictionary, restricting its minimum size, so that it does not contain less that specified minimum number of key/value pairs.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <param name="size">Dictionary minimum size (number of key/value pairs required).</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <exception cref="InvalidOperationException">If key/values pairs count in the wrapped dictionary is lesser than <paramref name="size"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Remove(TKey)"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsMinSized<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, int size)
    {
      Assertion.NotNull(dictionary);

      return new SizedDictionary<KEY, VALUE>(dictionary, null, size);
    }

    /// <summary>
    ///   <para>Wraps specified dictionary, making it "not-nullable", so that all methods that modify its state do no accept <c>null</c> references for key/values.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped dictionary contains any key/value pairs that are <c>null</c> references, they are removed on wrapping.</para>
    ///   <para>The list of affected methods is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.ContainsKey(TKey)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Remove(TKey)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.this[TKey]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsNonNullable<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary)
    {
      Assertion.NotNull(dictionary);

      return new NonNullableDictionary<KEY, VALUE>(dictionary);
    }

    /// <summary>
    ///   <para>Wraps specified dictionary, restricting its maximum and minimum size, so that it does not contain less than specified minimum and more than specified maximum number of key/value pairs.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsUnique{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <param name="max">Dictionary maximum size (number of key/value pairs allowed).</param>
    /// <param name="min">Collection minimum size (number of elements required).</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="max"/> is lesser than <paramref name="min"/>.</exception>
    /// <exception cref="InvalidOperationException">If key/values pairs count in the wrapped dictionary is lesser than <paramref name="min"/> or greater than <paramref name="max"/>.</exception>
    /// <remarks>
    ///   <para>The list of methods that may throw <see cref="InvalidOperationException"/> due to size constraints violation is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Remove(TKey)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.this[TKey]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsSized<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary, int? max = null, int min = 0)
    {
      Assertion.NotNull(dictionary);

      return new SizedDictionary<KEY, VALUE>(dictionary, max, min);
    }
    
    /// <summary>
    ///   <para>Wraps specified dictionary, so that wrapped dictionary cannot contain any two key/values pairs that are considered equal according to the <c>Equals()</c> method.</para>
    ///   <seealso cref="AsImmutable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsNonNullable{TKey, TValue}(IDictionary{TKey, TValue})"/>
    ///   <seealso cref="AsSized{TKey, TValue}(IDictionary{TKey, TValue}, int?, int)"/>
    ///   <seealso cref="AsMaxSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsMinSized{TKey, TValue}(IDictionary{TKey, TValue}, int)"/>
    ///   <seealso cref="AsConstrained{TKey, TValue}(IDictionary{TKey, TValue}, Predicate{KeyValuePair{TKey, TValue}})"/>
    /// </summary>
    /// <typeparam name="KEY">Type of keys, stored in a dictionary.</typeparam>
    /// <typeparam name="VALUE">Type of values, stored in a dictionary.</typeparam>
    /// <param name="dictionary">Target dictionary for wrapping.</param>
    /// <returns>Wrapped dictionary reference.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="dictionary"/> is a <c>null</c> reference.</exception>
    /// <remarks>
    ///   <para>If wrapped collection contains any two equal elements, only the first of them is added on wrapping.</para>
    ///   <para>The list of affected methods that may throw <see cref="InvalidOperationException"/> is as follows :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IDictionary{TKey, TValue}.Add(TKey, TValue)"/></description></item>
    ///     <item><description><see cref="IDictionary{TKey, TValue}.this[TKey]"/></description></item>
    ///   </list>
    /// </remarks>
    public static IDictionary<KEY, VALUE> AsUnique<KEY, VALUE>(this IDictionary<KEY, VALUE> dictionary)
    {
      Assertion.NotNull(dictionary);

      return new UniqueDictionary<KEY, VALUE>(dictionary);
    }
  }
}