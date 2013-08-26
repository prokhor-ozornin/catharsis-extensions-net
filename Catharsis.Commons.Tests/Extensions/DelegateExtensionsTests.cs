using System;
using System.Linq;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="DelegateExtensions"/>.</para>
  /// </summary>
  public sealed class DelegateExtensionsTests
  {
    private delegate int Increment(int value);
    private delegate int Decrement(int value);

    private readonly Delegate IncrementDelegate;
    private readonly Delegate DecrementDelegate;

    /// <summary>
    ///   <para></para>
    /// </summary>
    public DelegateExtensionsTests()
    {
      this.IncrementDelegate = Delegate.CreateDelegate(typeof(Increment), this.GetType().GetAnyMethod("IncrementValue"));
      this.DecrementDelegate = Delegate.CreateDelegate(typeof(Decrement), this.GetType().GetAnyMethod("DecrementValue"));
    }
    
    /// <summary>
    ///   <para>Performs testing of <see cref="DelegateExtensions.And(Delegate, Delegate)"/> method.</para>
    /// </summary>
    [Fact]
    public void And_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DelegateExtensions.And(null, IncrementDelegate));
      Assert.Throws<ArgumentNullException>(() => DelegateExtensions.And(IncrementDelegate, null));
      Assert.Throws<ArgumentException>(() => this.IncrementDelegate.And(DecrementDelegate));

      var andDelegate = IncrementDelegate.And(IncrementDelegate);
      Assert.True(andDelegate is MulticastDelegate);
      Assert.True(andDelegate.Method == this.GetType().GetAnyMethod("IncrementValue"));
      Assert.True(andDelegate.Target == null);
      Assert.True(andDelegate.GetInvocationList().SequenceEqual(new [] { IncrementDelegate, IncrementDelegate }));
      Assert.True(andDelegate.DynamicInvoke(0).To<int>() == 1);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="DelegateExtensions.Not(Delegate, Delegate)"/> method.</para>
    /// </summary>
    [Fact]
    public void Not_Method()
    {
      Assert.Throws<ArgumentNullException>(() => DelegateExtensions.Not(null, IncrementDelegate));
      Assert.Throws<ArgumentException>(() => this.IncrementDelegate.Not(DecrementDelegate));

      Assert.True(ReferenceEquals(IncrementDelegate.Not(null), IncrementDelegate));
      Assert.True(IncrementDelegate.Not(IncrementDelegate) == null);
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