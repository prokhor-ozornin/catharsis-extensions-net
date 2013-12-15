using System;
using System.Collections.Generic;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public sealed class QueryableTransformersFactory
  {
    private static readonly IDictionary<Type, IDictionary<Type, object>> cache = new Dictionary<Type, IDictionary<Type, object>>();
    private static readonly QueryableTransformersFactory instance = new QueryableTransformersFactory();

    private QueryableTransformersFactory()
    {
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    public static QueryableTransformersFactory Transformers
    {
      get { return instance; }
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="FROM"></typeparam>
    /// <typeparam name="TO"></typeparam>
    /// <param name="transformer"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="transformer"/> is a <c>null</c> reference.</exception>
    public QueryableTransformersFactory Register<FROM, TO>(IQueryableTransformer<FROM, TO> transformer)
    {
      Assertion.NotNull(transformer);

      lock (cache)
      {
        IDictionary<Type, object> transformers;
        if (!cache.TryGetValue(typeof(FROM), out transformers))
        {
          transformers = new Dictionary<Type, object>();
          cache[typeof(FROM)] = transformers;
        }

        if (!transformers.ContainsKey(typeof(TO)))
        {
          transformers[typeof(TO)] = transformer;
        }
      }

      return this;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="FROM"></typeparam>
    /// <typeparam name="TO"></typeparam>
    /// <returns></returns>
    public IQueryableTransformer<FROM, TO> Get<FROM, TO>()
    {
      return cache.ContainsKey(typeof(FROM)) && cache[typeof(FROM)].ContainsKey(typeof(TO)) ? cache[typeof(FROM)][typeof(TO)].To<IQueryableTransformer<FROM, TO>>() : null;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public QueryableTransformersFactory Clear()
    {
      lock (cache)
      {
        cache.Clear();
      }
      return this;
    }
  }
}