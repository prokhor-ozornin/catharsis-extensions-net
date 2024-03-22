using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ValueTaskExtensions"/>.</para>
/// </summary>
public sealed class ValueTaskExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ValueTaskExtensions.Await(ValueTask, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="ValueTaskExtensions.Await{T}(ValueTask{T}, TimeSpan?, CancellationToken)"/></description></item>
  ///     <item><description><see cref="ValueTaskExtensions.Await{T}(ValueTask{T}, out T, TimeSpan?, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Await_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ValueTask.CompletedTask.Await(null, Attributes.CancellationToken())).NotThrow<OperationCanceledException>();
      AssertionExtensions.Should(() => ValueTask.FromCanceled(Attributes.CancellationToken()).Await()).NotThrow<OperationCanceledException>();
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
  ///   <para>Performs testing of <see cref="ValueTaskExtensions.Execute(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/> method.</para>
  /// </summary>
  [Fact]
  public void Execute_Method()
  {
    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ValueTaskExtensions.ExecuteAsync(ValueTask, Action{ValueTask}, Action{ValueTask}, Action{ValueTask})"/> method.</para>
  /// </summary>
  [Fact]
  public void ExecuteAsync_Method()
  {
    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
   
  }
}