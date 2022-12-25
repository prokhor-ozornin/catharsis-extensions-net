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
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Execute(ValueTask, Action{ValueTask}?, Action{ValueTask}?, Action{ValueTask}?)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Execute(ValueTask, Action?, Action?, Action?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ValueTask_Execute_Methods()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.Execute(Task, Action{Task}?, Action{Task}?, Action{Task}?)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Execute(Task, Action?, Action?, Action?)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Execute{T}(Task{T}, Action{Task{T}}?, Action{Task{T}}?, Action{Task{T}}?)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.Execute{T}(Task{T}, Action?, Action?, Action?)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Task_Execute_Methods()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ToTask(Action, CancellationToken, TaskCreationOptions)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ToTask(Action{object?}, object?, CancellationToken, TaskCreationOptions)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Action_ToTask_Methods()
  {
    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="TaskExtensions.ToTask{T}(Func{T}, CancellationToken, TaskCreationOptions)"/></description></item>
  ///     <item><description><see cref="TaskExtensions.ToTask{T}(Func{object?, T}, object?, CancellationToken, TaskCreationOptions)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Func_ToTask_Methods()
  {
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
    throw new NotImplementedException();
  }
}