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
      AssertionExtensions.Should(() => ProcessExtensions.Finish(null!, TimeSpan.Zero)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Finish(null!, CancellationToken.None)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Restart(Process, TimeSpan?)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Restart_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Restart(null!)).ThrowExactly<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ProcessExtensions.Bytes(Process, CancellationToken)"/></description></item>
  ///     <item><description><see cref="ProcessExtensions.Bytes(Process, IEnumerable{byte}, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Process_Bytes_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Bytes(null!)).ThrowExactly<ArgumentNullException>();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Bytes(null!, Enumerable.Empty<byte>())).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().Bytes(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of following methods :</para>
  ///   <list type="bullet">
  ///     <item><description><see cref="ProcessExtensions.Text(Process)"/></description></item>
  ///     <item><description><see cref="ProcessExtensions.Text(Process, string, CancellationToken)"/></description></item>
  ///   </list>
  /// </summary>
  [Fact]
  public void Process_Text_Methods()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ProcessExtensions.Text(null!, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().Await();
      AssertionExtensions.Should(() => Process.GetCurrentProcess().Text(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();
    }

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(IEnumerable{byte}, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void IEnumerable_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((IEnumerable<byte>) null!).WriteTo(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(Enumerable.Empty<byte>(), null!)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.WriteTo(string, Process, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void String_WriteTo_Method()
  {
    AssertionExtensions.Should(() => ((string) null!).WriteTo(Process.GetCurrentProcess())).ThrowExactlyAsync<ArgumentNullException>();
    AssertionExtensions.Should(() => ProcessExtensions.WriteTo(string.Empty, null!)).ThrowExactlyAsync<ArgumentNullException>();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.UseTemporarily(Process, Action{Process})"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_UseTemporarily_Method()
  {
    //AssertionExtensions.Should(() => ProcessExtensions.UseTemporarily(null!, _ => {})).ThrowExactly<ArgumentNullException>();
    //AssertionExtensions.Should(() => Process.GetCurrentProcess().UseTemporarily(null!)).ThrowExactly<ArgumentNullException>();

    const string command = "cmd.exe";
    var process = Process.Start(command);
    process.UseTemporarily(_ => {}).Should().NotBeNull().And.BeSameAs(process);
    process.HasExited.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Print(Process, object, CancellationToken)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Print_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Print(null!, new object())).ThrowExactlyAsync<ArgumentNullException>().Await();
    AssertionExtensions.Should(() => Process.GetCurrentProcess().Print(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ProcessExtensions.Error(Process)"/> method.</para>
  /// </summary>
  [Fact]
  public void Process_Error_Method()
  {
    AssertionExtensions.Should(() => ProcessExtensions.Error(null!)).ThrowExactlyAsync<ArgumentNullException>().Await();

    throw new NotImplementedException();
  }
}