using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="FuncExtensions"/>.</para>
/// </summary>
public sealed class FuncExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="FuncExtensions.ToTask{T}(Func{T}, TaskCreationOptions, CancellationToken)"/></description></item>
  ///     <item><description><see cref="FuncExtensions.ToTask{T}(Func{object,T}, object, TaskCreationOptions, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void ToTask_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Func<object>) null).ToTask()).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("function").Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((Func<object, object>) null).ToTask(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("function").Await();

    }

    throw new NotImplementedException();

    return;

    static void Validate()
    {
    }
  }
}