using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xunit;

namespace Catharsis.Commons.Extensions
{
  /// <summary>
  ///   <para>Tests set for class <see cref="UriExtensions"/>.</para>
  /// </summary>
  public sealed class UriExtensionsTests
  {
    const string Yandex = "http://yandex.ru";

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.Bytes(Uri)"/> method.</para>
    /// </summary>
    [Fact]
    public void Bytes_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Bytes(null));

      Assert.True(new Uri(Yandex).Bytes().Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.DownloadFile(Uri, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void DownloadFile_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.DownloadFile(null, "file"));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.DownloadFile(new Uri(Yandex), null));
      Assert.Throws<ArgumentException>(() => UriExtensions.DownloadFile(new Uri(Yandex), string.Empty));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.DownloadFile(null));

      var uri = new Uri(Yandex);
      var file = Path.GetTempFileName();
      Assert.True(ReferenceEquals(uri.DownloadFile(file), uri));
      Assert.True(new FileInfo(file).Length > 0);
      Assert.True(new FileInfo(uri.DownloadFile()).Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.Stream(Uri)"/> method.</para>
    /// </summary>
    [Fact]
    public void Stream_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Stream(null));

      var stream = new Uri(Yandex).Stream();
      Assert.True(stream.Bytes().Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.TextReader(Uri, Encoding)"/> method.</para>
    /// </summary>
    [Fact]
    public void TextReader_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.TextReader(null));

      var uri = new Uri(Yandex);
      Assert.True(uri.TextReader().Text() != uri.TextReader(Encoding.Unicode).Text());
      var textReader = uri.TextReader();
      Assert.True(textReader is StreamReader);
      Assert.True(textReader.Text().Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.Text(Uri)"/> method.</para>
    /// </summary>
    [Fact]
    public void Text_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Text(null));

      Assert.True(new Uri(Yandex).Text().Length > 0);
    }

    /// <summary>
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="UriExtensions.Upload(Uri, byte[])"/></description></item>
    ///     <item><description><see cref="UriExtensions.Upload(Uri, string)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Upload_Methods()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Upload(null, Enumerable.Empty<byte>().ToArray()));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Upload(new Uri(Yandex), (byte[]) null));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Upload(null, string.Empty));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.Upload(new Uri(Yandex), (string)null));
      
      throw new NotImplementedException();
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="UriExtensions.UploadFile(Uri, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void UploadFile_Method()
    {
      Assert.Throws<ArgumentNullException>(() => UriExtensions.UploadFile(null, "file"));
      Assert.Throws<ArgumentNullException>(() => UriExtensions.UploadFile(new Uri(Yandex), null));
      Assert.Throws<WebException>(() => UriExtensions.UploadFile(new Uri(Yandex), string.Empty));

      throw new NotImplementedException();
    }
  }
}