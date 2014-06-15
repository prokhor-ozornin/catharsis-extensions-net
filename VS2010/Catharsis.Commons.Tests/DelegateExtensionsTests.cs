using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DelegateExtensions"/>.</para>
  /// </summary>
  public sealed class DelegateExtensionsTests
  {
    private delegate int Increment(int value);
    private delegate int Decrement(int value);

    private readonly Delegate incrementDelegate;
    private readonly Delegate decrementDelegate;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public DelegateExtensionsTests()
    {
      this.incrementDelegate = Delegate.CreateDelegate(typeof(Increment), this.GetType().AnyMethod("IncrementValue"));
      this.decrementDelegate = Delegate.CreateDelegate(typeof(Decrement), this.GetType().AnyMethod("DecrementValue"));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="DelegateExtensions.And(Delegate, Delegate)"/> method.</para>
    /// </summary>
    [Fact]
    public void And_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DelegateExtensions.And(null, this.incrementDelegate));
      Assert.Throws<ArgumentNullException>(() => this.incrementDelegate.And(null));
      Assert.Throws<ArgumentException>(() => this.incrementDelegate.And(this.decrementDelegate));

      var andDelegate = this.incrementDelegate.And(this.incrementDelegate);
      Assert.True(andDelegate is MulticastDelegate);
      Assert.Equal(this.GetType().AnyMethod("IncrementValue"), andDelegate.Method);
      Assert.Null(andDelegate.Target);
      Assert.True(andDelegate.GetInvocationList().SequenceEqual(new [] { this.incrementDelegate, this.incrementDelegate }));
      Assert.Equal(1, andDelegate.DynamicInvoke(0).To<int>());
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DelegateExtensions.Not(Delegate, Delegate)"/> method.</para>
    /// </summary>
    [Fact]
    public void Not_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DelegateExtensions.Not(null, this.incrementDelegate));
      Assert.Throws<ArgumentException>(() => this.incrementDelegate.Not(this.decrementDelegate));

      Assert.True(ReferenceEquals(this.incrementDelegate.Not(null), this.incrementDelegate));
      Assert.Null(this.incrementDelegate.Not(this.incrementDelegate));
    }

    private static int IncrementValue(int value)
    {
      return value + 1;
    }

    private static int DecrementValue(int value)
    {
      return value - 1;
    }
  }
}