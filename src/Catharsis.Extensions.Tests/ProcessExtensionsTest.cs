using System.Diagnostics;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
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
    ShellProcess = Attributes.ShellCommand().ToProcess(new ProcessStartInfo { RedirectStandardError = true, RedirectStandardInput = true, RedirectStandardOutput = true });
    ShellProcess.Start();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Run(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Run_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Run(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Restart_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Restart(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Finish(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Finish_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Finish(null, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.FinishAsync(Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void FinishAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.FinishAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().FinishAsync(Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.TryFinallyKill(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void TryFinallyKill_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.TryFinallyKill(null, _ => { })).ThrowExactly<ArgumentNullException>().WithParameterName("process");
      
      var process = Attributes.ShellCommand().ToProcess();
      
      AssertionExtensions.Should(() => process.TryFinallyKill(_ => { })).ThrowExactly<InvalidOperationException>();

      process.Start();
      AssertionExtensions.Should(() => process.TryFinallyKill(null)).ThrowExactly<ArgumentNullException>().WithParameterName("action");
      process.TryFinallyKill(_ => { }).Should().BeOfType<Process>().And.BeSameAs(process);
      process.HasExited.Should().BeTrue();
    }

    return;

    static void Validate(Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToErrorText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToErrorTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToErrorTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToErrorTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytes(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToBytesAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToBytesAsync(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(byte[] result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToText(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("process");
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.ToTextAsync(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.ToTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("process").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(string result, Process process)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytes(Process, IEnumerable{byte})"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.WriteBytes(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process, byte[] bytes)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteBytesAsync(Process, IEnumerable{byte}, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.WriteBytesAsync(null, [])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
      AssertionExtensions.Should(() => ShellProcess.WriteBytesAsync([], Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process, byte[] bytes)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteText(Process, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process, string text)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTextAsync(Process, string, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => ShellProcess.WriteTextAsync(string.Empty, Attributes.CancellationToken())).ThrowExactlyAsync<OperationCanceledException>().Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(Process process, string text)
    {
      using (process)
      {

      }
    }
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    ShellProcess.Kill();
  }
}