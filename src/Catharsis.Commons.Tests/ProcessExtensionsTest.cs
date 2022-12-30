using System.Diagnostics;
using FluentAssertions.Execution;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ProcessExtensions"/>.</para>
/// </summary>
public sealed class ProcessExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Restart_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Restart(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/></description></item>
  ///     <item><description><see cref="ProcessExtensions.Finish(Process, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Process_Finish_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Finish(null, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Finish(null, CancellationToken.None)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Print{T}(T, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Print<object>(null, Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ProcessExtensions.Print(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_TryFinallyKill_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.TryFinallyKill(null, _ => { })).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().TryFinallyKill(null)).ThrowExactly<ArgumentNullException>();

    const string command = "cmd.exe";
    var process = Process.Start(command);
    process.TryFinallyKill(_ => { }).Should().NotBeNull().And.BeSameAs(process);
    process!.HasExited.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytes(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToErrorText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteText(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteText(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(IEnumerable{byte}, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }
}