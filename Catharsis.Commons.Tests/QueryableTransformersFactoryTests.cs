using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="QueryableTransformersFactory"/>.</para>
  /// </summary>
  public sealed class QueryableTransformersFactoryTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="QueryableTransformersFactory.Transformers"/> property.</para>
    /// </summary>
    [Fact]
    public void Transformers_Property()
    {
      Assert.True(QueryableTransformersFactory.Transformers != null);
      Assert.True(ReferenceEquals(QueryableTransformersFactory.Transformers, QueryableTransformersFactory.Transformers));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="QueryableTransformersFactory.Register{FROM, TO}(IQueryableTransformer{FROM, TO})"/> method.</para>
    /// </summary>
    [Fact]
    public void Register_Method()
    {
      Assert.Throws<ArgumentNullException>(() => QueryableTransformersFactory.Transformers.Register<object, object>(null));

      QueryableTransformersFactory.Transformers.Clear();
       Assert.True(QueryableTransformersFactory.Transformers.Get<object, string>() == null);
      var transformer = new MockQueryableTransformer();
      Assert.True(ReferenceEquals(QueryableTransformersFactory.Transformers.Register(transformer), QueryableTransformersFactory.Transformers));
      Assert.True(ReferenceEquals(QueryableTransformersFactory.Transformers.Get<object, string>(), transformer));
      Assert.True(ReferenceEquals(QueryableTransformersFactory.Transformers.Get<object, string>(), QueryableTransformersFactory.Transformers.Get<object, string>()));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="QueryableTransformersFactory.Get{FROM, TO}()"/> method.</para>
    /// </summary>
    [Fact]
    public void Get_Method()
    {
      QueryableTransformersFactory.Transformers.Clear();
      Assert.True(QueryableTransformersFactory.Transformers.Get<object, string>() == null);
      var transformer = new MockQueryableTransformer();
      QueryableTransformersFactory.Transformers.Register(transformer);
      Assert.True(ReferenceEquals(QueryableTransformersFactory.Transformers.Get<object, string>(), transformer));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="QueryableTransformersFactory.Clear()"/> method.</para>
    /// </summary>
    [Fact]
    public void Clear_Method()
    {
      QueryableTransformersFactory.Transformers.Clear();
      Assert.True(QueryableTransformersFactory.Transformers.Get<object, string>() == null);
      var transformer = new MockQueryableTransformer();
      QueryableTransformersFactory.Transformers.Register(transformer);
      QueryableTransformersFactory.Transformers.Clear();
      Assert.True(QueryableTransformersFactory.Transformers.Get<object, string>() == null);
    }

    private sealed class MockQueryableTransformer : IQueryableTransformer<object, string>
    {
      public IQueryable<string> Transform(IQueryable<object> from)
      {
        return from.Select(x => x.ToString());
      }
    }
  }
}