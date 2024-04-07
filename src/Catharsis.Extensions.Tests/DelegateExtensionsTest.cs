using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="DelegateExtensions"/>.</para>
/// </summary>
public sealed class DelegateExtensionsTest : UnitTest
{
  private delegate int Increment(int value);
  private delegate int Decrement(int value);

  private Delegate IncrementDelegate { get; }
  private Delegate DecrementDelegate { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public DelegateExtensionsTest()
  {
    IncrementDelegate = Delegate.CreateDelegate(typeof(Increment), GetType().AnyMethod("IncrementValue"));
    DecrementDelegate = Delegate.CreateDelegate(typeof(Decrement), GetType().AnyMethod("DecrementValue"));
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DelegateExtensions.And(Delegate, Delegate)"/> method.</para>
  /// </summary>
  [Fact]
  public void And_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DelegateExtensions.And(null, IncrementDelegate)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => IncrementDelegate.And(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
      AssertionExtensions.Should(() => IncrementDelegate.And(DecrementDelegate)).ThrowExactly<ArgumentException>();

      /*var andDelegate = IncrementDelegate.And(IncrementDelegate);
      andDelegate.Should().BeOfType<MulticastDelegate>();
      andDelegate.Method.Should().Equals(GetType().Method("IncrementValue")).Should().BeTrue();
      andDelegate.Target.Should().BeNull();
      andDelegate.GetInvocationList().Should().Equal(IncrementDelegate, IncrementDelegate);
      andDelegate.DynamicInvoke(0).As<int>().Should().Be(1);*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="DelegateExtensions.Not(Delegate, Delegate)"/> method.</para>
  /// </summary>
  [Fact]
  public void Not_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => DelegateExtensions.Not(null, IncrementDelegate)).ThrowExactly<ArgumentNullException>().WithParameterName("left");
      AssertionExtensions.Should(() => IncrementDelegate.Not(null)).ThrowExactly<ArgumentNullException>().WithParameterName("right");
      AssertionExtensions.Should(() => IncrementDelegate.Not(DecrementDelegate)).ThrowExactly<ArgumentException>();

      /*IncrementDelegate.Not(null).Should().BeSameAs(IncrementDelegate);
      IncrementDelegate.Not(IncrementDelegate).Should().BeNull();*/
    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }
}