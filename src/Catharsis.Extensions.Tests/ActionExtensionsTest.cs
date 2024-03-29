using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ActionExtensions"/>.</para>
/// </summary>
public sealed class ActionExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ActionExtensions.Execute(Action, Func{bool})"/></description></item>
  ///     <item><description><see cref="ActionExtensions.Execute{T}(Action{T}, Predicate{T}, T)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Action) null).Execute(() => true)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => ActionExtensions.Execute(() => { }, null)).ThrowExactly<ArgumentNullException>().WithParameterName("condition");

      const int count = 1000;

      var counter = 0;

      Action action = () => counter++;

      action.Execute(() => false).Should().BeOfType<Action>().And.BeSameAs(action);
      counter.Should().Be(0);

      counter = 0;
      action.Execute(() => counter < count).Should().BeOfType<Action>().And.BeSameAs(action);
      counter.Should().Be(count);

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Action<object>) null).Execute(_ => true, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      AssertionExtensions.Should(() => ActionExtensions.Execute(_ => {}, null, new object())).ThrowExactly<ArgumentNullException>().WithParameterName("condition");

      const int count = 1000;
      
      Action<ICollection<int>> action = collection => collection?.Add(int.MaxValue);

      var collection = new List<int>();
      action.Execute(_ => false, collection).Should().BeOfType<Action<ICollection<int>>>().And.BeSameAs(action);
      collection.Should().BeOfType<List<int>>().And.BeEmpty();

      collection = [];
      action.Execute(_ => false, null).Should().BeOfType<Action<ICollection<int>>>().And.BeSameAs(action);
      collection.Should().BeOfType<List<int>>().And.BeEmpty();

      collection = [];
      action.Execute(x => x?.Count < count, collection).Should().BeOfType<Action<ICollection<int>>>().And.BeSameAs(action);
      collection.Should().BeOfType<List<int>>().And.HaveCount(count).And.AllBeEquivalentTo(int.MaxValue);

      collection = [];
      action.Execute(x => x?.Count < count, null).Should().BeOfType<Action<ICollection<int>>>().And.BeSameAs(action);
      collection.Should().BeOfType<List<int>>().And.BeEmpty();

      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ActionExtensions.ToTask(Action, TaskCreationOptions, CancellationToken)"/></description></item>
  ///     <item><description><see cref="ActionExtensions.ToTask(Action{object}, object, TaskCreationOptions, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTask_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ActionExtensions.ToTask(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();

      static void Validate()
      {
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Action<object>) null).ToTask(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();


      static void Validate()
      {
      }
    }

    throw new NotImplementedException();
  }
}