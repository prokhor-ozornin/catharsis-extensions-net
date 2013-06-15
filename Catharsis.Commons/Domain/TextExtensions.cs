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
    ///   <para></para>
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="texts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Text> InTextsCategory(this IEnumerable<Text> texts, TextsCategory category)
    {
      Assertion.NotNull(texts);
      
      return category != null ? texts.Where(text => text != null && text.Category.Id == category.Id) : texts.Where(text => text != null && text.Category == null);
    }

    /// <summary>
    ///   <para></para>
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
    ///   <para></para>
    /// </summary>
    /// <param name="texts"></param>
    /// <param name="person"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="texts"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<Text> WithPerson(this IEnumerable<Text> texts, Person person)
    {
      Assertion.NotNull(texts);

      return person != null ? texts.Where(text => text != null && text.Person.Id == person.Id) : texts.Where(text => text != null && text.Person == null);
    }
  }
}