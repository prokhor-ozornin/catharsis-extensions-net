using System;
using Catharsis.Commons.Extensions;
using Xunit;

namespace Catharsis.Commons.Domain
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <typeparam name="ENTITY"></typeparam>
  public abstract class EntityUnitTests<ENTITY>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="PROPERTY"></typeparam>
    /// <param name="property"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="property"/> is <see cref="string.Empty"/> string.</exception>
    protected void TestEquality<PROPERTY>(string property, PROPERTY oldValue, PROPERTY newValue)
    {
      Assertion.NotEmpty(property);

      var entity = typeof(ENTITY).NewInstance();

      Assert.False(entity.Equals(null));
      Assert.True(entity.Equals(entity));
      Assert.True(entity.Equals(typeof(ENTITY).NewInstance()));
      
      Assert.True(typeof(ENTITY).NewInstance().SetProperty(property, oldValue).Equals(typeof(ENTITY).NewInstance().SetProperty(property, oldValue)));
      Assert.False(typeof(ENTITY).NewInstance().SetProperty(property, oldValue).Equals(typeof(ENTITY).NewInstance().SetProperty(property, newValue)));
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <typeparam name="PROPERTY"></typeparam>
    /// <param name="property"></param>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    /// <exception cref="ArgumentNullException">If <paramref name="property"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="property"/> is <see cref="string.Empty"/> string.</exception>
    protected void TestHashCode<PROPERTY>(string property, PROPERTY oldValue, PROPERTY newValue)
    {
      Assertion.NotEmpty(property);

      var entity = typeof(ENTITY).NewInstance();

      Assert.True(entity.GetHashCode() == entity.GetHashCode());
      Assert.True(entity.GetHashCode() == typeof(ENTITY).NewInstance().GetHashCode());

      Assert.True(typeof(ENTITY).NewInstance().SetProperty(property, oldValue).GetHashCode() == typeof(ENTITY).NewInstance().SetProperty(property, oldValue).GetHashCode());
      Assert.True(typeof(ENTITY).NewInstance().SetProperty(property, oldValue).GetHashCode() != typeof(ENTITY).NewInstance().SetProperty(property, newValue).GetHashCode());
    }
  }
}