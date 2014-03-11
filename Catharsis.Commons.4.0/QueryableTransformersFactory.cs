using System;
using System.Collections.Generic;
using System.Linq;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Manages cached <see cref="IQueryableTransformer{FROM, TO}"/> implementations.</para>
  /// </summary>
  public sealed class QueryableTransformersFactory
  {
    private static readonly IDictionary<Type, IDictionary<Type, object>> cache = new Dictionary<Type, IDictionary<Type, object>>();
    private static readonly QueryableTransformersFactory instance = new QueryableTransformersFactory();

    private QueryableTransformersFactory()
    {
    }

    /// <summary>
    ///   <para>Instance of transformers factory.</para>
    /// </summary>
    public static QueryableTransformersFactory Transformers
    {
      get { return instance; }
    }

    /// <summary>
    ///   <para>Registers and adds to cache new <see cref="IQueryableTransformer{FROM, TO}"/> implementation.</para>
    /// </summary>
    /// <typeparam name="FROM">Source type of <see cref="IQueryable{T}"/> object.</typeparam>
    /// <typeparam name="TO">Destination type of <see cref="IQueryable{T}"/> object.</typeparam>
    /// <param name="transformer">Transformer instance to be registered.</param>
    /// <returns>Back reference to the current transformers factory.</returns>
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
    ///   <para>Retrieves previously registered <see cref="IQueryableTransformer{FROM, TO}"/> instance.</para>
    /// </summary>
    /// <typeparam name="FROM">Source type of <see cref="IQueryable{T}"/> object.</typeparam>
    /// <typeparam name="TO">Destination type of <see cref="IQueryable{T}"/> object.</typeparam>
    /// <returns>Registered transformer instance or a <c>null</c> reference in case it could not be found.</returns>
    public IQueryableTransformer<FROM, TO> Get<FROM, TO>()
    {
      return cache.ContainsKey(typeof(FROM)) && cache[typeof(FROM)].ContainsKey(typeof(TO)) ? cache[typeof(FROM)][typeof(TO)].To<IQueryableTransformer<FROM, TO>>() : null;
    }

    /// <summary>
    ///   <para>Removes all registered and cached <see cref="IQueryableTransformer{FROM, TO}"/> instances.</para>
    /// </summary>
    /// <returns>Back reference to the current transformers factory.</returns>
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