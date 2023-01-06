using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="ProcessExtensions"/>.</para>
/// </summary>
public sealed class ProcessExtensionsTest : UnitTest
{
  private Process ShellProcess { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public ProcessExtensionsTest()
  {
    ShellProcess = ShellCommand.ToProcess(new ProcessStartInfo { RedirectStandardError = true, RedirectStandardInput = true, RedirectStandardOutput = true });
    ShellProcess.Start();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Restart_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Restart(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Finish_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Finish(null, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    //throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.FinishAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_FinishAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.FinishAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().FinishAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Print{T}(T, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_Print_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Print<object>(null, Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("instance");
    AssertionExtensions.Should(() => ProcessExtensions.Print(new object(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.PrintAsync{T}(T, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Object_PrintAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.PrintAsync<object>(null, Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("instance").Await();
    AssertionExtensions.Should(() => ProcessExtensions.PrintAsync(new object(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => new object().PrintAsync(Process.GetCurrentProcess(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_TryFinallyKill_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.TryFinallyKill(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    
    var process = ShellCommand.ToProcess();
    
    AssertionExtensions.Should(() => process.TryFinallyKill(_ => { })).ThrowExactly<InvalidOperationException>();

    process.Start();
    AssertionExtensions.Should(() => process.TryFinallyKill(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
    process.TryFinallyKill(_ => { }).Should().NotBeNull().And.BeSameAs(process);
    process.HasExited.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytes(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytesAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytesAsync(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    AssertionExtensions.Should(() => ShellProcess.ToBytesAsync(Cancellation).ToArray()).ThrowExactly<AggregateException>().WithInnerExceptionExactly<TaskCanceledException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToErrorText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_ToErrorTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytesAsync(null, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => ShellProcess.WriteBytesAsync(Enumerable.Empty<byte>(), Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteText(Process, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTextAsync(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ShellProcess.WriteTextAsync(string.Empty, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(IEnumerable{byte}, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(Enumerable.Empty<byte>(), null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(string, Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteTo(Process.GetCurrentProcess())).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(string.Empty, null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteToAsync(IEnumerable{byte}, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    AssertionExtensions.Should(() => ProcessExtensions.WriteToAsync(Enumerable.Empty<byte>(), null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    AssertionExtensions.Should(() => Enumerable.Empty<byte>().WriteToAsync(ShellProcess, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteToAsync(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteToAsync_Method()
  {
    AssertionExtensions.Should(() => ((string) null).WriteToAsync(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    AssertionExtensions.Should(() => ProcessExtensions.WriteToAsync(string.Empty, null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    AssertionExtensions.Should(() => string.Empty.WriteToAsync(ShellProcess)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    ShellProcess.Kill();
  }
}