using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="Text"/>.</para>
  ///   <seealso cref="Text"/>
  /// </summary>
  public static class TextExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of texts, leaving those belonging to specified category.</para>
    /// </summary>
    /// <param name="texts">Source sequence of texts to filter.</param>
    /// <param name="category">Category of texts to search for.</param>
    /// <returns>Filtered sequence of texts with specified category.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="texts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Text> InTextsCategory(this IEnumerable<Text> texts, TextsCategory category)
    {
      Assertion.NotNull(texts);
      
      return category != null ? texts.Where(text => text != null && text.Category.Id == category.Id) : texts.Where(text => text != null && text.Category == null);
    }

    /// <summary>
    ///   <para>Sorts sequence of texts by category's name in ascending order.</para>
    /// </summary>
    /// <param name="texts"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="texts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Text> OrderByTextsCategoryName(this IEnumerable<Text> texts)
    {
      Assertion.NotNull(texts);

      return texts.OrderBy(text => text.Category.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If p</exception>
    public static IEnumerable<Text> OrderByTextsCategoryNameDescending(this IEnumerable<Text> texts)
    {
      Assertion.NotNull(texts);

      return texts.OrderByDescending(text => text.Category.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of texts, leaving those written by specified person.</para>
    /// </summary>
    /// <param name="texts">Source sequence of texts to filter.</param>
    /// <param name="person">Author of texts to search for.</param>
    /// <returns>Filtered sequence of texts with specified author.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="texts"/> or <paramref name="person"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Text> WithPerson(this IEnumerable<Text> texts, Person person)
    {
      Assertion.NotNull(texts);
      Assertion.NotNull(person);

      return texts.Where(text => text != null && text.Person.Id == person.Id);
    }
  }
}