using System.Web;

namespace Catharsis.Extensions;

/// <summary>
///   <para>Extension methods for URI/URL type.</para>
/// </summary>
/// <seealso cref="UriBuilder"/>
public static class UriBuilderExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="builder"/> is <see langword="null"/>.</exception>
  public static UriBuilder Empty(this UriBuilder builder)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));

    builder.Fragment = string.Empty;
    builder.Host = string.Empty;
    builder.Password = string.Empty;
    builder.Path = string.Empty;
    builder.Port = -1;
    builder.Query = string.Empty;
    builder.Scheme = string.Empty;
    builder.UserName = string.Empty;

    return builder;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="parameters"/> is <see langword="null"/>.</exception>
  public static UriBuilder WithQuery(this UriBuilder builder, IReadOnlyDictionary<string, object> parameters)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (parameters is null) throw new ArgumentNullException(nameof(parameters));

    return builder.WithQuery(parameters.ToValueTuple().AsArray());
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="parameters"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If either <paramref name="builder"/> or <paramref name="parameters"/> is <see langword="null"/>.</exception>
  public static UriBuilder WithQuery(this UriBuilder builder, params (string Name, object Value)[] parameters)
  {
    if (builder is null) throw new ArgumentNullException(nameof(builder));
    if (parameters is null) throw new ArgumentNullException(nameof(parameters));

    var query = HttpUtility.ParseQueryString(builder.Query);

    foreach (var parameter in parameters)
    {
      query.Add(parameter.Name, parameter.Value?.ToInvariantString());
    }

    builder.Query = query.ToString() ?? string.Empty;

    return builder;
  }
}