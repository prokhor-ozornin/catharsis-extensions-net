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
  }
}