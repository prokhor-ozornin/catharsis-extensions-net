using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ActionExtensions"/>.</para>
/// </summary>
/// <seealso cref="ActionExtensions"/>
public sealed class ActionExtensionsTest : Test
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ActionExtensions.get_Task(Action)"/> method.</para>
  /// </summary>
  [Fact]
  public void Action_Task_Property()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ActionExtensions.Execute(Action, Func{bool})"/> method.</para>
  /// </summary>
  [Fact]
  public void Action_Execute_Method()
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
    }
    
    return;

    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ActionExtensions.Execute{T}(Action{T}, Predicate{T}, T)"/> method.</para>
  /// </summary>
  public void GenericAction_Execute_Method()
  {
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
    }
    
    return;
    
    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ActionExtensions.ToTask(Action, TaskCreationOptions, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Action_ToTask_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ActionExtensions.ToTask(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();
    }

    return;
    
    static void Test()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ActionExtensions.ToTask(Action{object}, object, TaskCreationOptions, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void GenericAction_ToTask_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Action<object>) null).ToTask(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("action").Await();

    }

    return;
    
    static void Test()
    {
    }
  }
}