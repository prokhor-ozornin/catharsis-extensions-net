using System.Text;
using System.Xml;
using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Extensions.Tests;

/// <summary>
///   <para>Tests set for class <see cref="XmlWriterExtensions"/>.</para>
/// </summary>
public sealed class XmlWriterExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="XmlWriterExtensions.WriteBytes(XmlWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytes_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlWriterExtensions.WriteBytes(null, [])).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteBytes(null)).ThrowExactly<ArgumentNullException>().WithParameterName("bytes");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlWriter writer, byte[] bytes, Encoding encoding = null)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlWriterExtensions.WriteBytesAsync(XmlWriter, IEnumerable{byte}, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteBytesAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlWriterExtensions.WriteBytesAsync(null, [])).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteBytesAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("bytes").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlWriter writer, byte[] bytes, Encoding encoding = null)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlWriterExtensions.WriteText(XmlWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlWriterExtensions.WriteText(null, string.Empty)).ThrowExactly<ArgumentNullException>().WithParameterName("destination");
      AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("text");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlWriter writer, string text)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlWriterExtensions.WriteTextAsync(XmlWriter, string)"/> method.</para>
  /// </summary>
  [Fact]
  public void WriteTextAsync_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => XmlWriterExtensions.WriteTextAsync(null, string.Empty)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("destination").Await();
      AssertionExtensions.Should(() => Stream.Null.ToXmlWriter().WriteTextAsync(null)).ThrowExactlyAsync<ArgumentNullException>().WithParameterName("text").Await();
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlWriter writer, string text)
    {
      using (writer)
      {

      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="XmlWriterExtensions.ToXmlDictionaryWriter(XmlWriter)"/> method.</para>
  /// </summary>
  [Fact]
  public void ToXmlDictionaryWriter_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ((XmlWriter) null).ToXmlDictionaryWriter()).ThrowExactly<ArgumentNullException>().WithParameterName("writer");
    }

    throw new NotImplementedException();

    return;

    static void Validate(XmlWriter writer)
    {
      using (writer)
      {

      }
    }
  }
}