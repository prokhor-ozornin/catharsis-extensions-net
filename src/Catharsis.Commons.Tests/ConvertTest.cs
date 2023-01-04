using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Catharsis.Commons.Tests;

/// <summary>
///   <para>Tests set for class <see cref="Convert"/>.</para>
/// </summary>
public sealed class ConvertTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="Convert.To"/> property.</para>
  /// </summary>
  [Fact]
  public void To_Property()
  {
    Convert.To.Should().NotBeNull().And.BeSameAs(Convert.To);
  }
}

/// <summary>
///   <para>Tests set for class <see cref="ConvertExtensions"/>.</para>
/// </summary>
public sealed class ConvertExtensionsTests : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.String(Convert, object, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_String_Method()
  {
    void ValidateBinaryReader(Encoding encoding)
    {
      var text = RandomString;

      using (var stream = Stream.Null)
      {
        using (var reader = stream.ToBinaryReader(encoding))
        {
          AssertionExtensions.Should(() => Convert.To.String(reader, encoding)).ThrowExactly<EndOfStreamException>();
        }
      }

      using (var stream = new MemoryStream())
      {
        using (var writer = stream.ToBinaryWriter(encoding))
        {
          writer.Write(text);

          stream.MoveToStart();

          using (var reader = stream.ToBinaryReader(encoding))
          {
            var result = Convert.To.String(reader, encoding);
            stream.MoveToStart();
            result.Should().NotBeNull().And.NotBeSameAs(Convert.To.String(reader, encoding)).And.Be(text);
          }
        }
      }
    }

    void ValidateFile(Encoding encoding)
    {
      AssertionExtensions.Should(() => Convert.To.String(RandomFakeFile)).ThrowExactly<AggregateException>().WithInnerExceptionExactly<FileNotFoundException>();
      
      RandomEmptyFile.TryFinallyDelete(file =>
      {
        Convert.To.String(file, encoding).Should().NotBeNull().And.BeSameAs(Convert.To.String(file, encoding)).And.BeEmpty();
      });

      RandomNonEmptyFile.TryFinallyDelete(file =>
      {
        Convert.To.String(file, encoding).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(file, encoding)).And.Be(file.ToTextAsync(encoding).Await());
      });
    }

    void ValidateStream(Encoding encoding)
    {
      using (var stream = Stream.Null)
      {
        Convert.To.String(stream, encoding).Should().NotBeNull().And.BeSameAs(Convert.To.String(stream, encoding)).And.BeEmpty();
      }

      using (var stream = RandomStream)
      {
        Convert.To.String(stream, encoding).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(stream.MoveToStart(), encoding)).And.Be(stream.MoveToStart().ToTextAsync(encoding).Await());
      }
    }

    void ValidateUri(Encoding encoding)
    {
      var file = RandomNonEmptyFile;

      file.TryFinallyDelete(file =>
      {
        var uri = file.ToUri();
        Convert.To.String(uri, encoding).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(uri, encoding)).And.Be(file.ToTextAsync(encoding).Await());
      });
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.String(null, new object())).ThrowExactly<ArgumentNullException>();
    }

    // NULL
    using (new AssertionScope())
    {
      Convert.To.String(null).Should().BeNull();
    }

    // String
    using (new AssertionScope())
    {
      var text = RandomString;
      
      Convert.To.String(string.Empty).Should().NotBeNull().And.BeSameAs(Convert.To.String(string.Empty)).And.BeEmpty();
      Convert.To.String(text).Should().NotBeNull().And.BeSameAs(Convert.To.String(text)).And.Be(text);
    }

    // BinaryReader
    using (new AssertionScope())
    {
      ValidateBinaryReader(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateBinaryReader);
    }

    // SecureString
    using (new AssertionScope())
    {
      using (var secure = EmptySecureString)
      {
        Convert.To.String(secure).Should().NotBeNull().And.BeSameAs(Convert.To.String(secure)).And.BeEmpty();
      }

      using (var secure = RandomSecureString)
      {
        Convert.To.String(secure).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(secure)).And.Be(secure.ToText());
      }
    }

    // FileInfo
    using (new AssertionScope())
    {
      ValidateFile(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateFile);
    }

    // HttpContent
    using (new AssertionScope())
    {
      using (var content = new StringContent(string.Empty))
      {
        Convert.To.String(content).Should().NotBeNull().And.BeSameAs(Convert.To.String(content)).And.BeEmpty();
      }

      var text = RandomString;
      using (var content = new StringContent(text))
      {
        Convert.To.String(content).Should().NotBeNullOrEmpty().And.NotBeSameAs(Convert.To.String(content)).And.Be(text);
      }
    }

    // Process
    using (new AssertionScope())
    {
      var process = "cmd.exe".Execute("/c", "dir");
      process.Start();
      process.Finish(TimeSpan.FromSeconds(5));
      Convert.To.String(process).Should().NotBeNullOrEmpty().And.NotBeSameAs(Convert.To.String(process));
    }

    // Stream
    using (new AssertionScope())
    {
      ValidateStream(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateStream);
    }

    // TextReader
    using (new AssertionScope())
    {
      var text = RandomString;

      using (var stream = Stream.Null)
      {
        using (var reader = stream.ToStreamReader())
        {
          Convert.To.String(reader).Should().NotBeNull().And.BeSameAs(Convert.To.String(reader)).And.BeEmpty();
        }
      }

      using (var reader = text.ToStringReader())
      {
        var result = Convert.To.String(reader);
        result.Should().NotBeNull().And.NotBeSameAs(Convert.To.String(reader)).And.Be(text);
      }
    }

    // Uri
    using (new AssertionScope())
    {
      ValidateUri(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateUri);
    }

    // XmlDocument
    using (new AssertionScope())
    {
      var xml = new XmlDocument();
      xml.AppendChild(xml.CreateElement("root"));
      Convert.To.String(xml).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(xml)).And.Be(xml.ToText());
    }

    // XDocument
    using (new AssertionScope())
    {
      var xml = new XDocument(new XElement("root"));
      Convert.To.String(xml).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(xml)).And.Be(xml.ToTextAsync().Await());
    }

    // XmlReader
    using (new AssertionScope())
    {
      var value = RandomName;
      var xml = new XDocument(new XElement("root", value));
      using (var reader = xml.ToTextAsync().Await().ToXmlReader())
      {
        reader.MoveToContent();
        Convert.To.String(reader).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(reader)).And.Be(xml.Root.ToString());
      }
    }

    // Default
    using (new AssertionScope())
    {
      Convert.To.String(new object()).Should().NotBeNull().And.NotBeSameAs(Convert.To.String(new object())).And.Be(new object().ToString());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Binary(Convert, object, Encoding)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Binary_Method()
  {
    void ValidateString(Encoding encoding)
    {
      var text = RandomString;

      Convert.To.Binary(string.Empty, encoding).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(string.Empty, encoding)).And.BeEmpty();
      Convert.To.Binary(text, encoding).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(text, encoding)).And.Equal((encoding ?? Encoding.Default).GetBytes(text));
    }

    void ValidateSecureString(Encoding encoding)
    {
      using (var secure = EmptySecureString)
      {
        Convert.To.Binary(secure, encoding).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(secure, encoding)).And.BeEmpty();
      }

      using (var secure = RandomSecureString)
      {
        Convert.To.Binary(secure, encoding).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(secure, encoding)).And.Equal(secure.ToBytes(encoding));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Binary(null, new object())).ThrowExactly<ArgumentNullException>();
    }

    // NULL
    using (new AssertionScope())
    {
      Convert.To.Binary(null).Should().BeNull();
    }

    // IEnumerable<byte>
    using (new AssertionScope())
    {
      Convert.To.Binary(Enumerable.Empty<byte>()).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(Enumerable.Empty<byte>())).And.NotBeSameAs(Enumerable.Empty<byte>()).And.BeEmpty();
      Convert.To.Binary(Array.Empty<byte>()).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(Array.Empty<byte>())).And.BeSameAs(Array.Empty<byte>()).And.BeEmpty();

      var bytes = RandomBytes;
      Convert.To.Binary(bytes).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(bytes)).And.BeSameAs(bytes).And.Equal(bytes);
    }

    // String
    using (new AssertionScope())
    {
      ValidateString(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateString);
    }

    // SecureString
    using (new AssertionScope())
    {
      ValidateSecureString(null);
      Encoding.GetEncodings().Select(info => info.GetEncoding()).ForEach(ValidateSecureString);
    }

    // Guid
    foreach (var guid in new[] {Guid.Empty, Guid.NewGuid()})
    {
      Convert.To.Binary(guid).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(guid)).And.Equal(guid.ToByteArray());
    }

    // FileInfo
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => Convert.To.Binary(RandomFakeFile)).ThrowExactly<AggregateException>().WithInnerExceptionExactly<FileNotFoundException>();

      RandomEmptyFile.TryFinallyDelete(file =>
      {
        Convert.To.Binary(file).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(file)).And.BeEmpty();
      });

      RandomNonEmptyFile.TryFinallyDelete(file =>
      {
        Convert.To.Binary(file).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(file)).And.Equal(file.ToBytesAsync().ToArrayAsync().Await());
      });
    }

    // IPAddress
    using (new AssertionScope())
    {
      foreach (var address in new[] {IPAddress.Any, IPAddress.Broadcast, IPAddress.Loopback, IPAddress.None, IPAddress.IPv6Any, IPAddress.IPv6Loopback, IPAddress.IPv6None})
      {
        Convert.To.Binary(address).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(address)).And.Equal(address.ToBytes());
      }
    }

    // PhysicalAddress
    using (new AssertionScope())
    {
      foreach (var address in new[] {PhysicalAddress.None, new PhysicalAddress(RandomBytes)})
      {
        Convert.To.Binary(address).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(address)).And.Equal(address.ToBytes());
      }
    }

    // HttpContent
    using (new AssertionScope())
    {
      using (var content = new ByteArrayContent(Array.Empty<byte>()))
      {
        Convert.To.Binary(content).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(content)).And.BeEmpty();
      }

      var bytes = RandomBytes;
      using (var content = new ByteArrayContent(bytes))
      {
        Convert.To.Binary(content).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(content)).And.Equal(bytes);
      }
    }

    // Process
    using (new AssertionScope())
    {
      var process = "cmd.exe".Execute("/c", "dir");
      process.Start();
      process.Finish(TimeSpan.FromSeconds(5));
      Convert.To.Binary(process).Should().NotBeNullOrEmpty().And.NotBeSameAs(Convert.To.Binary(process));
    }

    // Stream
    using (new AssertionScope())
    {
      using (var stream = Stream.Null)
      {
        Convert.To.Binary(stream).Should().NotBeNull().And.BeSameAs(Convert.To.Binary(stream)).And.BeEmpty();
      }

      using (var stream = RandomStream)
      {
        Convert.To.Binary(stream).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(stream.MoveToStart())).And.Equal(stream.ToArray());
      }
    }

    using (new AssertionScope())
    {
      var file = RandomNonEmptyFile;
      var bytes = file.ToBytesAsync().ToArrayAsync().Await();

      file.TryFinallyDelete(file =>
      {
        var uri = file.ToUri();
        Convert.To.Binary(uri).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(uri)).And.Equal(bytes);
      });
    }

    // XmlDocument
    using (new AssertionScope())
    {
      var xml = new XmlDocument();
      xml.AppendChild(xml.CreateElement("root"));
      Convert.To.Binary(xml).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(xml)).And.Equal(xml.ToBytes());
    }

    // XDocument
    using (new AssertionScope())
    {
      var xml = new XDocument(new XElement("root"));
      Convert.To.Binary(xml).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(xml)).And.Equal(xml.ToBytesAsync().Await());
    }

    // Default
    using (new AssertionScope())
    {
      var instance = new object();
      Convert.To.Binary(instance).Should().NotBeNull().And.NotBeSameAs(Convert.To.Binary(instance)).And.Equal(instance.SerializeAsBinary());
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Array{T}(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Array_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Array<object>(null, new object())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Convert.To.Array<string>(new object())).ThrowExactly<InvalidCastException>();

    // NULL
    Convert.To.Array<object>(null).Should().BeNull();

    // Array
    Convert.To.Array<object>(Array.Empty<object>()).Should().NotBeNull().And.BeSameAs(Convert.To.Array<object>(Array.Empty<object>())).And.BeSameAs(Array.Empty<object>()).And.BeEmpty();

    var array = 1000.Objects(() => RandomString).AsArray();
    Convert.To.Array<string>(array).Should().NotBeNull().And.BeSameAs(Convert.To.Array<string>(array)).And.BeSameAs(array).And.Equal(array);

    // IEnumerable
    Convert.To.Array<object>(Enumerable.Empty<object>()).Should().NotBeNull().And.BeSameAs(Convert.To.Array<object>(Enumerable.Empty<object>())).And.NotBeSameAs(Enumerable.Empty<object>()).And.BeEmpty();

    var list = 1000.Objects(() => RandomString).ToList();
    Convert.To.Array<object>(list).Should().NotBeNull().And.NotBeSameAs(Convert.To.Array<object>(list)).And.Equal(list);

    // IAsyncEnumerable
    using (var stream = RandomStream)
    {
      var enumerable = stream.ToBytesAsync();
      var result = Convert.To.Array<byte>(enumerable);
      result.Should().NotBeNull().And.NotBeSameAs(Convert.To.Array<byte>(enumerable));
      stream.MoveToStart();
      result.Should().Equal(enumerable.ToArrayAsync().Await());
    }

    // Default
    var instance = new object();
    Convert.To.Array<object>(instance).Should().NotBeNull().And.NotBeSameAs(Convert.To.Array<object>(instance)).And.Equal(instance);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Sbyte(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Sbyte_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Sbyte(null, format).Should().BeNull();
      Convert.To.Sbyte(string.Empty, format).Should().BeNull();
      Convert.To.Sbyte(sbyte.MinValue.ToString(format), format).Should().Be(sbyte.MinValue);
      Convert.To.Sbyte(sbyte.MaxValue.ToString(format), format).Should().Be(sbyte.MaxValue);
      Convert.To.Sbyte(new object(), format).Should().BeNull();

      Convert.To.Sbyte(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Sbyte(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Sbyte(byte.MinValue, format).Should().Be((sbyte) byte.MinValue);
      Convert.To.Sbyte(byte.MaxValue, format).Should().Be(unchecked((sbyte) byte.MaxValue));

      Convert.To.Sbyte(short.MinValue, format).Should().Be(unchecked((sbyte) short.MinValue));
      Convert.To.Sbyte(short.MaxValue, format).Should().Be(unchecked((sbyte) short.MaxValue));

      Convert.To.Sbyte(int.MinValue, format).Should().Be(unchecked((sbyte) int.MinValue));
      Convert.To.Sbyte(int.MaxValue, format).Should().Be(unchecked((sbyte) int.MaxValue));

      Convert.To.Sbyte(long.MinValue, format).Should().Be(unchecked((sbyte) long.MinValue));
      Convert.To.Sbyte(long.MaxValue, format).Should().Be(unchecked((sbyte) long.MaxValue));

      Convert.To.Sbyte(ushort.MinValue, format).Should().Be((sbyte) ushort.MinValue);
      Convert.To.Sbyte(ushort.MaxValue, format).Should().Be(unchecked((sbyte) ushort.MaxValue));

      Convert.To.Sbyte(uint.MinValue, format).Should().Be((sbyte) uint.MinValue);
      Convert.To.Sbyte(uint.MaxValue, format).Should().Be(unchecked((sbyte) uint.MaxValue));

      Convert.To.Sbyte(ulong.MinValue, format).Should().Be((sbyte) ulong.MinValue);
      Convert.To.Sbyte(ulong.MaxValue, format).Should().Be(unchecked((sbyte) ulong.MaxValue));

      Convert.To.Sbyte(float.MinValue, format).Should().Be(unchecked((sbyte) float.MinValue));
      Convert.To.Sbyte(float.MaxValue, format).Should().Be(unchecked((sbyte) float.MaxValue));
      Convert.To.Sbyte(float.NaN, format).Should().Be(unchecked((sbyte) float.NaN));
      Convert.To.Sbyte(float.Epsilon, format).Should().Be((sbyte) float.Epsilon);
      Convert.To.Sbyte(float.NegativeInfinity, format).Should().Be(unchecked((sbyte) float.NegativeInfinity));
      Convert.To.Sbyte(float.PositiveInfinity, format).Should().Be(unchecked((sbyte) float.PositiveInfinity));
      Convert.To.Sbyte((float) 1.5, format).Should().Be(2);

      Convert.To.Sbyte(double.MinValue, format).Should().Be(unchecked((sbyte) double.MinValue));
      Convert.To.Sbyte(double.MaxValue, format).Should().Be(unchecked((sbyte) double.MaxValue));
      Convert.To.Sbyte(double.NaN, format).Should().Be(unchecked((sbyte) double.NaN));
      Convert.To.Sbyte(double.Epsilon, format).Should().Be((sbyte) double.Epsilon);
      Convert.To.Sbyte(double.NegativeInfinity, format).Should().Be(unchecked((sbyte) double.NegativeInfinity));
      Convert.To.Sbyte(double.PositiveInfinity, format).Should().Be(unchecked((sbyte) double.PositiveInfinity));
      Convert.To.Sbyte(1.5, format).Should().Be(2);

      Convert.To.Sbyte(decimal.Zero, format).Should().Be((sbyte) decimal.Zero);
      Convert.To.Sbyte(decimal.One, format).Should().Be((sbyte) decimal.One);
      Convert.To.Sbyte((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Sbyte(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Byte(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Byte_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Byte(null, format).Should().BeNull();
      Convert.To.Byte(string.Empty, format).Should().BeNull();
      Convert.To.Byte(byte.MinValue.ToString(format), format).Should().Be(byte.MinValue);
      Convert.To.Byte(byte.MaxValue.ToString(format), format).Should().Be(byte.MaxValue);
      Convert.To.Byte(new object(), format).Should().BeNull();

      Convert.To.Byte(sbyte.MinValue, format).Should().Be(unchecked((byte) sbyte.MinValue));
      Convert.To.Byte(sbyte.MaxValue, format).Should().Be((byte) sbyte.MaxValue);

      Convert.To.Byte(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Byte(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Byte(short.MinValue, format).Should().Be(unchecked((byte) short.MinValue));
      Convert.To.Byte(short.MaxValue, format).Should().Be(unchecked((byte) short.MaxValue));

      Convert.To.Byte(int.MinValue, format).Should().Be(unchecked((byte) int.MinValue));
      Convert.To.Byte(int.MaxValue, format).Should().Be(unchecked((byte) int.MaxValue));

      Convert.To.Byte(long.MinValue, format).Should().Be(unchecked((byte) long.MinValue));
      Convert.To.Byte(long.MaxValue, format).Should().Be(unchecked((byte) long.MaxValue));

      Convert.To.Byte(ushort.MinValue, format).Should().Be((byte) ushort.MinValue);
      Convert.To.Byte(ushort.MaxValue, format).Should().Be(unchecked((byte) ushort.MaxValue));

      Convert.To.Byte(uint.MinValue, format).Should().Be((byte) uint.MinValue);
      Convert.To.Byte(uint.MaxValue, format).Should().Be(unchecked((byte) uint.MaxValue));

      Convert.To.Byte(ulong.MinValue, format).Should().Be((byte) ulong.MinValue);
      Convert.To.Byte(ulong.MaxValue, format).Should().Be(unchecked((byte) ulong.MaxValue));

      Convert.To.Byte(float.MinValue, format).Should().Be(unchecked((byte) float.MinValue));
      Convert.To.Byte(float.MaxValue, format).Should().Be(unchecked((byte) float.MaxValue));
      Convert.To.Byte(float.NaN, format).Should().Be(unchecked((byte) float.NaN));
      Convert.To.Byte(float.Epsilon, format).Should().Be((byte) float.Epsilon);
      Convert.To.Byte(float.NegativeInfinity, format).Should().Be(unchecked((byte) float.NegativeInfinity));
      Convert.To.Byte(float.PositiveInfinity, format).Should().Be(unchecked((byte) float.PositiveInfinity));
      Convert.To.Byte((float) 1.5, format).Should().Be(2);

      Convert.To.Byte(double.MinValue, format).Should().Be(unchecked((byte) double.MinValue));
      Convert.To.Byte(double.MaxValue, format).Should().Be(unchecked((byte) double.MaxValue));
      Convert.To.Byte(double.NaN, format).Should().Be(unchecked((byte) double.NaN));
      Convert.To.Byte(double.Epsilon, format).Should().Be((byte) double.Epsilon);
      Convert.To.Byte(double.NegativeInfinity, format).Should().Be(unchecked((byte) double.NegativeInfinity));
      Convert.To.Byte(double.PositiveInfinity, format).Should().Be(unchecked((byte) double.PositiveInfinity));
      Convert.To.Byte(1.5, format).Should().Be(2);

      Convert.To.Byte(decimal.Zero, format).Should().Be((byte) decimal.Zero);
      Convert.To.Byte(decimal.One, format).Should().Be((byte) decimal.One);
      Convert.To.Byte((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Byte(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Short(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Short_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Short(null, format).Should().BeNull();
      Convert.To.Short(string.Empty, format).Should().BeNull();
      Convert.To.Short(short.MinValue.ToString(format), format).Should().Be(short.MinValue);
      Convert.To.Short(short.MaxValue.ToString(format), format).Should().Be(short.MaxValue);
      Convert.To.Short(new object(), format).Should().BeNull();

      Convert.To.Short(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Short(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Short(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Short(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Short(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Short(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Short(int.MinValue, format).Should().Be(unchecked((short) int.MinValue));
      Convert.To.Short(int.MaxValue, format).Should().Be(unchecked((short) int.MaxValue));

      Convert.To.Short(long.MinValue, format).Should().Be(unchecked((short) long.MinValue));
      Convert.To.Short(long.MaxValue, format).Should().Be(unchecked((short) long.MaxValue));

      Convert.To.Short(ushort.MinValue, format).Should().Be((short) ushort.MinValue);
      Convert.To.Short(ushort.MaxValue, format).Should().Be(unchecked((short) ushort.MaxValue));

      Convert.To.Short(uint.MinValue, format).Should().Be((short) uint.MinValue);
      Convert.To.Short(uint.MaxValue, format).Should().Be(unchecked((short) uint.MaxValue));

      Convert.To.Short(ulong.MinValue, format).Should().Be((short) ulong.MinValue);
      Convert.To.Short(ulong.MaxValue, format).Should().Be(unchecked((short) ulong.MaxValue));

      Convert.To.Short(float.MinValue, format).Should().Be(unchecked((short) float.MinValue));
      Convert.To.Short(float.MaxValue, format).Should().Be(unchecked((short) float.MaxValue));
      Convert.To.Short(float.NaN, format).Should().Be(unchecked((short) float.NaN));
      Convert.To.Short(float.Epsilon, format).Should().Be((short) float.Epsilon);
      Convert.To.Short(float.NegativeInfinity, format).Should().Be(unchecked((short) float.NegativeInfinity));
      Convert.To.Short(float.PositiveInfinity, format).Should().Be(unchecked((short) float.PositiveInfinity));
      Convert.To.Short((float) 1.5, format).Should().Be(2);

      Convert.To.Short(double.MinValue, format).Should().Be(unchecked((short) double.MinValue));
      Convert.To.Short(double.MaxValue, format).Should().Be(unchecked((short) double.MaxValue));
      Convert.To.Short(double.NaN, format).Should().Be(unchecked((short) double.NaN));
      Convert.To.Short(double.Epsilon, format).Should().Be((short) double.Epsilon);
      Convert.To.Short(double.NegativeInfinity, format).Should().Be(unchecked((short) double.NegativeInfinity));
      Convert.To.Short(double.PositiveInfinity, format).Should().Be(unchecked((short) double.PositiveInfinity));
      Convert.To.Short(1.5, format).Should().Be(2);

      Convert.To.Short(decimal.Zero, format).Should().Be((short) decimal.Zero);
      Convert.To.Short(decimal.One, format).Should().Be((short) decimal.One);
      Convert.To.Short((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Short(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Ushort(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Ushort_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Ushort(null, format).Should().BeNull();
      Convert.To.Ushort(string.Empty, format).Should().BeNull();
      Convert.To.Ushort(ushort.MinValue.ToString(format), format).Should().Be(ushort.MinValue);
      Convert.To.Ushort(ushort.MaxValue.ToString(format), format).Should().Be(ushort.MaxValue);
      Convert.To.Ushort(new object(), format).Should().BeNull();

      Convert.To.Ushort(sbyte.MinValue, format).Should().Be(unchecked((ushort) sbyte.MinValue));
      Convert.To.Ushort(sbyte.MaxValue, format).Should().Be((ushort) sbyte.MaxValue);

      Convert.To.Ushort(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Ushort(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Ushort(short.MinValue, format).Should().Be(unchecked((ushort) short.MinValue));
      Convert.To.Ushort(short.MaxValue, format).Should().Be(unchecked((ushort) short.MaxValue));

      Convert.To.Ushort(int.MinValue, format).Should().Be(unchecked((ushort) int.MinValue));
      Convert.To.Ushort(int.MaxValue, format).Should().Be(unchecked((ushort) int.MaxValue));

      Convert.To.Ushort(long.MinValue, format).Should().Be(unchecked((ushort) long.MinValue));
      Convert.To.Ushort(long.MaxValue, format).Should().Be(unchecked((ushort) long.MaxValue));

      Convert.To.Ushort(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Ushort(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Ushort(uint.MinValue, format).Should().Be((ushort) uint.MinValue);
      Convert.To.Ushort(uint.MaxValue, format).Should().Be(unchecked((ushort) uint.MaxValue));

      Convert.To.Ushort(ulong.MinValue, format).Should().Be((ushort) ulong.MinValue);
      Convert.To.Ushort(ulong.MaxValue, format).Should().Be(unchecked((ushort) ulong.MaxValue));

      Convert.To.Ushort(float.MinValue, format).Should().Be(unchecked((ushort) float.MinValue));
      Convert.To.Ushort(float.MaxValue, format).Should().Be(unchecked((ushort) float.MaxValue));
      Convert.To.Ushort(float.NaN, format).Should().Be(unchecked((ushort) float.NaN));
      Convert.To.Ushort(float.Epsilon, format).Should().Be((ushort) float.Epsilon);
      Convert.To.Ushort(float.NegativeInfinity, format).Should().Be(unchecked((ushort) float.NegativeInfinity));
      Convert.To.Ushort(float.PositiveInfinity, format).Should().Be(unchecked((ushort) float.PositiveInfinity));
      Convert.To.Ushort((float) 1.5, format).Should().Be(2);

      Convert.To.Ushort(double.MinValue, format).Should().Be(unchecked((ushort) double.MinValue));
      Convert.To.Ushort(double.MaxValue, format).Should().Be(unchecked((ushort) double.MaxValue));
      Convert.To.Ushort(double.NaN, format).Should().Be(unchecked((ushort) double.NaN));
      Convert.To.Ushort(double.Epsilon, format).Should().Be((ushort) double.Epsilon);
      Convert.To.Ushort(double.NegativeInfinity, format).Should().Be(unchecked((ushort) double.NegativeInfinity));
      Convert.To.Ushort(double.PositiveInfinity, format).Should().Be(unchecked((ushort) double.PositiveInfinity));
      Convert.To.Ushort(1.5, format).Should().Be(2);

      Convert.To.Ushort(decimal.Zero, format).Should().Be((ushort) decimal.Zero);
      Convert.To.Ushort(decimal.One, format).Should().Be((ushort) decimal.One);
      Convert.To.Ushort((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Ushort(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Int(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Int_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Int(null, format).Should().BeNull();
      Convert.To.Int(string.Empty, format).Should().BeNull();
      Convert.To.Int(int.MinValue.ToString(format), format).Should().Be(int.MinValue);
      Convert.To.Int(int.MaxValue.ToString(format), format).Should().Be(int.MaxValue);
      Convert.To.Int(new object(), format).Should().BeNull();

      Convert.To.Int(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Int(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Int(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Int(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Int(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Int(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Int(int.MinValue, format).Should().Be(int.MinValue);
      Convert.To.Int(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Int(long.MinValue, format).Should().Be(unchecked((int) long.MinValue));
      Convert.To.Int(long.MaxValue, format).Should().Be(unchecked((int) long.MaxValue));

      Convert.To.Int(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Int(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Int(uint.MinValue, format).Should().Be((int) uint.MinValue);
      Convert.To.Int(uint.MaxValue, format).Should().Be(unchecked((int) uint.MaxValue));

      Convert.To.Int(ulong.MinValue, format).Should().Be((int) ulong.MinValue);
      Convert.To.Int(ulong.MaxValue, format).Should().Be(unchecked((int) ulong.MaxValue));

      //Convert.To.Int(float.MinValue, culture).Should().Be(unchecked((int) float.MinValue));
      //Convert.To.Int(float.MaxValue, culture).Should().Be(unchecked((int) float.MaxValue));
      //Convert.To.Int(float.NaN, culture).Should().Be(unchecked((int) float.NaN));
      Convert.To.Int(float.Epsilon, format).Should().Be((int) float.Epsilon);
      //Convert.To.Int(float.NegativeInfinity, culture).Should().Be(unchecked((int) float.NegativeInfinity));
      //Convert.To.Int(float.PositiveInfinity, culture).Should().Be(unchecked((int) float.PositiveInfinity));
      Convert.To.Int((float) 1.5, format).Should().Be(2);
      
      //Convert.To.Int(double.MinValue, culture).Should().Be(unchecked((int) double.MinValue));
      //Convert.To.Int(double.MaxValue, culture).Should().Be(unchecked((int) double.MaxValue));
      //Convert.To.Int(double.NaN, culture).Should().Be(unchecked((int) double.NaN));
      Convert.To.Int(double.Epsilon, format).Should().Be((int) double.Epsilon);
      //Convert.To.Int(double.NegativeInfinity, culture).Should().Be(unchecked((int) double.NegativeInfinity));
      //Convert.To.Int(double.PositiveInfinity, culture).Should().Be(unchecked((int) double.PositiveInfinity));
      Convert.To.Int(1.5, format).Should().Be(2);

      Convert.To.Int(decimal.Zero, format).Should().Be((int) decimal.Zero);
      Convert.To.Int(decimal.One, format).Should().Be((int) decimal.One);
      Convert.To.Int((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Int(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Uint(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Uint_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Uint(null, format).Should().BeNull();
      Convert.To.Uint(string.Empty, format).Should().BeNull();
      Convert.To.Uint(uint.MinValue.ToString(format), format).Should().Be(uint.MinValue);
      Convert.To.Uint(uint.MaxValue.ToString(format), format).Should().Be(uint.MaxValue);
      Convert.To.Sbyte(new object(), format).Should().BeNull();

      Convert.To.Uint(sbyte.MinValue, format).Should().Be(unchecked((uint) sbyte.MinValue));
      Convert.To.Uint(sbyte.MaxValue, format).Should().Be((uint) sbyte.MaxValue);

      Convert.To.Uint(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Uint(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Uint(short.MinValue, format).Should().Be(unchecked((uint) short.MinValue));
      Convert.To.Uint(short.MaxValue, format).Should().Be(unchecked((uint) short.MaxValue));

      Convert.To.Uint(int.MinValue, format).Should().Be(unchecked((uint) int.MinValue));
      Convert.To.Uint(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Uint(long.MinValue, format).Should().Be(unchecked((uint) long.MinValue));
      Convert.To.Uint(long.MaxValue, format).Should().Be(unchecked((uint) long.MaxValue));

      Convert.To.Uint(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Uint(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Uint(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Uint(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Uint(ulong.MinValue, format).Should().Be((uint) ulong.MinValue);
      Convert.To.Uint(ulong.MaxValue, format).Should().Be(unchecked((uint) ulong.MaxValue));

      Convert.To.Uint(float.MinValue, format).Should().Be(unchecked((uint) float.MinValue));
      Convert.To.Uint(float.MaxValue, format).Should().Be(unchecked((uint) float.MaxValue));
      Convert.To.Uint(float.NaN, format).Should().Be(unchecked((uint) float.NaN));
      Convert.To.Uint(float.Epsilon, format).Should().Be((uint) float.Epsilon);
      Convert.To.Uint(float.NegativeInfinity, format).Should().Be(unchecked((uint) float.NegativeInfinity));
      Convert.To.Uint(float.PositiveInfinity, format).Should().Be(unchecked((uint) float.PositiveInfinity));
      Convert.To.Uint((float) 1.5, format).Should().Be(2);

      Convert.To.Uint(double.MinValue, format).Should().Be(unchecked((uint) double.MinValue));
      Convert.To.Uint(double.MaxValue, format).Should().Be(unchecked((uint) double.MaxValue));
      Convert.To.Uint(double.NaN, format).Should().Be(unchecked((uint) double.NaN));
      Convert.To.Uint(double.Epsilon, format).Should().Be((uint) double.Epsilon);
      Convert.To.Uint(double.NegativeInfinity, format).Should().Be(unchecked((uint) double.NegativeInfinity));
      Convert.To.Uint(double.PositiveInfinity, format).Should().Be(unchecked((uint) double.PositiveInfinity));
      Convert.To.Uint(1.5, format).Should().Be(2);

      Convert.To.Uint(decimal.Zero, format).Should().Be((uint) decimal.Zero);
      Convert.To.Uint(decimal.One, format).Should().Be((uint) decimal.One);
      Convert.To.Uint(1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Uint(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Long(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Long_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Long(null, format).Should().BeNull();
      Convert.To.Long(string.Empty, format).Should().BeNull();
      Convert.To.Long(long.MinValue.ToString(format), format).Should().Be(long.MinValue);
      Convert.To.Long(long.MaxValue.ToString(format), format).Should().Be(long.MaxValue);
      Convert.To.Long(new object(), format).Should().BeNull();

      Convert.To.Long(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Long(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Long(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Long(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Long(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Long(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Long(int.MinValue, format).Should().Be(int.MinValue);
      Convert.To.Long(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Long(long.MinValue, format).Should().Be(long.MinValue);
      Convert.To.Long(long.MaxValue, format).Should().Be(long.MaxValue);

      Convert.To.Long(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Long(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Long(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Long(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Long(ulong.MinValue, format).Should().Be((long) ulong.MinValue);
      Convert.To.Long(ulong.MaxValue, format).Should().Be(unchecked((long) ulong.MaxValue));

      //Convert.To.Long(float.MinValue, culture).Should().Be(unchecked((long) float.MinValue));
      //Convert.To.Long(float.MaxValue, culture).Should().Be(unchecked((long) float.MaxValue));
      //Convert.To.Long(float.NaN, culture).Should().Be(unchecked((long) float.NaN));
      Convert.To.Long(float.Epsilon, format).Should().Be((long) float.Epsilon);
      //Convert.To.Long(float.NegativeInfinity, culture).Should().Be(unchecked((long) float.NegativeInfinity));
      //Convert.To.Long(float.PositiveInfinity, culture).Should().Be(unchecked((long) float.PositiveInfinity));
      Convert.To.Long((float) 1.5, format).Should().Be(2);

      //Convert.To.Long(double.MinValue, culture).Should().Be(unchecked((long) double.MinValue));
      //Convert.To.Long(double.MaxValue, culture).Should().Be(unchecked((long) double.MaxValue));
      //Convert.To.Long(double.NaN, culture).Should().Be(unchecked((long) double.NaN));
      Convert.To.Long(double.Epsilon, format).Should().Be((long) double.Epsilon);
      //Convert.To.Long(double.NegativeInfinity, culture).Should().Be(unchecked((long) double.NegativeInfinity));
      //Convert.To.Long(double.PositiveInfinity, format).Should().Be(unchecked((long) double.PositiveInfinity));
      Convert.To.Long(1.5, format).Should().Be(2);

      Convert.To.Long(decimal.MinusOne, format).Should().Be((long) decimal.MinusOne);
      Convert.To.Long(decimal.Zero, format).Should().Be((long) decimal.Zero);
      Convert.To.Long(decimal.One, format).Should().Be((long) decimal.One);
      Convert.To.Long((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Long(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Ulong(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Ulong_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Ulong(null, format).Should().BeNull();
      Convert.To.Ulong(string.Empty, format).Should().BeNull();
      Convert.To.Ulong(ulong.MinValue.ToString(format), format).Should().Be(ulong.MinValue);
      Convert.To.Ulong(ulong.MaxValue.ToString(format), format).Should().Be(ulong.MaxValue);
      Convert.To.Ulong(new object(), format).Should().BeNull();

      Convert.To.Ulong(sbyte.MinValue, format).Should().Be(unchecked((ulong) sbyte.MinValue));
      Convert.To.Ulong(sbyte.MaxValue, format).Should().Be((ulong) sbyte.MaxValue);

      Convert.To.Ulong(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Ulong(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Ulong(short.MinValue, format).Should().Be(unchecked((ulong) short.MinValue));
      Convert.To.Ulong(short.MaxValue, format).Should().Be(unchecked((ulong) short.MaxValue));

      Convert.To.Ulong(int.MinValue, format).Should().Be(unchecked((ulong) int.MinValue));
      Convert.To.Ulong(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Ulong(long.MinValue, format).Should().Be(unchecked((ulong) long.MinValue));
      Convert.To.Ulong(long.MaxValue, format).Should().Be(long.MaxValue);

      Convert.To.Ulong(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Ulong(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Ulong(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Ulong(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Ulong(ulong.MinValue, format).Should().Be(ulong.MinValue);
      Convert.To.Ulong(ulong.MaxValue, format).Should().Be(ulong.MaxValue);

      //Convert.To.ULong(float.MinValue, culture).Should().Be(unchecked((ulong) float.MinValue));
      Convert.To.Ulong(float.MaxValue, format).Should().Be(unchecked((ulong) float.MaxValue));
      Convert.To.Ulong(float.NaN, format).Should().Be(unchecked((ulong) float.NaN));
      Convert.To.Ulong(float.Epsilon, format).Should().Be((ulong) float.Epsilon);
      //Convert.To.ULong(float.NegativeInfinity, culture).Should().Be(unchecked((ulong) float.NegativeInfinity));
      //Convert.To.ULong(float.PositiveInfinity, culture).Should().Be(unchecked((ulong) float.PositiveInfinity));
      Convert.To.Ulong((float) 1.5, format).Should().Be(2);

      //Convert.To.ULong(double.MinValue, culture).Should().Be(unchecked((ulong) double.MinValue));
      Convert.To.Ulong(double.MaxValue, format).Should().Be(unchecked((ulong) double.MaxValue));
      Convert.To.Ulong(double.NaN, format).Should().Be(unchecked((ulong) double.NaN));
      Convert.To.Ulong(double.Epsilon, format).Should().Be((ulong) double.Epsilon);
      //Convert.To.ULong(double.NegativeInfinity, culture).Should().Be(unchecked((ulong) double.NegativeInfinity));
      //Convert.To.ULong(double.PositiveInfinity, culture).Should().Be(unchecked((ulong) double.PositiveInfinity));
      Convert.To.Ulong(1.5, format).Should().Be(2);

      Convert.To.Ulong(decimal.Zero, format).Should().Be((ulong) decimal.Zero);
      Convert.To.Ulong(decimal.One, format).Should().Be((ulong) decimal.One);
      Convert.To.Ulong((decimal) 1.5, format).Should().Be(2);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Ulong(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Float(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Float_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Float(null, format).Should().BeNull();
      Convert.To.Float(string.Empty, format).Should().BeNull();
      Convert.To.Float(new object(), format).Should().BeNull();

      Convert.To.Float(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Float(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Float(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Float(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Float(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Float(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Float(int.MinValue, format).Should().Be(int.MinValue);
      Convert.To.Float(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Float(long.MinValue, format).Should().Be(long.MinValue);
      Convert.To.Float(long.MaxValue, format).Should().Be(long.MaxValue);

      Convert.To.Float(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Float(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Float(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Float(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Float(ulong.MinValue, format).Should().Be(ulong.MinValue);
      Convert.To.Float(ulong.MaxValue, format).Should().Be(ulong.MaxValue);

      Convert.To.Float(float.MinValue, format).Should().Be(float.MinValue);
      Convert.To.Float(float.MaxValue, format).Should().Be(float.MaxValue);
      Convert.To.Float(float.NaN, format).Should().Be(float.NaN);
      Convert.To.Float(float.Epsilon, format).Should().Be(float.Epsilon);
      Convert.To.Float(float.NegativeInfinity, format).Should().Be(float.NegativeInfinity);
      Convert.To.Float(float.PositiveInfinity, format).Should().Be(float.PositiveInfinity);

      Convert.To.Float(double.MinValue, format).Should().Be((float) double.MinValue);
      Convert.To.Float(double.MaxValue, format).Should().Be((float) double.MaxValue);
      Convert.To.Float(double.NaN, format).Should().Be((float) double.NaN);
      Convert.To.Float(double.Epsilon, format).Should().Be((float) double.Epsilon);
      Convert.To.Float(double.NegativeInfinity, format).Should().Be((float) double.NegativeInfinity);
      Convert.To.Float(double.PositiveInfinity, format).Should().Be((float) double.PositiveInfinity);

      Convert.To.Float(decimal.MinValue, format).Should().Be((float) decimal.MinValue);
      Convert.To.Float(decimal.MaxValue, format).Should().Be((float) decimal.MaxValue);
      Convert.To.Float(decimal.MinusOne, format).Should().Be((float) decimal.MinusOne);
      Convert.To.Float(decimal.Zero, format).Should().Be((float) decimal.Zero);
      Convert.To.Float(decimal.One, format).Should().Be((float) decimal.One);

      if (format != null)
      {
        Convert.To.Float(float.MinValue.ToString(format), format).Should().Be(float.MinValue);
        Convert.To.Float(float.MaxValue.ToString(format), format).Should().Be(float.MaxValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Float(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Double(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Double_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Double(null, format).Should().BeNull();
      Convert.To.Double(string.Empty, format).Should().BeNull();
      Convert.To.Double(new object(), format).Should().BeNull();

      Convert.To.Double(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Double(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Double(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Double(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Double(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Double(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Double(int.MinValue, format).Should().Be(int.MinValue);
      Convert.To.Double(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Double(long.MinValue, format).Should().Be(long.MinValue);
      Convert.To.Double(long.MaxValue, format).Should().Be(long.MaxValue);

      Convert.To.Double(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Double(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Double(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Double(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Double(ulong.MinValue, format).Should().Be(ulong.MinValue);
      Convert.To.Double(ulong.MaxValue, format).Should().Be(ulong.MaxValue);

      Convert.To.Double(float.MinValue, format).Should().Be(float.MinValue);
      Convert.To.Double(float.MaxValue, format).Should().Be(float.MaxValue);
      Convert.To.Double(float.NaN, format).Should().Be(float.NaN);
      Convert.To.Double(float.Epsilon, format).Should().Be(float.Epsilon);
      Convert.To.Double(float.NegativeInfinity, format).Should().Be(float.NegativeInfinity);
      Convert.To.Double(float.PositiveInfinity, format).Should().Be(float.PositiveInfinity);

      Convert.To.Double(double.MinValue, format).Should().Be(double.MinValue);
      Convert.To.Double(double.MaxValue, format).Should().Be(double.MaxValue);
      Convert.To.Double(double.NaN, format).Should().Be(double.NaN);
      Convert.To.Double(double.Epsilon, format).Should().Be(double.Epsilon);
      Convert.To.Double(double.NegativeInfinity, format).Should().Be(double.NegativeInfinity);
      Convert.To.Double(double.PositiveInfinity, format).Should().Be(double.PositiveInfinity);

      Convert.To.Double(decimal.MinValue, format).Should().Be((double) decimal.MinValue);
      Convert.To.Double(decimal.MaxValue, format).Should().Be((double) decimal.MaxValue);
      Convert.To.Double(decimal.MinusOne, format).Should().Be((double) decimal.MinusOne);
      Convert.To.Double(decimal.Zero, format).Should().Be((double) decimal.Zero);
      Convert.To.Double(decimal.One, format).Should().Be((double) decimal.One);

      if (format != null)
      {
        Convert.To.Double(double.MinValue.ToString(format), format).Should().Be(double.MinValue);
        Convert.To.Double(double.MaxValue.ToString(format), format).Should().Be(double.MaxValue);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Double(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Decimal(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Decimal_Method()
  {
    static void Validate(IFormatProvider format)
    {
      Convert.To.Decimal(null, format).Should().BeNull();
      Convert.To.Decimal(string.Empty, format).Should().BeNull();
      Convert.To.Decimal(decimal.MinValue.ToString(format), format).Should().Be(decimal.MinValue);
      Convert.To.Decimal(decimal.MaxValue.ToString(format), format).Should().Be(decimal.MaxValue);
      Convert.To.Decimal(new object(), format).Should().BeNull();

      Convert.To.Decimal(sbyte.MinValue, format).Should().Be(sbyte.MinValue);
      Convert.To.Decimal(sbyte.MaxValue, format).Should().Be(sbyte.MaxValue);

      Convert.To.Decimal(byte.MinValue, format).Should().Be(byte.MinValue);
      Convert.To.Decimal(byte.MaxValue, format).Should().Be(byte.MaxValue);

      Convert.To.Decimal(short.MinValue, format).Should().Be(short.MinValue);
      Convert.To.Decimal(short.MaxValue, format).Should().Be(short.MaxValue);

      Convert.To.Decimal(int.MinValue, format).Should().Be(int.MinValue);
      Convert.To.Decimal(int.MaxValue, format).Should().Be(int.MaxValue);

      Convert.To.Decimal(long.MinValue, format).Should().Be(long.MinValue);
      Convert.To.Decimal(long.MaxValue, format).Should().Be(long.MaxValue);

      Convert.To.Decimal(ushort.MinValue, format).Should().Be(ushort.MinValue);
      Convert.To.Decimal(ushort.MaxValue, format).Should().Be(ushort.MaxValue);

      Convert.To.Decimal(uint.MinValue, format).Should().Be(uint.MinValue);
      Convert.To.Decimal(uint.MaxValue, format).Should().Be(uint.MaxValue);

      Convert.To.Decimal(ulong.MinValue, format).Should().Be(ulong.MinValue);
      Convert.To.Decimal(ulong.MaxValue, format).Should().Be(ulong.MaxValue);

      Convert.To.Decimal(float.Epsilon, format).Should().Be((decimal) float.Epsilon);

      Convert.To.Decimal(double.Epsilon, format).Should().Be((decimal) double.Epsilon);

      Convert.To.Decimal(decimal.MinValue, format).Should().Be(decimal.MinValue);
      Convert.To.Decimal(decimal.MaxValue, format).Should().Be(decimal.MaxValue);
      Convert.To.Decimal(decimal.MinusOne, format).Should().Be(decimal.MinusOne);
      Convert.To.Decimal(decimal.Zero, format).Should().Be(decimal.Zero);
      Convert.To.Decimal(decimal.One, format).Should().Be(decimal.One);
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.Decimal(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Enum{T}(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Enum_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Enum<DayOfWeek>(null, DayOfWeek.Monday)).ThrowExactly<ArgumentNullException>();

    Convert.To.Enum<DayOfWeek>(string.Empty).Should().BeNull();

    Enum.GetValues<DayOfWeek>().Select(day => Convert.To.Enum<DayOfWeek>(day)).Should().Equal(Enum.GetValues<DayOfWeek>().Cast<DayOfWeek?>());
    Enum.GetNames<DayOfWeek>().Select(day => Convert.To.Enum<DayOfWeek>(day)).Should().Equal(Enum.GetValues<DayOfWeek>().Cast<DayOfWeek?>());
    Enum.GetNames<DayOfWeek>().Select(day => Convert.To.Enum<DayOfWeek>(day.ToLower())).Should().Equal(Enum.GetValues<DayOfWeek>().Cast<DayOfWeek?>());
    Enum.GetNames<DayOfWeek>().Select(day => Convert.To.Enum<DayOfWeek>(day.ToUpper())).Should().Equal(Enum.GetValues<DayOfWeek>().Cast<DayOfWeek?>());
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.DateTime(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_DateTime_Method()
  {
    static void Validate(IFormatProvider format)
    {
      foreach (var date in new[] {DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow})
      {
        var utcDate = date.ToUniversalTime();

        Convert.To.DateTime(null, format).Should().BeNull();
        Convert.To.DateTime(string.Empty, format).Should().BeNull();
        Convert.To.DateTime(new object(), format).Should().BeNull();
        Convert.To.DateTime($" {utcDate.ToString("o", format)} ", format).Should().Be(utcDate);

        Convert.To.DateTime(date, format).Should().Be(date);
        Convert.To.DateTime(date.ToDateTimeOffset(), format).Should().Be(utcDate);
        Convert.To.DateTime(date.ToDateOnly(), format).Should().Be(date.TruncateToDayStart());
        Convert.To.DateTime(date.ToTimeOnly(), format).Should().Be(DateTime.UtcNow.TruncateToDayStart().Add(date.ToTimeOnly().ToTimeSpan()));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.DateTime(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.DateTimeOffset(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_DateTimeOffset_Method()
  {
    static void Validate(IFormatProvider format)
    {
      foreach (var date in new[] {DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow})
      {
        var utcDate = date.ToUniversalTime();

        Convert.To.DateTimeOffset(null, format).Should().BeNull();
        Convert.To.DateTimeOffset(string.Empty, format).Should().BeNull();
        Convert.To.DateTimeOffset(new object(), format).Should().BeNull();
        Convert.To.DateTimeOffset($" {utcDate.ToString("o", format)} ", format).Should().Be(utcDate);

        Convert.To.DateTimeOffset(date, format).Should().Be(utcDate);
        Convert.To.DateTimeOffset(date.ToDateTimeOffset(), format).Should().Be(utcDate);
        Convert.To.DateTimeOffset(date.ToDateOnly(), format).Should().Be(date.ToDateTimeOffset().TruncateToDayStart());
        Convert.To.DateTimeOffset(date.ToTimeOnly(), format).Should().Be(DateTime.UtcNow.TruncateToDayStart().Add(date.ToTimeOnly().ToTimeSpan()));
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.DateTimeOffset(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.DateOnly(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_DateOnly_Method()
  {
    static void Validate(IFormatProvider format)
    {
      foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
      {
        var dateOnly = date.ToDateOnly();

        Convert.To.DateOnly(null, format).Should().BeNull();
        Convert.To.DateOnly(string.Empty, format).Should().BeNull();
        Convert.To.DateOnly(new object(), format).Should().BeNull();
        //Convert.To.DateOnly($" {dateOnly.ToShortDateString()} ").Should().Be(dateOnly);
        //Convert.To.DateOnly($" {dateOnly.ToLongDateString()} ").Should().Be(dateOnly);

        Convert.To.DateOnly(date, format).Should().Be(dateOnly);
        Convert.To.DateOnly(date.ToDateTimeOffset(), format).Should().Be(dateOnly);
        Convert.To.DateOnly(dateOnly, format).Should().Be(dateOnly);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.DateOnly(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.TimeOnly(Convert, object, IFormatProvider)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_TimeOnly_Method()
  {
    static void Validate(IFormatProvider format)
    {
      foreach (var date in new[] { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.UtcNow })
      {
        var utcDate = date.ToUniversalTime();
        var timeOnly = utcDate.ToTimeOnly();

        Convert.To.TimeOnly(null, format).Should().BeNull();
        Convert.To.TimeOnly(string.Empty, format).Should().BeNull();
        Convert.To.TimeOnly(new object(), format).Should().BeNull();
        Convert.To.TimeOnly($" {timeOnly.ToShortTimeString()} ").Should().Be(timeOnly.TruncateToMinuteStart());
        Convert.To.TimeOnly($" {timeOnly.ToLongTimeString()} ").Should().Be(timeOnly.TruncateToSecondStart());

        Convert.To.TimeOnly(utcDate, format).Should().Be(timeOnly);
        Convert.To.TimeOnly(utcDate.ToDateTimeOffset(), format).Should().Be(timeOnly);
        Convert.To.TimeOnly(timeOnly, format).Should().Be(timeOnly);
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ConvertExtensions.TimeOnly(null, new object())).ThrowExactly<ArgumentNullException>();

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Guid(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Guid_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Guid(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.Guid(null).Should().BeNull();
    Convert.To.Guid(string.Empty).Should().BeNull();
    Convert.To.Guid(new object()).Should().BeNull();
    Convert.To.Guid(Array.Empty<byte>()).Should().BeNull();

    foreach (var guid in new[] { Guid.Empty, Guid.NewGuid() })
    {
      Convert.To.Guid(guid).Should().Be(guid);
      Convert.To.Guid(guid.ToByteArray()).Should().Be(guid);
      
      Convert.To.Guid(guid.ToString()).Should().Be(guid);
      Convert.To.Guid(guid.ToString("N")).Should().Be(guid);
      Convert.To.Guid(guid.ToString("D")).Should().Be(guid);
      Convert.To.Guid(guid.ToString("B")).Should().Be(guid);
      Convert.To.Guid(guid.ToString("P")).Should().Be(guid);
      Convert.To.Guid(guid.ToString("X")).Should().Be(guid);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Regex(Convert, object, RegexOptions)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Regex_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Regex(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.Regex(null).Should().BeNull();

    var regex = new Regex(string.Empty);
    Convert.To.Regex(regex).Should().NotBeNull().And.BeSameAs(regex);

    regex = Convert.To.Regex(string.Empty);
    regex.Should().NotBeNull();
    regex.ToString().Should().BeEmpty();
    regex.Options.Should().Be(RegexOptions.None);
    regex.MatchTimeout.Should().Be(Timeout.InfiniteTimeSpan);
    regex.RightToLeft.Should().BeFalse();

    regex = Convert.To.Regex("[a-z]*", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
    regex.Should().NotBeNull();
    regex.ToString().Should().Be("[a-z]*");
    regex.Options.Should().Be(RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
    regex.MatchTimeout.Should().Be(Timeout.InfiniteTimeSpan);
    regex.RightToLeft.Should().BeTrue();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Uri(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Uri_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Uri(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.Uri(null).Should().BeNull();

    var uri = "https://localhost".ToUri();
    Convert.To.Uri(uri).Should().NotBeNull().And.BeSameAs(uri);

    uri = Convert.To.Uri(string.Empty);
    uri.Should().NotBeNull();
    uri.IsAbsoluteUri.Should().BeFalse();
    uri.OriginalString.Should().BeEmpty();
    uri.ToString().Should().BeEmpty();

    uri = Convert.To.Uri("path");
    uri.Should().NotBeNull();
    uri.IsAbsoluteUri.Should().BeFalse();
    uri.OriginalString.Should().Be("path");
    uri.ToString().Should().Be("path");

    uri = Convert.To.Uri("scheme:");
    uri.Should().NotBeNull();
    uri.IsAbsoluteUri.Should().BeTrue();
    uri.OriginalString.Should().Be("scheme:");
    uri.ToString().Should().Be("scheme:");

    uri = Convert.To.Uri("https://user:password@localhost:8080/path?query#id");
    uri.Should().NotBeNull();
    uri.IsAbsoluteUri.Should().BeTrue();
    uri.OriginalString.Should().Be("https://user:password@localhost:8080/path?query#id");
    uri.AbsolutePath.Should().Be("/path");
    uri.AbsoluteUri.Should().Be("https://user:password@localhost:8080/path?query#id");
    uri.Authority.Should().Be("localhost:8080");
    uri.Fragment.Should().Be("#id");
    uri.Host.Should().Be("localhost");
    uri.IsDefaultPort.Should().BeFalse();
    uri.IsFile.Should().BeFalse();
    uri.IsLoopback.Should().BeTrue();
    uri.IsUnc.Should().BeFalse();
    uri.LocalPath.Should().Be("/path");
    uri.PathAndQuery.Should().Be("/path?query");
    uri.Port.Should().Be(8080);
    uri.Query.Should().Be("?query");
    uri.Scheme.Should().Be(Uri.UriSchemeHttps);
    uri.UserEscaped.Should().BeFalse();
    uri.UserInfo.Should().Be("user:password");
    uri.ToString().Should().Be("https://user:password@localhost:8080/path?query#id");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.StringBuilder(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_StringBuilder_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.StringBuilder(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.StringBuilder(null).Should().BeNull();

    var builder = new StringBuilder();
    Convert.To.StringBuilder(builder).Should().NotBeNull().And.BeSameAs(builder);

    builder = Convert.To.StringBuilder(string.Empty);
    builder.Should().NotBeNull();
    builder.Length.Should().Be(0);
    builder.Capacity.Should().Be(16);
    builder.MaxCapacity.Should().Be(int.MaxValue);
    builder.ToString().Should().BeEmpty();

    var value = RandomString;
    builder = Convert.To.StringBuilder(value);
    builder.Should().NotBeNull();
    builder.Length.Should().Be(value.Length);
    builder.Capacity.Should().Be(value.Length);
    builder.MaxCapacity.Should().Be(int.MaxValue);
    builder.ToString().Should().Be(value);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.IpAddress(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_IpAddress_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.IpAddress(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.IpAddress(null).Should().BeNull();
    Convert.To.IpAddress(string.Empty).Should().BeNull();
    Convert.To.IpAddress("localhost").Should().BeNull();

    foreach (var ip in new[] { IPAddress.None, IPAddress.Any, IPAddress.Loopback, IPAddress.Broadcast, IPAddress.IPv6None, IPAddress.IPv6Any, IPAddress.IPv6Loopback })
    {
      Convert.To.IpAddress(ip).Should().NotBeNull().And.BeSameAs(ip);
      Convert.To.IpAddress(ip.ToString()).Should().Be(ip);

      if (ip.AddressFamily == AddressFamily.InterNetwork)
      {
        Convert.To.IpAddress(ip.Address).Should().Be(ip);
        Convert.To.IpAddress((uint) ip.Address).Should().Be(ip);
      }
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Directory(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Directory_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Directory(null, new object())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Convert.To.Directory(string.Empty)).ThrowExactly<ArgumentException>();

    Convert.To.Directory(null).Should().BeNull();

    Convert.To.Directory(RandomName).Should().BeNull();

    var directory = RandomFakeDirectory;
    Convert.To.Directory(directory).Should().NotBeNull().And.BeSameAs(directory);

    RandomDirectory.TryFinallyDelete(directory =>
    {
      Convert.To.Directory(directory.FullName).Should().NotBeNull().And.NotBeSameAs(Convert.To.Directory(directory.FullName));
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.File(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_File_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.File(null, new object())).ThrowExactly<ArgumentNullException>();
    AssertionExtensions.Should(() => Convert.To.File(string.Empty)).ThrowExactly<ArgumentException>();

    Convert.To.File(null).Should().BeNull();

    Convert.To.File(RandomName).Should().BeNull();

    var file = RandomFakeFile;
    Convert.To.File(file).Should().NotBeNull().And.BeSameAs(file);

    RandomEmptyFile.TryFinallyDelete(file =>
    {
      Convert.To.File(file.FullName).Should().NotBeNull().And.NotBeSameAs(Convert.To.File(file.FullName));
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Type(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Type_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Type(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.Type(null).Should().BeNull();
    Convert.To.Type(string.Empty).Should().BeNull();
    Convert.To.Type(RandomName).Should().BeNull();
    
    Convert.To.Type(nameof(Object)).Should().BeNull();
    Convert.To.Type(typeof(object).FullName).Should().NotBeNull().And.BeSameAs(Convert.To.Type(typeof(object).FullName)).And.Be(typeof(object));
    Convert.To.Type(typeof(object).AssemblyQualifiedName).Should().NotBeNull().And.BeSameAs(Convert.To.Type(typeof(object).AssemblyQualifiedName)).And.Be(typeof(object));

    Assembly.GetExecutingAssembly().DefinedTypes.ForEach(type =>
    {
      Convert.To.Type(nameof(type)).Should().BeNull();
      Convert.To.Type(type.AssemblyQualifiedName).Should().NotBeNull().And.BeSameAs(Convert.To.Type(type.AssemblyQualifiedName)).And.Be(type);
    });
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ConvertExtensions.Boolean(Convert, object)"/> method.</para>
  /// </summary>
  [Fact]
  public void Convert_Boolean_Method()
  {
    AssertionExtensions.Should(() => ConvertExtensions.Boolean(null, new object())).ThrowExactly<ArgumentNullException>();

    Convert.To.Boolean(null).Should().BeFalse();
    
    Convert.To.Boolean(false).Should().BeFalse();
    Convert.To.Boolean(true).Should().BeTrue();

    Convert.To.Boolean(char.MinValue).Should().BeFalse();
    Convert.To.Boolean(char.MaxValue).Should().BeTrue();
    Convert.To.Boolean((char) 0).Should().BeFalse();

    Convert.To.Boolean(sbyte.MinValue).Should().BeFalse();
    Convert.To.Boolean(sbyte.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(byte.MinValue).Should().BeFalse();
    Convert.To.Boolean(byte.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(short.MinValue).Should().BeFalse();
    Convert.To.Boolean(short.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(ushort.MinValue).Should().BeFalse();
    Convert.To.Boolean(ushort.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(int.MinValue).Should().BeFalse();
    Convert.To.Boolean(int.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(uint.MinValue).Should().BeFalse();
    Convert.To.Boolean(uint.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(long.MinValue).Should().BeFalse();
    Convert.To.Boolean(long.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(ulong.MinValue).Should().BeFalse();
    Convert.To.Boolean(ulong.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0).Should().BeFalse();

    Convert.To.Boolean(float.MinValue).Should().BeFalse();
    Convert.To.Boolean(float.MaxValue).Should().BeTrue();
    Convert.To.Boolean((float) 0.0).Should().BeFalse();
    Convert.To.Boolean(float.NaN).Should().BeFalse();
    Convert.To.Boolean(float.Epsilon).Should().BeTrue();
    Convert.To.Boolean(float.NegativeInfinity).Should().BeFalse();
    Convert.To.Boolean(float.PositiveInfinity).Should().BeTrue();

    Convert.To.Boolean(double.MinValue).Should().BeFalse();
    Convert.To.Boolean(double.MaxValue).Should().BeTrue();
    Convert.To.Boolean(0.0).Should().BeFalse();
    Convert.To.Boolean(double.NaN).Should().BeFalse();
    Convert.To.Boolean(double.Epsilon).Should().BeTrue();
    Convert.To.Boolean(double.NegativeInfinity).Should().BeFalse();
    Convert.To.Boolean(double.PositiveInfinity).Should().BeTrue();

    Convert.To.Boolean(decimal.MinValue).Should().BeFalse();
    Convert.To.Boolean(decimal.MaxValue).Should().BeTrue();
    Convert.To.Boolean(decimal.Zero).Should().BeFalse();
    Convert.To.Boolean(decimal.MinusOne).Should().BeFalse();
    Convert.To.Boolean(decimal.One).Should().BeTrue();

    Convert.To.Boolean(string.Empty).Should().BeFalse();
    Convert.To.Boolean(" ").Should().BeFalse();
    Convert.To.Boolean("a").Should().BeTrue();

    Convert.To.Boolean(Enumerable.Empty<object>()).Should().BeFalse();
    Convert.To.Boolean(new object[] { new() }).Should().BeTrue();

    Convert.To.Boolean(RandomFakeFile).Should().BeFalse();
    Convert.To.Boolean(Assembly.GetExecutingAssembly().Location.ToFile()).Should().BeTrue();

    Convert.To.Boolean(RandomFakeDirectory).Should().BeFalse();
    Convert.To.Boolean(Environment.SystemDirectory.ToDirectory()).Should().BeTrue();

    Convert.To.Boolean(Stream.Null).Should().BeFalse();
    using (var stream = Randomizer.MemoryStreamAsync(1).Await())
    {
      Convert.To.Boolean(stream).Should().BeTrue();
    }

    using (var reader = Stream.Null.ToBinaryReader())
    {
      Convert.To.Boolean(reader).Should().BeFalse();
    }
    using (var stream = Randomizer.MemoryStreamAsync(1, byte.MinValue, byte.MinValue).Await())
    {
      using (var reader = stream.ToBinaryReader())
      {
        Convert.To.Boolean(reader).Should().BeTrue();
      }
    }

    using (var reader = string.Empty.ToStringReader())
    {
      Convert.To.Boolean(reader).Should().BeFalse();
    }
    using (var reader = Randomizer.String(1).ToStringReader())
    {
      Convert.To.Boolean(reader).Should().BeTrue();
    }

    Convert.To.Boolean(Match.Empty).Should().BeFalse();
    Convert.To.Boolean(Regex.Match(RandomName, ".*")).Should().BeTrue();

    Convert.To.Boolean(new StringBuilder()).Should().BeFalse();
    Convert.To.Boolean(new StringBuilder(string.Empty)).Should().BeFalse();
    Convert.To.Boolean(new StringBuilder(Randomizer.String(1))).Should().BeTrue();

    using (var secure = new SecureString())
    {
      Convert.To.Boolean(secure).Should().BeFalse();
    }
    using (var secure = new SecureString())
    {
      secure.AppendChar(char.MinValue);
      Convert.To.Boolean(secure).Should().BeTrue();
    }
    
    Convert.To.Boolean(new object()).Should().BeTrue();
  }
}