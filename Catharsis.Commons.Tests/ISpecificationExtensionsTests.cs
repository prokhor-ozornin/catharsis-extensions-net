using System;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ISpecificationExtensions"/>.</para>
  /// </summary>
  public sealed class ISpecificationExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ISpecificationExtensions.Conforms{T}(ISpecification{T}, T)"/> method.</para>
    /// </summary>
    [Fact]
    public void Conforms_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ISpecificationExtensions.Conforms(null, new object()));
      Assert.Throws<ArgumentNullException>(() => new SpecificationBase<object>().Conforms(null));

      var specification = new ZeroValueSpecification();
      Assert.True(specification.Conforms(0));
      Assert.False(specification.Conforms(1));
    }
  }

  internal sealed class ZeroValueSpecification : SpecificationBase<int>
  {
    public ZeroValueSpecification() : base(value => value == 0)
    {
    }
  }
}