using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catharsis.Commons.Extensions;

namespace Catharsis.Commons.Domain
{
  public static class DomainExtensions
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Allowed<T>(this IEnumerable<T> entities) where T : IAccessable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.AccessGranted);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> Denied<T>(this IEnumerable<T> entities) where T : IAccessable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.AccessGranted == false);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="authorId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithAuthor<T>(this IEnumerable<T> entities, string authorId) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity !=null && entity.AuthorId == authorId);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByAuthor<T>(this IEnumerable<T> entities) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.AuthorId);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByAuthorDescending<T>(this IEnumerable<T> entities) where T : IAuthorable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.AuthorId);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithDescription<T>(this IEnumerable<T> entities, string description) where T : IDescriptable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Description == description);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithHeight<T>(this IEnumerable<T> entities, short? from = null, short? to = null) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height >= from.Value && entity.Height <= to.Value);
      }
      
      if (from.HasValue && !to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height >= from.Value);
      }
      
      if (!from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Height <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByHeight<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Height);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByHeightDescending<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Height);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithWidth<T>(this IEnumerable<T> entities, short? from = null, short? to = null) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width >= from.Value && entity.Width <= to.Value);
      }
      
      if (from.HasValue && !to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width >= from.Value);
      }
      
      if (!from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Width <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByWidth<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Width);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByWidthDescending<T>(this IEnumerable<T> entities) where T : IDimensionable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Width);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithEmail<T>(this IEnumerable<T> entities, string email) where T : IEmailable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Email == email);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="inetAddress"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithInetAddress<T>(this IEnumerable<T> entities, string inetAddress) where T : IInetAddressable
    {
      Assertion.NotNull(entities);
    
      return entities.Where(entity => entity != null && entity.InetAddress == inetAddress);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithLanguage<T>(this IEnumerable<T> entities, string language) where T : ILocalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Language == language);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithCulture<T>(this IEnumerable<T> entities, CultureInfo culture) where T : ILocalizable
    {
      Assertion.NotNull(entities);

      return entities.WithLanguage(culture != null ? culture.ThreeLetterISOLanguageName : null);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithName<T>(this IEnumerable<T> entities, string name) where T : INameable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Name == name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByName<T>(this IEnumerable<T> entities) where T : INameable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByNameDescending<T>(this IEnumerable<T> entities) where T : INameable
    {
      Assertion.NotNull(entities);
      
      return entities.OrderByDescending(entity => entity.Name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithFirstName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.NameFirst == name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByFirstName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameFirst);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByFirstNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameFirst);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithLastName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);
      
      return entities.Where(entity => entity != null && entity.NameLast == name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByLastName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameLast);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByLastNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameLast);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithMiddleName<T>(this IEnumerable<T> entities, string name) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.NameMiddle == name);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByMiddleName<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.NameMiddle);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByMiddleNameDescending<T>(this IEnumerable<T> entities) where T : IPersonalizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.NameMiddle);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is a <c>null</c> reference.</exception>
    public static string GetFullName<T>(this T entity) where T : IPersonalizable
    {
      Assertion.NotNull(entity);

      return string.Format(entity.NameMiddle.Whitespace() ? "{0} {1}".FormatValue(entity.NameLast, entity.NameFirst) : "{0} {1} {2}".FormatValue(entity.NameLast, entity.NameFirst, entity.NameMiddle));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithSize<T>(this IEnumerable<T> entities, long? from = null, long? to = null) where T : ISizable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size >= from.Value && entity.Size <= to.Value);
      }

      if (from.HasValue && !to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size >= from.Value);
      }

      if (!from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.Size <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderBySize<T>(this IEnumerable<T> entities) where T : ISizable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.Size);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderBySizeDescending<T>(this IEnumerable<T> entities) where T : ISizable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.Size);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithStatus<T>(this IEnumerable<T> entities, string status) where T : IStatusable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Status == status);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithText<T>(this IEnumerable<T> entities, string text) where T : ITextable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Text == text);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> CreatedOn<T>(this IEnumerable<T> entities, DateTime? from = null, DateTime? to = null) where T : ITimeable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated >= from.Value && entity.DateCreated <= to.Value);
      }

      if (from.HasValue && !to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated >= from.Value);
      }

      if (!from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.DateCreated <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByCreatedOn<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByCreatedOnDescending<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.DateCreated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> UpdatedOn<T>(this IEnumerable<T> entities, DateTime? from = null, DateTime? to = null) where T : ITimeable
    {
      Assertion.NotNull(entities);

      if (from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated >= from.Value && entity.LastUpdated <= to.Value);
      }

      if (from.HasValue && !to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated >= from.Value);
      }

      if (!from.HasValue && to.HasValue)
      {
        return entities.Where(entity => entity != null && entity.LastUpdated <= to.Value);
      }

      return entities;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByUpdatedOn<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderBy(entity => entity.LastUpdated);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> OrderByUpdatedOnDescending<T>(this IEnumerable<T> entities) where T : ITimeable
    {
      Assertion.NotNull(entities);

      return entities.OrderByDescending(entity => entity.LastUpdated);
    }
    
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithType<T>(this IEnumerable<T> entities, int type) where T : ITypeable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Type == type);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="entities"/> is a <c>null</c> reference.</exception>
    public static IEnumerable<T> WithUrl<T>(this IEnumerable<T> entities, string url) where T : IUrlAddressable
    {
      Assertion.NotNull(entities);

      return entities.Where(entity => entity != null && entity.Url == url);
    }
  }
}