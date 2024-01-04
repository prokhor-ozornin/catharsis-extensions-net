using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TaskExtensions"/>.</para>
/// </summary>
public sealed class TaskExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Await(Task, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(Task{T}, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(Task{T}, out T, TimeSpan?, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Await_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task) null).Await()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("task").Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).Await()).ThrowExactly<ArgumentNullException>().WithParameterName("task");

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).Await(out _)).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Execute(Task, Action{Task}, Action{Task}, Action{Task})"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Execute{T}(Task{T}, Action{Task{T}}, Action{Task{T}}, Action{Task{T}})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task) null).Execute()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("task").Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).Execute()).ThrowExactly<ArgumentNullException>().WithParameterName("task");

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ExecuteAsync(Task, Action{Task}, Action{Task}, Action{Task})"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ExecuteAsync{T}(Task{T}, Action{Task{T}}, Action{Task{T}}, Action{Task{T}})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ExecuteAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task) null).ExecuteAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("task").Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).ExecuteAsync()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("task").Await();

    }

    throw new NotImplementedException();
  }
  
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ToValueTask(Task)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ToValueTask{T}(Task{T})"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToValueTask_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TaskExtensions.ToValueTask(null)).ThrowExactly<ArgumentNullException>().WithParameterName("task");
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TaskExtensions.ToValueTask<object>(null)).ThrowExactly<ArgumentNullException>().WithParameterName("task");
    }

    throw new NotImplementedException();
  }
}