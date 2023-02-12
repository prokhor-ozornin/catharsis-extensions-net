using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Catharsis.Extensions.Tests;

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
  public void Restart_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Restart(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Finish_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Finish(null, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyKill_Method()
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
  public void ToBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytesAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToBytesAsync(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.ToErrorTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteBytes(null, Enumerable.Empty<byte>())).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
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
  public void WriteText_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTextAsync(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => ShellProcess.WriteTextAsync(string.Empty, Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.FinishAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FinishAsync_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.FinishAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().FinishAsync(Cancellation)).ThrowExactlyAsync<OperationCanceledException>().Await();

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