using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="TaskExtensions"/>.</para>
/// </summary>
public sealed class TaskExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Await(ValueTask, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(ValueTask{T}, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(ValueTask{T}, out T, TimeSpan?, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ValueTask_Await_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ValueTask.CompletedTask.Await(null, Cancellation)).NotThrow<OperationCanceledException>();
      AssertionExtensions.Should(() => ValueTask.FromCanceled(Cancellation).Await()).NotThrow<OperationCanceledException>();
    }

    using (new AssertionScope())
    {

    }

    using (new AssertionScope())
    {

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Await(Task, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(Task{T}, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Await{T}(Task{T}, out T, TimeSpan?, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Task_Await_Methods()
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
  ///   <para>Performs testing of <see cref="TaskExtensions.Execute(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/> method.</para>
  /// </summary>
  [Fact]
  public void ValueTask_Execute_Method()
  {
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
  public void Task_Execute_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task) null).Execute()).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).Execute()).ThrowExactly<ArgumentNullException>();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TaskExtensions.ExecuteAsync(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/> method.</para>
  /// </summary>
  [Fact]
  public void ValueTask_ExecuteAsync_Method()
  {
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
  public void Task_ExecuteAsync_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task) null).ExecuteAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Task<object>) null).ExecuteAsync()).ThrowExactlyAsync<ArgumentNullException>().Await();

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ToTask(Action, TaskCreationOptions, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ToTask(Action{object}, object, TaskCreationOptions, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Action_ToTask_Methods()
  {
    using (new AssertionScope())
    {

    }

    using (new AssertionScope())
    {

    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ToTask{T}(Func{T}, TaskCreationOptions, CancellationToken)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ToTask{T}(Func{object,T}, object, TaskCreationOptions, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Func_ToTask_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Func<object>) null).ToTask()).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Func<object, object>) null).ToTask(new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => TaskExtensions.ToTask(value => value, null)).ThrowExactlyAsync<ArgumentNullException>().Await();

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
  public void Task_ToValueTask_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TaskExtensions.ToValueTask(null)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => TaskExtensions.ToValueTask<object>(null)).ThrowExactly<ArgumentNullException>();
    }

    throw new NotImplementedException();
  }
}