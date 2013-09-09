using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public static class DomainExtensions
  {
    /// <summary>
    ///   <para>Filters sequence of entities, leaving only allowed ones.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <returns>Filtered sequence of allowed entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Allowed<T>(this IEnumerable<T> entities) where T : IAccessable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.AccessGranted);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving only disallowed ones.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <returns>Filtered sequence of disallowed entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Denied<T>(this IEnumerable<T> entities) where T : IAccessable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.AccessGranted == false);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given author's identifier.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="authorId">Identifier of author to search for.</param>
    /// <returns>Filtered sequence of entities with specified author.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithAuthor<T>(this IEnumerable<T> entities, string authorId) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity !=null && entity.AuthorId == authorId);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by author in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByAuthor<T>(this IEnumerable<T> entities) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.AuthorId);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by author in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByAuthorDescending<T>(this IEnumerable<T> entities) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.AuthorId);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given description text.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="description">Description text for search for.</param>
    /// <returns>Filtered sequence of entities with specified description.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithDescription<T>(this IEnumerable<T> entities, string description) where T : IDescriptable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Description == description);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with height in specified range.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="from">Lower bound of height range.</param>
    /// <param name="to">Upper bound of height range.</param>
    /// <returns>Filtered sequence of entities with height ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithHeight<T>(this IEnumerable<T> entities, short? from = null, short? to = null) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height >= from.Value && entity.Height <= to.Value);
      }
      
      if (@from.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height >= from.Value);
      }
      
      if (to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by height in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByHeight<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Height);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by height in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByHeightDescending<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Height);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with width in specified range.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="from">Lower bound of width range.</param>
    /// <param name="to">Upper bound of width range.</param>
    /// <returns>Filtered sequence of entities with width ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithWidth<T>(this IEnumerable<T> entities, short? from = null, short? to = null) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width >= from.Value && entity.Width <= to.Value);
      }
      
      if (from.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width >= from.Value);
      }
      
      if (to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by width in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByWidth<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Width);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by width in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByWidthDescending<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Width);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given email address.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="email">Email address to search for.</param>
    /// <returns>Filtered sequence of entities with specified email address.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithEmail<T>(this IEnumerable<T> entities, string email) where T : IEmailable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Email == email);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given internet address.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="inetAddress">Internet network address to search for.</param>
    /// <returns>Filtered sequence of entities with specified internet address.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithInetAddress<T>(this IEnumerable<T> entities, string inetAddress) where T : IInetAddressable
    {
      Assertion.NotNull(entities);
    
      return entities.Where(entity => entity != null && entity.InetAddress == inetAddress);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given ISO language code of their text content.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="language">ISO language code to search for.</param>
    /// <returns>Filtered sequence of entities with text content having specified ISO language code.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithLanguage<T>(this IEnumerable<T> entities, string language) where T : ILocalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Language == language);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given culture of their text content.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="culture">Culture to search for.</param>
    /// <returns>Filtered sequence of entities with text content having specified culture.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithCulture<T>(this IEnumerable<T> entities, CultureInfo culture) where T : ILocalizable
    {
      Assertion.NotNull(entities);

      return entities.WithLanguage(culture != null ? culture.TwoLetterISOLanguageName : null);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given name.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="name">Name to search for.</param>
    /// <returns>Filtered sequence of entities with specified name.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithName<T>(this IEnumerable<T> entities, string name) where T : INameable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Name == name);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by name in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByName<T>(this IEnumerable<T> entities) where T : INameable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Name);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by name in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByNameDescending<T>(this IEnumerable<T> entities) where T : INameable
    {
      Assertion.NotNull(entities);
      
      return entities.OrderByDescending(entity => entity.Name);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given first name.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="name">First name to search for.</param>
    /// <returns>Filtered sequence of entities with specified first name.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithFirstName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.NameFirst == name);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by first name in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByFirstName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameFirst);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by first name in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByFirstNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameFirst);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given last name.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="name">Last name to search for.</param>
    /// <returns>Filtered sequence of entities with specified last name.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithLastName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);
      
      return entities.Where(entity => entity != null && entity.NameLast == name);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by last name in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByLastName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameLast);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by last name in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByLastNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameLast);
    }
    
    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given middle name.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="name">Middle name to search for.</param>
    /// <returns>Filtered sequence of entities with specified middle name.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithMiddleName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.NameMiddle == name);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by middle name in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByMiddleName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameMiddle);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by middle name in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByMiddleNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameMiddle);
    }

    /// <summary>
    ///   <para>Returns a full name of entity that implements <see cref="IPersonalizable"/> interface.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entity">Named subject entity.</param>
    /// <returns>Full name of named <paramref name="entity"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is a <c>null</c> reference.</exception>
    public static string GetFullName<T>(this T entity) where T : IPersonalizable
    {
      Assertion.NotNull(entity);

      return string.Format(entity.NameMiddle.Whitespace() ? "{0} {1}".FormatValue(entity.NameLast, entity.NameFirst) : "{0} {1} {2}".FormatValue(entity.NameLast, entity.NameFirst, entity.NameMiddle));
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with size in specified range.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="from">Lower bound of size range.</param>
    /// <param name="to">Upper bound of size range.</param>
    /// <returns>Filtered sequence of entities with duration ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithSize<T>(this IEnumerable<T> entities, long? from = null, long? to = null) where T : ISizable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size >= from.Value && entity.Size <= to.Value);
      }

      if (from.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size >= from.Value);
      }

      if (to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by size in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderBySize<T>(this IEnumerable<T> entities) where T : ISizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Size);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by size in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderBySizeDescending<T>(this IEnumerable<T> entities) where T : ISizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Size);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given status.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="status">Status to search for.</param>
    /// <returns>Filtered sequence of entities with specified status.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithStatus<T>(this IEnumerable<T> entities, string status) where T : IStatusable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Status == status);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given text.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="text">Text to search for.</param>
    /// <returns>Filtered sequence of entities with specified text.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithText<T>(this IEnumerable<T> entities, string text) where T : ITextable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Text == text);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with creation date and time in specified range.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="from">Lower bound of date and time range.</param>
    /// <param name="to">Upper bound of date and time range.</param>
    /// <returns>Filtered sequence of entities with creation date and time ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> CreatedOn<T>(this IEnumerable<T> entities, DateTime? from = null, DateTime? to = null) where T : ITimeable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated >= from.Value && entity.DateCreated <= to.Value);
      }

      if (from.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated >= from.Value);
      }

      if (to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by creation date in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByCreatedOn<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.DateCreated);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by creation date in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByCreatedOnDescending<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.DateCreated);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with last modification date and time in specified range.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="from">Lower bound of date and time range.</param>
    /// <param name="to">Upper bound of date and time range.</param>
    /// <returns>Filtered sequence of entities with last modification date and time ranging inclusively from <paramref name="from"/> to <paramref name="to"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> UpdatedOn<T>(this IEnumerable<T> entities, DateTime? from = null, DateTime? to = null) where T : ITimeable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated >= from.Value && entity.LastUpdated <= to.Value);
      }

      if (from.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated >= from.Value);
      }

      if (to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by last update date in ascending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByUpdatedOn<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.LastUpdated);
    }

    /// <summary>
    ///   <para>Sorts sequence of entities by last update date in descending order.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities for sorting.</param>
    /// <returns>Sorted sequence of entities.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByUpdatedOnDescending<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.LastUpdated);
    }
    
    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given type.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="type">Type to search for.</param>
    /// <returns>Filtered sequence of entities with specified type.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithType<T>(this IEnumerable<T> entities, int type) where T : ITypeable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Type == type);
    }

    /// <summary>
    ///   <para>Filters sequence of entities, leaving those with given URL address.</para>
    /// </summary>
    /// <typeparam name="T">Type of entities.</typeparam>
    /// <param name="entities">Source sequence of entities to filter.</param>
    /// <param name="url">URL to search for.</param>
    /// <returns>Filtered sequence of entities with specified URL.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithUrl<T>(this IEnumerable<T> entities, string url) where T : IUrlAddressable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Url == url);
    }
  }
}