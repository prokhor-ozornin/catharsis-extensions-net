using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ActionExtensions"/>.</para>
/// </summary>
public sealed class ActionExtensionsTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ActionExtensions.Execute{T}(Action{T}, Predicate{T}, T)"/></description></item>
  ///     <item><description><see cref="ActionExtensions.Execute(Action, Func{bool})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Action_Execute_Methods()
  {
    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((Action<object?>) null!).Execute(_ => true, new object())).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => ActionExtensions.Execute(_ => {}, null!, new object())).ThrowExactly<ArgumentNullException>();

      const int count = 1000;
      
      Action<ICollection<int>?> action = collection => collection?.Add(int.MaxValue);

      var collection = new List<int>();
      action.Execute(_ => false, collection).Should().NotBeNull().And.BeSameAs(action);
      collection.Should().BeEmpty();

      collection = new List<int>();
      action.Execute(_ => false, null).Should().NotBeNull().And.BeSameAs(action);
      collection.Should().BeEmpty();

      collection = new List<int>();
      action.Execute(collection => collection?.Count < count, collection).Should().NotBeNull().And.BeSameAs(action);
      collection.Should().HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);

      collection = new List<int>();
      action.Execute(collection => collection?.Count < count, null).Should().NotBeNull().And.BeSameAs(action);
      collection.Should().BeEmpty();
    }

    using (new AssertionScope())
    {
      //AssertionExtensions.Should(() => ((Action) null!).Execute(() => true)).ThrowExactly<ArgumentNullException>();
      //AssertionExtensions.Should(() => ActionExtensions.Execute(() => { }, null!)).ThrowExactly<ArgumentNullException>();

      const int count = 1000;

      var counter = 0;

      Action action = () => counter++;

      action.Execute(() => false).Should().NotBeNull().And.BeSameAs(action);
      counter.Should().Be(0);

      counter = 0;
      action.Execute(() => counter < count).Should().NotBeNull().And.BeSameAs(action);
      counter.Should().Be(count);
    }
  }
}